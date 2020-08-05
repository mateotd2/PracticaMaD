<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="SportEventSearch.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents.SportEventSearch" meta:resourcekey="PageResource1" %>

<asp:Content ID="ContentWellcome" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="ContentExplanation" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" Text="Buscar eventos." meta:resourcekey="lclMenuExplanationResource1"></asp:Localize>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder_SearchLink" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="SearchForm" runat="server">
        <br />
        <br />
        <asp:Label ID="lblSearch" runat="server" Text="Search for a event." meta:resourcekey="lblSearchResource1"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="searchInput" runat="server" Width="40%" meta:resourcekey="searchInputResource1"></asp:TextBox>
        &nbsp
    <asp:DropDownList ID="categoriesList" runat="server" Width="15%" meta:resourcekey="categoriesListResource1">
        <asp:ListItem Text="Sin categoria" Value="" Selected="True" meta:resourcekey="defaultListItemResource" />
    </asp:DropDownList>
        <br />

        <br />
        <asp:Button ID="searchButton" runat="server" Text="Buscar Evento" Width="127px" OnClick="SearchEventsClick" meta:resourcekey="searchButtonResource1" />
    </form>
</asp:Content>