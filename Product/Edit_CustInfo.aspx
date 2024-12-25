<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Edit_CustInfo.aspx.vb" Inherits="Edit_CustInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit Customer Information</title>

</head>
<body>
    <form id="Edit_CustInfo" runat="server">
    <table style="margin:auto;width:35%;margin-top:10%;text-align:left">
    <tr>
    <td>
        <asp:Label ID="custIDLabel" runat="server" Text="Customer ID:"></asp:Label>
    </td>
    <td>
        <asp:Label ID="custID" runat="server" Text=""></asp:Label>
        &nbsp;
        <asp:Button ID="SearchCustID" runat="server" Text="Search by ID" />
    </td>
        <td rowspan="7" align="right">
            <asp:Button ID="save" runat="server" Text="Save" width="90px"/>
            <br />
            <br />
            <asp:Button ID="newCust" runat="server" Text="New &#13; Customer" width="90px"/>
            <br />
            <br />
            <asp:Button ID="delCust" runat="server" Text="Delete &#13; Customer" width="90px"/>
            <br />
            <br />
            <asp:Button ID="CancelButton" runat="server" Text="Cancel" Visible="False" width="90px"/>
        </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="custNameLabel" runat="server" Text="Customer Name:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="custName" runat="server" Height="15px" Width="200px"></asp:TextBox>
    </td>
    </tr>
        <tr>
    <td>
        <asp:Label ID="custNumberLabel" runat="server" Text="Phone Number:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="custNumber" runat="server" Height="15px" Width="200px"></asp:TextBox>
    </td>
    </tr>
        <tr>
    <td>
        <asp:Label ID="custAddressLabel" runat="server" Text="Address:"></asp:Label>
    </td>
    <td rowspan="4">
       <asp:TextBox ID="custAddress" runat="server" Rows="4" TextMode="MultiLine" EnableTheming="false" Height="80px" Width="200px"></asp:TextBox>
    </td>
    </tr>
         <tr><td>&nbsp;</td></tr>
         <tr><td>&nbsp;</td></tr>
         <tr><td>&nbsp;</td></tr>
        <tr>
            <td colspan="3">&nbsp;</td>
        </tr>
         <tr> 
             <td align="left" colspan="2">
        <asp:Button ID="prevCust" runat="server" Text="Previous" />
             &nbsp; &nbsp; &nbsp; 
        <asp:Button ID="nextCust" runat="server" Text="Next" /> 
            </td> 
             <td align="right">
         <asp:Button ID="Home" runat="server" Text="Back to Home" PostBackUrl="~/home.aspx" />
            </td> 
         </tr>
    </table>
        <asp:AccessDataSource ID="CustSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
        SelectCommand="SELECT MAX(Customer_ID) AS custid FROM [Customer_Information]">
        </asp:AccessDataSource>
        <asp:AccessDataSource ID="OrdersDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb"
        DeleteCommand="DELETE FROM Orders WHERE (Customer_ID = @custid)" SelectCommand="SELECT * FROM [Orders]">
        <DeleteParameters>
        <asp:parameter name="custid" defaultvalue=""/>
        </DeleteParameters>
        </asp:AccessDataSource>
      
        <asp:AccessDataSource ID="CustomerDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
        SelectCommand="SELECT Customer_ID, Customer_Name, Address, Phone_Number FROM Customer_Information ORDER BY Customer_ID" 
        UpdateCommand="UPDATE Customer_Information SET Customer_Information.Customer_Name = @custname, Customer_Information.Address = @address, Customer_Information.Phone_Number = @phone WHERE (Customer_Information.Customer_ID = @custid)" 
        DeleteCommand="DELETE FROM Customer_Information WHERE Customer_ID = @custid" 
        InsertCommand="INSERT INTO Customer_Information(Customer_ID, Customer_Name, Address, Phone_Number) VALUES (@custid, @custname, @address, @phone)">
        <DeleteParameters>
        <asp:parameter name="custid" defaultvalue=""/>
        </DeleteParameters>
        <InsertParameters>
        <asp:Parameter name="custid" DefaultValue="" />
        <asp:Parameter name="custname" DefaultValue="" dbtype="String" />
        <asp:Parameter name="address" DefaultValue="" dbtype="String"/>
        <asp:Parameter name="phone" DefaultValue="" dbtype="String"/>
        </InsertParameters>
        <UpdateParameters>
        <asp:Parameter name="custname" DefaultValue="" />
        <asp:Parameter name="address" DefaultValue="" />
        <asp:Parameter name="phone" DefaultValue="" />
        <asp:ControlParameter name="@custid" ControlID="custID" PropertyName="Text" />
        </UpdateParameters>
        </asp:AccessDataSource>
      
   </form>
</body>
</html>
