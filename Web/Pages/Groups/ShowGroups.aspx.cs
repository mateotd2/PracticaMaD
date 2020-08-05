using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.DTO;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents
{
    public partial class ShowGroups : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            IIoCManager ioCManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IRecommendationGroupService recommendationGroupService = ioCManager.Resolve<IRecommendationGroupService>();
            List<DTOGroups> groups = recommendationGroupService.ShowAllGroups();

            if (groups.Count == 0)
            {
                this.lblNoGroups.Visible = true;
                return;
            }
            else
            {
                this.lblGroups.Visible = true;
            }

            #region //Forma opcional : Utilizando paquete Nuget: FastMember

            DataTable table = new DataTable();
            using (var reader = ObjectReader.Create(groups, "group_usersId", "gr_name", "gr_description", "gr_amount_users", "gr_amount_recommendation"))
            {
                table.Load(reader);
            }

            #endregion //Forma opcional : Utilizando paquete Nuget: FastMember

            this.gvAllGroups.DataSource = table;
            this.DataBind();

            this.gvAllGroups.Columns[0].Visible = false;
        }

        /// <summary>Handles the RowDataBound event of the gvAllGroups control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs" /> instance containing the event data.</param>
        protected void GvAllGroups_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (SessionManager.IsUserAuthenticated(Context) &&
                e.Row.RowType == DataControlRowType.DataRow)
            {
                HttpCookie cookie = Request.Cookies["loginName"];
                IIoCManager ioCManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IRecommendationGroupService recommendationGroupService = ioCManager.Resolve<IRecommendationGroupService>();
                //Quiza despues en vez de DTOs, añado al viewState todos los ids.
                List<DTOGroupsUser> groupsFromUser = recommendationGroupService.ShowUserGroups(cookie.Value);

                long idRow = Convert.ToInt64(gvAllGroups.DataKeys[e.Row.RowIndex].Values[0].ToString());
                bool coincidence = false;
                //this.lblError.Text += "RowDataBound id= " + idRow;
                //this.lblError.Visible = true;
                foreach (DTOGroupsUser group in groupsFromUser)
                {
                    if (idRow == group.group_usersId)
                    {
                        coincidence = true;
                        break;
                    }
                }

                if (coincidence)
                {
                    var lbl = (Label)e.Row.FindControl("lblSubscribed");
                    var lnk = (HyperLink)e.Row.FindControl("lnkSubscribeAuth");
                    if (lbl != null)
                    {
                        //lbl.Text = "Already subscribed";
                        lbl.Visible = true;
                    }
                    if (lnk != null)
                    {
                        lnk.Visible = false;
                    }
                }
                else
                {
                    var lnk = (HyperLink)e.Row.FindControl("lnkSubscribeAuth");
                    if (lnk != null)
                    {
                        lnk.Visible = false;
                    }
                    var lnkAuthUser = (LinkButton)e.Row.FindControl("lnkSubscribeUser");
                    if (lnkAuthUser != null)
                    {
                        lnkAuthUser.Visible = true;
                    }
                }
            }
        }

        protected void LnkSubscribeUser_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            lblSubscribed.Visible = false;
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            long groupId = Convert.ToInt64(gvAllGroups.DataKeys[row.RowIndex].Value.ToString());

            HttpCookie cookie = Request.Cookies["loginName"];
            IIoCManager ioCManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IRecommendationGroupService recommendationGroupService = ioCManager.Resolve<IRecommendationGroupService>();

            try
            {
                recommendationGroupService.AddUserToGroup(cookie.Value, groupId);
                var lnk = (LinkButton)row.FindControl("lnkSubscribeUser");

                if (lnk != null)
                {
                    lnk.Visible = false;
                }

                var lbl = (Label)row.FindControl("lblSubscribed");
                if (lbl != null)
                {
                    lbl.Visible = true;
                }
                this.lblSubscribed.Visible = true;
            }
            catch (Exception)
            {
                lblError.Text = "Something go wrong";
                lblError.Visible = true;
            }
        }
    }
}