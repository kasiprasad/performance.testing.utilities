using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Performance.Testing.Utilities.ReportConsole.Framework.DataTransfer;
using Performance.Testing.Utilities.ReportConsole.Framework.Services;

namespace Performance.Testing.Utilities.ReportConsole.Framework.Mappers
{
    public class LoadTestRunMapper : IMapper<XElement, IEnumerable<LoadTestRun>>
    {
        private readonly ILoadTestDataService dataService;

        Dictionary<string, XNamespace> namespaces;
        XNamespace ns;
        
        public LoadTestRunMapper(ILoadTestDataService dataService)
        {
            this.dataService = dataService;
        }

        public IEnumerable<LoadTestRun> MapFrom(XElement input)
        {
            if (input == null) return null;
            var testRunId = Guid.Parse(input.Attribute("id").Value);

            namespaces = input.Document.GetNamespaces();
            ns = namespaces[namespaces.Keys.First()];

            //Get the Load Test Results XElements
            var loadTestResultsElements = GetLoadTestResults(input);

            List<LoadTestRun> output = new List<LoadTestRun>();
            loadTestResultsElements.ToList().ForEach(r =>
            {
                var result = MapLoadTestRun(r, testRunId);
                output.Add(result);
            });

            return output;
        }

        private LoadTestRun MapLoadTestRun(XElement r, Guid testRunId)
        {
            var runId = int.Parse(r.Attribute("runId").Value.ToString(CultureInfo.InvariantCulture));

            //calculate total of all Tests Passed/Failed in this load test run
            //map test summaries
            var testSummaryElements = GetTestSummaries(r);
            var totalTests = 0;
            var totalPassed = 0;
            var totalFailed = 0;

            testSummaryElements.ToList().ForEach(ts =>
                {
                    var total = int.Parse(ts.Attribute("totalTests").Value);
                    var failed = int.Parse(ts.Attribute("testsFailed").Value);
                    var passed = total - failed;

                    totalTests += total;
                    totalPassed += passed;
                    totalFailed += failed;
                });

            var result = new LoadTestRun
                             {
                                 //Id = testRunId.ToString(),
                                 RunId = runId,
                                 TestName = r.Attribute("testName").Value,
                                 StartTime = DateTime.Parse(r.Attribute("startTime").Value).ToString(),
                                 EndTime = DateTime.Parse(r.Attribute("endTime").Value).ToString(),
                                 Duration = TimeSpan.Parse(r.Attribute("duration").Value),
                                 TotalTests = totalTests,
                                 TotalTestsFailed = totalFailed,
                                 TotalTestsPassed = totalPassed,
                                 Requests = MapRequestSummaries(r, runId)
                             };

            
            return result;
            
            
        }

        List<LoadTestRequestSummary> MapRequestSummaries(XElement r, int runId)
        {
            //get associated request summary data from the load test database 
            var summaries = dataService.GetRequestSummaries(runId).ToList();
            var breakdowns = dataService.GetRequestsPerSec(runId).ToList();

            summaries.ForEach(summary =>
                {
                    //join the per second breakdows to each request summary
                    var matching = breakdowns.Where(b => b.WebTestName == summary.WebTestName
                                                         && b.ScenarioName == summary.ScenarioName
                                                         && b.Url == summary.Url).ToList();

                    summary.PerSecondBreakdown.AddRange(matching);
                });

            return summaries;
        }



        
        private IEnumerable<XElement> GetTestSummaries(XElement element)
        {
            var testSummariesElements =
                (from ts in element.Element(ns + "Summary").Elements(ns + "TestSummaries")
                 select ts);

            var testSummaries = (from ts in testSummariesElements.Elements(ns + "TestSummary")
                                 select ts);
            return testSummaries;
        }

      
        private IEnumerable<XElement> GetLoadTestResults(XElement input)
        {
            var results = input.Elements(ns + "Results").First();

            var loadTestResults = (from r in results.Elements(ns + "LoadTestResult")
                                   select r);
            return loadTestResults;
        }
    }
}