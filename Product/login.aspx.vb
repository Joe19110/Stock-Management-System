
Partial Class login
    Inherits System.Web.UI.Page
    Protected Sub loginbutton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loginbutton.Click
        Dim reader As Data.IDataReader = CType(LoginDataSource.Select(DataSourceSelectArguments.Empty), Data.IDataReader)
        reader.Read()
        If uname.Text = reader("Username") Then
            If pass.Text = reader("Password") Then
                MsgBox("Access Granted")
                Response.Redirect("home.aspx")
            Else
                MsgBox("Wrong password")
            End If
        Else
            MsgBox("Wrong username")
        End If
    End Sub
End Class
