using Pedidos.Web.Security;
using System;

namespace Pedidos
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebSecurity.Validate();
        }
    }
}