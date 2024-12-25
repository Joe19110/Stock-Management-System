Imports System.Data
Partial Class View_CustInfo
    Inherits System.Web.UI.Page
    Dim flag As Integer
    Public Sub displayInformation(ByVal flag As Integer, ByVal filtercount As Integer)
        Dim filterflag As Boolean
        filterflag = True
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
                flag = 1
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
                flag = 0
            End If
        End If
        If NameSearchData.Text <> "" Then
            If NameSearchData.Text.Substring(0, NameSearchData.Text.Length) <> custName.Text.Substring(0, NameSearchData.Text.Length) Then
                filterflag = False
            End If
        End If
        If filtercount <= getRecordCount() Then
            If filterflag = False Then
                filtercount = filtercount + 1
                If flag = -1 Then
                    displayInformation(0, filtercount)
                Else
                    displayInformation(flag, filtercount)
                End If
            End If
        Else
            MsgBox("No customers found.")
            NameSearchData.Text = ""
            displayInformation(-1, 0)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If custID.Text = "" Then
            displayInformation(-1, 0)
        End If
    End Sub

    Protected Sub nextCust_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles nextCust.Click
        displayInformation(0, -99999)
    End Sub

    Protected Sub prevCust_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles prevCust.Click
        displayInformation(1, -99999)
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
                    displayInformation(1, 0)
                    custID.Text = temp
                    flag = True
                    NameSearchData.Text = ""
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
    Public Function getRecordCount() As Integer
        Dim count As Integer
        Dim countreader As Data.IDataReader = CType(CustomerDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        While countreader.Read()
            count = count + 1
        End While
        countreader.Close()
        Return count
    End Function
    Protected Sub SearchName_Click(sender As Object, e As EventArgs) Handles SearchName.Click
        displayInformation(-1, 0)
    End Sub
End Class
