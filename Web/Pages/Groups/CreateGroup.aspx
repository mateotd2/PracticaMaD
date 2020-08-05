<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="CreateGroup.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents.CreateGroup" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclCreateGroup" runat="server" Text="Create Group" meta:resourcekey="lclCreateGroupResource1"></asp:Localize>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder_menuGroups" runat="server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <br />
    <br />
    <br />

    <form id="createGroupForm" method="post" runat="server">

        <div class="field">
            <span class="label">
                <asp:Localize ID="lclNameGroup" runat="server" Text="Name" meta:resourcekey="lclNameGroupResource1"></asp:Localize>
            </span>
            <span class="entry">
                <asp:TextBox ID="txtNameGroup" runat="server" Width="100px"
                    placeholder="Nombre" MaxLength="25" meta:resourcekey="txtNameGroupResource1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNameGroup" runat="server"
                    Text="<%$ Resources:Common, mandatoryField %>"
                    Display="Dynamic"
                    ControlToValidate="txtNameGroup"
                    ErrorMessage="RequiredFieldValidator" meta:resourcekey="rfvNameGroupResource1"></asp:RequiredFieldValidator>

                <asp:Label ID="lblGroupError" runat="server" Text="GroupAlreadyExist" ForeColor="Red" Style="position: relative"
                    Visible="False" meta:resourcekey="lblGroupErrorResource1"></asp:Label>
            </span>
        </div>

        <div class="field">
            <span class="label">
                <asp:Localize ID="lclDescription" runat="server" Text="Description" meta:resourcekey="lclDescriptionResource1"></asp:Localize>
            </span>
            <span class="entry">
                <textarea id="txtDescriptionGroup" maxlength='150' name="txtDescriptionGroup"
                    style="resize: none" rows="10" cols="20" runat="server"></textarea>
                <asp:RequiredFieldValidator ID="rfvDescriptionGroup" runat="server"
                    Text="<%$ Resources:Common, mandatoryField %>"
                    Display="Dynamic"
                    Columns="16"
                    Width="100px"
                    ControlToValidate="txtDescriptionGroup"
                    ErrorMessage="RequiredFieldValidator" meta:resourcekey="rfvDescriptionGroupResource1"></asp:RequiredFieldValidator>
            </span>
        </div>

        <div class="button">
            <asp:Button ID="btnCreateGroup" runat="server" Text="Create Group" OnClick="CreateGroupClick" meta:resourcekey="btnCreateGroupResource1" />
        </div>
    </form>

    <br />
    <br />
        <asp:Label ID="lblGroupCreated" runat="server" Text="Group Created" Visible="false" ForeColor="Green" meta:resourcekey="lblGroupCreated" ></asp:Label>
        <asp:Label ID="lblErrorCreatingGroup" runat="server" Text="Error making the group" Visible="false" ForeColor="Red" meta:resourcekey="lblErrorCreatingGroup"></asp:Label>
    <br />
    <br />
    <br />
</asp:Content>