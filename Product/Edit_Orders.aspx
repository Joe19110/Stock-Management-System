<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Edit_Orders.aspx.vb" Inherits="Edit_Orders" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit Orders</title>
    <link rel="stylesheet" href="ia.css" />
</head>
<body>
    <form id="Edit_Orders" runat="server">

    <table style="margin:auto;width:45%;margin-top:10%;text-align:left">
    <tr>
        <td align="left" colspan="2">
            <asp:Label ID="OrderIDLabel" runat="server" Text="Order ID:">
            </asp:Label>

            <asp:Label ID="OrderIDData" runat="server">
            </asp:Label>
            &nbsp; 
            <asp:Button ID="SearchOrder" runat="server" Text="Search by ID" />
        </td>
        <td rowspan="4" align="right">   
            <asp:Button ID="Save" runat="server" Text="Save" width="100px"/>
            <br />
            <br />
            <asp:Button ID="NewOrder" runat="server" Text="New Order" width="100px"/>    
            <br />
            <br />
            <asp:Button ID="DeleteOrder" runat="server" Text="Delete Order" width="100px"/>
            <asp:Button ID="CancelButton" runat="server" Text="Cancel" Visible="False" width="100px"/>             
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
    <td colspan="2">
     <asp:Label ID="DatetobeShippedLabel" runat="server" Text="Date to be Shipped:">
            </asp:Label>
        <div style="font-size:24px;display:inline;">
        <asp:TextBox ID="DayInput" runat="server" MaxLength="2" Width="40px" style="display: inline;"></asp:TextBox>
        <ajaxToolkit:NumericUpDownExtender ID="DayInput_NumericUpDownExtender" runat="server" BehaviorID="DayInput_NumericUpDownExtender" Maximum="31" Minimum="1" RefValues="" Step="1" ServiceDownPath="" Tag="" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="DayInput" Width="40"/>
        <asp:Label ID="divider1" runat="server" Text="/"></asp:Label>
        <asp:TextBox ID="MonthInput" runat="server" MaxLength="2" Width="40px" style="display: inline;"></asp:TextBox>
        <ajaxToolkit:NumericUpDownExtender ID="MonthInput_NumericUpDownExtender" runat="server" BehaviorID="MonthInput_NumericUpDownExtender" Maximum="12" Minimum="1" RefValues="" Step="1" ServiceDownPath="" Tag="" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="MonthInput" Width="40" />
        <asp:Label ID="divider2" runat="server" Text="/"></asp:Label>
        <asp:TextBox ID="YearInput" runat="server" MaxLength="4" Width="70px" style="display: inline;"></asp:TextBox>
        <ajaxToolkit:NumericUpDownExtender ID="YearInput_NumericUpDownExtender" runat="server" BehaviorID="YearInput_NumericUpDownExtender" Maximum="3000" Minimum="2023" RefValues="" Step="1" ServiceDownPath=""  Tag="" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="YearInput" Width="70" />
    </div>
            </td>
    </tr>
    <tr>  
        <td align="left" colspan="2">
            <asp:Label ID="CustomerNameLabel" runat="server" Text="Name:">
            </asp:Label>
            
            <ajaxToolkit:ComboBox ID="NameDropDownList" runat="server" AutoCompleteMode="SuggestAppend" DataSourceID="CustInfoSource" DataTextField="Customer_Name" DataValueField="Customer_Name" DropDownStyle="DropDownList" style="display: inline;" Width="60px">
            </ajaxToolkit:ComboBox>
            &nbsp; 
            <asp:Button ID="SearchName" runat="server" Text="Search by ID" />
        </td> 
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
            <ajaxToolkit:ComboBox ID="ProductDropDownList" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="True" DataSourceID="ProductListSource" DataTextField="Product_Name" DataValueField="Product_ID" DropDownStyle="DropDownList" MaxLength="0" style="display: inline;" Width="60px">
            </ajaxToolkit:ComboBox>
            &nbsp; 
            <asp:TextBox ID="AmountInput" runat="server" Width="40px" Wrap="False" EnableTheming="True"></asp:TextBox>
            <ajaxToolkit:NumericUpDownExtender ID="AmountInput_NumericUpDownExtender" runat="server" BehaviorID="AmountInput_NumericUpDownExtender" Maximum="1.7976931348623157E+308" Minimum="0" RefValues="" Step="1" ServiceDownPath=""  Tag="" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="AmountInput" Width="40"/>

            <asp:Button ID="SearchProduct" runat="server" Text="Search by ID" />

            </td>   
        <td align="left" rowspan="3">
            <asp:Label ID="DeleteProductLabel" runat="server" Text="Product to be Deleted:"></asp:Label>
            <br />
            <ajaxToolkit:ComboBox ID="ProductDeleteDropDownList" runat="server" AutoCompleteMode="SuggestAppend" 
                                  AutoPostBack="True" DataSourceID="GridViewDataSource1" DataTextField="Product_Name" 
                                  DataValueField="Product_ID" DropDownStyle="DropDownList" MaxLength="0" style="display: inline;" Width="60px">
            </ajaxToolkit:ComboBox>
            &nbsp; 
            <asp:Button ID="SearchProductDelete" runat="server" Text="Search by ID" style="height: 26px" />
            <br />
            <br />
            <asp:Button ID="DeleteProduct" runat="server" Text="Delete Product"/>
            
        </td>    
     </tr>
     <tr>
     <td>
     &nbsp;
     </td>
     </tr>   
     <tr>
        <td style="text-align:center">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="GridViewDataSource1" EnableSortingAndPagingCallbacks="True" HorizontalAlign="Left">
                <Columns>
                    <asp:BoundField DataField="Product_Name" HeaderText="Product_Name" SortExpression="Product_Name" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                </Columns>
            </asp:GridView>
            </td>
         <td align="left">
            <asp:CheckBox ID="ready" runat="server" AutoPostBack="True" Text="Ready to be shipped?" />
            <br />
            <br />
            <asp:CheckBox ID="paid" runat="server" AutoPostBack="True" Text="Paid?" />
            <br />
             <br />
             <br />
             <asp:Label ID="StatusLabel" runat="server" Text="Status:" Font-Size="Large"></asp:Label>
             <asp:DropDownList ID="CompletedDropDownList" runat="server" AutoPostBack="True">
                 <asp:ListItem Value="True">Completed</asp:ListItem>
                 <asp:ListItem Selected="True" Value="False">Ongoing</asp:ListItem>
             </asp:DropDownList>
        </td>  
        
     </tr> 
     <tr>
     <td>
     &nbsp;
     </td>
     </tr>  
     <tr>
        <td align="left" colspan="2">
            <asp:Button ID="PreviousRecord" runat="server" Text="Previous" />
            &nbsp; &nbsp; &nbsp; 
            <asp:Button ID="NextRecord" runat="server" Text="Next" />
        </td> 
        
        <td align="right">
         <asp:Button ID="Home" runat="server" Text="Back to Home" PostBackUrl="~/home.aspx" />
        </td> 
     </tr> 
    
    </table>

    <div>
            
            <asp:ScriptManager ID="ScriptManager1" runat="server"  EnablePageMethods="true">
            </asp:ScriptManager>
            
            
            <asp:AccessDataSource ID="LinkedLoadDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
            SelectCommand="SELECT Orders.Order_ID, Customer_Information.Customer_Name, Orders.[Completed?], Orders.[Paid?], Orders.[Ready_to_be_shipped?], Customer_Information.Customer_ID, Orders.Date_of_Order, Orders.Date_to_be_Shipped FROM (Orders INNER JOIN Customer_Information ON Orders.Customer_ID = Customer_Information.Customer_ID)">
            </asp:AccessDataSource>
            
            
            <asp:AccessDataSource ID="GridViewDataSource2" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
            SelectCommand="SELECT Product_List.[Product_Name], Products_of_Orders.[Amount], Products_of_Orders.[Product_ID] FROM (Product_List INNER JOIN Products_of_Orders ON Product_List.[Product_ID] = Products_of_Orders.[Product_ID]) WHERE (Products_of_Orders.[Order_ID] = @orderid) ORDER BY Product_List.[Product_ID]" 
            DeleteCommand="DELETE FROM Products_of_Orders WHERE (Order_ID = @orderid AND Product_ID = @productid)">
            <SelectParameters>
                      <asp:controlparameter name="@orderid" controlid="OrderIDData" propertyname="Text"/>
                </SelectParameters>
                <DeleteParameters>
                    <asp:parameter name="orderid" defaultvalue=""/>
                    <asp:parameter name="productid" defaultvalue=""/>
                </DeleteParameters>
            </asp:AccessDataSource>
            
            
            
            <asp:AccessDataSource ID="GridViewDataSource1" runat="server" DataFile="~/App_Data/Computer Science IA.mdb"
            SelectCommand="SELECT Product_List.[Product_Name], Products_of_Orders.[Amount], Products_of_Orders.[Product_ID] FROM (Product_List INNER JOIN Products_of_Orders ON Product_List.[Product_ID] = Products_of_Orders.[Product_ID]) WHERE (Products_of_Orders.[Order_ID] = @orderid) ORDER BY Product_List.[Product_ID]">
                <SelectParameters>
                      <asp:controlparameter name="@orderid" controlid="OrderIDData" propertyname="Text"/>
                </SelectParameters>
            </asp:AccessDataSource>
            
            <asp:AccessDataSource ID="CustInfoSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
                SelectCommand="SELECT * FROM [Customer_Information] ORDER BY [Customer_ID]">
            </asp:AccessDataSource>
            
            <asp:AccessDataSource ID="ValidationSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
            SelectCommand="SELECT [Order_ID] FROM [Orders]">
            </asp:AccessDataSource>
            
            <asp:AccessDataSource ID="ProductListSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
            SelectCommand="SELECT * FROM [Product_List] ORDER BY Product_ID">
            </asp:AccessDataSource>
            
            <asp:AccessDataSource ID="OrdersSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
            SelectCommand="SELECT * FROM [Orders]" 
            UpdateCommand="UPDATE Orders SET Orders.Customer_ID = @custid, Orders.[Completed?] = @completed, Orders.[Paid?] = @paid, Orders.[Ready_to_be_shipped?] = @ready, Orders.[Date_to_be_Shipped] = @shipdate WHERE (Orders.Order_ID = @orderid)" 
            InsertCommand="INSERT INTO Orders(Order_ID, Customer_ID, [Completed?], [Paid?], [Ready_to_be_shipped?], [Date_of_Order], [Date_to_be_Shipped]) VALUES (@orderid, @custid, @completed, @paid, @ready, @orderdate, @shipdate)" 
            DeleteCommand="DELETE FROM Orders WHERE (Orders.Order_ID = @orderid)">
                <UpdateParameters>
                      <asp:parameter name="custid" defaultvalue=""/>
                      <asp:parameter name="completed" defaultvalue="" DbType="Boolean"/>
                      <asp:parameter name="paid" defaultvalue="" DbType="Boolean"/>
                      <asp:parameter name="ready" defaultvalue="" DbType="Boolean"/>
                      <asp:parameter name="shipdate" defaultvalue="" DbType="Date"/>
                      <asp:controlparameter name="@orderid" controlid="OrderIDData" propertyname="Text"/>
                </UpdateParameters>
                <InsertParameters>
                      <asp:parameter name="orderid" defaultvalue=""/>
                      <asp:parameter name="custid" defaultvalue=""/>
                      <asp:parameter name="completed" defaultvalue="" DbType="Boolean"/>
                      <asp:parameter name="paid" defaultvalue="" DbType="Boolean"/>
                      <asp:parameter name="ready" defaultvalue="" DbType="Boolean"/>
                      <asp:parameter name="orderdate" defaultvalue="" DbType="Date"/>
                      <asp:parameter name="shipdate" defaultvalue="" DbType="Date"/>
                </InsertParameters>
                <DeleteParameters>
                      <asp:parameter name="orderid" defaultvalue=""/>
                </DeleteParameters>
            </asp:AccessDataSource>
            
            <asp:AccessDataSource ID="ProductsofOrdersSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader"
            SelectCommand="SELECT * FROM [Products_of_Orders]" 
            DeleteCommand="DELETE FROM Products_of_Orders WHERE (Products_of_Orders.Order_ID = @orderid)" 
            InsertCommand="INSERT INTO Products_of_Orders (Product_ID, Amount, Order_ID) VALUES (@productid, @amount, @orderid)" 
            UpdateCommand="UPDATE Products_of_Orders SET Amount = @amount WHERE (Product_ID = @productid AND Order_ID = @orderid)">
                <DeleteParameters>
                     <asp:parameter name="orderid" defaultvalue=""/>
                </DeleteParameters>
                <InsertParameters>
                      <asp:parameter name="productid" defaultvalue=""/>
                      <asp:parameter name="amount" defaultvalue=""/>
                      <asp:parameter name="orderid" defaultvalue=""/>
                </InsertParameters>
                <UpdateParameters>
                      <asp:controlparameter name="@amount" controlid="AmountInput" propertyname="Text" DbType="Int16"/>
                      <asp:controlparameter name="@productid" controlid="ProductDropDownList" propertyname="SelectedValue"/>
                      <asp:controlparameter name="@orderid" controlid="OrderIDData" propertyname="Text"/>
                </UpdateParameters>
            </asp:AccessDataSource>
        <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/Computer Science IA.mdb"
            DataSourceMode="DataReader" SelectCommand="SELECT * FROM [Test]"></asp:AccessDataSource>
            
    </div>
    
</form>
</body>
</html>