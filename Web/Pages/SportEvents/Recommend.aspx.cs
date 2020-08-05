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
    public partial class Recommend : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                long eventId;
                try
                {
                    eventId = Convert.ToInt64(Request.Params.Get("eventId"));
                }
                catch (ArgumentNullException)
                {
                    //Hacer un redirect a los grupos
                    eventId = 0;
                }

                if (eventId != 0)
                {
                    ViewState["eventId"] = eventId;
                    IIoCManager ioCManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                    IRecommendationGroupService recommendationGroupService = ioCManager.Resolve<IRecommendationGroupService>();

                    HttpCookie cookie = Request.Cookies["loginName"];
                    List<DTOGroupsUser> groupsUsers = recommendationGroupService.ShowUserGroups(cookie.Value);

                    if (groupsUsers.Count == 0)
                    {
                        this.lblEvents.Visible = false;
                        lclRecommendation.Visible = false;
                        txtRecommend.Visible = false;
                        lblNoGroupsToRecommend.Visible = true;
                        lblSelectCheckBox.Visible = false;
                        this.btnCreateRecommendation.Visible = false;
                        return;
                    }

                    DataTable table = new DataTable();
                    using (var reader = ObjectReader.Create(groupsUsers, "group_usersId", "gr_name"))
                    {
                        table.Load(reader);
                    }
                    ViewState["groupsRecommend"] = table;
                    this.gvGroups.DataSource = (DataTable)ViewState["groupsRecommend"];
                    this.DataBind();
                    this.gvGroups.Columns[0].Visible = false;
                }
                else
                {
                    lblNoeventId.Visible = true;
                    txtRecommend.Visible = false;
                    btnCreateRecommendation.Visible = false;
                }
            }
        }

        protected void BtnCreateRecommendation_Click(object sender, EventArgs e)
        {
            this.lblError.Visible = false;
            this.lblSuccess.Visible = false;
            this.lblNoGroupsSelected.Visible = false;

            IIoCManager ioCManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IRecommendationGroupService recommendationGroupService = ioCManager.Resolve<IRecommendationGroupService>();
            List<long> groupsChecked = new List<long>();

            foreach (GridViewRow row in gvGroups.Rows)
            {
                long groupId = Convert.ToInt64(gvGroups.DataKeys[row.RowIndex].Value.ToString());

                var chk = row.FindControl("chkGroup") as CheckBox;
                if (chk != null && chk.Checked)
                {
                    groupsChecked.Add(groupId);
                }
            }

            if (groupsChecked.Count != 0)
            {
                try
                {
                    long eventId = (long)ViewState["eventId"];
                    HttpCookie cookie = Request.Cookies["loginName"];
                    recommendationGroupService.AddRecommendation(cookie.Value, eventId, groupsChecked, this.txtRecommend.Value);
                    
                }
                catch (Exception)
                {
                    lblError.Visible = true;
                }
                this.lblSuccess.Visible = true;
            }
            else
            {
                this.lblNoGroupsSelected.Visible = true;
            }
        }
    }
}