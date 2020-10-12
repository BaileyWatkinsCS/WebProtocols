<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="program3_protocols.WebForm4" %>

<%@Register Src="~/UserControlFooter.ascx"TagName="footer"TagPrefix="UCFooter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label" CssClass="label"></asp:Label>
    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Please Select An Event To Delete For This Date"></asp:Label>
    <br />
    <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="button" Height="18px" Width="110px">
    </asp:DropDownList>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" PostBackUrl="~/WebForm1.aspx" Text="Delete" CssClass="button" />
    <br />
    <UCFooter:footer ID="footerdiv" runat="server" />
</asp:Content>
