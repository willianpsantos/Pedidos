using System;
using System.Web.UI;
using Pedidos.Classes;
using System.ComponentModel;
using System.Security;

namespace Pedidos.UserControls
{
    public partial class PageTitle : System.Web.UI.UserControl
    {
        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        public string Text { get; set; }

        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        public string IconCssClass { get; set; } = "fa fa-bookmark";

        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        public bool ShowButtonAdd { get; set; } = false;

        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        public event ImageClickEventHandler ButtonAddClick;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            btnAdd.Visible = this.ShowButtonAdd;

            if (!IsPostBack)
            {
                if (Page.IsLookupRequest())
                {
                    this.Visible = false;
                    this.Attributes.Add("style", "height:0px;");
                }
            }
        }
                

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            ButtonAddClick?.Invoke(this, e);
        }
    }
}