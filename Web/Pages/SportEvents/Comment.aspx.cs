using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.SportEventService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Web;
using System.Web.UI;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents
{
    public partial class Comment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
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
                string url = ("./ShowGroups.aspx");
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }

            ViewState["eventId"] = eventId;
        }

        /// <summary>Handles the Click event of the BtnCreateComment control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void BtnCreateComment_Click(object sender, EventArgs e)
        {
            this.lblSuccess.Visible = false;
            this.lblError.Visible = false;
            if (Page.IsValid)
            {
                long eventId = (long)ViewState["eventId"];

                string comment = txtComment.Value;

                IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];
                ISportEventService sportEventService = ioCManager.Resolve<ISportEventService>();
                HttpCookie cookie = Request.Cookies["loginName"];
                try
                {
                    //long eventId = Convert.ToInt64(Request.Params.Get("sportEventId"));
                    sportEventService.AddComment(cookie.Value, eventId, comment);
                    this.btnCreateComment.Visible = false;
                    this.lblSuccess.Visible = true;
                }
                catch (Exception)
                {
                    this.lblError.Visible = true;
                }
            }
        }
    }
}