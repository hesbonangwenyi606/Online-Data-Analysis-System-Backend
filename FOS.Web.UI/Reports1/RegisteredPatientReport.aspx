<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisteredPatientReport.aspx.cs" Inherits="FOS.Web.UI.Views.Reports.RegisteredPatientReport" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script runat="server">

    </script>
</head>
<body>
    <div class="row">
        <div class="col-sm-12">

    
            <form id="form1" runat="server">
                <div >
                    <rsweb:ReportViewer ID="CustomerListReportViewer" runat="server" Width="70%">
                    </rsweb:ReportViewer>
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                    <br />
                    <div style="margin-left:3%">
                    Format:
                    <asp:RadioButtonList ID="rbFormat" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Word" Value="WORD"  />
                        <asp:ListItem Text="Excel" Value="EXCEL" />
                        <asp:ListItem Text="PDF" Value="PDF" Selected="True" />
                        <asp:ListItem Text="Image" Value="IMAGE" />
                    </asp:RadioButtonList>
                    <br />
                    <asp:Button ID="btnExport" Text="Export" runat="server" OnClick="Export" />
                        </div>
                </div>
            </form>
          </div>
    </div>
</body>
</html>
