using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace program3_protocols
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.PreviousPage != null)
            {
                SqlDataReader rdr;
                string name = "";
                string desc = "";
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
                SqlDataReader rdr;
                var query = "Select * From Events Where EventName =  '" + DropDownList1.SelectedValue.ToString() + "' AND EventDate = '" + Label1.Text + "'";
                var cmd = new SqlCommand(query, conn);
                conn.Open();
                rdr = cmd.ExecuteReader();

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

            //using XML File
            try
            {

                var path = "E:/WebProtocols/WebProtocols Git/WebProtocols/program3_protocols(current)/program3_protocols/program3_protocols/App_Data/XMLFile1.xml";
                XElement root = XElement.Load(path);

                var EventNameXML = selecteditem.ToString();
                var EventDateXML = Label1.Text.ToString();

                var values = root.Descendants("Table")
                    .Where(i => i.Element("EventName").Value == EventNameXML)
                    .Where(i => i.Element("EventDate").Value == EventDateXML)
                    .Select(i => i.Attribute("ID").Value)
                    .Distinct();

                String Id = values.FirstOrDefault().ToString();

                XElement update = root.Descendants("Table").FirstOrDefault(p => p.Attribute("ID").Value == Id);
                if (update != null)
                {
                    update.Element("EventName").Value = NameText.Text;
                    update.Element("EventDescription").Value = DescText.Text;
                    update.Element("EventTime").Value = TimeText.Text;
                    update.Element("EventDate").Value = DateText.Text;
                    //root.Add(update);
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