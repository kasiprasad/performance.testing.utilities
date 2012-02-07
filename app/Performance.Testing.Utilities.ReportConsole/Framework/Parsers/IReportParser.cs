using Performance.Testing.Utilities.ReportConsole.Framework.DataTransfer;

namespace Performance.Testing.Utilities.ReportConsole.Framework.Parsers
{
    public interface IReportParser
    {
        Report Parse(string pathToDirectoryContainingReports, string applicationName);
    }
}