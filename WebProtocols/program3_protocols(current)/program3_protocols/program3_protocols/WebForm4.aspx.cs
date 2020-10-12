using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace program3_protocols
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\razzb\Desktop\program3_protocols(current)\program3_protocols(current)\program3_protocols\program3_protocols\App_Data\Database1.mdf;Integrated Security=True");
        public static string globaldate;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.PreviousPage != null)
            {
                SqlDataReader rdr;
                string fileName = "";
                using (conn)
                {
                    Control ContentPlaceHolder1 = this.Page.PreviousPage.Master.FindControl("ContentPlaceHolder1");
                    Calendar calendar = (Calendar)ContentPlaceHolder1.FindControl("Calendar1");
                    string date = calendar.SelectedDate.ToShortDateString();
                    Label1.Text = date;

                    var query = "Select EventName, EventDate From Events Where EventDate =  '" + date + "'";
                    var cmd = new SqlCommand(query, conn);
                    conn.Open();
                    rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            fileName = rdr["EventName"].ToString();
                            DropDownList1.Items.Add(fileName);
                        }
                    }
                    conn.Close();
                }
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (conn)
            {
                string removeitem = DropDownList1.SelectedValue;
                    var query = "DELETE From Events Where EventName =  '" + removeitem + "' AND EventDate =  '" + Label1.Text +"'";
                    var cmd = new SqlCommand(query, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}