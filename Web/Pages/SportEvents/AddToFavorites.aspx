<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="AddToFavorites.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents.AddToFavorites" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclAddToFavorites" runat="server" Text="Add to favorites" meta:resourcekey="lclAddToFavoritesResource1"></asp:Localize>
</asp:Content>

<asp:Content ID="Content9" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <br />
    <span class="label">
        <asp:Localize ID="lclAddFavorite" runat="server" Text="Add to favorites" meta:resourcekey="lclAddFavoriteResource1"></asp:Localize>
    </span>
    <br />
    <form id="editCommentForm" method="post" runat="server">
        <div class="field">

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclNameFavorite" runat="server" Text="Name" meta:resourcekey="lclNameFavoriteResource1"></asp:Localize></span>
                <span class="entry">
                    <asp:TextBox ID="favoriteName" runat="server" Width="150px" Columns="16" MaxLength="120" meta:resourcekey="favoriteNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFavoriteName" runat="server" Text="<%$ Resources:Common, mandatoryField %>"
                        Display="Dynamic" Width="100px" ControlToValidate="favoriteName" ErrorMessage="RequiredFieldValidator" meta:resourcekey="rfvFavoriteNameResource1" />
                </span>
            </div>
            <br />
            <br />
            <br />
            <br />
            <span class="label">
                <asp:Localize ID="lclfvComment" runat="server" Text="Comment" meta:resourcekey="lclfvCommentResource1"></asp:Localize></span>
            <span class="entry">
                <textarea id="txtFavorite" name="txtFavorite" style="resize: none" cols="20" rows="10" runat="server" maxlenght="150"></textarea>
                <asp:RequiredFieldValidator ID="rfvFavoriteComment" runat="server" Text="<%$ Resources:Common, mandatoryField %>"
                    Display="Dynamic" Width="100px" ControlToValidate="txtFavorite" ErrorMessage="RequiredFieldValidator" meta:resourcekey="rfvFavoriteCommentResource1"></asp:RequiredFieldValidator>
            </span>
        </div>
        <br />
        <div class="button">
            <asp:Button ID="btnAddFavorite" runat="server" Text="Add Favorite" OnClick="BtnAddFavorite_Click" meta:resourcekey="btnAddFavoriteResource1" />
        </div>
    </form>
    <br />
    <br />
        <asp:Label ID="lblError" runat="server" Text="Error adding to favorite" Visible="False" ForeColor="Red" meta:resourcekey="lblErrorResource1"></asp:Label>
        <asp:Label ID="lblSuccess" runat="server" Text="Favorite added" Visible="False" ForeColor="Green" meta:resourcekey="lblSuccessResource1"></asp:Label>
    <br />
    <br />
    <br />
    <br />
</asp:Content>