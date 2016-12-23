using System;
using System.Web.UI;
using Newtonsoft.Json;
using Pedidos.Classes;
using System.IO;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security;

namespace Pedidos.UserControls
{
    public partial class MainMenu : System.Web.UI.UserControl
    {   
        [Browsable(true)]
        [UrlProperty]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string MenuFileConfig { get; set; } = "~/menu.json";

        protected string MenuCssClass { get; set; }

        protected string MenuJsFile { get; set; }

        protected string MenuItemCssClass { get; set; }

        protected MainMenuItem[] Items { get; set; }

        protected JsonSerializerSettings Settings
        {
            get
            {
                return new JsonSerializerSettings()
                {   
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                };
            }
        }
        

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Visible = true;

            if (Page.IsLookupRequest())
            {
                this.Visible = false;
                this.Attributes.Add("style", "width: 0px;");
                return;
            }

            var config = GetConfig();

            if (config == null || config.Items == null || config.Items.Length == 0)
                return;

            string html = string.Empty;
            this.MenuItemCssClass = config.MenuItemCssClass;
            this.MenuCssClass = config.MenuCssClass;
            this.MenuJsFile = config.MenuJsFile;
            this.Items = config.Items;
        }

        protected MainMenuConfig GetConfig()
        {
            var text = File.ReadAllText(Page.Server.MapPath(MenuFileConfig));
            var list = JsonConvert.DeserializeObject<MainMenuConfig>(text, Settings);
            return list;
        }
    }
}