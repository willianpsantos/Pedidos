<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptPedidos.aspx.cs" Inherits="Pedidos.Reports.RptPedidos" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        html, body, form {
            width:100%;
            height:100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <rsweb:ReportViewer ID="ReportViewer1" 
                            runat="server" 
                            Height="500px" 
                            Width="100%" 
                            Font-Names="Verdana" 
                            Font-Size="8pt" 
                            WaitMessageFont-Names="Verdana" 
                            WaitMessageFont-Size="14pt" 
                            InteractivityPostBackMode="AlwaysSynchronous"
                            ShowReportBody="true"
                            ShowToolBar="true">
            <LocalReport ShowDetailedSubreportMessages="true">
            </LocalReport>           
        </rsweb:ReportViewer>
    </div>
       
    </form>
</body>
</html>
