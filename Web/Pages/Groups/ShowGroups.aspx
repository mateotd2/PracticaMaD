<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ShowGroups.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents.ShowGroups" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" Text="Grupos creados." meta:resourcekey="lclMenuExplanationResource1"></asp:Localize>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_menuGroups" runat="server">
    <asp:Label ID="lblDash4" runat="server" Text="-" meta:resourcekey="lblDash4Resource1" />
    <asp:HyperLink ID="lnkCreateGroup" Text="CreateGroup" runat="server"
        NavigateUrl="~/Pages/Groups/CreateGroup.aspx" meta:resourcekey="lnkCreateGroupResource1" />
    <asp:Label ID="lblDash5" runat="server" Text="-" meta:resourcekey="lblDash5Resource1" />
    <asp:HyperLink ID="lnkMyGroups" Text="MyGroups" runat="server"
        NavigateUrl="~/Pages/Groups/MyGroups.aspx" meta:resourcekey="lnkMyGroupsResource1" />
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <br />
    <br />

    <p>
        <asp:Label ID="lblNoGroups" Text="No groups" runat="server" Visible="False" meta:resourcekey="lblNoGroupsResource1"></asp:Label>
        <asp:Label ID="lblGroups" runat="server" Text="All Groups" Visible="False" meta:resourcekey="lblGroupsResource1"></asp:Label>
    </p>

    <br />
    <form runat="server">
        <asp:GridView ID="gvAllGroups" runat="server" AutoGenerateColumns="False"
            GridLines="None"
            DataKeyNames="group_usersId"
            Style="margin: 0 auto; text-align: center; width: 50%;" ForeColor="#333333"
            OnRowDataBound="GvAllGroups_RowDataBound" meta:resourcekey="gvAllGroupsResource1">
            <RowStyle Height="40px" />
            <AlternatingRowStyle Height="40px" BackColor="White" />

            <Columns>

                <asp:BoundField DataField="group_usersId" HeaderText="group_usersId" meta:resourcekey="BoundFieldResource1" />
                <asp:BoundField DataField="gr_name" HeaderText="name" meta:resourcekey="BoundFieldResource2" />
                <asp:BoundField DataField="gr_description" HeaderText="Description" meta:resourcekey="BoundFieldResource3" />
                <asp:BoundField DataField="gr_amount_users" HeaderText="Members" meta:resourcekey="BoundFieldResource4" />
                <asp:BoundField DataField="gr_amount_recommendation" HeaderText="Recommendations" meta:resourcekey="BoundFieldResource5" />

                <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkSubscribeAuth" CommandName="Subscribe" NavigateUrl="~/Pages/User/Authentication.aspx" runat="server" Text="Subscribe" ForeColor="Blue" meta:resourcekey="lnkSubscribeAuthResource1" />
                        <asp:Label ID="lblSubscribed" Text="Already subscribed" runat="server" Visible="False" ForeColor="Green" meta:resourcekey="lblSubscribedResource1"></asp:Label>
                        <asp:LinkButton ID="lnkSubscribeUser" runat="server" Text="Subscribe" Visible="False" ForeColor="Blue" CommandArgument="group_usersId" OnClick="LnkSubscribeUser_Click" meta:resourcekey="lnkSubscribeUserResource1"></asp:LinkButton>
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
    </form>
    <br />
    <%--   <div class="previusNextLinks">
        <span>
            <asp:HyperLink ID="lnkPrevious" Text="Anterior" Visible="False" runat="server"></asp:HyperLink>
        </span>
        &nbsp
        &nbsp
        &nbsp
        &nbsp
        <span>
            <asp:HyperLink ID="lnkNext" Text="Siguiente" Visible="false" runat="server"></asp:HyperLink>
        </span>
    </div>--%>

    <asp:Label ID="lblSubscribed" runat="server" Visible="False" Text="Subscribed!" ForeColor="Green" meta:resourcekey="lblSubscribedResource2"></asp:Label>
    <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="Red" Text="Could not subscribe" meta:resourcekey="lblErrorResource1"></asp:Label>
</asp:Content>