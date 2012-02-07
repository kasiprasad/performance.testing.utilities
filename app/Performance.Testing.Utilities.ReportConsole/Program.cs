using System;
using System.IO;
using System.Linq;
using NDesk.Options;
using Newtonsoft.Json;
using Performance.Testing.Utilities.ReportConsole.Framework.Parsers;

namespace Performance.Testing.Utilities.ReportConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var input = string.Empty;
            var output = string.Empty;
            var applicationName = string.Empty;
            var versionNumber = string.Empty;

            var options = new OptionSet();
            options.Add("i=|input=", v => input = v);
            options.Add("o=|output=", v => output = v);
            
            options.Add("app=|applicationName=", v => applicationName = v);
            

            options.Parse(args);


            if (IsValid(applicationName, input, output))
                ShowHelp();
            else
                GenerateReport(input, output, applicationName, versionNumber);
        }

        static bool IsValid(string applicationName, string input, string output)
        {
            return string.IsNullOrEmpty(input) || string.IsNullOrEmpty(output) || string.IsNullOrEmpty(applicationName);
        }

        static void GenerateReport(string inputDirectory, string outputDirectory, string applicationName, string versionNumber)
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

                        var serialized = JsonConvert.SerializeObject(report);

                        if (!Directory.Exists(outputDirectory)) Directory.CreateDirectory(outputDirectory);

                        var fileName = string.Format("{0}\\{1}_{2}_PerformanceAnalysisResult.json", outputDirectory, 
                            report.DateCreated.ToString("yyyyMMddhhssff"),
                            report.ApplicationName);

                        using (var writer = File.CreateText(fileName))
                        {
                            writer.Write(serialized);
                            writer.Flush();
                            writer.Close();
                        }

                        Console.WriteLine("Created report {0}", fileName);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Report Generation Failed.");
                        Console.WriteLine(ex);
                        throw ex;
                    }
                });

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
