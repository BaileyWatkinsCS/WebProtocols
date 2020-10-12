<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="program3_protocols.WebForm2" %>

<%@Register Src="~/UserControlFooter.ascx"TagName="footer"TagPrefix="UCFooter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Please Enter Event Information and Click 'Add Event'"></asp:Label>
        <asp:Label ID="Label1" runat="server" Text="Date: " CssClass="button"></asp:Label>
        <asp:TextBox ID="DateText" runat="server" CssClass="button" ValidationGroup="vg"></asp:TextBox>
        <asp:CompareValidator
            id="dateValidator" runat="server" 
            Type="Date"
            Operator="DataTypeCheck"
            ControlToValidate="DateText" 
            ErrorMessage="Please enter a valid date." CssClass="button" ValidationGroup="vg" ForeColor="Red"></asp:CompareValidator>
            <asp:RequiredFieldValidator
            ID="RequiredFieldValidator3" 
            runat="server" 
            ControlToValidate ="DateText"
            ErrorMessage="Date Cannot Be Empty"
            CssClass="button" ValidationGroup="vg" ForeColor="Red"></asp:RequiredFieldValidator>
    </p>
    <asp:Label ID="Label6" runat="server" Text="Time" CssClass="button"></asp:Label>
    <asp:TextBox ID="TimeText" runat="server" CssClass="button" ValidationGroup="vg"></asp:TextBox>
     <asp:RequiredFieldValidator
            ID="RequiredFieldValidator2" 
            runat="server" 
            ControlToValidate ="TimeText"
            ErrorMessage="Time Cannot Be Empty"
            CssClass="button" ValidationGroup="vg" ForeColor="Red"></asp:RequiredFieldValidator>
    <p>
        <asp:Label ID="Label7" runat="server" Text="Name" CssClass="button"></asp:Label>
        <asp:TextBox ID="NameText" runat="server" CssClass="button" ValidationGroup="vg"></asp:TextBox>
        <asp:RequiredFieldValidator
            ID="rfvcandidate" 
            runat="server" 
            ControlToValidate ="NameText"
            ErrorMessage="Name Cannot Be Empty"
            CssClass="button" ValidationGroup="vg" ForeColor="Red"></asp:RequiredFieldValidator>
    </p>
    <asp:Label ID="Label8" runat="server" Text="Description" CssClass="button"></asp:Label>
    <asp:TextBox ID="DescText" runat="server" CssClass="button" ValidationGroup="vg"></asp:TextBox>
    <asp:RequiredFieldValidator
            ID="RequiredFieldValidator1" 
            runat="server" 
            ControlToValidate ="DescText"
            ErrorMessage="Description Cannot Be Empty"
            CssClass="button" ValidationGroup="vg" ForeColor="Red"></asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Add Event: " PostBackUrl="~/WebForm1.aspx" CssClass="button" ValidationGroup="vg" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="vg" />
    <UCFooter:footer ID="footerdiv" runat="server" />
</asp:Content>
