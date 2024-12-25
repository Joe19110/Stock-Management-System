<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Login Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <table style="margin:auto;width:20%;margin-top:10%;text-align:left">    
    <tr>
        <td colspan="2" align="center">
            <asp:Image ID="Image1" runat="server" ImageUrl="logo.png" Width="50%"/>

        </td>
    </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
   <tr>
       <td>
           <asp:Label ID="Username" runat="server" Text="Username:"></asp:Label>
       </td>
       <td>
           <asp:TextBox ID="uname" runat="server" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
       </td>
   </tr>
       <tr>
           <td>
               <asp:Label ID="Password" runat="server" Text="Password:"></asp:Label>
           </td>
           <td>
                <asp:TextBox ID="pass" runat="server" TextMode="Password"></asp:TextBox>
           </td>
       </tr>
            <tr>
                <td colspan="2">
                    &nbsp; 
                </td>
            </tr>
        <tr>
            <td align="center" colspan="2">
                 <asp:Button ID="loginbutton" runat="server" Text="Login" />
            </td>
        </tr>

            </table>
        <asp:AccessDataSource ID="LoginDataSource" runat="server" DataFile="~/App_Data/Computer Science IA.mdb" DataSourceMode="DataReader" SelectCommand="SELECT * FROM [Login]">
        </asp:AccessDataSource>
        
    </form>
</body>
</html>
