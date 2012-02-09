using Performance.Testing.Utilities.ReportConsole.Framework.DataTransfer;

namespace Performance.Testing.Utilities.ReportConsole.Framework.Data.ReportDB
{
    public interface IReportWriter
    {
        void Write(Report report);
    }
}