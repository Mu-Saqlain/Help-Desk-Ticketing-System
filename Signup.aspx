<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="YourNamespace.Signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signup Page</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="StyleSheet.css" />
</head>
<body>
    <asp:Label ID="lblConnectionString" runat="server" Text=""></asp:Label>
    <form id="form1" runat="server">
        <div class="header">
            <img src="nadralogo.jpg" alt="Logo" class="logo" />
        </div>
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header">
                            <h3 class="text-center">Sign Up</h3>
                            <div class="text-right">
                                <asp:Button ID="Button2" runat="server" CssClass="btn btn-link" Text="Login" OnClick="btnLogin_Click" style="color: green;" />
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="form-group">
                                <label for="txtName">Name</label>
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label for="txtEmail">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                            </div>
                            <div class="form-group">
                                <label for="txtPassword">Password</label>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                            </div>
                            <div class="form-group">
                                <label for="txtContact">Contact</label>
                                <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label for="txtDept">Department</label>
                                <asp:TextBox ID="txtDept" runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label for="txtLocation">Location</label>
                                <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label for="txtErp">ERP ID</label>
                                <asp:TextBox ID="txtErp" runat="server" CssClass="form-control" TextMode="Number" />
                            </div>
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                            <asp:Button ID="btnSignup" runat="server" CssClass="btn btn-primary btn-block" Text="Sign Up" OnClick="btnSignup_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
