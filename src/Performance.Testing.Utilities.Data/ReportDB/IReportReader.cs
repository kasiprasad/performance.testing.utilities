using System;
using Performance.Testing.Utilities.ReportConsole.Framework.DataTransfer;

namespace Performance.Testing.Utilities.ReportConsole.Framework.Data.ReportDB
{
    public interface IReportReader
    {
        Report Get(string loadTestDbId);
    }
}