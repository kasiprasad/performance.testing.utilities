using System;

namespace Performance.Testing.Utilities.ReportConsole.Framework.DataTransfer
{
    public class LoadTestRequestPerSecondSummary
    {
        public string WebTestName { get; set; }
        public string ScenarioName { get; set; }
        public int? RequestsPerSec { get; set; }
        public DateTime? Date1SecIncrement { get; set; }
        public string Url { get; set; }
    }
}