﻿using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace program3_protocols
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get Previous page information of the calendar day
            if (this.Page.PreviousPage != null)
            {
                Control ContentPlaceHolder1 = this.Page.PreviousPage.Master.FindControl("ContentPlaceHolder1");
                Calendar calendar = (Calendar)ContentPlaceHolder1.FindControl("Calendar1");
                DateText.Text = calendar.SelectedDate.ToShortDateString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //DB Add
            con.Open();
            String str = "INSERT INTO Events (Eventname,EventDate,EventDescription,EventTime) VALUES ('" + NameText.Text + "','" + DateText.Text + "','" + DescText.Text + "','" + TimeText.Text + "')";
            SqlCommand cmd = new SqlCommand(str, con);
            int OBJ = Convert.ToInt32(cmd.ExecuteNonQuery());
            con.Close();

            //XML ADD
            try
            {
                var path = Server.MapPath("~/App_Data/XMLFile1.xml");
                XDocument testXML = XDocument.Load(path);

                XElement newEvent = new XElement("Table",
                                               new XElement("EventName", NameText.Text), new XElement("EventDescription", DescText.Text),
                                               new XElement("EventTime", TimeText.Text), new XElement("EventDate", DateText.Text));

                var lastEvent = testXML.Descendants("Table").Last();
                int newID = Convert.ToInt32(lastEvent.Attribute("ID").Value);
                newEvent.SetAttributeValue("ID", newID + 1);
                testXML.Element("NewDataSet").Add(newEvent);
                testXML.Save(path);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }
    }
}