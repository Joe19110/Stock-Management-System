<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Reports.aspx.vb" Inherits="Reports" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reports</title>
</head>
<body>
    <form id="Reports" runat="server">
<table style="margin:auto;width:50%;text-align:center;margin-top:8%" cellspacing="0px">
    <tr>
            <td align="center" colspan="7">
                <asp:Image runat="server" ImageUrl="~/logo.PNG" Width="20%" />
            </td>
        </tr>
    <tr>
            <td colspan="7">
&nbsp;
            </td>
        </tr>
    <tr>
        <td>
            <asp:Button ID="DailyReportButton" runat="server" Text="Daily Report" Width="120px" height="50px" PostBackUrl="~/Daily_Report.aspx"/>
        </td>
        <td>
             &nbsp;
        </td>
        <td>
            <asp:Button ID="MonthlyReportButton" runat="server" Text="Monthly Report" Width="120px" height="50px" PostBackUrl="~/Monthly_Report.aspx"/>
        </td>
        <td>
             &nbsp;
        </td>
        <td>
            <asp:Button ID="YearlyReportButton" runat="server" Text="Yearly Report" Width="120px" height="50px" PostBackUrl="~/Yearly_Report.aspx"/>
        </td>
        <td>
             &nbsp;
        </td>
        <td>
            <asp:Button ID="AllTimeReportButton" runat="server" Text="All Time Report" Width="120px" height="50px" PostBackUrl="~/AllTime_Report.aspx"/>
        </td>
    </tr>
    <tr>
        <td colspan="7">
            &nbsp;
        </td>
    </tr>
        <tr>
        <td colspan="6" style="height:50px">
            &nbsp;
        </td>
            <td align="center">
            <asp:Button ID="HomeButton" runat="server" Text="Back To Home" Width="120px" height="30px" PostBackUrl="~/home.aspx"/>

            </td>
    </tr>
</table>
        
    </form>
</body>
</html>
