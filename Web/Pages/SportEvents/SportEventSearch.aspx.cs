using Castle.Core.Internal;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.SportEventService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Data;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.SportEvents
{
    public partial class SportEventSearch : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

                ISportEventService sportEventService = ioCManager.Resolve<ISportEventService>();

                List<string> categories = sportEventService.GetCategories();

                UpdateComboCategories(categories);
            }
        }

        private void UpdateComboCategories(List<string> categories)
        {
            DataTable dt = new DataTable();

            string valueDefault = this.categoriesList.SelectedItem.Value;
            string txtDefault = this.categoriesList.SelectedItem.Text;

            dt.Columns.Add(new DataColumn("categoryTextField", typeof(string)));
            dt.Columns.Add(new DataColumn("categoryValueField", typeof(string)));

            dt.Rows.Add(CreateRow(txtDefault, valueDefault, dt));

            foreach (string category in categories)
            {
                dt.Rows.Add(CreateRow(category, category, dt));
            }

            DataView dv = new DataView(dt);

            categoriesList.DataSource = dt;
            categoriesList.DataTextField = "categoryTextField";
            categoriesList.DataValueField = "categoryValueField";

            categoriesList.DataBind();
            //categoriesList.SelectedIndex = 0;
        }

        private DataRow CreateRow(string text, string value, DataTable dt)
        {
            DataRow dr = dt.NewRow();

            dr[0] = text;
            dr[1] = value;

            return dr;
        }

        protected void SearchEventsClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string category = this.categoriesList.SelectedItem.Value;

                string keywords = searchInput.Text;

                string url;

                if (category.IsNullOrEmpty())
                {
                    if (keywords.IsNullOrEmpty())
                    {
                        url = String.Format("./ResultSearchEvents.aspx");
                    }
                    else
                    {
                        url = String.Format("./ResultSearchEvents.aspx?" +
                               "keywords={0}", keywords);
                    }
                }
                else
                {
                    if (keywords.IsNullOrEmpty())
                    {
                        url = String.Format("./ResultSearchEvents.aspx?" +
                            "category={0}", category);
                    }
                    else
                    {
                        url = String.Format("./ResultSearchEvents.aspx?" +
                            "category={0}&keywords={1}", category, keywords);
                    }
                }

                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }
    }
}