<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserHome.aspx.cs" Inherits="YourNamespace.UserHome" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Home Page</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="StyleSheet.css" />
    <script type="text/javascript">
        function showConfirmation() {
            alert('Ticket Submitted');
        }
    </script>
    <style>
        .table td {
            word-wrap: break-word;
            white-space: normal !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <img src="nadralogo.jpg" alt="Logo" class="logo" />
        </div>
        <br/><br/><br/><br/>
        <div class="container mt-5">
            <div class="row">
                <div class="col-md-4">
                    <div class="card">
                        <div class="card-header">
                            <h3>Create Ticket</h3>
                        </div>
                        <div class="card-body">
                            <div class="form-group">
                                <label for="ddlProblem">Problem</label>
                                <asp:DropDownList ID="ddlProblem" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Select Problem" Value="" />
                                    <asp:ListItem Text="Network Issue" Value="Network Issue" />
                                    <asp:ListItem Text="Software Installation" Value="Software Installation" />
                                    <asp:ListItem Text="Hardware Failure" Value="Hardware Failure" />
                                    <asp:ListItem Text="Other" Value="Other" />
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="txtComment">Additional Note</label>
                                <asp:TextBox ID="txtComment" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                            </div>
                            <asp:Button ID="btnSubmitTicket" runat="server" CssClass="btn btn-primary btn-block" Text="Submit Ticket" OnClick="btnSubmitTicket_Click" />
                        </div>
                    </div>
                </div>
                <div class="col-md-8">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="card">
                                <div class="card-header">
                                    <h3>Pending Tickets</h3>
                                </div>
                                <div class="card-body table-responsive">
                                    <asp:GridView ID="gvPendingTickets" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="TicketID" HeaderText="Ticket ID" />
                                            <asp:BoundField DataField="Problem" HeaderText="Problem" />
                                            <asp:BoundField DataField="Comment" HeaderText="Comment" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="card">
                                <div class="card-header">
                                    <h3>Completed Tickets</h3>
                                </div>
                                <div class="card-body table-responsive">
                                    <div class="form-group">
                                        <label for="ddlTimeFrame">Show completed tickets from:</label>
                                        <asp:DropDownList ID="ddlTimeFrame" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTimeFrame_SelectedIndexChanged">
                                            <asp:ListItem Text="Select Time Frame" Value="" />
                                            <asp:ListItem Text="Last Week" Value="7" />
                                            <asp:ListItem Text="Last 10 Days" Value="10" />
                                            <asp:ListItem Text="This Month" Value="30" />
                                        </asp:DropDownList>
                                    </div>
                                    <asp:GridView ID="gvCompletedTickets" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="TicketID" HeaderText="Ticket ID" />
                                            <asp:BoundField DataField="Problem" HeaderText="Problem" />
                                            <asp:BoundField DataField="Comment" HeaderText="Comment" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="text-success" Visible="false" />
    </form>
</body>
</html>
