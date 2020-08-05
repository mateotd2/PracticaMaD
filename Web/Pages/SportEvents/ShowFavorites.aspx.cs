using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.DTO.DTOFavorite;
using Es.Udc.DotNet.PracticaMaD.Model.SportEventService;
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
    public partial class ShowFavorites : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                HttpCookie cookie = Request.Cookies["loginName"];

                IIoCManager ioCManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ISportEventService sportEventService = ioCManager.Resolve<ISportEventService>();

                List<DTOFavorite> dtoFavorites = sportEventService.ListFavorites(cookie.Value);
                DataTable dataTable = new DataTable();

                if (dtoFavorites.Count == 0)
                {
                    this.lblNoFavorites.Visible = true;
                    this.lblFavorites.Visible = false;
                    return;
                }

                using (var reader = ObjectReader.Create(dtoFavorites, "eventId", "fv_name", "fv_date", "comment"))
                {
                    dataTable.Load(reader);
                }

                ViewState["favorites"] = dataTable;

                this.gvFavorites.DataSource = dataTable;
                this.DataBind();
                this.gvFavorites.Columns[0].Visible = false;
            }
        }

        /// <summary>Handles the RowDeleting event of the GvFavorites control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewDeleteEventArgs" /> instance containing the event data.</param>
        protected void GvFavorites_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.lblError.Visible = false;
            this.lblSuccess.Visible = false;

            long eventId = Convert.ToInt64(gvFavorites.DataKeys[e.RowIndex].Value.ToString());

            IIoCManager ioCManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ISportEventService sportEventService = ioCManager.Resolve<ISportEventService>();
            HttpCookie cookie = Request.Cookies["loginName"];
            try
            {
                sportEventService.DeleteFromFavorites(cookie.Value, eventId);
            }
            catch (Exception)
            {
                this.lblError.Visible = true;
                e.Cancel = true;
                return;
            }

            DataTable dt = (DataTable)ViewState["favorites"];
            dt.Rows[e.RowIndex].Delete();
            if (dt.Rows.Count == 0)
            {
                this.lblNoFavorites.Visible = true;
                this.lblFavorites.Visible = false;
            }
            ViewState["favorites"] = dt;
            lblSuccess.Visible = true;
            gvFavorites.DataSource = dt;
            gvFavorites.DataBind();
        }
    }
}