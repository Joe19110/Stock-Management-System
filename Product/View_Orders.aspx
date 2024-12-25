<%@ Page Language="VB" AutoEventWireup="false" CodeFile="View_Orders.aspx.vb" Inherits="View_Orders" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View Orders</title>
    <link rel="stylesheet" href="ia.css" />
</head>
<body>
    <form id="View_Orders" runat="server">
    <table style="margin:auto;width:40%;margin-top:10%;text-align:left">
    <tr>
        <td align="left">
            <asp:Label ID="OrderIDLabel" runat="server" Text="Order ID:">
            </asp:Label>
            <asp:Label ID="OrderIDData" runat="server"></asp:Label>
            &nbsp; &nbsp;
        <asp:Button ID="SearchOrderID" runat="server" Text="Search by ID" />
        </td>
        <td style="border: 1px solid black;padding:1%" rowspan="5">
        Filter:
        <br />
            <asp:CheckBox ID="readyfilter" runat="server" Text="Ready to be shipped?" AutoPostBack="True" />
            &nbsp; &nbsp;
            <asp:CheckBox ID="SortByDate" runat="server" AutoPostBack="True" Text="Sort by Date?" />
            <br /> 

            <asp:CheckBox ID="paidfilter" runat="server" Text="Paid?" AutoPostBack="True" />
            <br />
            <br />
             <asp:Label ID="StatusFilterLabel" runat="server" Text="Status:"></asp:Label>
             <asp:DropDownList ID="CompletedDropDownList" runat="server" AutoPostBack="True">
                 <asp:ListItem Value="True">Completed</asp:ListItem>
                 <asp:ListItem Value="False">Ongoing</asp:ListItem>
                 <asp:ListItem Selected="True"></asp:ListItem>
             </asp:DropDownList>
            <br />
            <asp:Label ID="CustNameFilterLabel" runat="server" Text="Customer Name:"></asp:Label>
            <ajaxToolkit:ComboBox ID="NameFilterDropDownList" runat="server" AutoCompleteMode="SuggestAppend" DataSourceID="CustInfoSource" DataTextField="Customer_Name" DataValueField="Customer_Name" DropDownStyle="DropDownList" style="display: inline;" Width="60px" AutoPostBack="True">
                <asp:ListItem Selected="True"></asp:ListItem>
            </ajaxToolkit:ComboBox>
            &nbsp;
            <asp:Button ID="ClearButton" runat="server" Text="Clear"/>
        </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="CustomerNameLabel" runat="server" Text="Name:">
            </asp:Label>
            <asp:Label ID="CustomerNameData" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>

     <asp:Label ID="DateofOrderLabel" runat="server" Text="Date of Order:">
            </asp:Label>
        <asp:Label ID="DateofOrderData" runat="server">
            </asp:Label>  

        </td>
    </tr>
        <tr>
            <td>
                <asp:Label ID="DatetobeShippedLabel" runat="server" Text="Date to be Shipped:">
            </asp:Label>
        <asp:Label ID="DatetobeShippedData" runat="server"></asp:Label>
            </td>
        </tr>
     <tr>
     <td align="left">
        <asp:Label ID="TotalPriceLabel" runat="server" Text="Total Price: Rp. "></asp:Label>
        <asp:Label ID="TotalPriceData" runat="server"></asp:Label>
        </td>
     </tr> 
    <tr>
        <td>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="GridViewDataSource2" EnableSortingAndPagingCallbacks="True" AutoGenerateColumns="False" HorizontalAlign="left">
            <Columns>
                <asp:BoundField DataField="Product_Name" HeaderText="Product_Name" SortExpression="Product_Name" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
            </Columns>
        </asp:GridView>
        </td>
        <td align="left">
            <br />
             <asp:Label ID="StatusLabel" runat="server" Text="Status:"></asp:Label>
            <asp:Label ID="CompletedData" runat="server"></asp:Label>
        <br />
            <br />
        <asp:Label ID="PaidLabel" runat="server" Text="Paid?:">
        </asp:Label>
        <asp:Label ID="PaidData" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="ReadyLabel" runat="server" Text="Ready to be shipped?:">
        </asp:Label>
        <asp:Label ID="ReadyData" runat="server"></asp:Label>
        </td>
    </tr>
     <tr>
     <td>
     &nbsp;
     </td>
     </tr> 
    <tr>
        <td>
        <asp:Button ID="previousbutton" runat="server" Text="Previous" />
        &nbsp; &nbsp; &nbsp; 
        <asp:Button ID="nextbutton" runat="server" Text="Next" />
        </td>    
        <td align="right">
         <asp:Button ID="Home" runat="server" Text="Back to Home" PostBackUrl="~/home.aspx" />
        </td>
    </tr>
    </table>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:AccessDataSource ID="GridViewDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
        SelectCommand="SELECT Product_List.Product_Name, Products_of_Orders.Amount FROM (Product_List INNER JOIN Products_of_Orders ON Product_List.Product_ID = Products_of_Orders.Product_ID) WHERE Products_of_Orders.Order_ID = @orderid">
        <SelectParameters>
        <asp:controlparameter name="@orderid" controlid="OrderIDData" propertyname="Text"/>
        </SelectParameters>
        </asp:AccessDataSource>
        
        <asp:AccessDataSource ID="CustInfoSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader" 
        SelectCommand="SELECT * FROM [Customer_Information]">
        </asp:AccessDataSource>
        
        <asp:AccessDataSource ID="LoadDataSource1" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
        SelectCommand="SELECT [Order_ID], [Customer_ID] FROM [Orders]">
        </asp:AccessDataSource>
        
        <asp:AccessDataSource ID="LoadDataSource2" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
        SelectCommand="SELECT [Customer_ID], [Customer_Name] FROM [Customer_Information]">
        </asp:AccessDataSource>
        
        <asp:AccessDataSource ID="OrdersDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
        SelectCommand="SELECT * FROM [Orders]">
        </asp:AccessDataSource>
       
        <asp:AccessDataSource ID="GridViewDataSource2" runat="server" DataFile="~/App_Data/Computer Science IA.mdb"
        SelectCommand="SELECT Product_List.Product_Name, Products_of_Orders.Amount FROM (Product_List INNER JOIN Products_of_Orders ON Product_List.Product_ID = Products_of_Orders.Product_ID) WHERE Products_of_Orders.Order_ID = @orderid">
        <SelectParameters>
        <asp:ControlParameter ControlID="OrderIDData" Name="@orderid" PropertyName="Text" />
        </SelectParameters>
        </asp:AccessDataSource>
        
        <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader" 
        SelectCommand="SELECT Product_List.Product_Name, Products_of_Orders.Amount FROM (Product_List INNER JOIN Products_of_Orders ON Product_List.Product_ID = Products_of_Orders.Product_ID) WHERE Products_of_Orders.Order_ID = @orderid">
        <SelectParameters>
        <asp:ControlParameter ControlID="OrderIDData" Name="@orderid" PropertyName="Text" />
        </SelectParameters>
        </asp:AccessDataSource>
        
        <asp:AccessDataSource ID="LinkedLoadDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
        SelectCommand="SELECT Orders.Order_ID, Customer_Information.Customer_Name, Orders.[Completed?], Orders.[Paid?], Orders.[Ready_to_be_shipped?], Customer_Information.Customer_ID, Orders.Date_of_Order, Orders.Date_to_be_Shipped FROM (Orders INNER JOIN Customer_Information ON Orders.Customer_ID = Customer_Information.Customer_ID)">
        </asp:AccessDataSource>
        
        <asp:AccessDataSource ID="TotalPriceSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
        SelectCommand="SELECT Product_List.Price, Products_of_Orders.Amount FROM (Product_List INNER JOIN Products_of_Orders ON Product_List.Product_ID = Products_of_Orders.Product_ID) WHERE Products_of_Orders.Order_ID = @orderid">
        <SelectParameters>
        <asp:controlparameter name="@orderid" controlid="OrderIDData" propertyname="Text"/>
        </SelectParameters>
        </asp:AccessDataSource>
        </div>
    </form>
</body>
</html>
