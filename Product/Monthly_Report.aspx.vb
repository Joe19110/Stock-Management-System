
Partial Class Monthly_Report
    Inherits System.Web.UI.Page
    Public Function sortTopCust() As String(,)
        Dim topcust(4, 1) As String
        Dim total(getMaxCustID()), totrevenue As Double
        Dim max, custid As Integer
        Dim reader1 As Data.IDataReader = CType(FilteredOrdersDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader1.Read()
            custid = reader1("Customer_ID")
            For count = 1 To getMaxCustID()
                If custid = count Then
                    FilteredProductsOfOrdersDataSource.SelectParameters("orderid").DefaultValue = reader1("Order_ID")
                    Dim reader2 As Data.IDataReader = CType(FilteredProductsOfOrdersDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                    While reader2.Read()
                        FilteredProductListDataSource.SelectParameters("productid").DefaultValue = reader2("Product_ID")
                        Dim reader3 As Data.IDataReader = CType(FilteredProductListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                        reader3.Read()
                        total(count) = total(count) + CInt(reader2("Amount")) * CDbl(reader3("Price"))
                        reader3.Close()
                    End While
                    reader2.Close()
                End If
            Next
        End While
        reader1.Close()
        For i = 1 To getMaxCustID()
            totrevenue = totrevenue + total(i)
        Next
        TotalRevenueData.Text = totrevenue
        For i = 0 To 4
            max = 0
            For j = 1 To getMaxCustID()
                If total(j) > total(max) Then
                    max = j
                End If
            Next
            topcust(i, 0) = max
            total(max) = 0
        Next
        For i = 0 To 4
            Dim reader4 As Data.IDataReader = CType(CustInfoDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader4.Read()
                If topcust(i, 0) = reader4("Customer_ID") Then
                    topcust(i, 1) = reader4("Customer_Name")
                    Exit While
                End If
            End While
            reader4.Close()
        Next
        Return topcust
    End Function
    Public Sub refreshReport()
        TopCust1.Text = "1. " & sortTopCust()(0, 1)
        TopCust2.Text = "2. " & sortTopCust()(1, 1)
        TopCust3.Text = "3. " & sortTopCust()(2, 1)
        TopCust4.Text = "4. " & sortTopCust()(3, 1)
        TopCust5.Text = "5. " & sortTopCust()(4, 1)
        TopProduct1.Text = "1. " & sortTopProduct()(0, 1)
        TopProduct2.Text = "2. " & sortTopProduct()(1, 1)
        TopProduct3.Text = "3. " & sortTopProduct()(2, 1)
        TopProduct4.Text = "4. " & sortTopProduct()(3, 1)
        TopProduct5.Text = "5. " & sortTopProduct()(4, 1)
    End Sub
    Public Function getMaxOrderID() As Integer
        Dim maxid, temp As Integer
        maxid = 0
        Dim reader As Data.IDataReader = CType(OrdersDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader.Read()
            temp = reader("Order_ID")
            If maxid < temp Then
                maxid = temp
            End If
        End While
        reader.Close()
        Return maxid
    End Function
    Public Function getMaxCustID() As Integer
        Dim maxid, temp As Integer
        maxid = 0
        Dim reader As Data.IDataReader = CType(CustInfoDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader.Read()
            temp = reader("Customer_ID")
            If maxid < temp Then
                maxid = temp
            End If
        End While
        reader.Close()
        Return maxid
    End Function
    Public Function getMaxProductID() As Integer
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
    Public Function sortTopProduct() As String(,)
        Dim totcost As Double
        Dim topproduct(4, 1) As String
        Dim amount(getMaxProductID()), max, productid As Integer
        Dim reader1 As Data.IDataReader = CType(FilteredOrdersDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader1.Read()
            FilteredProductsOfOrdersDataSource.SelectParameters("orderid").DefaultValue = reader1("Order_ID")
            Dim reader2 As Data.IDataReader = CType(FilteredProductsOfOrdersDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader2.Read()
                productid = reader2("Product_ID")
                amount(productid) = amount(productid) + reader2("amount")
            End While
            reader2.Close()
        End While
        reader1.Close()
        For i = 1 To getMaxProductID()
            FilteredIngredientsNeededDataSource.SelectParameters("productid").DefaultValue = i
            Dim reader3 As Data.IDataReader = CType(FilteredIngredientsNeededDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader3.Read()
                FilteredIngredientListDataSource.SelectParameters("ingredientid").DefaultValue = reader3("Ingredient_ID")
                Dim reader5 As Data.IDataReader = CType(FilteredIngredientListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                reader5.Read()
                totcost = totcost + amount(i) * (reader3("Amount_Needed") * reader5("Cost"))
                reader5.Close()
            End While
            reader3.Close()
        Next
        TotalCostData.Text = totcost
        For i = 0 To 4
            max = 0
            For j = 1 To getMaxProductID()
                If amount(j) > amount(max) Then
                    max = j
                End If
            Next
            topproduct(i, 0) = max
            amount(max) = 0
        Next
        For i = 0 To 4
            Dim reader4 As Data.IDataReader = CType(ProductListDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader4.Read()
                If topproduct(i, 0) = reader4("Product_ID") Then
                    topproduct(i, 1) = reader4("Product_Name")
                    Exit While
                End If
            End While
            reader4.Close()
        Next
        Return topproduct
    End Function
    Protected Sub MonthFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MonthFilter.SelectedIndexChanged
        If YearFilter.Text <> "" Then
            refreshReport()
            TotalProfitsData.Text = CDbl(TotalRevenueData.Text) - CDbl(TotalCostData.Text)
        End If
    End Sub
    Protected Sub YearFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles YearFilter.SelectedIndexChanged
        refreshReport()
        TotalProfitsData.Text = CDbl(TotalRevenueData.Text) - CDbl(TotalCostData.Text)
    End Sub
End Class
