using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pedidos.Reports
{
    public partial class RptPedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int codigo = Convert.ToInt32(Request.QueryString["id"]);
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                LocalReport localReport = ReportViewer1.LocalReport;

                localReport.ReportPath = Server.MapPath("rptPedido.rdlc");
                
                dstPedidos dataSource = new dstPedidos();
                dstPedidosTableAdapters.PEDIDOSTableAdapter adapter = new dstPedidosTableAdapters.PEDIDOSTableAdapter();
                adapter.Fill(dataSource.PEDIDOS, codigo);

                localReport.DataSources.Add(new ReportDataSource("dstPedidos", dataSource.Tables["PEDIDOS"]));
                localReport.SetParameters(new ReportParameter("Codigo", codigo.ToString()));
                byte[] buffer = localReport.Render("pdf");

                Response.Clear();
                Response.AddHeader("Content-Type", "application/pdf");
                Response.AddHeader("Content-Length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
                Response.Flush();
                Response.End();
            }
        }
    }
}