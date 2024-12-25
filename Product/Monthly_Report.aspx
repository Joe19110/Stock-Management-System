﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Monthly_Report.aspx.vb" Inherits="Monthly_Report" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Monthly Report</title>
</head>
<body>
    <style>
        td {border: 1px solid black;padding:1%}
    </style>
    <form id="form1" runat="server">
<table style="margin:auto;width:50%;margin-top:10%;text-align:left;border-collapse:collapse" >
    <tr>
        <th colspan="2" style="text-align:center; padding-bottom:2%">
        <asp:Label ID="TitleHeading" runat="server" Text="Monthly Report"></asp:Label>                     
        </th>
    </tr>
    <tr>
        <td style="text-align:center">
        <asp:Label ID="TopCustomersHeading" runat="server" Text="Top Customers 
 of the Month"></asp:Label>                     
        </td>
        <td style="text-align:center">
        <asp:Label ID="TopProductsHeading" runat="server" Text="Top Selling Products 
 of the Month"></asp:Label>          
        </td>
        <td style="border:none;padding-left:2%" rowspan="2">
        <asp:Label ID="MonthFilterLabel" runat="server" Text="Month:"></asp:Label>
            <br />
        <ajaxToolkit:ComboBox ID="MonthFilter" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="True" DropDownStyle="DropDownList" MaxLength="0" style="display: inline;" DataTextFormatString="{0:d}" Width="80px" BorderWidth="0px">
            <asp:ListItem Value="1">January</asp:ListItem>
            <asp:ListItem Value="2">February</asp:ListItem>
            <asp:ListItem Value="3">March</asp:ListItem>
            <asp:ListItem Value="4">April</asp:ListItem>
            <asp:ListItem Value="5">May</asp:ListItem>
            <asp:ListItem Value="6">June</asp:ListItem>
            <asp:ListItem Value="7">July</asp:ListItem>
            <asp:ListItem Value="8">August</asp:ListItem>
            <asp:ListItem Value="9">September</asp:ListItem>
            <asp:ListItem Value="10">October</asp:ListItem>
            <asp:ListItem Value="11">November</asp:ListItem>
            <asp:ListItem Value="12">December</asp:ListItem>
        </ajaxToolkit:ComboBox>
            <br />
            <br />
        <asp:Label ID="YearFilterLabel" runat="server" Text="Year:"></asp:Label>
            <br />
            <ajaxToolkit:ComboBox ID="YearFilter" runat="server" AutoPostBack="True" DataSourceID="YearDataSource" DataTextField="Expr1000" DataValueField="Expr1000" MaxLength="0" style="display: inline;" Width="60px" BorderWidth="0px">
            </ajaxToolkit:ComboBox>
        </td>
    </tr>
    <tr>
        <td>
        <asp:Label ID="TopCust1" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="TopCust2" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="TopCust3" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="TopCust4" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="TopCust5" runat="server" Text=""></asp:Label>
        </td>
        <td>
        <asp:Label ID="TopProduct1" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="TopProduct2" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="TopProduct3" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="TopProduct4" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="TopProduct5" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="border:none">
         <asp:Label ID="TotalRevenueLabel" runat="server" Text="Total Revenue: Rp. "></asp:Label>
         <asp:Label ID="TotalRevenueData" runat="server" Text=""></asp:Label> 
        </td>
        <td style="border:none">
            <asp:Label ID="TotalCostLabel" runat="server" Text="Total Cost: Rp. "></asp:Label>
         <asp:Label ID="TotalCostData" runat="server" Text=""></asp:Label> 
        </td>
    </tr>
    <tr>
        <td colspan="2" style="border:none">
         <asp:Label ID="TotalProfitsLabel" runat="server" Text="Total Profits: Rp. "></asp:Label>
         <asp:Label ID="TotalProfitsData" runat="server" Text=""></asp:Label>         
        </td>
        <td align="right" style="border:none">
         <asp:Button ID="Home" runat="server" Text="Back to Reports" PostBackUrl="~/Reports.aspx" />
        </td>
    </tr>
</table>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <asp:AccessDataSource ID="FilteredOrdersDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader" SelectCommand="SELECT * FROM [Orders] WHERE (MONTH(Date_of_Order) = @month AND YEAR(Date_of_Order) = @year)">
        <SelectParameters>
        <asp:ControlParameter ControlID="MonthFilter" PropertyName="SelectedValue" Name="@month" />
        <asp:ControlParameter ControlID="YearFilter" PropertyName="Text" Name="@year" />
        </SelectParameters>
        </asp:AccessDataSource>
        
        
        <asp:AccessDataSource ID="CustInfoDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader" SelectCommand="SELECT * FROM [Customer_Information]">
        </asp:AccessDataSource>
        
        <asp:AccessDataSource ID="FilteredProductsOfOrdersDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader" SelectCommand="SELECT * FROM [Products_of_Orders] WHERE Order_ID = @orderid">
        <SelectParameters>
            <asp:Parameter Name="orderid" DefaultValue="" />
        </SelectParameters>
        </asp:AccessDataSource>
        
        <asp:AccessDataSource ID="FilteredProductListDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader" SelectCommand="SELECT * FROM [Product_List] WHERE Product_ID = @productid">
        <SelectParameters>
            <asp:Parameter Name="productid" DefaultValue="" />
        </SelectParameters>
        </asp:AccessDataSource>
        
        <asp:AccessDataSource ID="ProductListDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader" SelectCommand="SELECT * FROM [Product_List]">
        </asp:AccessDataSource>
        
        <asp:AccessDataSource ID="OrdersDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader" SelectCommand="SELECT * FROM [Orders]">
        </asp:AccessDataSource>
        
        <asp:AccessDataSource ID="FilteredIngredientsNeededDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader" SelectCommand="SELECT * FROM [Ingredients_Needed] WHERE Product_ID = @productid">
        <SelectParameters>
        <asp:Parameter Name="productid" DefaultValue="" />
        </SelectParameters>
        </asp:AccessDataSource>
        
        <asp:AccessDataSource ID="FilteredIngredientListDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader" SelectCommand="SELECT * FROM [Ingredient_List] WHERE Ingredient_ID = @ingredientid">
        <SelectParameters>
        <asp:Parameter Name="ingredientid" DefaultValue="" />
        </SelectParameters>
        </asp:AccessDataSource>
        
        <asp:AccessDataSource ID="YearDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader" SelectCommand="SELECT DISTINCT YEAR(Date_of_Order) FROM [Orders]">
        </asp:AccessDataSource>
                
    </form>
</body>
</html>
