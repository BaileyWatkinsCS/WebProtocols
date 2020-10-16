using System;
using System.Web.UI;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace program3_protocols
{


    public partial class WebForm1 : System.Web.UI.Page
    {
        protected DataGrid DataGrid1;
        private DataTable socialEvents = new DataTable();
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            //buttons will be hidden unless something is in DB or XML
            BuildSocialEventTable();
            Button1.Visible = true;
            Button2.Visible = false;
            Button3.Visible = false;

            if (this.Page.PreviousPage != null)
            {
                Control ContentPlaceHolder1 = this.Page.PreviousPage.Master.FindControl("ContentPlaceHolder1");

                TextBox Name = (TextBox)ContentPlaceHolder1.FindControl("NameText");
                TextBox Date = (TextBox)ContentPlaceHolder1.FindControl("DateText");
                TextBox Desc = (TextBox)ContentPlaceHolder1.FindControl("DescText");
            }
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            //Formats and renders what day is in database to highlight on calendar
            DataRow[] rows = socialEvents.Select(
                  String.Format(
                     "EventDate >= #{0}# AND EventDate < #{1}#",
                     e.Day.Date.ToShortDateString(),
                     e.Day.Date.AddDays(1).ToShortDateString()));

            foreach (DataRow row in rows)
            {
                e.Cell.BackColor = Color.Wheat;
            }
        }

        public void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            System.Data.DataView view = socialEvents.DefaultView;
            view.RowFilter = String.Format(
                              "EventDate >= #{0}# AND EventDate < #{1}#",
                              Calendar1.SelectedDate.ToShortDateString(),
                              Calendar1.SelectedDate.AddDays(1).ToShortDateString());

            //if in DB you can see the event
            if (view.Count > 0)
            {
                Button2.Visible = true;
                Button3.Visible = true;
                GridView1.Visible = true;
                GridView1.DataSource = view;
                GridView1.DataBind();
            }
            else
            {
                GridView1.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        private void BuildSocialEventTable()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select EventName, EventDescription, EventTime, EventDate FROM Events", con);
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            sda.Fill(ds);
            ds2.ReadXml(Server.MapPath("~/App_Data/XMLFile1.xml")); //Store the XMl in a data set
            socialEvents = ds2.Tables[0]; // Load the XML dataset into the datatable
            //var path = "C:/Users/razzb/OneDrive/Documents/GitHub/WebProtocols/WebProtocols/program3_protocols(current)/program3_protocols/program3_protocols/App_Data/XMLFile1.xml";
            //ds.WriteXml(path); Un comment this to create an XML file from the stuff in the database file
            con.Close();
        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
        }
    }
}
