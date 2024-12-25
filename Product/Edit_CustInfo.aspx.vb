
Partial Class Edit_CustInfo
    Inherits System.Web.UI.Page
    Public Sub displayInformation(ByVal flag As Integer)
        Dim nextflag As Boolean = False
        If flag = -1 Then
            Dim reader1 As Data.IDataReader = CType(CustomerDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            reader1.Read()
            custID.Text = reader1("Customer_ID")
            custName.Text = reader1("Customer_Name")
            custAddress.Text = reader1("Address")
            custNumber.Text = reader1("Phone_Number")
            reader1.Close()
        ElseIf flag = 0 Then
            Dim nextid As Integer
            nextid = custID.Text
            getNextRecord(1)
            If custID.Text <> 0 Then
                Dim reader2 As Data.IDataReader = CType(CustomerDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                While reader2.Read()
                    If reader2("Customer_ID") = custID.Text Then
                        custName.Text = reader2("Customer_Name")
                        custAddress.Text = reader2("Address")
                        custNumber.Text = reader2("Phone_Number")
                    End If
                End While
                reader2.Close()
            Else
                custID.Text = nextid
            End If
        ElseIf flag = 1 Then
            Dim previd As Integer
            previd = custID.Text
            getPreviousRecord(1)
            If custID.Text <> 0 Then
                Dim reader2 As Data.IDataReader = CType(CustomerDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
                While reader2.Read()
                    If reader2("Customer_ID") = custID.Text Then
                        custName.Text = reader2("Customer_Name")
                        custAddress.Text = reader2("Address")
                        custNumber.Text = reader2("Phone_Number")
                    End If
                End While
                reader2.Close()
            Else
                custID.Text = previd
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If custID.Text = "" Then
            displayInformation(-1)
        End If

    End Sub

    Protected Sub nextCust_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles nextCust.Click
        displayInformation(0)
    End Sub

    Protected Sub prevCust_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles prevCust.Click
        Dim reader As Data.IDataReader = CType(CustomerDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        Dim emptyflag As Boolean
        emptyflag = False
        While reader.Read()
            If custID.Text = reader("Customer_ID") Then
                displayInformation(1)
                emptyflag = True
                Exit While
            End If
        End While
        reader.Close()
        If emptyflag = False Then
            MsgBox("Please save the new customer first.", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Protected Sub save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles save.Click
        Dim newcustflag As Integer = 0
        Dim reader5 As Data.IDataReader = CType(CustomerDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader5.Read()
            If custID.Text = reader5("Customer_ID") Then
                newcustflag = 1
                CustomerDataSource.UpdateParameters("custname").DefaultValue = custName.Text
                CustomerDataSource.UpdateParameters("address").DefaultValue = custAddress.Text
                CustomerDataSource.UpdateParameters("phone").DefaultValue = custNumber.Text
                CustomerDataSource.Update()
                MsgBox("Changes successfully saved.")
                Exit While
            End If
        End While
        reader5.Close()
        If newcustflag = 0 Then
            CustomerDataSource.InsertParameters("custid").DefaultValue = CInt(custID.Text)
            CustomerDataSource.InsertParameters("custname").DefaultValue = CStr(custName.Text)
            CustomerDataSource.InsertParameters("address").DefaultValue = CStr(custAddress.Text)
            CustomerDataSource.InsertParameters("phone").DefaultValue = CStr(custNumber.Text)
            CustomerDataSource.Insert()
            CancelButton.Visible = False
            MsgBox("New customer successfully added.")
        End If
        newCust.Visible = True
        delCust.Visible = True
    End Sub

    Protected Sub newCust_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles newCust.Click
        Dim reader As Data.IDataReader = CType(CustSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader.Read()
            custID.Text = reader("custid") + 1
        End While
        reader.Close()
        custName.Text = ""
        custNumber.Text = ""
        custAddress.Text = ""
        newCust.Visible = False
        delCust.Visible = False
        SearchCustID.Visible = False
        CancelButton.Visible = True
    End Sub

    Protected Sub delCust_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles delCust.Click
        Dim ans As String
        Dim id As Integer
        ans = MsgBox("Are you sure you want to delete this Customer?", MsgBoxStyle.YesNo)
        If ans = MsgBoxResult.Yes Then
            id = custID.Text
            displayInformation(-1)
            CustomerDataSource.DeleteParameters("custid").DefaultValue = id
            CustomerDataSource.Delete()
            OrdersDataSource.DeleteParameters("custid").DefaultValue = id
            OrdersDataSource.Delete()
            MsgBox("Customer successfully deleted.")
        End If
        displayInformation(-1)
    End Sub
    Protected Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
        displayInformation(-1)
        CancelButton.Visible = False
        newCust.Visible = True
        delCust.Visible = True
        SearchCustID.Visible = True
    End Sub
    Protected Sub SearchCustID_Click(sender As Object, e As EventArgs) Handles SearchCustID.Click
        Dim temp As Integer
        Dim ans As String
        Dim flag As Boolean = False
        ans = InputBox("Enter ID")
        If ans <> "" Then
            Dim reader As Data.IDataReader = CType(CustomerDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                temp = reader("Customer_ID")
                If temp = ans Then
                    custID.Text = temp + 1
                    displayInformation(1)
                    custID.Text = temp
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
    Public Function getMaxID() As Integer
        Dim maxid, temp As Integer
        maxid = 0
        Dim reader As Data.IDataReader = CType(CustomerDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader.Read()
            temp = reader("Customer_ID")
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
        Dim reader As Data.IDataReader = CType(CustomerDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While reader.Read()
            temp = reader("Customer_ID")
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
        While Val(CInt(custID.Text) + count) <= getMaxID() And nextflag = False
            Dim reader As Data.IDataReader = CType(CustomerDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                tempid = reader("Customer_ID")
                If Val(CInt(custID.Text) + count) = tempid Then
                    nextflag = True
                    custID.Text = tempid
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
            custID.Text = 0
        End If
    End Sub
    Public Sub getPreviousRecord(ByVal count As Integer)
        Dim prevflag As Boolean = False
        Dim tempid As Integer
        While Val(CInt(custID.Text) - count) >= getMinID() And prevflag = False
            Dim reader As Data.IDataReader = CType(CustomerDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
            While reader.Read()
                tempid = reader("Customer_ID")
                If Val(CInt(custID.Text) - count) = tempid Then
                    prevflag = True
                    custID.Text = tempid
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
            custID.Text = 0
        End If
    End Sub
End Class
