using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using NDesk.Options;
using Nustache.Core;
using Performance.Testing.Utilities.ReportConsole.Framework.Data.ReportDB;
using Performance.Testing.Utilities.ReportConsole.Framework.DataTransfer;
using Performance.Testing.Utilities.ReportConsole.Framework.Parsers;

//todo: Refactor Program - possibly command pattern

namespace Performance.Testing.Utilities.ReportConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var input = string.Empty;
            var output = string.Empty;
            var applicationName = string.Empty;
            var reportSiteUrl = string.Empty;
            
            var options = new OptionSet();
            options.Add("i=|input=", v => input = v);
            options.Add("o=|output=", v => output = v);
            
            options.Add("app=|applicationName=", v => applicationName = v);
            options.Add("url=|reportSiteUrl=", v => reportSiteUrl = v);

            if (reportSiteUrl == string.Empty)
                reportSiteUrl = ConfigurationManager.AppSettings["ReportSiteUrl"];

            options.Parse(args);


            if (IsValid(applicationName, input, output, reportSiteUrl))
                ShowHelp();
            else
                GenerateReport(input, applicationName, output, reportSiteUrl);
        }

        static bool IsValid(string applicationName, string input, string output, string reportSiteUrl)
        {
            return (!string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(output) && !string.IsNullOrEmpty(applicationName) && !string.IsNullOrEmpty(reportSiteUrl));
        }

        static void GenerateReport(string inputDirectory, string applicationName, string outputDirectory, string reportSiteUrl)
        {
            if (!Directory.Exists(inputDirectory))
            {
                ShowError();
                return;
            }

            var trxFiles = Directory.GetFiles(inputDirectory, "*.trx");
            trxFiles.ToList().ForEach(file =>
                {
                    var parser = new ReportParser();
                    try
                    {
                        var report = parser.Parse(file, applicationName);

                        var previousReport = GetPreviousReport(report.ApplicationName, report.DateCreated, report.LoadTestDBId);

                        WriteReport(report);

                        RenderTemplate(outputDirectory, reportSiteUrl, previousReport, report);

                        Console.WriteLine("Created report {0}", report.LoadTestDBId);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Report Generation Failed.");
                        Console.WriteLine(ex);
                    }
                });

        }

        static void RenderTemplate(string outputDirectory, string reportSiteUrl, Report previousReport, Report report)
        {
            var outputFileName = string.Format("{0}\\{1}", outputDirectory, "performance_testing_results.html");
            var templateStream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("Performance.Testing.Utilities.ReportConsole.index.html.template");

            if (!Directory.Exists(outputDirectory)) Directory.CreateDirectory(outputDirectory);

            using (var streamReader = new StreamReader(templateStream))
            {
                var template = streamReader.ReadToEnd();

                var leftReportUrl = string.Format("{0}?report={1}", reportSiteUrl,
                                                  previousReport == null ? string.Empty : previousReport.LoadTestDBId);
                var rightReportUrl = string.Format("{0}?report={1}", reportSiteUrl, report.LoadTestDBId);

                var data = new
                               {
                                   LeftReportUrl = leftReportUrl,
                                   RightReportUrl = rightReportUrl
                               };

                Render.StringToFile(template, data, outputFileName);
            }
        }

        static void WriteReport(Report report)
        {
            var writer = new ReportWriter();
            writer.Write(report);
        }

        static Report GetPreviousReport(string applicationName, DateTime dateCreated, string loadTestDbId)
        {
            //get the instance of the most recent report for this application (excluding any previous report generated for the exact same test run)
            var reader = new ReportReader();
            var previousReport = reader.GetPreviousReport(applicationName, dateCreated, loadTestDbId);
            return previousReport;
        }

        static void ShowError()
        {
            Console.WriteLine("Input directory specified does not exist");
        }

        static void ShowHelp()
        {
            Console.WriteLine("Usage: Performance.Testing.Utilities.ReportConsole.exe --i:[INPUT_DIRECTORY] --o:[OUTPUT_DIRECTORY] --app:[APPLICATION_NAME]");
        }
    }
}
