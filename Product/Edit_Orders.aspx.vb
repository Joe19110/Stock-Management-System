Imports System.Web.Services
Imports System.Data
Imports System.Data.OleDb
Imports System.Collections.Generic
Partial Class Edit_Orders
    Inherits System.Web.UI.Page
    Public Sub displayInformation(ByVal flag As Integer)
        Dim nextflag As Boolean = False
        Dim shipdate As Date
        Dim tempid As Integer
        If flag = -1 Then
            Dim linkedreader As Data.IDataReader = CType(LinkedLoadDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While linkedreader.Read()
                tempid = linkedreader("Order_ID")
                If tempid = getMaxID() Then
                    OrderIDData.Text = tempid
                    NameDropDownList.Text = linkedreader("Customer_Name")
                    DateofOrderData.Text = linkedreader("Date_of_Order")
                    shipdate = linkedreader("Date_to_be_Shipped")
                    DayInput.Text = Day(shipdate)
                    MonthInput.Text = Month(shipdate)
                    YearInput.Text = Year(shipdate)
                    Exit While
                End If
            End While
            linkedreader.Close()
            ElseIf flag = 0 Then
                Dim nextid As Integer
                nextid = OrderIDData.Text
                getNextRecord(1)
                If OrderIDData.Text <> 0 Then
                    Dim reader1 As Data.IDataReader = CType(OrdersSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                    Dim reader2 As Data.IDataReader = CType(CustInfoSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                    Dim custidtemp As Integer
                    While reader1.Read()
                        If reader1("Order_ID") = OrderIDData.Text Then
                            DateofOrderData.Text = reader1("Date_of_Order")
                            shipdate = reader1("Date_to_be_Shipped")
                            DayInput.Text = Day(shipdate)
                            MonthInput.Text = Month(shipdate)
                            YearInput.Text = Year(shipdate)
                            custidtemp = reader1("Customer_ID")
                            Exit While
                        End If
                    End While
                    reader1.Close()
                    While reader2.Read()
                        If custidtemp = reader2("Customer_ID") Then
                            NameDropDownList.Text = reader2("Customer_Name")
                            Exit While
                        End If
                    End While
                    reader2.Close()
                Else
                    OrderIDData.Text = nextid
                End If
            ElseIf flag = 1 Then
                Dim previd As Integer
            previd = OrderIDData.Text
            getPreviousRecord(1)
            If OrderIDData.Text <> 0 Then
                Dim reader1 As Data.IDataReader = CType(OrdersSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                Dim reader2 As Data.IDataReader = CType(CustInfoSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                Dim custidtemp As Integer
                While reader1.Read()
                    If reader1("Order_ID") = OrderIDData.Text Then
                        DateofOrderData.Text = reader1("Date_of_Order")
                        shipdate = reader1("Date_to_be_Shipped")
                        DayInput.Text = Day(shipdate)
                        MonthInput.Text = Month(shipdate)
                        YearInput.Text = Year(shipdate)
                        custidtemp = reader1("Customer_ID")
                        Exit While
                    End If
                End While
                reader1.Close()
                While reader2.Read()
                    If custidtemp = reader2("Customer_ID") Then
                        NameDropDownList.Text = reader2("Customer_Name")
                        Exit While
                    End If
                End While
                reader2.Close()
            Else
                OrderIDData.Text = previd
            End If
        End If
        Dim reader3 As Data.IDataReader = CType(OrdersSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader3.Read()
            If reader3("Order_ID") = OrderIDData.Text Then
                If reader3("Completed?") = True Then
                    CompletedDropDownList.SelectedValue = True
                Else
                    CompletedDropDownList.SelectedValue = False
                End If
                If reader3("Paid?") = True Then
                    paid.Checked = True
                Else
                    paid.Checked = False
                End If
                If reader3("Ready_to_be_shipped?") = True Then
                    ready.Checked = True
                Else
                    ready.Checked = False
                End If
            End If
        End While
        reader3.Close()
        selectProductGridView()
    End Sub
    Public Sub refreshAmount()
        GridView1.DataBind()
        If GridView1.Rows.Count > 0 Then
            If ProductDropDownList.SelectedItem IsNot Nothing Then
                Dim reader1 As Data.IDataReader = CType(GridViewDataSource2.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                Dim flag As Integer = 0
                AmountInput.Text = 0
                While reader1.Read()
                    If ProductDropDownList.SelectedItem.Text = reader1("Product_Name") Then
                        AmountInput.Text = reader1("Amount")
                        Exit While
                    Else
                        AmountInput.Text = 0
                    End If
                End While
                reader1.Close()
            Else
                selectProductGridView()
            End If
        Else
            AmountInput.Text = 0
        End If
    End Sub
    Public Sub selectProductGridView()
        Dim flag As Boolean = False
        Dim reader2 As Data.IDataReader = CType(GridViewDataSource2.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        reader2.Read()
        Dim reader1 As Data.IDataReader = CType(ProductsofOrdersSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader1.Read()
            If OrderIDData.Text = reader1("Order_ID") Then
                ProductDropDownList.SelectedValue = reader2("Product_ID")
                flag = True
                Exit While
            End If
        End While
        reader1.Close()
        reader2.Close()
        If flag = False Then
            Dim reader3 As Data.IDataReader = CType(ProductListSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            reader3.Read()
            ProductDropDownList.Items.FindByText(reader3("Product_Name")).Selected = True
            reader3.Close()
        End If
        refreshAmount()
    End Sub
    Protected Sub PreviousRecord_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PreviousRecord.Click
        Dim reader As Data.IDataReader = CType(OrdersSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        Dim emptyflag As Boolean
        emptyflag = False
        While reader.Read()
            If OrderIDData.Text = reader("Order_ID") Then
                displayInformation(1)
                emptyflag = True
                Exit While
            End If
        End While
        reader.Close()
        If emptyflag = False Then
            MsgBox("Please save the new order first.")
        End If
    End Sub

    Protected Sub NextRecord_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NextRecord.Click
        displayInformation(0)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            ProductDropDownList.DataBind()
        End If
        If OrderIDData.Text = "" Then
            displayInformation(-1)
        End If
        GridView1.Sort("Product_ID", SortDirection.Ascending)

    End Sub

    Protected Sub Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Save.Click
        If CDate(DayInput.Text & "/" & MonthInput.Text & "/" & YearInput.Text) >= CDate(DateofOrderData.Text) Or CInt(DayInput.Text) > 31 Or CInt(DayInput.Text) < 1 Or CInt(MonthInput.Text) > 12 Or CInt(MonthInput.Text) < 1 Or CInt(YearInput.Text) > 3000 Or CInt(YearInput.Text) < 2023 Then
            Dim neworderflag As Integer = 0
            Dim reader5 As Data.IDataReader = CType(OrdersSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader5.Read()
                If OrderIDData.Text = reader5("Order_ID") Then
                    neworderflag = 1
                    Dim presenceflag As Integer = 0
                    Dim reader As Data.IDataReader = CType(GridViewDataSource2.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                    While reader.Read()
                        If ProductDropDownList.SelectedItem.ToString = reader("Product_Name") Then
                            ProductsofOrdersSource.Update()
                            presenceflag = 1
                            Exit While
                        End If
                    End While
                    reader.Close()
                    If presenceflag = 0 Then
                        ProductsofOrdersSource.InsertParameters("amount").DefaultValue = CInt(AmountInput.Text)
                        ProductsofOrdersSource.InsertParameters("productid").DefaultValue = ProductDropDownList.SelectedValue
                        ProductsofOrdersSource.InsertParameters("orderid").DefaultValue = CInt(OrderIDData.Text)
                        ProductsofOrdersSource.Insert()
                    End If
                    GridView1.DataBind()
                    Dim reader4 As Data.IDataReader = CType(CustInfoSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                    While reader4.Read()
                        If NameDropDownList.SelectedValue = reader4("Customer_Name") Then
                            OrdersSource.UpdateParameters("custid").DefaultValue = reader4("Customer_ID")
                            OrdersSource.UpdateParameters("shipdate").DefaultValue = CDate(DayInput.Text & "/" & MonthInput.Text & "/" & YearInput.Text)
                            If CompletedDropDownList.SelectedValue = True Then
                                OrdersSource.UpdateParameters("completed").DefaultValue = True
                            Else
                                OrdersSource.UpdateParameters("completed").DefaultValue = False
                            End If
                            If paid.Checked = True Then
                                OrdersSource.UpdateParameters("paid").DefaultValue = True
                            Else
                                OrdersSource.UpdateParameters("paid").DefaultValue = False
                            End If
                            If ready.Checked = True Then
                                OrdersSource.UpdateParameters("ready").DefaultValue = True
                            Else
                                OrdersSource.UpdateParameters("ready").DefaultValue = False
                            End If
                            OrdersSource.Update()
                            MsgBox("Changes successfully saved.")
                        End If
                    End While
                    reader4.Close()
                End If
            End While
            reader5.Close()
            If neworderflag = 0 Then
                If AmountInput.Text > 0 Then
                    OrdersSource.InsertParameters("orderid").DefaultValue = OrderIDData.Text
                    OrdersSource.InsertParameters("orderdate").DefaultValue = Today
                    OrdersSource.InsertParameters("shipdate").DefaultValue = CDate(DayInput.Text & "/" & MonthInput.Text & "/" & YearInput.Text)
                    Dim reader6 As Data.IDataReader = CType(CustInfoSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                    While reader6.Read()
                        If NameDropDownList.SelectedValue = reader6("Customer_Name") Then
                            OrdersSource.InsertParameters("custid").DefaultValue = reader6("Customer_ID")
                        End If
                    End While
                    reader6.Close()
                    If CompletedDropDownList.SelectedValue = True Then
                        OrdersSource.InsertParameters("completed").DefaultValue = True
                    Else
                        OrdersSource.InsertParameters("completed").DefaultValue = False
                    End If
                    If paid.Checked = True Then
                        OrdersSource.InsertParameters("paid").DefaultValue = True
                    Else
                        OrdersSource.InsertParameters("paid").DefaultValue = False

                    End If
                    If ready.Checked = True Then
                        OrdersSource.InsertParameters("ready").DefaultValue = True
                    Else
                        OrdersSource.InsertParameters("ready").DefaultValue = False
                    End If
                    OrdersSource.Insert()
                    ProductsofOrdersSource.InsertParameters("amount").DefaultValue = CInt(AmountInput.Text)
                    ProductsofOrdersSource.InsertParameters("productid").DefaultValue = ProductDropDownList.SelectedValue
                    ProductsofOrdersSource.InsertParameters("orderid").DefaultValue = CInt(OrderIDData.Text)
                    ProductsofOrdersSource.Insert()
                    MsgBox("New order successfully added.")
                End If
            End If
        Else
            MsgBox("Date to be Shipped is invalid.")
        End If

        ProductDropDownList.DataBind()
        ProductDeleteDropDownList.DataBind()
        GridViewDataSource2.DataBind()
        selectProductGridView()
        showAll()
    End Sub
    Protected Sub ProductDropDownList_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProductDropDownList.TextChanged
        refreshAmount()
    End Sub
    Protected Sub NewOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewOrder.Click
        Dim reader As Data.IDataReader = CType(OrdersSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader.Read()
            OrderIDData.Text = reader("Order_ID") + 1
            AmountInput.Text = 0
            CompletedDropDownList.SelectedValue = False
            paid.Checked = False
            ready.Checked = False
            ProductDropDownList.SelectedIndex = 0
            NameDropDownList.SelectedIndex = 0
            DateofOrderData.Text = Today
            DayInput.Text = Day(Today)
            MonthInput.Text = Month(Today)
            YearInput.Text = Year(Today)
        End While
        reader.Close()
        refreshAmount()
        DeleteProductLabel.Visible = False
        ProductDeleteDropDownList.Visible = False
        SearchProductDelete.Visible = False
        DeleteProduct.Visible = False
        DeleteOrder.Visible = False
        NewOrder.Visible = False
        CancelButton.Visible = True
    End Sub

    Protected Sub DeleteOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteOrder.Click
        Dim ans As String
        Dim id As Integer
        ans = MsgBox("Are you sure you want to delete this order?", MsgBoxStyle.YesNo)
        If ans = MsgBoxResult.Yes Then
            id = OrderIDData.Text
            OrdersSource.DeleteParameters("orderid").DefaultValue = id
            OrdersSource.Delete()
            ProductsofOrdersSource.DeleteParameters("orderid").DefaultValue = id
            ProductsofOrdersSource.Delete()
            MsgBox("Order successfully deleted.")
            displayInformation(-1)
        End If
    End Sub

    Protected Sub SearchName_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchName.Click
        Dim ans As String
        Dim flag As Boolean = False
        ans = InputBox("Enter ID")
        If ans <> "" Then
            Dim reader As Data.IDataReader = CType(CustInfoSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                If reader("Customer_ID") = ans Then
                    NameDropDownList.Text = reader("Customer_Name")
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

    Protected Sub SearchProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchProduct.Click
        Dim ans As String
        Dim flag As Boolean = False
        ans = InputBox("Enter ID")
        If ans <> "" Then
            Dim reader As Data.IDataReader = CType(ProductListSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                If reader("Product_ID") = ans Then
                    ProductDropDownList.SelectedValue = reader("Product_ID")
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
    Protected Sub SearchOrder_Click(sender As Object, e As EventArgs) Handles SearchOrder.Click
        Dim temp As Integer
        Dim ans As String
        Dim flag As Boolean = False
        ans = InputBox("Enter ID")
        If ans <> "" Then
            Dim reader As Data.IDataReader = CType(OrdersSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                temp = reader("Order_ID")
                If temp = ans Then
                    OrderIDData.Text = temp + 1
                    displayInformation(1)
                    OrderIDData.Text = temp
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
    Protected Sub DeleteProduct_Click(sender As Object, e As EventArgs) Handles DeleteProduct.Click
        If ProductDeleteDropDownList.SelectedItem IsNot Nothing Then
            Dim ans As String
            ans = MsgBox(("Are you sure you want to delete the product: " & ProductDeleteDropDownList.SelectedItem.ToString & " from the order?"), MsgBoxStyle.YesNo)
            If ans = MsgBoxResult.Yes Then
                GridViewDataSource2.DeleteParameters("orderid").DefaultValue = OrderIDData.Text
                GridViewDataSource2.DeleteParameters("productid").DefaultValue = ProductDeleteDropDownList.SelectedValue
                GridViewDataSource2.Delete()
                refreshAmount()
                MsgBox("Product successfully deleted from the order.")
                selectProductGridView()
            End If
        Else
            MsgBox("Please select the product to be deleted from the order.", MsgBoxStyle.Exclamation)
        End If
    End Sub
    Protected Sub SearchProductDelete_Click(sender As Object, e As EventArgs) Handles SearchProductDelete.Click
        Dim ans As String
        Dim flag As Boolean = False
        ans = InputBox("Enter ID")
        If ans <> "" Then
            Dim reader As Data.IDataReader = CType(GridViewDataSource2.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                If reader("Product_ID") = ans Then
                    ProductDeleteDropDownList.SelectedValue = reader("Product_ID")
                    flag = True
                    Exit While
                End If
            End While
            reader.Close()
            If flag = False Then
                MsgBox("Product not found.", MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("ID has not been entered, please try again", MsgBoxStyle.Exclamation)
        End If
    End Sub
    Public Sub hideAll()
        OrderIDLabel.Visible = False
        OrderIDData.Visible = False
        SearchOrder.Visible = False
        DateofOrderLabel.Visible = False
        DateofOrderData.Visible = False
        DatetobeShippedLabel.Visible = False
        DayInput.Visible = False
        MonthInput.Visible = False
        YearInput.Visible = False
        divider1.Visible = False
        divider2.Visible = False
        CustomerNameLabel.Visible = False
        NameDropDownList.Visible = False
        SearchName.Visible = False
        ProductDropDownList.Visible = False
        AmountInput.Visible = False
        SearchProduct.Visible = False
        GridView1.Visible = False
        ready.Visible = False
        paid.Visible = False
        StatusLabel.Visible = False
        CompletedDropDownList.Visible = False
        DeleteOrder.Visible = False
        DeleteProductLabel.Visible = False
        ProductDeleteDropDownList.Visible = False
        SearchProductDelete.Visible = False
        DeleteProduct.Visible = False
    End Sub
    Public Sub showAll()
        OrderIDLabel.Visible = True
        OrderIDData.Visible = True
        SearchOrder.Visible = True
        DateofOrderLabel.Visible = True
        DateofOrderData.Visible = True
        DatetobeShippedLabel.Visible = True
        DayInput.Visible = True
        MonthInput.Visible = True
        YearInput.Visible = True
        divider1.Visible = True
        divider2.Visible = True
        CustomerNameLabel.Visible = True
        NameDropDownList.Visible = True
        SearchName.Visible = True
        ProductDropDownList.Visible = True
        AmountInput.Visible = True
        SearchProduct.Visible = True
        GridView1.Visible = True
        ready.Visible = True
        paid.Visible = True
        StatusLabel.Visible = True
        CompletedDropDownList.Visible = True
        DeleteOrder.Visible = True
        DeleteProductLabel.Visible = True
        ProductDeleteDropDownList.Visible = True
        SearchProductDelete.Visible = True
        DeleteProduct.Visible = True
        NewOrder.Visible = True
        CancelButton.Visible = False
    End Sub
    Protected Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
        displayInformation(-1)
        CancelButton.Visible = False
        showAll()
    End Sub
    Public Function getMaxID() As Integer
        Dim maxid, temp As Integer
        maxid = 0
        Dim reader As Data.IDataReader = CType(OrdersSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader.Read()
            temp = reader("Order_ID")
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
        Dim reader As Data.IDataReader = CType(OrdersSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader.Read()
            temp = reader("Order_ID")
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
        While Val(CInt(OrderIDData.Text) + count) <= getMaxID() And nextflag = False
            Dim reader As Data.IDataReader = CType(OrdersSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                tempid = reader("Order_ID")
                If Val(CInt(OrderIDData.Text) + count) = tempid Then
                    nextflag = True
                    OrderIDData.Text = tempid
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
            OrderIDData.Text = 0
        End If
    End Sub
    Public Sub getPreviousRecord(ByVal count As Integer)
        Dim prevflag As Boolean = False
        Dim tempid As Integer
        While Val(CInt(OrderIDData.Text) - count) >= getMinID() And prevflag = False
            Dim reader As Data.IDataReader = CType(OrdersSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                tempid = reader("Order_ID")
                If Val(CInt(OrderIDData.Text) - count) = tempid Then
                    prevflag = True
                    OrderIDData.Text = tempid
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
            OrderIDData.Text = 0
        End If
    End Sub
End Class

