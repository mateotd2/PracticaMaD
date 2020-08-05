using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Web;
using System.Web.UI;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents
{
    public partial class CreateGroup : SpecificCulturePage
    {
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        /// <summary>Creates the group click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void CreateGroupClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    string groupName = this.txtNameGroup.Text;
                    string groupDescription = txtDescriptionGroup.Value;
                    IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

                    IRecommendationGroupService recommendationGroupService = ioCManager.Resolve<IRecommendationGroupService>();
                    HttpCookie cookie = Request.Cookies["loginName"];
                    try
                    {
                        recommendationGroupService.CreateGroup(groupName, groupDescription, cookie.Value);
                        //Response.Redirect(Response.ApplyAppPathModifier("~/Pages/MainPage.aspx"));
                        this.lblGroupCreated.Visible = true;
                        this.btnCreateGroup.Visible = false;
                    }
                    catch (GroupAlreadyExistsException)
                    {
                        this.lblGroupError.Visible = true;
                    }
                }
                catch (DuplicateInstanceException)
                {
                    this.lblGroupError.Visible = true;
                }
            }
        }
    }
}