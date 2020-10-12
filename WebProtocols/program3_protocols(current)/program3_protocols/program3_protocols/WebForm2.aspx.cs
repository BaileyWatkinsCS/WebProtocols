using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace program3_protocols
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.PreviousPage != null)
            {
                Control ContentPlaceHolder1 = this.Page.PreviousPage.Master.FindControl("ContentPlaceHolder1");
                Calendar calendar = (Calendar)ContentPlaceHolder1.FindControl("Calendar1");
                DateText.Text = calendar.SelectedDate.ToShortDateString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            con.Open();
            String str = "INSERT INTO Events (Eventname,EventDate,EventDescription,EventTime) VALUES ('" + NameText.Text + "','" + DateText.Text + "','" + DescText.Text + "','" + TimeText.Text + "')";
            SqlCommand cmd = new SqlCommand(str, con);
            int OBJ = Convert.ToInt32(cmd.ExecuteNonQuery());
            con.Close();

        }
    }
}