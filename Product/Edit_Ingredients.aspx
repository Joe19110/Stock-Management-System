<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Edit_Ingredients.aspx.vb" Inherits="Edit_Ingredients" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit Ingredients</title>
</head>
<body>
    <form id="Edit_Ingredients" runat="server">
    <table style="margin:auto;width:27%;margin-top:14%;text-align:left">
    <tr>
        <td align="left">
            <asp:Label ID="IngredientIDLabel" runat="server" Text="Ingredient ID:">
            </asp:Label>

            <asp:Label ID="IngredientIDData" runat="server">
            </asp:Label>
        </td>
        <td align="right" rowspan="3">            
            <asp:Button ID="NewIngredient" runat="server" Text="New&#13;Ingredient" Width="100px"/>
            <br />
            <br />
            <asp:Button ID="DeleteIngredient" runat="server" Text="Delete&#13;Ingredient" Width="100px"/>  
        </td>
    </tr>
       
    <tr>  
        <td align="left">
            <asp:Label ID="IngredientNameLabel" runat="server" Text="Name:">
            </asp:Label>
            <asp:TextBox ID="IngredientNameData" runat="server" Width="80px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="CostLabel" runat="server" Text="Cost: Rp. "></asp:Label>
            <asp:TextBox ID="CostInput" runat="server" Width="60px"></asp:TextBox>
            &nbsp;
            <asp:Button ID="Save" runat="server" Text="Save" />
        </td>      
        <td align="center">          
        <asp:Button ID="CancelButton" runat="server" Text="Cancel" Visible="False" />
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
        <asp:AccessDataSource ID="IngredientListDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb"
            DataSourceMode="DataReader" 
            DeleteCommand="DELETE FROM Ingredient_List WHERE Ingredient_ID = @ingredientid"
            InsertCommand="INSERT INTO Ingredient_List (Ingredient_ID, Ingredient_Name, Cost) VALUES (@ingredientid, @ingredientname, @cost)"
            SelectCommand="SELECT * FROM [Ingredient_List]" 
            UpdateCommand="UPDATE Ingredient_List SET Ingredient_Name = @ingredientname, Cost = @cost WHERE Ingredient_ID = @ingredientid">
         <UpdateParameters>
                      <asp:controlparameter name="@ingredientname" controlid="ingredientnamedata" propertyname="text" />
                      <asp:controlparameter name="@cost" controlid="costinput" propertyname="text" DbType="Int16"/>
                      <asp:controlparameter name="@ingredientid" controlid="ingredientiddata" propertyname="text" />
            </UpdateParameters>
            <InsertParameters>
                      <asp:Parameter Name="ingredientid" DefaultValue="" />
                      <asp:Parameter Name="ingredientname" DefaultValue="" />
                      <asp:Parameter Name="cost" DefaultValue="" />
                </InsertParameters>
                <DeleteParameters>
                     <asp:parameter name="ingredientid" defaultvalue=""/>
                </DeleteParameters>
        </asp:AccessDataSource>
        <asp:AccessDataSource ID="IngredientsNeededDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb"
            DataSourceMode="DataReader" DeleteCommand="DELETE FROM Ingredients_Needed WHERE Ingredients_Needed.[Ingredient_ID] = @ingredientid"
            SelectCommand="SELECT Ingredients_Needed.* FROM Ingredients_Needed">
            <DeleteParameters>
                <asp:Parameter Name="ingredientid" DefaultValue="" />
            </DeleteParameters>
        </asp:AccessDataSource>
    
    </form>
</body>
</html>
