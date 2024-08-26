<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="YourNamespace.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <img src="nadralogo.jpg" alt="Logo" class="logo" />
        </div>
        <br/><br/>
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header">
                            <h3 class="text-center">Login</h3>
                            <div class="text-right">
                                <asp:Button ID="btnSignup" runat="server" CssClass="btn btn-link" Text="Sign Up" OnClick="btnSignup_Click" style="color: green;" />
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="form-group">
                                 <label for="txtERP">ERP</label>
                                 <asp:TextBox ID="txtERP" runat="server" CssClass="form-control" TextMode="Number" />
                            </div>
                            <div class="form-group">
                                <label for="txtEmail">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                            </div>
                            <div class="form-group">
                                <label for="txtPassword">Password</label>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                            </div>
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                            <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary btn-block" Text="Login" OnClick="btnLogin_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
