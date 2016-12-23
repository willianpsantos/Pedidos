using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace Pedidos
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/fontaweasome").Include("~/Content/font-awesome-4.1.0/css/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/bundles/css").Include("~/Content/kendo/styles/kendo.common.min.css")
                                                        .Include("~/Content/kendo/styles/kendo.bootstrap.min.css")                                                        
                                                        .Include("~/Content/css/site.css")
                                                        .Include("~/Content/css/temas.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/library/jquery/jquery-2.1.3.min.js")
                                                            .Include("~/Scripts/library/jquery/jquery.maskedinput.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/library").Include("~/Scripts/library/kendo/js/kendo.web.min.js")
                                                             .Include("~/Scripts/library/kendo/js/cultures/kendo.culture.pt-BR.min.js")
                                                             .Include("~/Scripts/library/kendo/js/messages/kendo.messages.pt-BR.min.js")                                                             
                                                             .Include("~/Scripts/library/date.editor.js")
                                                             .Include("~/Scripts/library/messagebox.js"));

            bundles.Add(new ScriptBundle("~/bundles/default").Include("~/Scripts/applib.js")
                                                             .Include("~/Scripts/lookup.modal.manager.js")
                                                             .Include("~/Scripts/index.js"));
        }
    }
}