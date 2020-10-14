using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace program3_protocols
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
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
            string removeitem = DropDownList1.SelectedValue;
            using (conn)
            {          
                    var query = "DELETE From Events Where EventName =  '" + removeitem + "' AND EventDate =  '" + Label1.Text +"'";
                    var cmd = new SqlCommand(query, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                conn.Close();
            }

            try
            {

                var path = "E:/WebProtocols/WebProtocols Git/WebProtocols/program3_protocols(current)/program3_protocols/program3_protocols/App_Data/XMLFile1.xml";
                XElement root = XElement.Load(path);

                var EventNameXML = removeitem.ToString();
                var EventDateXML = Label1.Text.ToString();

                var values = root.Descendants("Table")
                    .Where(i => i.Element("EventName").Value == EventNameXML)
                    .Where(i => i.Element("EventDate").Value == EventDateXML)
                    .Select(i => i.Attribute("ID").Value)
                    .Distinct();

                String Id = values.FirstOrDefault().ToString();

                XElement remove = root.Descendants("Table").FirstOrDefault(p => p.Attribute("ID").Value == Id);
                if (remove != null)
                {
                    remove.Remove();
                    root.Save(path);

                }           

            }
            catch (Exception err)
            {
                //error msg here
            }
        }
    }
}