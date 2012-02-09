using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Performance.Testing.Utilities.ReportConsole.Framework.DataTransfer
{
    public class Report
    {
        [BsonId]
        public Guid Id { get; set; }

        public List<LoadTestRun> LoadTestRuns { get; set; }

        public string LoadTestDBId { get; set; }        
        public DateTime DateCreated { get; set; }

        public string ApplicationName { get; set; }
        public string VersionNumber { get; set; }

        public string VersionUrl { get; set; }
    }
}