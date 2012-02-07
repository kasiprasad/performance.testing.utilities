using System.Collections.Generic;
using Performance.Testing.Utilities.ReportConsole.Framework.DataTransfer;

namespace Performance.Testing.Utilities.ReportConsole.Framework.Services
{
    public interface ILoadTestDataService
    {
        IEnumerable<LoadTestRequestSummary> GetRequestSummaries(int id);
        IEnumerable<LoadTestRequestPerSecondSummary> GetRequestsPerSec(int id);
    }
}