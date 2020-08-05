<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ShowRecommendation.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents.ShowRecommendation" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclRecommendation" runat="server" Text="Recommendations" meta:resourcekey="lclRecommendationResource1"></asp:Localize>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <br />
    <p>
        <asp:Label ID="lblRecommendations" runat="server" Text="Recommendations" meta:resourcekey="lblRecommendationsResource1"></asp:Label>
        <asp:Label ID="lblNorecommendations" runat="server" Text="No Recommendations" Visible="False" meta:resourcekey="lblNorecommendationsResource1"></asp:Label>
    </p>
    <br />
    <br />
    <form runat="server">
        <asp:GridView ID="gvRecommendations" DataKeyNames="recommendationId" runat="server" AutoGenerateColumns="False"
            CellPadding="4" ForeColor="#333333" GridLines="None"
            Style="margin: 0 auto; text-align: center; width: 80%;" meta:resourcekey="gvRecommendationsResource1">

            <Columns>
                <asp:BoundField DataField="recommendationId" HeaderText="RecommendationId" meta:resourcekey="BoundFieldResource1" />
                <asp:BoundField DataField="eventId" HeaderText="EventId" meta:resourcekey="BoundFieldResource2" />
                <asp:HyperLinkField DataTextField="eventName" DataTextFormatString="{0:c}" DataNavigateUrlFields="eventId"
                    DataNavigateUrlFormatString="/Pages/SportEvents/ShowComments.aspx?eventId={0}" HeaderText="EventName" meta:resourcekey="HyperLinkFieldResource1" />
                <asp:BoundField DataField="login_user" HeaderText="Author" meta:resourcekey="BoundFieldResource3" />

                <asp:BoundField DataField="recommendation_text" HeaderText="Description" meta:resourcekey="BoundFieldResource4" />
            </Columns>

            <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>

            <EditRowStyle BackColor="#2461BF"></EditRowStyle>

            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle>

            <HeaderStyle BackColor="#097099" Font-Bold="True" ForeColor="White"></HeaderStyle>

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
    <br />
</asp:Content>