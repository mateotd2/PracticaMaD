using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.DTO.DTOComment;
using Es.Udc.DotNet.PracticaMaD.Model.SportEventService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents
{
    public partial class ShowComments : SpecificCulturePage
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
                eventId = 0;
                lblNoComments.Visible = true;
                lblComments.Visible = false;
                return;
            }
            int count;

            IIoCManager ioCManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ISportEventService sportEventService = ioCManager.Resolve<ISportEventService>();

            count = Settings.Default.PracticaMaD_defaultCount;

            List<DTOComment> comments = sportEventService.FindComments(eventId, 0, count);

            if (comments.Count == 0)
            {
                lblNoComments.Visible = true;
                lblComments.Visible = false;
                return;
            }
            else
            {
                lblComments.Visible = true;
            }

            DataTable table = new DataTable();

            using (var reader = ObjectReader.Create(comments, "commentId", "loginName", "eventId", "comment_description", "publishDate"))
            {
                table.Load(reader);
            }

            ViewState["comments"] = table;
            this.gvComments.DataSource = table;
            this.DataBind();

            this.gvComments.Columns[0].Visible = false;
            this.gvComments.Columns[2].Visible = false;
        }

        /// <summary>Handles the Click event of the Btn control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Btn_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string commentId = gvComments.DataKeys[row.RowIndex].Value.ToString();
            string txtComment = row.Cells[4].Text;

            Session["commentId"] = Server.HtmlEncode(commentId.ToString());
            Session["txtComment"] = txtComment;

            Server.Transfer("/Pages/SportEvents/EditComment.aspx");
        }


        protected void GvComments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (SessionManager.IsUserAuthenticated(Context) &&
                e.Row.RowType == DataControlRowType.DataRow)
            {
                HttpCookie cookie = Request.Cookies["loginName"];

                GridViewRow row = e.Row;
                string authorComment = row.Cells[1].Text;
                

                DataTable comments = (DataTable)ViewState["comments"];
                var btn = (LinkButton)e.Row.FindControl("btnEdit");
                if (authorComment.Equals(cookie.Value))
                {
                    if (btn != null)
                    {
                        btn.Visible = true;
                    }
                }
                else
                {
                    btn.Visible = false;
                }
            }
            else
            {
                var btn = (LinkButton)e.Row.FindControl("btnEdit");
                if (btn != null)
                {
                    btn.Visible = false;
                }
            }
        }
    }
}