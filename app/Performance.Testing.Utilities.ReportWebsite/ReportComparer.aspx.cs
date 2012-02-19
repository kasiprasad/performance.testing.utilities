using System;
using Newtonsoft.Json;
using Performance.Testing.Utilities.ReportConsole.Framework.Data.ReportDB;

public partial class ReportComparer : System.Web.UI.Page
{
    string leftReportModel;
    string rightReportModel;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["leftReport"] != null)
        {
            var loadTestDbId = Request.QueryString["leftReport"];
            var reader = new ReportReader();
            var report = reader.Get(loadTestDbId);
            leftReportModel = JsonConvert.SerializeObject(report);
        }

        if (Request.QueryString["rightReport"] != null)
        {
            var loadTestDbId = Request.QueryString["rightReport"];
            var reader = new ReportReader();
            var report = reader.Get(loadTestDbId);
            rightReportModel = JsonConvert.SerializeObject(report);
        }
    }

    protected string LeftReportModel { get { return leftReportModel; } }
    protected string RightReportModel { get { return rightReportModel; } }
}