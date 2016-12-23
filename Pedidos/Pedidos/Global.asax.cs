using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;

namespace Pedidos
{
    public class Global : HttpApplication
    {
        public const String QUERYSTRINGID = "id";
        public const string QUERYSTRINGMODE = "mode";
        public const string QUERYSTRINGLOOKUP = "lookup";

        public const string COMMANDINSERT = "insert";
        public const string COMMANDEDIT = "edit";
        public const string COMMANDDELETE = "delete";
        public const string COMMANDVIEW = "view";
        public const string COMMANDSELECT = "select";

        public const int PAGESIZE = 50;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}