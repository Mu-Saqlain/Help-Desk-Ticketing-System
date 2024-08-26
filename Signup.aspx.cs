using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Configuration;
using System.Web;

namespace YourNamespace
{
    public partial class Signup : Page
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
            Response.Redirect("Login.aspx");
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string contact = txtContact.Text;
            string dept = txtDept.Text;
            string location = txtLocation.Text;
            int erp;
            string usertype = "user";

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(contact) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(dept) || string.IsNullOrWhiteSpace(location))
            {
                DisplayMessage("Please fill in all fields.", "red");
                return;
            }

            if (!int.TryParse(txtErp.Text, out erp))
            {
                DisplayMessage("Enter a valid ERP", "red");
                return;
            }

            if (!IsValidEmail(email))
            {
                DisplayMessage("Invalid Email! Enter a valid Email Address", "red");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users (Name, Email, VoIP, Dept, Location, ERP, UserType, Password) VALUES (@Name, @Email, @Contact, @Dept, @Location, @ERP, @UserType, @Password)";
                lblConnectionString.Text = connectionString;
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Contact", contact);
                    cmd.Parameters.AddWithValue("@Dept", dept);
                    cmd.Parameters.AddWithValue("@Location", location);
                    cmd.Parameters.AddWithValue("@ERP", erp);
                    cmd.Parameters.AddWithValue("@UserType", usertype);
                    cmd.Parameters.AddWithValue("@Password", password);

                    cmd.ExecuteNonQuery();
                }
            }

            Response.Redirect("Login.aspx");
        }

        private void DisplayMessage(string message, string color)
        {
            lblMessage.Text = message;
            lblMessage.ForeColor = System.Drawing.Color.FromName(color);
        }
        private bool IsValidEmail(string email)
        {
            return email.EndsWith("@nadra.pk", StringComparison.OrdinalIgnoreCase);
        }
    }
}
