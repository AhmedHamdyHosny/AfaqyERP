<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeliveryNoteDeviceReportViewer.aspx.cs" Inherits="Afaqy_Store.Reports.Store.DeliveryNoteDeviceReportViewer" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <rsweb:ReportViewer ID="InvoiceRptViewer" runat="server" Height="1000" Width="99%" ZoomMode="PageWidth" BackColor="White" BorderColor="White" ></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
