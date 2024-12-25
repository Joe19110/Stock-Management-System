<%@ Page Language="VB" AutoEventWireup="false" CodeFile="View_CustInfo.aspx.vb" Inherits="View_CustInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>View Customer Information</title>
</head>
<body>
    <form id="Edit_CustInfo" runat="server">
    <table style="margin:auto;width:36%;margin-top:10%;text-align:left">
    <tr>
    <td>
        <asp:Label ID="custIDLabel" runat="server" Text="Customer ID:"></asp:Label>
    </td>
    <td>
        <asp:Label ID="custID" runat="server" Text=""></asp:Label>
        &nbsp; &nbsp; &nbsp;
        <asp:Button ID="SearchCustID" runat="server" Text="Search by ID" />
    </td>
        <td rowspan="3" align="right" style="border: 1px solid black;padding:2%">
                
                <asp:Label ID="CustNameSearchLabel" runat="server" Text="Search Name:"></asp:Label>
                <asp:TextBox ID="NameSearchData" runat="server" Width="80px"></asp:TextBox>
                <br />
                <br />
             <asp:Button ID="SearchName" runat="server" Text="Search" />

            </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="custNameLabel" runat="server" Text="Customer Name:"></asp:Label>
    </td>
    <td>
        <asp:Label ID="custName" runat="server" Text=""></asp:Label>
    </td>
         
    </tr>
        <tr>
    <td>
        <asp:Label ID="custNumberLabel" runat="server" Text="Phone Number:"></asp:Label>
    </td>
    <td>
        <asp:Label ID="custNumber" runat="server" Text=""></asp:Label>
    </td>
    </tr>
        <tr>
    <td>
        <asp:Label ID="custAddressLabel" runat="server" Text="Address:"></asp:Label>
    </td>
    <td rowspan="4" style="vertical-align:top">
    <asp:Label ID="custAddress" runat="server" Text="" Width="100px"></asp:Label>
    </td>
           
    </tr>
    <tr>
    <td></td>
    </tr>
    <tr>
    <td></td>
    </tr>
    <tr>
    <td align="center">
        &nbsp;</td>
    </tr>
    <tr>
    <td>
    &nbsp;
    </td>
    </tr>
    <tr>
    <td colspan="2">
        <asp:Button ID="prevCust" runat="server" Text="Previous" />
        &nbsp; &nbsp; &nbsp; 
        <asp:Button ID="nextCust" runat="server" Text="Next" />
    </td>
    <td align="right">
         <asp:Button ID="Home" runat="server" Text="Back to Home" PostBackUrl="~/home.aspx" />
        </td>
    </tr>
    </table>
        <asp:AccessDataSource ID="CustomerDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
        SelectCommand="SELECT Customer_ID, Customer_Name, Address, Phone_Number FROM Customer_Information ORDER BY Customer_ID"> 
        </asp:AccessDataSource>
        <asp:AccessDataSource ID="CustSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
        SelectCommand="SELECT MAX(Customer_ID) AS custid FROM [Customer_Information]">
        </asp:AccessDataSource>
        <asp:AccessDataSource ID="OrdersDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb"
        SelectCommand="SELECT * FROM [Orders]">
        </asp:AccessDataSource>
        
   </form>
</body>
</html>
