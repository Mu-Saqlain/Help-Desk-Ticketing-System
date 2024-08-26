<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupportAssistantHome.aspx.cs" Inherits="YourNamespace.SupportAssistantHome" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Support Assistant Home</title>
    <%--<link href="Styles.css" rel="stylesheet" />--%>
    <style>
         body {
            font-family: Arial, sans-serif;
            background-color: white;
            margin: 0;
            padding: 0;
        }

        .container {
            display: flex;
        }

        .header {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 150px;
            background-color: white;
            padding: 10px;
        }

        .logo {
            position: absolute;
            top: 10px;
            left: 10px;
            width: 100px; 
            height: auto; 
        }

        .sidebar {
            width: 250px;
            background-color: #f4f4f4;
            padding: 20px;
            height: 100vh;
            box-shadow: 2px 0 5px rgba(0,0,0,0.1);
        }

        .menu {
            list-style-type: none;
            padding: 0;
        }

        .menu li {
            margin: 1px 10px 30px 10px ;
        }

        .menu li a, .menu li asp\:LinkButton {
            text-decoration: none;
            color: #333;
            font-size: 18px;
            display: block;
            padding: 20px;
            background-color: #e4e4e4;
            border-radius: 5px;
            text-align: center;
        }

        .menu li a:hover, .menu li asp\:LinkButton:hover {
            background-color: #ddd;
        }

        .content {
            flex-grow: 1;
            padding: 20px;
            margin-left: 250px;
            margin-top: 170px;
        }

        h2 {
            color: #333;
            border-bottom: 2px solid #ddd;
            padding-bottom: 10px;
        }

        .table {
            width: 100%;
            border-collapse: collapse;
            margin-left: auto;
        }

        .table th, .table td {
            border: 1px solid #ddd;
            padding: 10px;
            text-align: left;
        }

        .table th {
            background-color: #f4f4f4;
        }

        .btn {
            padding: 10px 20px;
            background-color: #5cb85c;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn:hover {
            background-color: #4cae4c;
        }

        .input {
            width: calc(100% - 20px);
            padding: 10px;
            margin-bottom: 10px;
            border: 1px solid #ddd;
            border-radius: 5px;
        }

        .message {
            color: #d9534f;
            font-weight: bold;
            margin-bottom: 10px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <img src="nadralogo.jpg" alt="Logo" class="logo" />
            </div>
            <div class="sidebar">
                <ul class="menu">
                    <li style="margin-top:180px"><asp:LinkButton ID="LinkButtonPendingTickets" runat="server" OnClick="LinkButtonPendingTickets_Click">Pending Tickets</asp:LinkButton></li>
                    <li><asp:LinkButton ID="LinkButtonTakenTickets" runat="server" OnClick="LinkButtonTakenTickets_Click">Taken Tickets</asp:LinkButton></li>
                    <li><asp:LinkButton ID="LinkButtonCompletedTickets" runat="server" OnClick="LinkButtonCompletedTickets_Click">Completed Tickets</asp:LinkButton></li>
                    <li><asp:LinkButton ID="LinkButtonEditProfile" runat="server" OnClick="LinkButtonEditProfile_Click">Edit Profile</asp:LinkButton></li>
                    <li><asp:HyperLink ID="HyperLinkUserHome" runat="server" NavigateUrl="UserHome.aspx">Create Ticket</asp:HyperLink></li>
                </ul>
            </div>
            <div class="content" style="margin-left:10px;margin-top:150px">
                <asp:Panel ID="PendingTicketsPanel" runat="server" Visible="true">
                    <h2>Pending Tickets</h2>
                    <asp:GridView ID="GridViewPendingTickets" runat="server" AutoGenerateColumns="False" CssClass="table">
                        <Columns>
                            <asp:BoundField DataField="TicketID" HeaderText="Ticket ID" />
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="VoIP" HeaderText="VoIP" />
                            <asp:BoundField DataField="Location" HeaderText="Location" />
                            <asp:BoundField DataField="Dept" HeaderText="Department" />
                            <asp:BoundField DataField="Problem" HeaderText="Problem" />
                            <asp:BoundField DataField="Comment" HeaderText="Comment" />
                            <asp:BoundField DataField="CreatedAt" HeaderText="Date Created" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="ButtonTakeTicket" runat="server" Text="Take" CssClass="btn" OnClick="ButtonTakeTicket_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="TakenTicketsPanel" runat="server" Visible="false">
                    <h2>Taken Tickets</h2>
                    <asp:GridView ID="GridViewTakenTickets" runat="server" AutoGenerateColumns="False" CssClass="table">
                        <Columns>
                            <asp:BoundField DataField="TicketID" HeaderText="Ticket ID" />
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="VoIP" HeaderText="VoIP" />
                            <asp:BoundField DataField="Location" HeaderText="Location" />
                            <asp:BoundField DataField="Dept" HeaderText="Department" />
                            <asp:BoundField DataField="Problem" HeaderText="Problem" />
                            <asp:BoundField DataField="Comment" HeaderText="Comment" />
                            <asp:BoundField DataField="CreatedAt" HeaderText="Date Created" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="ButtonCompleteTicket" runat="server" Text="Complete" CssClass="btn" OnClick="ButtonCompleteTicket_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="CompletedTicketsPanel" runat="server" Visible="false">
                    <h2>Completed Tickets</h2>
                    <asp:GridView ID="GridViewCompletedTickets" runat="server" AutoGenerateColumns="False" CssClass="table">
                        <Columns>
                            <asp:BoundField DataField="TicketID" HeaderText="Ticket ID" />
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="VoIP" HeaderText="VoIP" />
                            <asp:BoundField DataField="Location" HeaderText="Location" />
                            <asp:BoundField DataField="Dept" HeaderText="Department" />
                            <asp:BoundField DataField="Problem" HeaderText="Problem" />
                            <asp:BoundField DataField="Comment" HeaderText="Comment" />
                            <asp:BoundField DataField="CreatedAt" HeaderText="Date Created" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="EditProfilePanel" runat="server" Visible="false">
                    <h2>Edit Profile</h2>
                    <asp:Label ID="LabelMessage" runat="server" Text="" CssClass="message"></asp:Label>
                    <asp:TextBox ID="TextBoxName" runat="server" CssClass="input" Placeholder="Name"></asp:TextBox><br />
                    <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="input" Placeholder="Email"></asp:TextBox><br />
                    <asp:TextBox ID="TextBoxVoIP" runat="server" CssClass="input" Placeholder="VoIP"></asp:TextBox><br />
                    <asp:TextBox ID="TextBoxLocation" runat="server" CssClass="input" Placeholder="Location"></asp:TextBox><br />
                    <asp:TextBox ID="TextBoxDepartment" runat="server" CssClass="input" Placeholder="Department"></asp:TextBox><br />
                    <asp:TextBox ID="TextBoxOldPassword" runat="server" TextMode="Password" CssClass="input" Placeholder="Old Password"></asp:TextBox><br />
                    <asp:TextBox ID="TextBoxNewPassword" runat="server" TextMode="Password" CssClass="input" Placeholder="New Password"></asp:TextBox><br />
                    <asp:Button ID="ButtonUpdateProfile" runat="server" Text="Update Profile" CssClass="btn" OnClick="ButtonUpdateProfile_Click" />
                </asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
