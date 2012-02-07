using System;
using System.Collections.Generic;

namespace Performance.Testing.Utilities.ReportConsole.Framework.DataTransfer
{
    public class LoadTestRun
    {
        public Guid Id { get; set; }
        public int RunId { get; set; }

        public string TestName { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public TimeSpan Duration { get; set; }

        public int TotalTests { get; set; }
        public int TotalTestsFailed { get; set; }
        public int TotalTestsPassed { get; set; }
        
        public List<LoadTestRequestSummary> Requests { get; set; }
        
    }
}