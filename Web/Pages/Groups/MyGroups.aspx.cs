using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.DTO;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents
{
    public partial class MyGroups : SpecificCulturePage

    {
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                IIoCManager ioCManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IRecommendationGroupService recommendationGroupService = ioCManager.Resolve<IRecommendationGroupService>();

                HttpCookie cookie = Request.Cookies["loginName"];
                List<DTOGroupsUser> groupsUsers = recommendationGroupService.ShowUserGroups(cookie.Value);

                if (groupsUsers.Count == 0)
                {
                    lblNoGroups.Visible = true;
                    lblGroups.Visible = false;
                    return;
                }
                lblGroups.Visible = false;

                #region //Forma normal

                //this.gvGroups.DataSource = groupsUsers;
                //this.DataBind();

                #endregion //Forma normal

                #region //Forma opcional : Utilizando paquete Nuget: FastMember

                DataTable table = new DataTable();
                using (var reader = ObjectReader.Create(groupsUsers, "group_usersId", "gr_name"))
                {
                    table.Load(reader);
                }
                ViewState["groups"] = table;
                this.gvGroups.DataSource = (DataTable)ViewState["groups"]; ;
                this.DataBind();

                this.gvGroups.Columns[0].Visible = false;

                #endregion //Forma opcional : Utilizando paquete Nuget: FastMember
            }
        }

        /// <summary>Handles the RowDeleting event of the gvGroups control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewDeleteEventArgs" /> instance containing the event data.</param>
        protected void GvGroups_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblErrorOwner.Visible = false;
            lblSuccess.Visible = false;
            //TableCell cell = gvGroups.Rows[e.RowIndex].Cells[2];
            long groupId = Convert.ToInt64(gvGroups.DataKeys[e.RowIndex].Value.ToString());

            IIoCManager ioCManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IRecommendationGroupService recommendationGroupService = ioCManager.Resolve<IRecommendationGroupService>();
            HttpCookie cookie = Request.Cookies["loginName"];

            try { 
                recommendationGroupService.AbandonGroup(cookie.Value, groupId);
                DataTable dt = (DataTable)ViewState["groups"];
                dt.Rows[e.RowIndex].Delete();
                dt.AcceptChanges();
                ViewState["groups"] = dt;
                lblSuccess.Visible = true;
                gvGroups.DataSource = dt;
                gvGroups.DataBind();
            }
            catch (OwnerGroupAbandonException)
            {
                lblErrorOwner.Visible = true;
                e.Cancel = true;
                return;
            }


        }
    }
}