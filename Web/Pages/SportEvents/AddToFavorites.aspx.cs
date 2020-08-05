using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.SportEventService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Web;
using System.Web.UI;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents
{
    public partial class AddToFavorites : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string eventName = Session["eventName"].ToString();

                if (eventName == null)
                {
                    this.lblError.Visible = true;
                    return;
                }

                this.favoriteName.Text = eventName;
            }
        }

        /// <summary>Handles the Click event of the btnAddFavorite control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void BtnAddFavorite_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string eventIdString = Session["eventId"].ToString();
                if (eventIdString == null)
                {
                    this.lblError.Visible = true;
                    return;
                }

                IIoCManager ioCManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ISportEventService sportEventService = ioCManager.Resolve<ISportEventService>();

                long eventId = Convert.ToInt64(eventIdString);
                string favoriteName = this.favoriteName.Text;
                string favoriteDesc = this.txtFavorite.Value;
                HttpCookie cookie = Request.Cookies["loginName"];
                try
                {
                    sportEventService.AddToFavorites(cookie.Value, eventId, favoriteName, favoriteDesc);
                    this.btnAddFavorite.Visible = false;
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