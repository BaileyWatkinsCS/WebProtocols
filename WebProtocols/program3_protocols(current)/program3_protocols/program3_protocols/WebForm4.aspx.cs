using System;
using System.Data.SqlClient;
using System.Linq;
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

            //if there is a previous page this will load the date from the previous calander and then get a ,list of event names on that name from the DB
            //XML Selection not utilized here could be implemented but not at the same time 
            //without adding a select from database function.
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Selects item from drop down based on the date removed and then removes it using DB
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
                //XML Remove
                var path = Server.MapPath("~/App_Data/XMLFile1.xml");
                XElement root = XElement.Load(path);

                var EventNameXML = removeitem.ToString();
                var EventDateXML = Label1.Text.ToString();

                //gets ID of event based on Name and Date of Event
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
                Console.WriteLine(err);
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}