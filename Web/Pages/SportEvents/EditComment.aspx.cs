using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.SportEventService;
using System;
using System.Web;
using System.Web.UI;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents
{
    public partial class EditComment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string commentId = Session["commentId"].ToString();
                string txtComment = Session["txtComment"].ToString();

                if (commentId == null || txtComment == null)
                {
                    this.lblError.Visible = true;
                }

                this.txtComment.InnerText = txtComment;
            }
        }

        /// <summary>Handles the Click event of the btnEditComment control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void BtnEditComment_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string commentId = Session["commentId"].ToString();
                string txtComment = this.txtComment.Value;

                IIoCManager ioCManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ISportEventService sportEventService = ioCManager.Resolve<ISportEventService>();
                long commentIdfinal = Convert.ToInt64(commentId);
                try
                {
                    sportEventService.UpdateComment(commentIdfinal, txtComment);
                    this.lblSuccess.Visible = true;
                    this.btnEditComment.Visible = false;
                }
                catch (Exception)
                {
                    this.lblError.Text += txtComment;
                    this.lblError.Visible = true;
                    return;
                }
                
            }
        }
    }
}