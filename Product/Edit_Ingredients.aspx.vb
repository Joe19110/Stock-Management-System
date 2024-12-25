
Partial Class Edit_Ingredients
    Inherits System.Web.UI.Page
    Public Sub displayInformation(ByVal flag As Integer)
        Dim nextflag As Boolean = False
        If flag = -1 Then
            Dim reader1 As Data.IDataReader = CType(IngredientListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            reader1.Read()
            IngredientIDData.Text = reader1("Ingredient_ID")
            IngredientNameData.Text = reader1("Ingredient_Name")
            CostInput.Text = reader1("Cost")
            reader1.Close()
        ElseIf flag = 0 Then
            Dim nextid As Integer
            nextid = IngredientIDData.Text
            getNextRecord(1)
            If IngredientIDData.Text <> 0 Then
                Dim reader2 As Data.IDataReader = CType(IngredientListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                While reader2.Read()
                    If reader2("Ingredient_ID") = IngredientIDData.Text Then
                        IngredientIDData.Text = reader2("Ingredient_ID")
                        IngredientNameData.Text = reader2("Ingredient_Name")
                        CostInput.Text = reader2("Cost")
                    End If
                End While
                reader2.Close()
            Else
                IngredientIDData.Text = nextid
            End If
        ElseIf flag = 1 Then
            Dim previd As Integer
            previd = IngredientIDData.Text
            getPreviousRecord(1)
            If IngredientIDData.Text <> 0 Then
                Dim reader2 As Data.IDataReader = CType(IngredientListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                While reader2.Read()
                    If reader2("Ingredient_ID") = IngredientIDData.Text Then
                        IngredientIDData.Text = reader2("Ingredient_ID")
                        IngredientNameData.Text = reader2("Ingredient_Name")
                        CostInput.Text = reader2("Cost")
                    End If
                End While
                reader2.Close()
            Else
                IngredientIDData.Text = previd
            End If
        End If

    End Sub
    Protected Sub PreviousRecord_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PreviousRecord.Click
        Dim reader As Data.IDataReader = CType(IngredientListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        Dim emptyflag As Boolean
        emptyflag = False
        'efficient sequential search
        While reader.Read()
            If IngredientIDData.Text = reader("Ingredient_ID") Then
                displayInformation(1)
                emptyflag = True
                Exit While
            End If
        End While
        If emptyflag = False Then
            MsgBox("Please save the new order first.")
        End If
    End Sub

    Protected Sub NextRecord_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NextRecord.Click
        displayInformation(0)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IngredientIDData.Text = "" Then
            displayInformation(-1)
        End If

    End Sub

    Protected Sub Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Save.Click
        Dim neworderflag As Integer = 0
        Dim reader5 As Data.IDataReader = CType(IngredientListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader5.Read()
            If IngredientIDData.Text = reader5("Ingredient_ID") Then
                neworderflag = 1
                IngredientListDataSource.Update()
                MsgBox("Changes successfully saved.")
            End If
        End While
        reader5.Close()
        If neworderflag = 0 Then
            IngredientListDataSource.InsertParameters("ingredientid").DefaultValue = IngredientIDData.Text
            IngredientListDataSource.InsertParameters("ingredientname").DefaultValue = IngredientNameData.Text
            IngredientListDataSource.InsertParameters("cost").DefaultValue = CostInput.Text
            IngredientListDataSource.Insert()
            CancelButton.Visible = False
            NewIngredient.Visible = True
            DeleteIngredient.Visible = True
            MsgBox("New ingredient successfully added.")
        End If

    End Sub

    Protected Sub NewProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewIngredient.Click
        Dim reader As Data.IDataReader = CType(IngredientListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader.Read()
            IngredientIDData.Text = reader("Ingredient_ID") + 1
            CostInput.Text = 0
            IngredientNameData.Text = ""
        End While
        reader.Close()
        CancelButton.Visible = True
        NewIngredient.Visible = False
        DeleteIngredient.Visible = False
    End Sub

    Protected Sub DeleteProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteIngredient.Click
        IngredientListDataSource.DeleteParameters("ingredientid").DefaultValue = IngredientIDData.Text
        IngredientListDataSource.Delete()
        IngredientsNeededDataSource.DeleteParameters("ingredientid").DefaultValue = IngredientIDData.Text
        IngredientsNeededDataSource.Delete()
        displayInformation(-1)
        MsgBox("Ingredient successfully deleted.")
    End Sub
    Public Function getMaxID() As Integer
        Dim maxid, temp As Integer
        maxid = 0
        Dim reader As Data.IDataReader = CType(IngredientListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader.Read()
            temp = reader("Ingredient_ID")
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
        Dim reader As Data.IDataReader = CType(IngredientListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader.Read()
            temp = reader("Ingredient_ID")
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
        While Val(CInt(IngredientIDData.Text) + count) <= getMaxID() And nextflag = False
            Dim reader As Data.IDataReader = CType(IngredientListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                tempid = reader("Ingredient_ID")
                If Val(CInt(IngredientIDData.Text) + count) = tempid Then
                    nextflag = True
                    IngredientIDData.Text = tempid
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
            IngredientIDData.Text = 0
        End If
    End Sub
    Public Sub getPreviousRecord(ByVal count As Integer)
        Dim prevflag As Boolean = False
        Dim tempid As Integer
        While Val(CInt(IngredientIDData.Text) + count) >= getMinID() And prevflag = False
            Dim reader As Data.IDataReader = CType(IngredientListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                tempid = reader("Ingredient_ID")
                If Val(CInt(IngredientIDData.Text) - count) = tempid Then
                    prevflag = True
                    IngredientIDData.Text = tempid
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
            IngredientIDData.Text = 0
        End If
    End Sub
    Protected Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
        displayInformation(-1)
        CancelButton.Visible = False
        NewIngredient.Visible = True
        DeleteIngredient.Visible = True
    End Sub
End Class
