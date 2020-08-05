<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="Recommend.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents.Recommend" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMakeRecommendation" runat="server" Text="Make recommendation" meta:resourcekey="lclMakeRecommendationResource1"></asp:Localize>
</asp:Content>

<asp:Content ID="Content9" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <br />
    <asp:Label ID="lblNoeventId" runat="server" Text="Recommendation" Visible="False" meta:resourcekey="lblNoeventIdResource1"></asp:Label>
    <asp:Label ID="lblEvents" runat="server" Text="Events" meta:resourcekey="lblEventsResource1"></asp:Label>
    <br />
    <br />
    <br />
    <form id="makeRecommendForm" method="post" runat="server">

        <div class="field">
            <span class="label">
                <asp:Localize ID="lclRecommendation" runat="server" Text="MakeRecommendation" meta:resourcekey="lclRecommendationResource1"></asp:Localize>
            </span>
            <span class="entry">
                <textarea id="txtRecommend" style="resize: none" cols="20" maxlength='300' rows="10" runat="server"></textarea>
                <asp:RequiredFieldValidator ID="rfvRecommendation" runat="server" Text="<%$ Resources:Common, mandatoryField %>"
                    Display="Dynamic" Width="100px" ControlToValidate="txtRecommend" ErrorMessage="RequiredFieldValidator" meta:resourcekey="rfvRecommendationResource1"></asp:RequiredFieldValidator>
            </span>
        </div>

        <br />
        <br />
        <br />
        <p>
            <asp:Label ID="lblSelectCheckBox" runat="server" Text="Select Groups to Recommend" meta:resourcekey="lblSelectCheckBoxResource1"></asp:Label>
            <asp:Label ID="lblNoGroupsToRecommend" runat="server" Text="No groups to recommend" Visible="False" meta:resourcekey="lblNoGroupsToRecommendResource1"></asp:Label>
        </p>
        <br />
        <br />
        <br />
        <div>
            <asp:GridView ID="gvGroups" runat="server" AutoGenerateColumns="False"
                GridLines="None" DataKeyNames="group_usersId"
                Style="margin: 0 auto; text-align: center; width: 50%;" CellPadding="4" ForeColor="#333333" meta:resourcekey="gvGroupsResource1">
                <AlternatingRowStyle Height="40px" BackColor="White" />
                <RowStyle Height="40px" />
                <Columns>

                    <asp:BoundField DataField="group_usersId" HeaderText="group_usersId" meta:resourcekey="BoundFieldResource1" />
                    <asp:BoundField DataField="gr_name" HeaderText="name" meta:resourcekey="BoundFieldResource2" />
                    <asp:TemplateField HeaderText="ChechRecommend" meta:resourcekey="TemplateFieldResource1">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkGroup" runat="server" meta:resourcekey="chkGroupResource1" />
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
            <br />
        </div>
        <div class="button">
            <asp:Button ID="btnCreateRecommendation" runat="server" Text="makeRecommendation" OnClick="BtnCreateRecommendation_Click" meta:resourcekey="btnCreateRecommendationResource1" />
        </div>
    </form>
    <br />
    <br />
    <br />
    <p>
        <asp:Label ID="lblError" runat="server" Text="Something bad happend" ForeColor="Red" Visible="False" meta:resourcekey="lblErrorResource1"></asp:Label>
        <asp:Label ID="lblSuccess" runat="server" Text="Recommendation created" ForeColor="Green" Visible="False" meta:resourcekey="lblSuccessResource1"></asp:Label>
        <asp:Label ID="lblNoGroupsSelected" runat="server" Text="Select at least 1 group" ForeColor="Red" Visible="False" meta:resourcekey="lblNoGroupsSelectedResource1"></asp:Label>
    </p>
    <br />
    <br />
</asp:Content>