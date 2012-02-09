using System.Collections.Generic;
using System.Linq;
using Performance.Testing.Utilities.Data.LoadTestDB;
using Performance.Testing.Utilities.ReportConsole.Framework.DataTransfer;

namespace Performance.Testing.Utilities.ReportConsole.Framework.Services
{
    public class LoadTestDataService : ILoadTestDataService
    {
        public IEnumerable<LoadTestRequestSummary> GetRequestSummaries(int id)
        {
            //refactor to repository implementation
            var context = new LoadTestingDatabaseDataContext();

            var output = new List<LoadTestRequestSummary>();

            var result = context.Reporting_GetRequestSummary(id);

            result.ToList().
                ForEach(r => output.Add(new LoadTestRequestSummary
                                            {
                                                ScenarioName = r.ScenarioName,
                                                WebTestName = r.TestCaseName,
                                                RunId = id,
                                                Url = r.RequestUri,
                                                AverageResponseTime = r.AverageResponseTime,
                                                MaximumResponseTime = r.MaximumResponseTime,
                                                MinimumResponseTime = r.MinimumResponseTime,
                                                Percentile95ResponseTime = r.Percentile95.HasValue ? r.Percentile95.Value : 0,
                                                TotalRequests = r.TotalRequests
                                            }));

            return output;
        }

        public IEnumerable<LoadTestRequestPerSecondSummary> GetRequestsPerSec(int id)
        {
            //refactor to repository implementation
            var context = new LoadTestingDatabaseDataContext();

            var output = new List<LoadTestRequestPerSecondSummary>();

            var result = context.Reporting_GetRequestsBySec(id);
            result.ToList().ForEach(r => output.Add(new LoadTestRequestPerSecondSummary
                                                        {
                                                            WebTestName = r.TestCaseName,
                                                            ScenarioName = r.ScenarioName,
                                                            Url = r.RequestUri,
                                                            Date1SecIncrement = r.Date1SecIncrement,
                                                            RequestsPerSec = r.RequestsPerSec
                                                        }));

            return output;
        }
    }
}