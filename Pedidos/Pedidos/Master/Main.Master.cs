using System;
using Pedidos.Classes;

namespace Pedidos
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsLookupRequest())
            {
                mainFooter.Visible = false;
                mainFooter.Attributes.Add("style", "height:0px;");
                sectionContent.Attributes.Add("style", "margin-top:0px;");
            }
        }
    }
}