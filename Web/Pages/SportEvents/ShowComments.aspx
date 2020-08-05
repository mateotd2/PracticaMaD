<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ShowComments.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents.ShowComments" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclComments" runat="server" Text="All comments fron the event." meta:resourcekey="lclCommentsResource1"></asp:Localize>
</asp:Content>

<asp:Content ID="Content9" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <br />
    <asp:Label ID="lblComments" runat="server" Text="Comments" meta:resourcekey="lblCommentsResource1"></asp:Label>
    <asp:Label ID="lblNoComments" runat="server" Text="No Comments" Visible="False" meta:resourcekey="lblNoCommentsResource1"></asp:Label>
    <br />
    <br />
    <form runat="server">
        <asp:GridView ID="gvComments" runat="server" Style="margin: 0 auto; text-align: center;"
            OnRowDataBound="GvComments_RowDataBound" DataKeyNames="commentId"
            GridLines="None" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" Width="60%" meta:resourcekey="gvCommentsResource1">

            <Columns>
                <asp:BoundField DataField="commentId" HeaderText="commentId" meta:resourcekey="BoundFieldResource1" />
                <asp:BoundField DataField="loginName" HeaderText="author" meta:resourcekey="BoundFieldResource2" />
                <asp:BoundField DataField="eventId" HeaderText="eventId" meta:resourcekey="BoundFieldResource3" />
                <asp:BoundField DataField="publishDate" HeaderText="date" meta:resourcekey="BoundFieldResource4" />
                <asp:BoundField DataField="comment_description" HeaderText="Comment" meta:resourcekey="BoundFieldResource5" />
                <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="btnEdit" Text="Edit" OnClick="Btn_Click" meta:resourcekey="btnEditResource1"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            <RowStyle Height="40px" />

            <AlternatingRowStyle Height="40px" BackColor="White"></AlternatingRowStyle>

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
</asp:Content>