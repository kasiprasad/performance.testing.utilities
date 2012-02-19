using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.WebTesting;
using Performance.Testing.Utilities.ReportConsole.Framework.DataTransfer;
using Performance.Testing.Utilities.ReportConsole.Framework.Mappers;
using Performance.Testing.Utilities.ReportConsole.Framework.Services;

namespace Performance.Testing.Utilities.ReportConsole.Framework.Parsers
{
    //todo: refactor / SoC
    //todo: add ioc injection

    public class ReportParser : IReportParser
    {
        private readonly IMapper<XElement, IEnumerable<LoadTestRun>> loadTestRunMapper;

        public ReportParser(IMapper<XElement, IEnumerable<LoadTestRun>> loadTestRunMapper)
        {
            this.loadTestRunMapper = loadTestRunMapper;
        }

        public ReportParser() : this(new LoadTestRunMapper(new LoadTestDataService())) { }

        public Report Parse(string reportFilePath, string applicationName)
        {
            ValidateReportFilePath(reportFilePath);

            var reportFile = XDocument.Load(reportFilePath);
            var testRunId = reportFile.Root.Attribute("id").Value;

            //identification of version number through the use of a web test within the load test run
            //we will expect a webtest within the test run list targeting the app version number
            var namespaces = reportFile.Document.GetNamespaces();
            var ns = namespaces[namespaces.Keys.First()];

            var reportInputDirectory = Path.GetDirectoryName(reportFilePath);

            var versionWebTestResultFilePath = reportFile.Root.Element(ns + "Results")
                .Element(ns + "WebTestResult")
                .Element(ns + "WebTestResultFilePath").Value;

            //extract the version number
            var versionNumber = ExtractVersionInfoFromWebTestResult(reportInputDirectory + "\\" + versionWebTestResultFilePath);

            var report = new Report()
                             {
                                 LoadTestDBId = testRunId,
                                 LoadTestRuns = loadTestRunMapper.MapFrom(reportFile.Root).ToList(),
                                 DateCreated = DateTime.Now,
                                 ApplicationName = applicationName,
                                 VersionNumber = versionNumber.Number,
                                 VersionUrl = versionNumber.Url
                             };

            return report;
        }

        static void ValidateReportFilePath(string reportFilePath)
        {
            if (string.IsNullOrEmpty(reportFilePath))
                throw new ArgumentNullException("reportFilePath", "target cannot be empty");

            if (!File.Exists(reportFilePath))
                throw new ApplicationException(string.Format("The report file you specified at {0} does not exist.",
                                                             reportFilePath));
        }

        VersionInfo ExtractVersionInfoFromWebTestResult(string value)
        {
            if(!File.Exists(value)) return new VersionInfo();
            var serializer = new WebTestResultDetailsSerializer();
            var details = serializer.Deserialize(value);
            //assume there is only one webtest and its the version web test
            var requestResult = details.GetWebTestRequestResults().First();
            
            var versionUrl = requestResult.Request.Url;
            var versionNumber = requestResult.Response.BodyString
                .Replace("\r", "")
                .Replace("\n", "");

            return  new VersionInfo{
                           Url = versionUrl,
                           Number = versionNumber
                       };
        }
    }

    internal class VersionInfo
    {
        public string Url { get; set; }
        public string Number { get; set; }
    }
}