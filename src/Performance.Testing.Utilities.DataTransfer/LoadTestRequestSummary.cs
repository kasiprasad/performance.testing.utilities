using System.Collections.Generic;
using System.Linq;

namespace Performance.Testing.Utilities.ReportConsole.Framework.DataTransfer
{
    public class LoadTestRequestSummary
    {
        
        public LoadTestRequestSummary()
        {
            PerSecondBreakdown = new List<LoadTestRequestPerSecondSummary>();
        }

        public string ScenarioName { get; set; }
        public string WebTestName { get; set; }
        public int RunId { get; set; }

        public int TotalRequests { get; set; }

        public string Url { get; set; }
        public string RequestMethod { get; set; }

        public double AverageResponseTime { get; set; }
        public double MaximumResponseTime { get; set; }
        public double MinimumResponseTime { get; set; }
        public double Percentile95ResponseTime { get; set; }

        public int MaximumRequestsPerSecond
        {
            get { return PerSecondBreakdown.Max(b => b.RequestsPerSec.Value); }
        }

        public int MinimumRequestsPerSecond
        {
            get { return PerSecondBreakdown.Min(b => b.RequestsPerSec.Value); }
        }

        public double AverageRequestsPerSecond
        {
            get { return PerSecondBreakdown.Average(b => b.RequestsPerSec.Value); }
        }


        public List<LoadTestRequestPerSecondSummary> PerSecondBreakdown { get; set; }
        
    }
}