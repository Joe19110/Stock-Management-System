<%@ Page Language="VB" AutoEventWireup="false" CodeFile="home.aspx.vb" Inherits="home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Home</title>
    <link rel="stylesheet" href="ia.css" />
</head>
<body>
    <form id="Home" runat="server">
    <table style="margin:auto;width:50%;text-align:center;margin-top:1%" cellspacing="0px">
        <tr>
            <td align="center" colspan="5">
                <asp:Image runat="server" ImageUrl="~/logo.PNG" Width="20%" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
&nbsp;
            </td>
        </tr>
   <tr align="center">
    <td style="border:1px solid black;height:50px;width:100px">
        <asp:Label ID="OrdersLabel" runat="server" Text="Orders">
        </asp:Label>
    </td>
    <td style="width:100px">
    &nbsp;
    </td>
    <td style="border:1px solid black;height:50px;width:100px">
        <asp:Label ID="CustomerLabel" runat="server" Text="Customer Information">
        </asp:Label>
    </td>
    <td style="width:100px">
    &nbsp;
    </td>
    <td style="border:1px solid black;height:50px;width:100px">
        <asp:Label ID="OthersLabel" runat="server" Text="Others">
        </asp:Label>
    </td>
   </tr>
   <tr>
   <td style="height:30px">
    &nbsp;
   </td>
   <td style="height:30px">
    &nbsp;
   </td>
   <td style="height:30px">
    &nbsp;
   </td>
   <td style="height:30px">
    &nbsp;
   </td>
   <td style="height:30px">
    &nbsp;
   </td>
   </tr>
   <tr align="center">
    <td>
        <asp:Button ID="EditOrdersButton" runat="server" Text="Edit Orders" Width="120px" height="50px" PostBackUrl="~/Edit_Orders.aspx"/>
    </td>
    <td style="width:100px">
    &nbsp;
    </td>
    <td>
        <asp:Button ID="EditCustomerButton" runat="server" Text="Edit Customer &#13; Information"  Width="120px" height="50px" PostBackUrl="~/Edit_CustInfo.aspx"/>
    </td>
    <td style="width:100px">
    &nbsp;
    </td>
    <td>
        <asp:Button ID="EditProductButton" runat="server" Text="Edit Products" Width="120px" height="50px" PostBackUrl="~/Edit_Products.aspx"/>
    </td>
   </tr>
   <tr>
   <td style="height:30px">
    &nbsp;
   </td>
   <td style="height:30px">
    &nbsp;
   </td>
   <td style="height:30px">
    &nbsp;
   </td>
   <td style="height:30px">
    &nbsp;
   </td>
   <td style="height:30px">
    &nbsp;
   </td>
   </tr>
   <tr align="center">
    <td style="height: 52px">
        <asp:Button ID="ViewOrdersButton" runat="server" Text="View Orders" Width="120px" height="50px" PostBackUrl="~/View_Orders.aspx"/>
    </td>
    <td style="width:100px; height: 52px;">
    &nbsp;
    </td>
    <td style="height: 52px">
        <asp:Button ID="ViewCustomerButton" runat="server" Text="View Customer &#13; Information" Width="120px" height="50px" PostBackUrl="~/View_CustInfo.aspx"/>
    </td>
    <td style="width:100px; height: 52px;">
    &nbsp;
    </td>
    <td style="height: 52px">
        <asp:Button ID="EditIngredientsButton" runat="server" Text="Edit Ingredients" Width="120px" height="50px" PostBackUrl="~/Edit_Ingredients.aspx"/>
    </td>
   </tr>
        <tr>
            <td colspan="5" style="height:70px">
                    &nbsp;
            </td>
        </tr>
        <tr>
            <td>
              <asp:Button ID="ReportsButton" runat="server" Text="Reports" Width="120px" height="50px" PostBackUrl="~/Reports.aspx"/>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
              <asp:Button ID="ChangeUnamePassButton" runat="server" Text="Change Username &#13; and/or Password" Width="140px" height="50px"/>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
              <asp:Button ID="LogoutButton" runat="server" Text="Logout" Width="120px" height="50px" PostBackUrl="~/login.aspx"/>
            </td>
        </tr>
    </table>
        <asp:AccessDataSource ID="LoginDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
            SelectCommand="SELECT * FROM [Login]" 
            UpdateCommand="UPDATE Login SET Login.[Username] = @uname, Login.[Password] = @pass">
            <UpdateParameters>
                <asp:Parameter Name="uname" DefaultValue="" />
                <asp:Parameter Name="pass" DefaultValue="" />
            </UpdateParameters>
        </asp:AccessDataSource>
        
    </form>
</body>
</html>
