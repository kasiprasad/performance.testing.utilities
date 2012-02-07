using System;
using System.Collections.Generic;

namespace Performance.Testing.Utilities.ReportConsole.Framework.DataTransfer
{
    public class Report
    {
        public List<LoadTestRun> LoadTestRuns { get; set; }

        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public string ApplicationName { get; set; }
        public string VersionNumber { get; set; }
    }
}