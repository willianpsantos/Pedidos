using System;
using System.Web.UI;
using Pedidos.Classes;
using System.Security;

namespace Pedidos.UserControls
{
    public partial class PageHeader : System.Web.UI.UserControl
    {
        public string Text { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.IsLookupRequest())
                {
                    this.Visible = false;
                    this.Attributes.Add("style", "height: 0px;");
                }                
            }
        }
    }
}