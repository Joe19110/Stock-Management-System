
Partial Class Edit_Products
    Inherits System.Web.UI.Page
    Public Sub displayInformation(ByVal flag As Integer)
        Dim nextflag As Boolean = False
        If flag = -1 Then
            Dim reader1 As Data.IDataReader = CType(ProductListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            reader1.Read()
            ProductIDData.Text = reader1("Product_ID")
            ProductNameData.Text = reader1("Product_Name")
            ProductPriceData.Text = reader1("Price")
            reader1.Close()
        ElseIf flag = 0 Then
            Dim nextid As Integer
            nextid = ProductIDData.Text
            getNextRecord(1)
            If ProductIDData.Text <> 0 Then
                Dim reader2 As Data.IDataReader = CType(ProductListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                While reader2.Read()
                    If reader2("Product_ID") = ProductIDData.Text Then
                        ProductIDData.Text = reader2("Product_ID")
                        ProductNameData.Text = reader2("Product_Name")
                        ProductPriceData.Text = reader2("Price")
                    End If
                End While
                reader2.Close()
            Else
                ProductIDData.Text = nextid
            End If
        ElseIf flag = 1 Then
            Dim previd As Integer
            previd = ProductIDData.Text
            getPreviousRecord(1)
            If ProductIDData.Text <> 0 Then
                Dim reader2 As Data.IDataReader = CType(ProductListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                While reader2.Read()
                    If reader2("Product_ID") = ProductIDData.Text Then
                        ProductIDData.Text = reader2("Product_ID")
                        ProductNameData.Text = reader2("Product_Name")
                        ProductPriceData.Text = reader2("Price")
                    End If
                End While
                reader2.Close()
            Else
                ProductIDData.Text = previd
            End If
        End If
        selectIngredientGridView()
    End Sub
    Public Sub refreshAmount()
        GridView1.DataBind()
        If GridView1.Rows.Count > 0 Then
            If IngredientDropDownList.SelectedItem IsNot Nothing Then
                Dim reader1 As Data.IDataReader = CType(GridViewDataSource2.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                Dim flag As Integer = 0
                AmountInput.Text = 0
                While reader1.Read()
                    If IngredientDropDownList.SelectedItem.Text = reader1("Ingredient_Name") Then
                        AmountInput.Text = reader1("Amount_Needed")
                        Exit While
                    Else
                        AmountInput.Text = 0
                    End If
                End While
                reader1.Close()
            Else
                selectIngredientGridView()
            End If
        Else
            AmountInput.Text = 0
        End If
    End Sub
    Public Sub selectIngredientGridView()
        Dim flag As Boolean = False
        Dim reader2 As Data.IDataReader = CType(GridViewDataSource2.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        reader2.Read()
        Dim reader1 As Data.IDataReader = CType(IngredientsNeededDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader1.Read()
            If ProductIDData.Text = reader1("Product_ID") Then
                IngredientDropDownList.SelectedValue = reader2("Ingredient_ID")
                flag = True
                Exit While
            End If
        End While
        reader1.Close()
        reader2.Close()
        If flag = False Then
            Dim reader3 As Data.IDataReader = CType(IngredientListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            reader3.Read()
            IngredientDropDownList.Items.FindByText(reader3("Ingredient_Name")).Selected = True
            reader3.Close()
        End If
        refreshAmount()
    End Sub
    Protected Sub PreviousRecord_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PreviousRecord.Click
        Dim reader As Data.IDataReader = CType(ProductListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        Dim emptyflag As Boolean
        emptyflag = False
        While reader.Read()
            If ProductIDData.Text = reader("Product_ID") Then
                displayInformation(1)
                refreshAmount()
                emptyflag = True
                Exit While
            End If
        End While
        If emptyflag = False Then
            MsgBox("Please save the new product first.", MsgBoxStyle.Exclamation)
        End If
        checkTotalCost()
    End Sub

    Protected Sub NextRecord_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NextRecord.Click
        displayInformation(0)
        refreshAmount()
        checkTotalCost()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            IngredientDropDownList.DataBind()
        End If

        If ProductIDData.Text = "" Then
            displayInformation(-1)
        End If
        checkTotalCost()
    End Sub
    Public Sub checkTotalCost()
        ProductCostData.Text = 0
        Dim reader1 As Data.IDataReader = CType(GridViewDataSource2.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader1.Read()
            ProductCostData.Text = ProductCostData.Text + checkCost(reader1("Ingredient_Name"), reader1("Amount_Needed"))
        End While
    End Sub
    Public Function checkCost(ByVal name As String, ByVal amount As Double) As Double
        Dim cost As Integer
        Dim reader2 As Data.IDataReader = CType(IngredientListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader2.Read()
            If name = reader2("Ingredient_Name") Then
                cost = cost + reader2("Cost") * amount
            End If
        End While
        reader2.Close()
        Return cost
    End Function
    Protected Sub Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Save.Click
        Dim neworderflag As Integer = 0
        Dim reader5 As Data.IDataReader = CType(ProductListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader5.Read()
            If ProductIDData.Text = reader5("Product_ID") Then
                neworderflag = 1
                Dim presenceflag As Integer = 0
                Dim reader As Data.IDataReader = CType(GridViewDataSource2.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                While reader.Read()
                    If IngredientDropDownList.SelectedItem.ToString = reader("Ingredient_Name") Then
                        IngredientsNeededDataSource.Update()
                        ProductListDataSource.Update()
                        presenceflag = 1
                        MsgBox("Changes successfully saved.")
                    End If
                End While
                reader.Close()
                If presenceflag = 0 Then
                    IngredientsNeededDataSource.InsertParameters("amount").DefaultValue = CInt(AmountInput.Text)
                    IngredientsNeededDataSource.InsertParameters("ingredientid").DefaultValue = IngredientDropDownList.SelectedValue
                    IngredientsNeededDataSource.InsertParameters("productid").DefaultValue = CInt(ProductIDData.Text)
                    IngredientsNeededDataSource.Insert()
                End If
                GridView1.DataBind()
            End If
        End While
        reader5.Close()
        If neworderflag = 0 Then
            ProductListDataSource.InsertParameters("productid").DefaultValue = ProductIDData.Text
            ProductListDataSource.InsertParameters("productname").DefaultValue = ProductNameData.Text
            ProductListDataSource.InsertParameters("productprice").DefaultValue = ProductPriceData.Text
            ProductListDataSource.Insert()
            IngredientsNeededDataSource.InsertParameters("amount").DefaultValue = CInt(AmountInput.Text)
            IngredientsNeededDataSource.InsertParameters("ingredientid").DefaultValue = IngredientDropDownList.SelectedValue
            IngredientsNeededDataSource.InsertParameters("productid").DefaultValue = CInt(ProductIDData.Text)
            IngredientsNeededDataSource.Insert()
            CancelButton.Visible = False
            NewProduct.Visible = True
            SearchProduct.Visible = True
            DeleteProduct.Visible = True
            DeleteIngredientLabel.Visible = True
            IngredientDeleteDropDownList.Visible = True
            DeleteIngredientButton.Visible = True
            SearchIngredientDelete.Visible = True
            MsgBox("New product successfully added.")
        End If
        IngredientDropDownList.Items.Clear()
        IngredientDropDownList.DataBind()
        IngredientDeleteDropDownList.DataBind()
        selectIngredientGridView()
        checkTotalCost()
    End Sub
    Protected Sub DropDownList1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles IngredientDropDownList.TextChanged
        refreshAmount()
    End Sub

    Protected Sub NewProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewProduct.Click
        Dim reader As Data.IDataReader = CType(ProductListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader.Read()
            ProductIDData.Text = reader("Product_ID") + 1
            AmountInput.Text = 0
            ProductNameData.Text = ""
            ProductPriceData.Text = ""
            ProductCostData.Text = ""
            IngredientDropDownList.SelectedIndex = 0
        End While
        reader.Close()
        refreshAmount()
        CancelButton.Visible = True
        NewProduct.Visible = False
        SearchProduct.Visible = False
        DeleteProduct.Visible = False
        DeleteIngredientLabel.Visible = False
        IngredientDeleteDropDownList.Visible = False
        DeleteIngredientButton.Visible = False
        SearchIngredientDelete.Visible = False
    End Sub

    Protected Sub DeleteProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteProduct.Click
        Dim ans As String
        Dim id As Integer
        ans = MsgBox("Are you sure you want to delete this product?", MsgBoxStyle.YesNo)
        If ans = MsgBoxResult.Yes Then
            id = ProductIDData.Text
            ProductListDataSource.DeleteParameters("productid").DefaultValue = ProductIDData.Text
            ProductListDataSource.Delete()
            ProductsOfOrdersDataSource.DeleteParameters("productid").DefaultValue = ProductIDData.Text
            ProductsOfOrdersDataSource.Delete()
            IngredientsNeededDataSource.DeleteParameters("productid").DefaultValue = ProductIDData.Text
            IngredientsNeededDataSource.Delete()
            MsgBox("Product successfully deleted.")
            displayInformation(-1)
        End If

    End Sub
    Public Function getMaxID() As Integer
        Dim maxid, temp As Integer
        maxid = 0
        Dim reader As Data.IDataReader = CType(ProductListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader.Read()
            temp = reader("Product_ID")
            If maxid < temp Then
                maxid = temp
            End If
        End While
        reader.Close()
        Return maxid
    End Function
    Public Function getMinID() As Integer
        Dim minid, temp As Integer
        minid = 999999999
        Dim reader As Data.IDataReader = CType(ProductListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader.Read()
            temp = reader("Product_ID")
            If minid > temp Then
                minid = temp
            End If
        End While
        reader.Close()
        Return minid
    End Function
    Public Sub getNextRecord(ByVal count As Integer)
        Dim nextflag As Boolean = False
        Dim tempid As Integer
        While Val(CInt(ProductIDData.Text) + count) <= getMaxID() And nextflag = False
            Dim reader As Data.IDataReader = CType(ProductListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                tempid = reader("Product_ID")
                If Val(CInt(ProductIDData.Text) + count) = tempid Then
                    nextflag = True
                    ProductIDData.Text = tempid
                    Exit While
                End If
            End While
            reader.Close()
            If nextflag = False Then
                nextflag = True
                getNextRecord(count + 1)
            Else
                Exit While
            End If
        End While
        If nextflag = False Then
            ProductIDData.Text = 0
        End If
    End Sub
    Public Sub getPreviousRecord(ByVal count As Integer)
        Dim prevflag As Boolean = False
        Dim tempid As Integer
        While Val(CInt(ProductIDData.Text) + count) >= getMinID() And prevflag = False
            Dim reader As Data.IDataReader = CType(ProductListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                tempid = reader("Product_ID")
                If Val(CInt(ProductIDData.Text) - count) = tempid Then
                    prevflag = True
                    ProductIDData.Text = tempid
                    Exit While
                End If
            End While
            reader.Close()
            If prevflag = False Then
                prevflag = True
                getPreviousRecord(count + 1)
            Else
                Exit While
            End If
        End While
        If prevflag = False Then
            ProductIDData.Text = 0
        End If
    End Sub
    Protected Sub SearchOrder_Click(sender As Object, e As EventArgs) Handles SearchProduct.Click
        Dim temp As Integer
        Dim ans As String
        Dim flag As Boolean = False
        ans = InputBox("Enter ID")
        If ans <> "" Then
            Dim reader As Data.IDataReader = CType(ProductListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                temp = reader("Product_ID")
                If temp = ans Then
                    ProductIDData.Text = temp + 1
                    displayInformation(1)
                    ProductIDData.Text = temp
                    flag = True
                    Exit While
                End If
            End While
            reader.Close()
            If flag = False Then
                MsgBox("ID not found.", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub
    Protected Sub SearchIngredientDelete_Click(sender As Object, e As EventArgs) Handles SearchIngredientDelete.Click
        Dim ans As String
        Dim flag As Boolean = False
        ans = InputBox("Enter ID")
        If ans <> "" Then
            Dim reader As Data.IDataReader = CType(GridViewDataSource2.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                If reader("Ingredient_ID") = ans Then
                    IngredientDeleteDropDownList.SelectedValue = reader("Ingredient_ID")
                    flag = True
                    Exit While
                End If
            End While
            reader.Close()
            If flag = False Then
                MsgBox("Ingredient not found.", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub
    Protected Sub DeleteIngredientButton_Click(sender As Object, e As EventArgs) Handles DeleteIngredientButton.Click
        If IngredientDeleteDropDownList.SelectedItem IsNot Nothing Then
            Dim ans As String
            ans = MsgBox(("Are you sure you want to delete the ingredient: " & IngredientDeleteDropDownList.SelectedItem.ToString & " from the product?"), MsgBoxStyle.YesNo)
            If ans = MsgBoxResult.Yes Then
                GridViewDataSource2.DeleteParameters("productid").DefaultValue = ProductIDData.Text
                GridViewDataSource2.DeleteParameters("ingredientid").DefaultValue = IngredientDeleteDropDownList.SelectedValue
                GridViewDataSource2.Delete()
                refreshAmount()
                checkTotalCost()
                MsgBox("Ingredient successfully deleted from the order.")
                selectIngredientGridView()
            End If
        Else
            MsgBox("Please select the ingredient to be deleted from the order.", MsgBoxStyle.Exclamation)
        End If
    End Sub
    Protected Sub SearchIngredient_Click(sender As Object, e As EventArgs) Handles SearchIngredient.Click
        Dim ans As String
        Dim flag As Boolean = False
        ans = InputBox("Enter ID")
        If ans <> "" Then
            Dim reader As Data.IDataReader = CType(IngredientListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                If reader("Ingredient_ID") = ans Then
                    IngredientDropDownList.SelectedValue = reader("Ingredient_ID")
                    flag = True
                    Exit While
                End If
            End While
            reader.Close()
            If flag = False Then
                MsgBox("ID not found.", MsgBoxStyle.Exclamation)
            End If
            refreshAmount()
        End If
    End Sub
    Protected Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
        displayInformation(-1)
        CancelButton.Visible = False
        NewProduct.Visible = True
        SearchProduct.Visible = True
        DeleteProduct.Visible = True
        DeleteIngredientLabel.Visible = True
        IngredientDeleteDropDownList.Visible = True
        DeleteIngredientButton.Visible = True
        SearchIngredientDelete.Visible = True
    End Sub
End Class
