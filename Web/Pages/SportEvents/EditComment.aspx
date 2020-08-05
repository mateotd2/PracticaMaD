<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="EditComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents.EditComment" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" Text="Edit Comment" meta:resourcekey="lclMenuExplanationResource1"></asp:Localize>
</asp:Content>

<asp:Content ID="Content9" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <br />
    <br />
    <span class="label">
        <asp:Localize ID="lclComment" runat="server" Text="Edit Comment" meta:resourcekey="lclCommentResource1"></asp:Localize>
    </span>
    <br />
    <br />
    <form id="editCommentForm" method="post" runat="server">
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclfvComment" runat="server" Text="Comment" meta:resourcekey="lclfvCommentResource1"></asp:Localize>
            </span>
            <span class="entry">
                <textarea id="txtComment" name="txtComment" style="resize: none" maxlength='200' cols="20" rows="10" runat="server"></textarea>
                <asp:RequiredFieldValidator ID="rfvComment" runat="server" Text="<%$ Resources:Common, mandatoryField %>"
                    Display="Dynamic" Width="100px" ControlToValidate="txtComment" ErrorMessage="RequiredFieldValidator" meta:resourcekey="rfvCommentResource1"></asp:RequiredFieldValidator>
            </span>
        </div>

        <div class="button">
            <asp:Button ID="btnEditComment" runat="server" Text="EditComment" OnClick="BtnEditComment_Click" meta:resourcekey="btnEditCommentResource1" />
        </div>
    </form>
    <br />
    <br />
    <asp:Label ID="lblError" runat="server" Text="Error doing comment" Visible="False" ForeColor="Red" meta:resourcekey="lblErrorResource1"></asp:Label>
    <asp:Label ID="lblSuccess" runat="server" Text="Comment edited" Visible="False" ForeColor="Green" meta:resourcekey="lblSuccessResource1"></asp:Label>
    <br />
    <br />
</asp:Content>