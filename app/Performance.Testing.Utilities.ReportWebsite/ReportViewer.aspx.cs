using System;
using Newtonsoft.Json;
using Performance.Testing.Utilities.ReportConsole.Framework.Data.ReportDB;

public partial class ReportViewer : System.Web.UI.Page
{
    string reportModel;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["report"] != null)
        {
            var loadTestDbId = Request.QueryString["report"];
            var reader = new ReportReader();
            var report = reader.Get(loadTestDbId);
            reportModel = JsonConvert.SerializeObject(report);
        }
    }

    protected string ReportModel { get { return reportModel;  } }
}