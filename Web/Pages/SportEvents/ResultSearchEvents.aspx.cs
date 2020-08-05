using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.DTO.DTOSportEvent;
using Es.Udc.DotNet.PracticaMaD.Model.SportEventService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents
{
    public partial class ResultSearchEvents : SpecificCulturePage

    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;

            string category, keywords;

            count = Settings.Default.PracticaMaD_defaultCount;
            //Obtain keywords

            try
            {
                keywords = Convert.ToString(Request.Params.Get("keywords"));
            }
            catch (ArgumentNullException)
            {
                keywords = "";
            }

            try
            {
                //Obtain category
                category = Convert.ToString(Request.Params.Get("category"));
            }
            catch (ArgumentNullException)
            {
                category = null;
            }

            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            IIoCManager ioCManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ISportEventService sportEventService =
                ioCManager.Resolve<ISportEventService>();
            SportEventBlock sportEventBlock;

            if (category != null)
            {
                sportEventBlock = sportEventService.FindEvents(keywords, startIndex, count, category);
            }
            else
            {
                sportEventBlock = sportEventService.FindEvents(keywords, startIndex, count);
            }

            if (sportEventBlock.result.Count == 0)
            {
                lblResultsEvents.Visible = false;
                lblNotCoincidenceEvents.Visible = true;
                return;
            }

            List<DTOSportEvent> sportEvents = sportEventBlock.result;

            this.gvEvents.DataSource = sportEvents;

            this.gvEvents.DataBind();
            gvEvents.Columns[0].Visible = false;

            // Previus link
            if ((startIndex - count) >= 0)
            {
                string url =
                    "/Pages/SportEvents/ResultSearchEvents.aspx" +
                    "?category=" + category + "&keywords=" + keywords +
                    "&startIndex=" + (startIndex - count);
                this.lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            if (sportEventBlock.existMoreSportEvents)
            {
                string url =
                     "/Pages/SportEvents/ResultSearchEvents.aspx" +
                    "?category=" + category + "&keywords=" + keywords +
                    "&startIndex=" + (startIndex + count);

                this.lnkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }

        protected void BtnFavorite_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string eventIdString = gvEvents.DataKeys[row.RowIndex].Value.ToString();
            //string eventIdString = row.Cells[0].Text.ToString();
            string eventName = row.Cells[1].Text;

            Session["eventName"] = eventName;
            Session["eventId"] = Server.HtmlEncode(eventIdString);

            if (SessionManager.IsUserAuthenticated(Context))
            {
                Server.Transfer("/Pages/SportEvents/AddToFavorites.aspx");
            }
            else
            {
                Response.Redirect("/Pages/User/Authentication.aspx");
            }
        }
    }
}