<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="program3_protocols.WebForm3" %>

<%@Register Src="~/UserControlFooter.ascx"TagName="footer"TagPrefix="UCFooter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label" CssClass="label"></asp:Label>
    <br />
    <asp:Label ID="Label6" runat="server" CssClass="label" Text="Select Event To Update For This Date"></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True" CssClass="button" Height="19px" Width="113px">
    </asp:DropDownList>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Update" PostBackUrl="~/WebForm1.aspx" CssClass="button" ValidationGroup="vg" />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Name: " CssClass="button"></asp:Label>
    <asp:TextBox ID="NameText" runat="server" CssClass="button" ValidationGroup="vg"></asp:TextBox>
    <asp:RequiredFieldValidator
            ID="RequiredFieldValidator2" 
            runat="server" 
            ControlToValidate ="NameText"
            ErrorMessage="Name Cannot Be Empty"
            CssClass="button" ValidationGroup="vg" ForeColor="Red"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="Label5" runat="server" CssClass="button" Text="Time:"></asp:Label>
<asp:TextBox ID="TimeText" runat="server" CssClass="button" ValidationGroup="vg"></asp:TextBox>
    <asp:RequiredFieldValidator
            ID="RequiredFieldValidator3" 
            runat="server" 
            ControlToValidate ="TimeText"
            ErrorMessage="Time Cannot Be Empty"
            CssClass="button" ValidationGroup="vg" ForeColor="Red"></asp:RequiredFieldValidator>
    <asp:Label ID="Label3" runat="server" Text="Date: " CssClass="button"></asp:Label>
    <asp:TextBox ID="DateText" runat="server" CssClass="button" ValidationGroup="vg"></asp:TextBox>
     <asp:CompareValidator
            id="dateValidator" runat="server" 
            Type="Date"
            Operator="DataTypeCheck"
            ControlToValidate="DateText" 
            ErrorMessage="Please enter a valid date." CssClass="button" ValidationGroup="vg" ForeColor="Red"></asp:CompareValidator>
            <asp:RequiredFieldValidator
            ID="RequiredFieldValidator4" 
            runat="server" 
            ControlToValidate ="DateText"
            ErrorMessage="Date Cannot Be Empty"
            CssClass="button" ValidationGroup="vg" ForeColor="Red"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="Label4" runat="server" Text="Description: " CssClass="button"></asp:Label>
    <asp:TextBox ID="DescText" runat="server" CssClass="button" ValidationGroup="vg"></asp:TextBox>
    <asp:RequiredFieldValidator
            ID="RequiredFieldValidator1" 
            runat="server" 
            ControlToValidate ="DescText"
            ErrorMessage="Description Cannot Be Empty"
            CssClass="button" ValidationGroup="vg" ForeColor="Red"></asp:RequiredFieldValidator>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Events]"></asp:SqlDataSource>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="vg" />
    <UCFooter:footer ID="footerdiv" runat="server" />
</asp:Content>

