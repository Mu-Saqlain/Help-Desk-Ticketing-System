using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class SupportAssistantHome : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ERP"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            string erpString = Session["ERP"]?.ToString();

            if (!int.TryParse(erpString, out int erp))
            {
                throw new Exception("ERP session value is not a valid integer.");
            }

            string userType = string.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT UserType FROM Users WHERE ERP = @ERP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ERP", erp);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        userType = result.ToString();
                    }
                }
            }

           

            // Check if user type is "User", if so, redirect to login page
            if (userType.Equals("User", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                // Add cache control headers
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                Response.Cache.SetNoStore();

                LoadPendingTickets();
            }
        }

        private void LoadPendingTickets()
        {
            string erpString = Session["ERP"]?.ToString();

            if (!int.TryParse(erpString, out int erp))
            {
                throw new Exception("ERP session value is not a valid integer.");
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Tickets.TicketID, Users.Name, Users.VoIP, Users.Location, Users.Dept, Tickets.Problem, Tickets.Comment, Tickets.CreatedAt " +
                               "FROM Tickets " +
                               "JOIN Users ON Tickets.UserERP = Users.ERP " +
                               "WHERE Tickets.Status = 'Pending'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        GridViewPendingTickets.DataSource = reader;
                        GridViewPendingTickets.DataBind();
                    }
                }
            }
        }

        protected void ButtonTakeTicket_Click(object sender, EventArgs e)
        {
            string erpString = Session["ERP"]?.ToString();

            if (!int.TryParse(erpString, out int erp))
            {
                throw new Exception("ERP session value is not a valid integer.");
            }

            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string ticketID = row.Cells[0].Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Tickets SET Status = 'Taken', AssistantERP = @ERP WHERE TicketID = @TicketID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ERP", erp);
                    cmd.Parameters.AddWithValue("@TicketID", ticketID);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadPendingTickets(); // Refresh the grid after taking a ticket
        }

        protected void ButtonCompleteTicket_Click(object sender, EventArgs e)
        {
            string erpString = Session["ERP"]?.ToString();

            if (!int.TryParse(erpString, out int erp))
            {
                throw new Exception("ERP session value is not a valid integer.");
            }

            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string ticketID = row.Cells[0].Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Tickets SET Status = 'Completed' WHERE TicketID = @TicketID AND AssistantERP = @ERP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ERP", erp);
                    cmd.Parameters.AddWithValue("@TicketID", ticketID);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadTakenTickets(); // Refresh the grid after completing a ticket
        }

        private void LoadTakenTickets()
        {
            string erpString = Session["ERP"]?.ToString();

            if (!int.TryParse(erpString, out int erp))
            {
                throw new Exception("ERP session value is not a valid integer.");
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Tickets.TicketID, Users.Name, Users.VoIP, Users.Location, Users.Dept, Tickets.Problem, Tickets.Comment, Tickets.CreatedAt " +
                               "FROM Tickets " +
                               "JOIN Users ON Tickets.AssistantERP = Users.ERP " +
                               "WHERE Tickets.Status = 'Taken' AND Tickets.AssistantERP = @ERP";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ERP", erp);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        GridViewTakenTickets.DataSource = reader;
                        GridViewTakenTickets.DataBind();
                    }
                }
            }
        }

        private void LoadCompletedTickets()
        {
            string erpString = Session["ERP"]?.ToString();

            if (!int.TryParse(erpString, out int erp))
            {
                throw new Exception("ERP session value is not a valid integer.");
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Tickets.TicketID, Users.Name, Users.VoIP, Users.Location, Users.Dept, Tickets.Problem, Tickets.Comment, Tickets.CreatedAt " +
                               "FROM Tickets " +
                               "JOIN Users ON Tickets.AssistantERP = Users.ERP " +
                               "WHERE Tickets.Status = 'Completed' AND Tickets.AssistantERP = @ERP";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ERP", erp);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        GridViewCompletedTickets.DataSource = reader;
                        GridViewCompletedTickets.DataBind();
                    }
                }
            }
        }

        protected void LinkButtonPendingTickets_Click(object sender, EventArgs e)
        {
            LoadPendingTickets();
            PendingTicketsPanel.Visible = true;
            TakenTicketsPanel.Visible = false;
            CompletedTicketsPanel.Visible = false;
            EditProfilePanel.Visible = false;
        }

        protected void LinkButtonTakenTickets_Click(object sender, EventArgs e)
        {
            LoadTakenTickets();
            PendingTicketsPanel.Visible = false;
            TakenTicketsPanel.Visible = true;
            CompletedTicketsPanel.Visible = false;
            EditProfilePanel.Visible = false;
        }

        protected void LinkButtonCompletedTickets_Click(object sender, EventArgs e)
        {
            LoadCompletedTickets();
            PendingTicketsPanel.Visible = false;
            TakenTicketsPanel.Visible = false;
            CompletedTicketsPanel.Visible = true;
            EditProfilePanel.Visible = false;
        }

        protected void LinkButtonEditProfile_Click(object sender, EventArgs e)
        {
            LoadUserProfile();
            PendingTicketsPanel.Visible = false;
            TakenTicketsPanel.Visible = false;
            CompletedTicketsPanel.Visible = false;
            EditProfilePanel.Visible = true;
        }

        private void LoadUserProfile()
        {
            string erpString = Session["ERP"]?.ToString();

            if (!int.TryParse(erpString, out int erp))
            {
                throw new Exception("ERP session value is not a valid integer.");
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Name, Email, VoIP, Location, Dept FROM Users WHERE ERP = @ERP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ERP", erp);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            TextBoxName.Text = reader["Name"].ToString();
                            TextBoxEmail.Text = reader["Email"].ToString();
                            TextBoxVoIP.Text = reader["VoIP"].ToString();
                            TextBoxLocation.Text = reader["Location"].ToString();
                            TextBoxDepartment.Text = reader["Dept"].ToString();
                        }
                    }
                }
            }
        }

        protected void ButtonUpdateProfile_Click(object sender, EventArgs e)
        {
            string erpString = Session["ERP"]?.ToString();

            if (!int.TryParse(erpString, out int erp))
            {
                throw new Exception("ERP session value is not a valid integer.");
            }

            string oldPassword = TextBoxOldPassword.Text;
            string newPassword = TextBoxNewPassword.Text;
            string name = TextBoxName.Text;
            string email = TextBoxEmail.Text;
            string VoIP = TextBoxVoIP.Text;
            string location = TextBoxLocation.Text;
            string department = TextBoxDepartment.Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Password FROM Users WHERE ERP = @ERP";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ERP", erp);
                conn.Open();
                string currentPassword = cmd.ExecuteScalar()?.ToString();

                if (currentPassword == oldPassword)
                {
                    string updateQuery = "UPDATE Users SET Name = @Name, Email = @Email, VoIP = @VoIP, Location = @Location, Dept = @Department, Password = @NewPassword WHERE ERP = @ERP";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@Name", name);
                    updateCmd.Parameters.AddWithValue("@Email", email);
                    updateCmd.Parameters.AddWithValue("@VoIP", VoIP);
                    updateCmd.Parameters.AddWithValue("@Location", location);
                    updateCmd.Parameters.AddWithValue("@Department", department);
                    updateCmd.Parameters.AddWithValue("@NewPassword", newPassword);
                    updateCmd.Parameters.AddWithValue("@ERP", erp);
                    updateCmd.ExecuteNonQuery();

                    LabelMessage.Text = "Profile updated successfully.";
                }
                else
                {
                    LabelMessage.Text = "Old password is incorrect.";
                }
            }
        }
    }
}
