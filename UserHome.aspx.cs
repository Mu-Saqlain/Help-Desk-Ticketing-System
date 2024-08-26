using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Configuration;
using System.Web;

namespace YourNamespace
{
    public partial class UserHome : Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)

        {
            if (Session["ERP"] == null)
            {
                //throw new Exception("ERP session value is null or empty.");
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                // Add cache control headers
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                Response.Cache.SetNoStore();

                LoadPendingTickets();
                LoadCompletedTickets();
            }
        }

        protected void btnSubmitTicket_Click(object sender, EventArgs e)
        {
            string problem = ddlProblem.SelectedValue;
            string comment = txtComment.Text;

            

            string erpString = Session["ERP"].ToString();
            if (!int.TryParse(erpString, out int erp))
            {
                throw new Exception("ERP session value is not a valid integer.");
            }

            string status = "Pending";
            Guid ticketID = Guid.NewGuid();
            DateTime createdAt = DateTime.Now;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM Users WHERE ERP = @ERP";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@ERP", erp);
                    int userCount = (int)checkCmd.ExecuteScalar();

                    if (userCount == 0)
                    {
                        throw new Exception("The ERP value does not exist in the Users table.");
                    }
                }

                string query = "INSERT INTO Tickets (TicketID, UserERP, Problem, Comment, Status, CreatedAt) VALUES (@TicketID, @ERP, @Problem, @Comment, @Status, @CreatedAt)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TicketID", ticketID);
                    cmd.Parameters.AddWithValue("@ERP", erp);
                    cmd.Parameters.AddWithValue("@Problem", problem);
                    cmd.Parameters.AddWithValue("@Comment", comment);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@CreatedAt", createdAt);

                    cmd.ExecuteNonQuery();
                }
            }

            LoadPendingTickets();

            ClientScript.RegisterStartupScript(this.GetType(), "alert", "showConfirmation();", true);
        }

        private void LoadPendingTickets()
        {
            if (Session["ERP"] == null)
            {
                throw new Exception("ERP session value is null or empty.");
            }

            string erpString = Session["ERP"].ToString();
            if (!int.TryParse(erpString, out int erp))
            {
                throw new Exception("ERP session value is not a valid integer.");
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT TicketID, Problem, Comment, Status FROM Tickets WHERE UserERP = @ERP AND Status = 'Pending'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ERP", erp);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        gvPendingTickets.DataSource = reader;
                        gvPendingTickets.DataBind();
                    }
                }
            }
        }

        private void LoadCompletedTickets()
        {
            if (Session["ERP"] == null)
            {
                throw new Exception("ERP session value is null or empty.");
            }

            string erpString = Session["ERP"].ToString();
            if (!int.TryParse(erpString, out int erp))
            {
                throw new Exception("ERP session value is not a valid integer.");
            }

            int days = int.TryParse(ddlTimeFrame.SelectedValue, out days) ? days : 30;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT TicketID, Problem, Comment, Status, CreatedAt FROM Tickets WHERE UserERP = @ERP AND Status = 'Completed' AND CreatedAt >= DATEADD(day, -@Days, GETDATE())";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ERP", erp);
                    cmd.Parameters.AddWithValue("@Days", days);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        gvCompletedTickets.DataSource = reader;
                        gvCompletedTickets.DataBind();
                    }
                }
            }
        }

        protected void ddlTimeFrame_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCompletedTickets();
        }
    }
}
