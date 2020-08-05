using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.DTO;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents
{
    public partial class ShowRecommendation : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager ioCManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IRecommendationGroupService recommendationGroupService = ioCManager.Resolve<IRecommendationGroupService>();

            HttpCookie cookie = Request.Cookies["loginName"];
            DataTable dataTable = new DataTable();

            HashSet<DTORecommendation> recommendations = recommendationGroupService.ShowUserRecommendations(cookie.Value);

            if (recommendations.Count == 0)
            {
                this.lblNorecommendations.Visible = true;
                this.lblRecommendations.Visible = false;
                return;
            }

            using (var reader = ObjectReader.Create(recommendations, "recommendationId", "eventId", "login_user", "eventName", "recommendation_text"))
            {
                dataTable.Load(reader);
            }

            this.gvRecommendations.DataSource = dataTable;
            this.DataBind();
            gvRecommendations.Columns[0].Visible = false;
            gvRecommendations.Columns[1].Visible = false;
        }
    }
}