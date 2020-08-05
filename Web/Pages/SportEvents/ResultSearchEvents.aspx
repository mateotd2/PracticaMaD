<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ResultSearchEvents.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents.ResultSearchEvents" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" Text="Resultado de busqueda" meta:resourcekey="lclMenuExplanationResource1"></asp:Localize>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_menuGroups" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <br />
    <p>
        <asp:Label ID="lblNotCoincidenceEvents" Text="No coincidences" runat="server" Visible="False" meta:resourcekey="lblNotCoincidenceEventsResource1"></asp:Label>

        <asp:Label ID="lblResultsEvents" Text="Resultados" runat="server" meta:resourcekey="lblResultsEventsResource1"></asp:Label>
    </p>

    <%--    Opcion 1 de gridview    --%>

    <form runat="server">
        <asp:GridView ID="gvEvents" runat="server" DataKeyNames="eventId"
            GridLines="None" AutoGenerateColumns="False"
            Style="margin: 0 auto; text-align: center; width: 80%;" CellPadding="4" ForeColor="#333333" meta:resourcekey="gvEventsResource1">
            <AlternatingRowStyle Height="40px" BackColor="White"></AlternatingRowStyle>
            <RowStyle Height="40px" />
            <Columns>
                <asp:BoundField DataField="eventId" HeaderText="eventId" meta:resourcekey="BoundFieldResource1" />
                <asp:BoundField DataField="ev_name" HeaderText="name" meta:resourcekey="BoundFieldResource2" />
                <asp:BoundField DataField="ev_description" HeaderText="description" meta:resourcekey="BoundFieldResource3" />
                <asp:BoundField DataField="ev_category" HeaderText="category" meta:resourcekey="BoundFieldResource4" />
                <asp:BoundField DataField="ev_date" HeaderText="date" meta:resourcekey="BoundFieldResource5" />

                <asp:HyperLinkField Text="Comments"
                    DataTextFormatString="{0:c}"
                    DataNavigateUrlFields="eventId"
                    DataNavigateUrlFormatString="/Pages/SportEvents/ShowComments.aspx?eventId={0}"  />

                <asp:HyperLinkField Text="Make a comment"
                    DataTextFormatString="{0:c}"
                    DataNavigateUrlFields="eventId"
                    DataNavigateUrlFormatString="/Pages/SportEvents/Comment.aspx?eventId={0}"  />

                <asp:HyperLinkField Text="Recommend"
                    DataTextFormatString="{0:c}"
                    DataNavigateUrlFields="eventId"
                    DataNavigateUrlFormatString="/Pages/SportEvents/Recommend.aspx?eventId={0}"  />

                <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="btnFavorite" Text="Add to favorites" OnClick="BtnFavorite_Click" meta:resourcekey="btnFavoriteResource1"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#097099" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

        <br />
    </form>

    <%--    Opcion 2 de gridview--%>
    <%--    <form id="form1" runat="server">
        <asp:GridView ID="gvEvents" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
            <Columns>
                <asp:HyperLinkField DataTextField="Comments" DataNavigateUrlFields="Comments" />
            </Columns>
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </form>--%>

    <br />
    <div class="previusNextLinks">
        <span>
            <asp:HyperLink ID="lnkPrevious" Text="Previous" Visible="False" runat="server" meta:resourcekey="lnkPreviousResource1"></asp:HyperLink>
        </span>
        &nbsp
        &nbsp
        &nbsp
        &nbsp
        <span>
            <asp:HyperLink ID="lnkNext" Text="Next" Visible="False" runat="server" meta:resourcekey="lnkNextResource1"></asp:HyperLink>
        </span>
    </div>
    <br />
</asp:Content>