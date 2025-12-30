using FOS.DataLayer;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Data.SqlClient;
//using Microsoft.Reporting.WebForms;

namespace FOS.Web.UI.Views.Reports
{
    public partial class RegisteredPatientReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
     
                string doctorID = Request.QueryString["DoctorID"];
                int DRID = 0;
                DateTime fromDate = DateTime.Now;
                DateTime toDate = DateTime.Now;
                if (doctorID != null && doctorID != "" && doctorID != "0")
                {
                   doctorID = Request.QueryString["DoctorID"];
                   DRID = Convert.ToInt32(doctorID);
                   fromDate = Convert.ToDateTime(Request.QueryString["FromDate"]);
                   toDate = Convert.ToDateTime(Request.QueryString["ToDate"]);
                }
                FOSDataModel db = new FOSDataModel();
    
                List<GetPatientDocrorWise1_1_Result> data = db.GetPatientDocrorWise1_1(DRID, fromDate, toDate).ToList();
 
                    CustomerListReportViewer.LocalReport.ReportPath = Server.MapPath("../Reports1/AllPatientReport.rdlc");
                    CustomerListReportViewer.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", data);
                    CustomerListReportViewer.LocalReport.DataSources.Add(rdc);
                    CustomerListReportViewer.LocalReport.Refresh();
                    CustomerListReportViewer.DataBind();
                   //}

                }
            }
        protected void Export(object sender, EventArgs e)
        {
            Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension;

            //Export the RDLC Report to Byte Array.
            byte[] bytes = CustomerListReportViewer.LocalReport.Render(rbFormat.SelectedItem.Value, null, out contentType, out encoding, out extension, out streamIds, out warnings);

            //Download the RDLC Report in Word, Excel, PDF and Image formats.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }
}