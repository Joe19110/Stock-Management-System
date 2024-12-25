<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Edit_Products.aspx.vb" Inherits="Edit_Products" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Edit Products</title>
</head>
<body>
    <form id="Edit_Products" runat="server">
    <table style="margin:auto;width:37%;margin-top:10%;text-align:left">
    <tr>
        <td align="left">
            <asp:Label ID="ProductIDLabel" runat="server" Text="Product ID:">
            </asp:Label>

            <asp:Label ID="ProductIDData" runat="server">
            </asp:Label>
            &nbsp;
            <asp:Button ID="SearchProduct" runat="server" Text="Search by ID" />
            
        </td>
        <td align="center" rowspan="4">
            <asp:Button ID="Save" runat="server" Text="Save" Width="120px"/>
            <br />
            <br />
            <asp:Button ID="NewProduct" runat="server" Text="New&#13;Product" Width="120px" />   
            <br />
            <br />
            <asp:Button ID="DeleteProduct" runat="server" Text="Delete&#13;Product" Width="120px" />  
            <asp:Button ID="CancelButton" runat="server" Text="Cancel" Visible="False" />
        </td>
    </tr>
    <tr>  
        <td align="left">
            <asp:Label ID="ProductNameLabel" runat="server" Text="Name:">
            </asp:Label>
            
            <asp:TextBox ID="ProductNameData" runat="server" Width="100px">
            </asp:TextBox>
        </td>
        
    </tr>
    <tr>  
        <td align="left">
            <asp:Label ID="ProductPriceLabel" runat="server" Text="Price: Rp. "></asp:Label>
            
            <asp:TextBox ID="ProductPriceData" runat="server" Width="100px">
            </asp:TextBox>
        </td>

    </tr>
    <tr>  
        <td align="left">
            <asp:Label ID="ProductCostLabel" runat="server" Text="Cost: Rp. "></asp:Label>
            
            <asp:Label ID="ProductCostData" runat="server">
            </asp:Label>
        </td>
 
    </tr>
    <tr>
        <td>
            <ajaxToolkit:ComboBox ID="IngredientDropDownList" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="True" DataSourceID="IngredientListDataSource" DataTextField="Ingredient_Name" DataValueField="Ingredient_ID" DropDownStyle="DropDownList" MaxLength="0" style="display: inline;" Width="60px">
            </ajaxToolkit:ComboBox>
            &nbsp;
            <asp:TextBox ID="AmountInput" runat="server" Width="40px"></asp:TextBox>
            <ajaxToolkit:NumericUpDownExtender ID="AmountInput_NumericUpDownExtender" runat="server" BehaviorID="AmountInput_NumericUpDownExtender" Maximum="1.7976931348623157E+308" Minimum="0" RefValues="" ServiceDownMethod="" ServiceDownPath="" ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="AmountInput" Width="40" />
            <asp:Button ID="SearchIngredient" runat="server" Text="Search by ID" />
            </td>     
        <td rowspan="4">
            <div align="left">
            <asp:Label ID="DeleteIngredientLabel" runat="server" Text="Ingredient to be Deleted:"></asp:Label>
            <br />
            <ajaxToolkit:ComboBox ID="IngredientDeleteDropDownList" runat="server" AutoCompleteMode="SuggestAppend" 
                                  AutoPostBack="True" DataSourceID="GridViewDataSource2" DataTextField="Ingredient_Name" 
                                  DataValueField="Ingredient_ID" DropDownStyle="DropDownList" MaxLength="0" style="display: inline;" Width="60px">
            </ajaxToolkit:ComboBox>
            &nbsp;<asp:Button ID="SearchIngredientDelete" runat="server" Text="Search by ID" style="height: 26px" />
            <br />
            <br />
            </div>
            <div align="right" style="margin-right:7%">
            <asp:Button ID="DeleteIngredientButton" runat="server" Text="Delete Ingredient" Width="120px"/>
            </div>
        </td> 
     </tr>
     <tr>
     <td style="height: 21px">
     &nbsp;
     </td>
     </tr>   
     <tr>
        <td style="text-align:center">
            <asp:GridView ID="GridView1" runat="server" DataSourceID="GridViewDataSource1" EnableSortingAndPagingCallbacks="True" AutoGenerateColumns="False" HorizontalAlign="Left">
                <Columns>
                    <asp:BoundField DataField="Ingredient_Name" HeaderText="Ingredient_Name" SortExpression="Ingredient_Name" />
                    <asp:BoundField DataField="Amount_Needed" HeaderText="Amount_Needed" SortExpression="Amount_Needed" />
                </Columns>
            </asp:GridView>
        </td>    
     </tr> 
     <tr>
     <td>
     &nbsp;
     </td>
     </tr>  
     <tr>
        <td align="left">
            <asp:Button ID="PreviousRecord" runat="server" Text="Previous" />
            &nbsp; &nbsp; &nbsp; 
            <asp:Button ID="NextRecord" runat="server" Text="Next" />
        </td>    
        <td align="right">
         <asp:Button ID="Home" runat="server" Text="Back to Home" PostBackUrl="~/home.aspx" />
        </td>   
     </tr> 
    
    </table>
            
            <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            
            
        <asp:AccessDataSource ID="GridViewDataSource1" runat="server" DataFile="~/App_Data/Computer Science IA.mdb"
            SelectCommand="SELECT Ingredient_List.Ingredient_Name, Ingredients_Needed.Amount_Needed FROM (Ingredients_Needed INNER JOIN Ingredient_List ON Ingredients_Needed.Ingredient_ID = Ingredient_List.Ingredient_ID) WHERE Ingredients_Needed.Product_ID = @productid ORDER BY Ingredient_List.[Ingredient_ID]">
        <SelectParameters>
        <asp:controlparameter name="@productid" controlid="ProductIDData" propertyname="Text"/>
        </SelectParameters>
        </asp:AccessDataSource>
        <asp:AccessDataSource ID="GridViewDataSource2" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader" 
            SelectCommand="SELECT Ingredient_List.Ingredient_Name, Ingredients_Needed.Amount_Needed, Ingredients_Needed.Ingredient_ID FROM (Ingredients_Needed INNER JOIN Ingredient_List ON Ingredients_Needed.Ingredient_ID = Ingredient_List.Ingredient_ID) WHERE Ingredients_Needed.Product_ID = @productid ORDER BY Ingredient_List.[Ingredient_ID]" 
            DeleteCommand="DELETE FROM Ingredients_Needed WHERE (Ingredient_ID = @ingredientid AND Product_ID = @productid)">
        <SelectParameters>
         <asp:controlparameter name="@productid" controlid="productiddata" propertyname="text" />
        </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="ingredientid" DefaultValue="" />
                <asp:Parameter Name="productid" DefaultValue="" />
            </DeleteParameters>
        </asp:AccessDataSource>
        <asp:AccessDataSource ID="IngredientListDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb"
            SelectCommand="SELECT * FROM [Ingredient_List]" DataSourceMode="DataReader"></asp:AccessDataSource>
        <asp:AccessDataSource ID="ProductListDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb"
            SelectCommand="SELECT * FROM [Product_List]" DataSourceMode="DataReader" 
            DeleteCommand="DELETE FROM Product_List WHERE Product_ID = @productid" 
            InsertCommand="INSERT INTO Product_List (Product_ID, Product_Name, Price) VALUES (@productid, @productname, @price)" 
            UpdateCommand="UPDATE Product_List SET Product_Name = @productname, Price = @productprice WHERE (Product_ID = @productid)">
            <UpdateParameters>
                      <asp:controlparameter name="@productname" controlid="productnamedata" propertyname="text" />
                      <asp:controlparameter name="@productprice" controlid="productpricedata" propertyname="text" />
                      <asp:controlparameter name="@productid" controlid="productiddata" propertyname="text" />
            </UpdateParameters>
            <InsertParameters>
                      <asp:Parameter Name="productid" DefaultValue="" />
                      <asp:Parameter Name="productname" DefaultValue="" />
                      <asp:Parameter Name="productprice" DefaultValue="" />
                </InsertParameters>
                <DeleteParameters>
                     <asp:parameter name="productid" defaultvalue=""/>
                </DeleteParameters>
            </asp:AccessDataSource>
        <asp:AccessDataSource ID="IngredientsNeededDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb"
            SelectCommand="SELECT * FROM [Ingredients_Needed]" 
            DeleteCommand="DELETE FROM Ingredients_Needed WHERE Product_ID = @productid" 
            InsertCommand="INSERT INTO Ingredients_Needed (Ingredient_ID, Product_ID, Amount_Needed) VALUES (@ingredientid, @productid, @amount)" 
            UpdateCommand="UPDATE Ingredients_Needed SET Amount_Needed = @amount WHERE (Product_ID = @productid AND Ingredient_ID = @ingredientid)" DataSourceMode="DataReader">
            <UpdateParameters>
                      <asp:controlparameter name="@amount" controlid="AmountInput" propertyname="text" DbType="Int16" />
                      <asp:controlparameter name="@productid" controlid="productiddata" propertyname="text" />
                      <asp:controlparameter name="@ingredientid" controlid="IngredientDropDownList" propertyname="selectedvalue" />
            </UpdateParameters>
             <InsertParameters>
                      <asp:Parameter Name="ingredientid" DefaultValue="" />
                      <asp:Parameter Name="productid" DefaultValue="" />
                      <asp:Parameter Name="amount" DefaultValue="" />
                </InsertParameters>
                <DeleteParameters>
                     <asp:parameter name="productid" defaultvalue=""/>
                </DeleteParameters>
            </asp:AccessDataSource>
    
        <asp:AccessDataSource ID="ProductsOfOrdersDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb"
            SelectCommand="SELECT Products_of_Orders.* FROM Products_of_Orders" 
            DeleteCommand="DELETE FROM Products_of_Orders WHERE Product_ID = @productid" DataSourceMode="DataReader">
                <DeleteParameters>
                     <asp:parameter name="productid" defaultvalue=""/>
                </DeleteParameters>
            </asp:AccessDataSource>
    
    </form>
</body>
</html>
