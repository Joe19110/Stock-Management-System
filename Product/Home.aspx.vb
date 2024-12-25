
Partial Class home
    Inherits System.Web.UI.Page
    Protected Sub ChangeUnamePassButton_Click(sender As Object, e As EventArgs) Handles ChangeUnamePassButton.Click
        changeUnamePass()
    End Sub
    Public Sub changeUnamePass()
        Dim ans, ans1, ans2 As String
        Dim reader As Data.IDataReader = CType(LoginDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        reader.Read()
        ans1 = InputBox("Username", DefaultResponse:=reader("Username"))
        ans2 = InputBox("Password", DefaultResponse:=reader("Password"))
        reader.Close()
        If ans1 <> "" And ans2 <> "" Then
            ans = MsgBox("Is the information provided below correct?" & vbCrLf & vbCrLf & "Username: " & ans1 & vbCrLf & "Password: " & ans2, MsgBoxStyle.YesNo)
            If ans = MsgBoxResult.Yes Then
                LoginDataSource.UpdateParameters("uname").DefaultValue = ans1
                LoginDataSource.UpdateParameters("pass").DefaultValue = ans2
                LoginDataSource.Update()
                MsgBox("Username and Password successfully updated.")
            Else
                MsgBox("Please reenter the Username and Password.")
                changeUnamePass()
            End If
        End If
    End Sub
End Class
