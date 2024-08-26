using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace YourNamespace
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Clear all sessions only if it's the first time the page is loaded
                Session.Clear();
                Session.Abandon();

                // Add cache control headers
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                Response.Cache.SetNoStore();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string erp = txtERP.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(erp))
            {
                DisplayMessage("Please fill in all fields.", "red");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Password, UserType FROM Users WHERE Email = @Email AND ERP = @ERP";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@ERP", erp);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string storedPassword = reader["Password"].ToString();
                    string userType = reader["UserType"].ToString();

                    if (storedPassword == password)
                    {
                             Session["ERP"] = erp;
                            switch (userType.ToLower())
                            {
                                case "admin":
                                    Response.Redirect("AdminHome.aspx");
                                    break;
                                case "user":
                                    Response.Redirect("UserHome.aspx");
                                    break;
                                case "support assistant":
                                    Response.Redirect("SupportAssistantHome.aspx");
                                    break;
                                default:
                                    DisplayMessage("Unknown user type.", "red");
                                    break;
                            }
                    }
                    else
                    {
                        DisplayMessage("Incorrect Password! Please Try Again", "red");
                    }
                }
                else
                {
                    DisplayMessage("Invalid email or ERP.", "red");
                }
            }
        }

        private void DisplayMessage(string message, string color)
        {
            lblMessage.Text = message;
            lblMessage.ForeColor = System.Drawing.Color.FromName(color);
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect("Signup.aspx");
        }
    }
}
