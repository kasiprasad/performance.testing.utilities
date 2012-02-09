using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Performance.Testing.Utilities.ReportConsole.Framework.DataTransfer;

namespace Performance.Testing.Utilities.ReportConsole.Framework.Data.ReportDB
{
    public class ReportWriter : IReportWriter
    {
        
        public void Write(Report report)
        {
            var server = ServerFactory.Create();
            var db = server.GetDatabase("LoadTestReports");
            if(!db.CollectionExists("Reports")) db.CreateCollection("Reports");
            var reports = db.GetCollection<Report>("Reports");

            var reader = new ReportReader();
            var existingReport = reader.Get(report.LoadTestDBId);
            if (existingReport != null) report.Id = existingReport.Id;

            reports.Save(report);
        }
    }
}