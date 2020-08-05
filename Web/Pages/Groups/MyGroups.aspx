<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" CodeBehind="MyGroups.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents.MyGroups" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclExplanation" runat="server" Text="My Groups" meta:resourcekey="lclExplanationResource1"></asp:Localize>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder_menuGroups" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <br />
    <br />

    <p>
        <asp:Label ID="lblNoGroups" Text="No groups" runat="server" Visible="False" meta:resourcekey="lblNoGroupsResource1"></asp:Label>
        <asp:Label ID="lblGroups" Text="Groups from user" runat="server" Visible="False" meta:resourcekey="lblGroupsResource1"></asp:Label>
    </p>
    <br />

    <form runat="server">
        <asp:GridView ID="gvGroups" runat="server" AutoGenerateColumns="False"
            GridLines="None" DataKeyNames="group_usersId"
            Style="margin: 0 auto; text-align: center; width: 50%;" CellPadding="4" ForeColor="#333333"
            OnRowDeleting="GvGroups_RowDeleting" meta:resourcekey="gvGroupsResource1">
            <AlternatingRowStyle Height="40px" BackColor="White" />
            <RowStyle Height="40px" />
            <Columns>

                <asp:BoundField DataField="group_usersId" HeaderText="group_usersId" meta:resourcekey="BoundFieldResource1" />
                <asp:BoundField DataField="gr_name" HeaderText="name" meta:resourcekey="BoundFieldResource2" />
                <asp:CommandField ShowDeleteButton="true" DeleteText="Unsubscribe" ButtonType="Link" ControlStyle-ForeColor="Red" meta:resourcekey="CommandFieldResource1">

                    <ControlStyle ForeColor="Red"></ControlStyle>
                </asp:CommandField>
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
    </form>
    <br />
    <br />
    <asp:Label ID="lblErrorOwner" runat="server" ForeColor="Red" Style="position: relative"
        Visible="False" Text="Not posile to unsubscribe, you are the owner" meta:resourcekey="lblErrorOwnerResource1"></asp:Label>
    <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" Style="position: relative"
        Visible="False" Text="Unsuscribed" meta:resourcekey="lblSuccessResource1"></asp:Label>
    <br />

    <br />
</asp:Content>