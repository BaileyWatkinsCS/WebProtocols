using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace program3_protocols
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            //if there is a previous page this will load the date from the previous calander and then get a ,list of event names on that name from the DB
            //XML Selection not utilized here could be implemented but not at the same time 
            //without adding a select from database function. 
            if (this.Page.PreviousPage != null)
            {
                SqlDataReader rdr;
                string name = "";
                using (conn)
                {
                    Control ContentPlaceHolder1 = this.Page.PreviousPage.Master.FindControl("ContentPlaceHolder1");
                    Calendar calendar = (Calendar)ContentPlaceHolder1.FindControl("Calendar1");
                    string date = calendar.SelectedDate.ToShortDateString();
                    Label1.Text = date;

                    var query = "Select * From Events Where EventDate =  '" + date + "'";
                    var cmd = new SqlCommand(query, conn);
                    conn.Open();
                    rdr = cmd.ExecuteReader();
                    DropDownList1.Items.Add(" ");
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            name = rdr["EventName"].ToString();

                            DropDownList1.Items.Add(name);

                        }
                    }
                    conn.Close();
                }
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (conn)
            {
                //this retrieves selected item in the dropdown from the DB. XML Selection not utilized here could be implemented but not at the same time 
                //without adding a select from database function. 
                SqlDataReader rdr;
                var query = "Select * From Events Where EventName =  '" + DropDownList1.SelectedValue.ToString() + "' AND EventDate = '" + Label1.Text + "'";
                var cmd = new SqlCommand(query, conn);
                conn.Open();
                rdr = cmd.ExecuteReader();

                //Reads in vaules from database dpeneding on selected item
                while (rdr.Read())
                {
                    DateText.Text = Label1.Text;
                    NameText.Text = rdr["EventName"].ToString();
                    DescText.Text = rdr["EventDescription"].ToString();
                    TimeText.Text = rdr["EventTime"].ToString();

                }
                conn.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string selecteditem = DropDownList1.SelectedValue;
            using (conn)
            {
                //connection to database
                var query = "UPDATE Events SET EventName = '" + NameText.Text + "', EventDate = '" + DateText.Text + "', EventDescription =  '" + DescText.Text + "', EventTime ='" + TimeText.Text + "' Where EventName =  '" + selecteditem + "' AND EventDate =  '" + Label1.Text + "'";
                var cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            
            try
            {
                //using XML File
                var path = Server.MapPath("~/App_Data/XMLFile1.xml");
                XElement root = XElement.Load(path);

                var EventNameXML = selecteditem.ToString();
                var EventDateXML = Label1.Text.ToString();

                //gets ID of event based on Name and Date of Event
                var values = root.Descendants("Table")
                    .Where(i => i.Element("EventName").Value == EventNameXML)
                    .Where(i => i.Element("EventDate").Value == EventDateXML)
                    .Select(i => i.Attribute("ID").Value)
                    .Distinct();

                //Store ID
                String Id = values.FirstOrDefault().ToString();

                //Update based on ID and the elements of the attributes
                XElement update = root.Descendants("Table").FirstOrDefault(p => p.Attribute("ID").Value == Id);
                if (update != null)
                {
                    update.Element("EventName").Value = NameText.Text;
                    update.Element("EventDescription").Value = DescText.Text;
                    update.Element("EventTime").Value = TimeText.Text;
                    update.Element("EventDate").Value = DateText.Text;
                    root.Save(path);
                }

            }
            catch (Exception err)
            {
                //error catching
                Console.WriteLine(err);
            }
        }
    }
}