using System;
using System.Linq;
using MongoDB.Driver.Builders;
using Performance.Testing.Utilities.ReportConsole.Framework.DataTransfer;

namespace Performance.Testing.Utilities.ReportConsole.Framework.Data.ReportDB
{
    public class ReportReader : IReportReader
    {
        public Report   Get(string loadTestDbId)
        {
            var server = ServerFactory.Create();
            var db = server.GetDatabase("LoadTestReports");
            if(!db.CollectionExists("Reports")) return new Report();

            var reports = db.GetCollection<Report>("Reports");

            return reports.FindOne(Query.EQ("LoadTestDBId", loadTestDbId));
        }

        public Report GetPreviousReport(string applicationName, DateTime dateCreated, string loadTestDbId)
        {
            var server = ServerFactory.Create();
            var db = server.GetDatabase("LoadTestReports");
            if (!db.CollectionExists("Reports")) return new Report();
            
            var reports = db.GetCollection<Report>("Reports");

            var previousReport =
                reports.Find(Query.And(Query.EQ("ApplicationName", applicationName), Query.NE("LoadTestDBId", loadTestDbId)))
                    .SetSortOrder(SortBy.Descending("DateCreated")).FirstOrDefault();

            return previousReport;
        }
    }
}