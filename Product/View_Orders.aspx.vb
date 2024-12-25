
Partial Class View_Orders
    Inherits System.Web.UI.Page
    Public Sub displayInformation(ByVal flag As Integer, ByVal filtercount As Integer)
        Dim totprice As Double
        Dim filterflag As Boolean
        Dim tempid As Integer
        filterflag = True
        If SortByDate.Checked = False Then
            If flag = -1 Then
                Dim linkedreader As Data.IDataReader = CType(LinkedLoadDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                While linkedreader.Read()
                    tempid = linkedreader("Order_ID")
                    If tempid = getMaxID() Then
                        OrderIDData.Text = tempid
                        CustomerNameData.Text = linkedreader("Customer_Name")
                        DateofOrderData.Text = linkedreader("Date_of_Order")
                        DatetobeShippedData.Text = linkedreader("Date_to_be_Shipped")
                        Exit While
                    End If
                End While
                linkedreader.Close()
            ElseIf flag = 0 Then
                Dim nextid As Integer
                nextid = OrderIDData.Text
                getNextRecord(1)
                If OrderIDData.Text <> 0 Then
                    Dim reader1 As Data.IDataReader = CType(OrdersDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                    Dim reader2 As Data.IDataReader = CType(CustInfoSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                    Dim custidtemp As Integer
                    While reader1.Read()
                        If reader1("Order_ID") = OrderIDData.Text Then
                            DateofOrderData.Text = reader1("Date_of_Order")
                            DatetobeShippedData.Text = reader1("Date_to_be_Shipped")
                            custidtemp = reader1("Customer_ID")
                            Exit While
                        End If
                    End While
                    reader1.Close()
                    While reader2.Read()
                        If custidtemp = reader2("Customer_ID") Then
                            CustomerNameData.Text = reader2("Customer_Name")
                            Exit While
                        End If
                    End While
                    reader2.Close()
                Else
                    OrderIDData.Text = nextid
                    flag = 1
                End If
            ElseIf flag = 1 Then
                Dim previd As Integer
                previd = OrderIDData.Text
                getPreviousRecord(1)
                If OrderIDData.Text <> 0 Then
                    Dim reader1 As Data.IDataReader = CType(OrdersDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                    Dim reader2 As Data.IDataReader = CType(CustInfoSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                    Dim custidtemp As Integer
                    While reader1.Read()
                        If reader1("Order_ID") = OrderIDData.Text Then
                            DateofOrderData.Text = reader1("Date_of_Order")
                            DatetobeShippedData.Text = reader1("Date_to_be_Shipped")
                            custidtemp = reader1("Customer_ID")
                            Exit While
                        End If
                    End While
                    reader1.Close()
                    While reader2.Read()
                        If custidtemp = reader2("Customer_ID") Then
                            CustomerNameData.Text = reader2("Customer_Name")
                            Exit While
                        End If
                    End While
                    reader2.Close()
                Else
                    OrderIDData.Text = previd
                    flag = 0
                End If
            End If
        Else
            If flag = -1 Then
                Dim linkedreader As Data.IDataReader = CType(LinkedLoadDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                While linkedreader.Read()
                    tempid = linkedreader("Order_ID")
                    If tempid = getSortArray()(0) Then
                        OrderIDData.Text = tempid
                        CustomerNameData.Text = linkedreader("Customer_Name")
                        DateofOrderData.Text = linkedreader("Date_of_Order")
                        DatetobeShippedData.Text = linkedreader("Date_to_be_Shipped")
                        Exit While
                    End If
                End While
                linkedreader.Close()
            Else
                If flag = 1 Then
                    Dim nextindex As Integer
                    For i = 0 To getRecordCount() - 1
                        If getSortArray()(i) = OrderIDData.Text Then
                            nextindex = i
                            Exit For
                        End If
                    Next
                    If nextindex - 1 >= 0 Then
                        OrderIDData.Text = getSortArray()(nextindex - 1)
                        Dim reader1 As Data.IDataReader = CType(OrdersDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                        Dim reader2 As Data.IDataReader = CType(CustInfoSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                        Dim custidtemp As Integer
                        While reader1.Read()
                            If reader1("Order_ID") = OrderIDData.Text Then
                                DateofOrderData.Text = reader1("Date_of_Order")
                                DatetobeShippedData.Text = reader1("Date_to_be_Shipped")
                                custidtemp = reader1("Customer_ID")
                                Exit While
                            End If
                        End While
                        reader1.Close()
                        While reader2.Read()
                            If custidtemp = reader2("Customer_ID") Then
                                CustomerNameData.Text = reader2("Customer_Name")
                                Exit While
                            End If
                        End While
                        reader2.Close()
                    Else
                        OrderIDData.Text = getSortArray()(nextindex)
                        flag = -1
                    End If
                Else
                    If flag = 0 Then
                        Dim previndex As Integer
                        For i = 0 To getRecordCount() - 1
                            If getSortArray()(i) = OrderIDData.Text Then
                                previndex = i
                                Exit For
                            End If
                        Next
                        If previndex + 1 <= getRecordCount() - 1 Then
                            OrderIDData.Text = getSortArray()(previndex + 1)
                            Dim reader1 As Data.IDataReader = CType(OrdersDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                            Dim reader2 As Data.IDataReader = CType(CustInfoSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                            Dim custidtemp As Integer
                            While reader1.Read()
                                If reader1("Order_ID") = OrderIDData.Text Then
                                    DateofOrderData.Text = reader1("Date_of_Order")
                                    DatetobeShippedData.Text = reader1("Date_to_be_Shipped")
                                    custidtemp = reader1("Customer_ID")
                                    Exit While
                                End If
                            End While
                            reader1.Close()
                            While reader2.Read()
                                If custidtemp = reader2("Customer_ID") Then
                                    CustomerNameData.Text = reader2("Customer_Name")
                                    Exit While
                                End If
                            End While
                        Else
                            OrderIDData.Text = getSortArray()(previndex)
                            flag = 1
                        End If
                    End If
                End If
            End If
        End If
        Dim reader3 As Data.IDataReader = CType(OrdersDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader3.Read()
            If reader3("Order_ID") = OrderIDData.Text Then
                If reader3("Completed?") = True Then
                    CompletedData.Text = "Completed"
                Else
                    CompletedData.Text = "Ongoing"
                End If
                If reader3("Paid?") = True Then
                    PaidData.Text = "Yes"
                Else
                    PaidData.Text = "No"
                End If
                If reader3("Ready_to_be_shipped?") = True Then
                    ReadyData.Text = "Yes"
                Else
                    ReadyData.Text = "No"
                End If
            End If
        End While
        reader3.Close()
        Dim reader4 As Data.IDataReader = CType(TotalPriceSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader4.Read()
            totprice = (reader4("Amount") * reader4("Price")) + totprice
        End While
        reader4.Close()
        TotalPriceData.Text = totprice
        If CompletedDropDownList.SelectedItem.ToString IsNot "" Then
            If (CompletedDropDownList.SelectedValue = True And CompletedData.Text = "Ongoing") Or (CompletedDropDownList.SelectedValue = False And CompletedData.Text = "Completed") Then
                filterflag = False
            End If
        End If
        If paidfilter.Checked = True And PaidData.Text = "No" Then
            filterflag = False
        End If
        If readyfilter.Checked = True And ReadyData.Text = "No" Then
            filterflag = False
        End If
        If NameFilterDropDownList.SelectedIndex <> -1 Then
            If NameFilterDropDownList.SelectedItem.ToString IsNot "" Then
                If CStr(NameFilterDropDownList.Text) <> CStr(CustomerNameData.Text) Then
                    filterflag = False
                End If
            End If
        End If
        If filtercount <= getRecordCount() Then
            If filterflag = False Then
                filtercount = filtercount + 1
                If flag = -1 Then
                    displayInformation(1, filtercount)
                Else
                    displayInformation(flag, filtercount)
                End If

            End If
        Else
            MsgBox("No orders found.", MsgBoxStyle.Exclamation)
            readyfilter.Checked = False
            paidfilter.Checked = False
            CompletedDropDownList.Text = ""
            NameFilterDropDownList.Text = Nothing
            displayInformation(-1, 0)
        End If
    End Sub
    Public Function getSortArray() As Integer()
        Dim id(getRecordCount() - 1), count, min As Integer
        Dim shipdate(getRecordCount() - 1) As Date
        count = 0
        Dim reader As Data.IDataReader = CType(OrdersDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader.Read()
            id(count) = reader("Order_ID")
            shipdate(count) = reader("Date_to_be_Shipped")
            count = count + 1
        End While
        reader.Close()
        For i = 0 To getRecordCount() - 2
            min = i
            For j = i + 1 To getRecordCount() - 1
                If shipdate(j) < shipdate(min) Then
                    min = j
                End If
            Next
            Dim tempid As Integer = id(min)
            Dim tempdate As Date = shipdate(min)
            id(min) = id(i)
            id(i) = tempid
            shipdate(min) = shipdate(i)
            shipdate(i) = tempdate
        Next
        Return id
    End Function
    Protected Sub previousbutton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles previousbutton.Click
        displayInformation(1, -99999)
    End Sub

    Protected Sub nextbutton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles nextbutton.Click
        displayInformation(0, -99999)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If OrderIDData.Text = "" Then
            displayInformation(-1, 0)
        End If
    End Sub
    Protected Sub paidfilter_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles paidfilter.CheckedChanged
        displayInformation(-1, 0)
    End Sub

    Protected Sub readyfilter_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles readyfilter.CheckedChanged
        displayInformation(-1, 0)
    End Sub
    Protected Sub CompletedDropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CompletedDropDownList.SelectedIndexChanged
        displayInformation(-1, 0)
    End Sub
    Protected Sub NameFilterDropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles NameFilterDropDownList.SelectedIndexChanged
        displayInformation(-1, 0)
    End Sub
    Public Function getRecordCount() As Integer
        Dim count As Integer
        Dim countreader As Data.IDataReader = CType(OrdersDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While countreader.Read()
            count = count + 1
        End While
        countreader.Close()
        Return count
    End Function
    Protected Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        NameFilterDropDownList.SelectedIndex = -1
        displayInformation(-1, 0)
    End Sub
    Public Function getMaxID() As Integer
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
    Public Function getMinID() As Integer
        Dim minid, temp As Integer
        minid = 999999999
        Dim reader As Data.IDataReader = CType(OrdersDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
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
            Dim reader As Data.IDataReader = CType(OrdersDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
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
            Dim reader As Data.IDataReader = CType(OrdersDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                tempid = reader("Order_ID")
                If Val(CInt(orderiddata.Text) - count) = tempid Then
                    prevflag = True
                    orderiddata.Text = tempid
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
    Protected Sub SearchOrderID_Click(sender As Object, e As EventArgs) Handles SearchOrderID.Click
        Dim temp As Integer
        Dim ans As String
        Dim flag As Boolean = False
        ans = InputBox("Enter ID")
        If ans <> "" Then
            Dim reader As Data.IDataReader = CType(OrdersDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                temp = reader("Order_ID")
                If temp = ans Then
                    OrderIDData.Text = temp + 1
                    readyfilter.Checked = False
                    paidfilter.Checked = False
                    CompletedDropDownList.Text = ""
                    NameFilterDropDownList.SelectedIndex = -1
                    displayInformation(1, 0)
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
    Protected Sub SortByDate_CheckedChanged(sender As Object, e As EventArgs) Handles SortByDate.CheckedChanged
        displayInformation(-1, 0)
    End Sub
End Class
