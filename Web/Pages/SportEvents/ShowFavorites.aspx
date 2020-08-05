<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ShowFavorites.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents.ShowFavorites" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclFavorites" runat="server" Text="Show Favorites" meta:resourcekey="lclFavoritesResource1"></asp:Localize>
</asp:Content>

<asp:Content ID="Content9" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <br />
    <br />
    <p>
        <asp:Label ID="lblFavorites" runat="server" Text="Favorites" meta:resourcekey="lblFavoritesResource1"></asp:Label>
        <asp:Label ID="lblNoFavorites" runat="server" Text="No Favorites" Visible="False" meta:resourcekey="lblNoFavoritesResource1"></asp:Label>
    </p>
    <br />
    <br />
    <br />
    <form runat="server">
        <asp:GridView ID="gvFavorites" runat="server" AutoGenerateColumns="False" DataKeyNames="eventId" CellPadding="4" ForeColor="#333333"
            GridLines="None" Style="margin: 0 auto; text-align: center;" Width="80%" OnRowDeleting="GvFavorites_RowDeleting" meta:resourcekey="gvFavoritesResource1">

            <Columns>
                <asp:BoundField DataField="eventId" HeaderText="eventId" meta:resourcekey="BoundFieldResource1" />
                <asp:HyperLinkField DataTextField="fv_name" DataTextFormatString="{0:c}"
                    DataNavigateUrlFields="eventId" ControlStyle-ForeColor="Blue" HeaderText="name"
                    DataNavigateUrlFormatString="/Pages/SportEvents/ShowComments.aspx?eventId={0}" meta:resourcekey="HyperLinkFieldResource1">

                    <ControlStyle ForeColor="Blue"></ControlStyle>
                </asp:HyperLinkField>

                <asp:BoundField DataField="fv_date" HeaderText="date" meta:resourcekey="BoundFieldResource2" />
                <asp:BoundField DataField="comment" HeaderText="comment" meta:resourcekey="BoundFieldResource3" />
                <asp:CommandField ButtonType="Link" ShowDeleteButton="true" ControlStyle-ForeColor="Red" meta:resourcekey="CommandFieldResource1">
                    <ControlStyle ForeColor="Red"></ControlStyle>
                </asp:CommandField>
            </Columns>

            <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>

            <EditRowStyle BackColor="#2461BF"></EditRowStyle>

            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle>

            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>

            <PagerStyle HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White"></PagerStyle>

            <RowStyle BackColor="#EFF3FB"></RowStyle>

            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

            <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>

            <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>

            <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>

            <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
        </asp:GridView>
    </form>
    <br />
    <br />
    <br />
    <br />
    <asp:Label ID="lblSuccess" runat="server" Text="Favorite deleted" Visible="False" ForeColor="Green" meta:resourcekey="lblSuccessResource1"></asp:Label>
    <asp:Label ID="lblError" runat="server" Text="Cant remove the favorite" ForeColor="Red" Visible="False" meta:resourcekey="lblErrorResource1"></asp:Label>

    <br />
</asp:Content>