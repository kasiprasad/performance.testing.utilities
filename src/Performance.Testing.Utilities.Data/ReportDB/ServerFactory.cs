using System.Configuration;
using MongoDB.Driver;

namespace Performance.Testing.Utilities.ReportConsole.Framework.Data.ReportDB
{
    public class ServerFactory
    {
        public static MongoServer Create()
        {
            return MongoServer.Create(
                ConfigurationManager.ConnectionStrings["Performance.Testing.Utilities.ReportConsole.Properties.Settings.MongoLoadTestReportDatabaseConnectionString"]
                .ConnectionString);
        }
    }
}