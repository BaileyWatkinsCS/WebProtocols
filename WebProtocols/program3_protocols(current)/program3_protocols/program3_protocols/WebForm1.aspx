<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="program3_protocols.WebForm1" %>

<%@Register Src="~/UserControlFooter.ascx"TagName="footer"TagPrefix="UCFooter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="338px" NextPrevFormat="FullMonth" OnDayRender="Calendar1_DayRender" OnInit="Page_Load" OnSelectionChanged="Calendar1_SelectionChanged" Width="642px" CssClass="center">
    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
    <OtherMonthDayStyle ForeColor="#999999" />
    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
    <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
    <TodayDayStyle BackColor="#CCCCCC" />
</asp:Calendar>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" PostBackUrl="~/WebForm2.aspx" Text="Add" CssClass="button" Width="80px" />
    <asp:Button ID="Button2" runat="server" OnClick="Button1_Click" PostBackUrl="~/WebForm3.aspx" Text="Edit" CssClass="button" Width="80px" />
    <asp:Button ID="Button3" runat="server" OnClick="Button1_Click" PostBackUrl="~/WebForm4.aspx" Text="Remove" CssClass="button" Width="80px" />
<p>
    <asp:GridView ID="GridView1" runat="server" Width="635px" BorderColor="Black" BorderStyle="Double" CssClass="center">
    </asp:GridView>
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False" CssClass="center"></asp:Label>
</p>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    <UCFooter:footer ID="footerdiv" runat="server" />
</asp:Content>
