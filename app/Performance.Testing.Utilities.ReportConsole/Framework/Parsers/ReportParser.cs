using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Performance.Testing.Utilities.ReportConsole.Framework.DataTransfer;
using Performance.Testing.Utilities.ReportConsole.Framework.Mappers;
using Performance.Testing.Utilities.ReportConsole.Framework.Services;

namespace Performance.Testing.Utilities.ReportConsole.Framework.Parsers
{
    public class ReportParser : IReportParser
    {
        private readonly IMapper<XElement, IEnumerable<LoadTestRun>> loadTestRunMapper;

        public ReportParser(IMapper<XElement, IEnumerable<LoadTestRun>> loadTestRunMapper)
        {
            this.loadTestRunMapper = loadTestRunMapper;
        }

        public ReportParser() : this(new LoadTestRunMapper(new LoadTestDataService())) { }

        public Report Parse(string pathToDirectoryContainingReports, string applicationName)
        {
            if (string.IsNullOrEmpty(pathToDirectoryContainingReports))
                throw new ArgumentNullException("pathToDirectoryContainingReports", "target cannot be empty");
            
            if (!File.Exists(pathToDirectoryContainingReports))
                throw new ApplicationException(string.Format("The report file you specified at {0} does not exist.", pathToDirectoryContainingReports));

            var reportFile = XDocument.Load(pathToDirectoryContainingReports);
            var testRunId = Guid.Parse(reportFile.Root.Attribute("id").Value);

            //todo: implement identification of version number through the use of a web test within the load test run

            //var versionWebTest = reportFile.Root.Elements("WebTestResult").FirstOrDefault(e => 
            //    e.Attribute("testName") != null && e.Attribute("testName").Value == "Version");

            //if (versionWebTest == null)
            //    throw new ApplicationException("Could not locate a version web test within your test run results. Version number is unknown.");

            //extract the version number
            var versionNumber = string.Empty; //todo: ExtractVersionNumberFromWebTestResult(versionWebTest.Element("WebTestResultFilePath").Value);

            var report = new Report()
                             {
                                 Id = testRunId,
                                 LoadTestRuns = loadTestRunMapper.MapFrom(reportFile.Root).ToList(),
                                 DateCreated = DateTime.Now,
                                 ApplicationName = applicationName,
                                 VersionNumber = versionNumber
                             };

            return report;
        }
    }
}