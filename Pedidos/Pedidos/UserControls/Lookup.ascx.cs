using Pedidos.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Security;

namespace Pedidos.UserControls
{
    public enum LookupType
    {
        Client,
        Server
    }

    public delegate void LookupFilterMethod(string field, string operation, string value);

    public partial class Lookup : System.Web.UI.UserControl
    {
        [Browsable(true)]
        [UrlProperty]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string ServiceUrl { get; set; }

        [Browsable(true)]
        [UrlProperty]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string PageUrl { get; set; }

        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Unit Width
        {
            get { return InternalDropDownList.Width; }
            set { InternalDropDownList.Width = value; }
        }

        [Browsable(false)]
        public object DataSource
        {
            get { return InternalDropDownList.DataSource; }
            set { InternalDropDownList.DataSource = value; }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<BoundColumn> Columns { get; set; }

        [Browsable(false)]
        public string JsonColumns
        {
            get { return this.ColumnsToJson(); }
        }
                
        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public LookupType LookupType { get; set; }

        [Browsable(false)]
        public string Uid { get; private set; }

        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string DataValueField
        {
            get { return InternalDropDownList.DataValueField; }
            set { InternalDropDownList.DataValueField = value; }
        }

        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string DataTextField
        {
            get { return InternalDropDownList.DataTextField; }
            set { InternalDropDownList.DataTextField = value; }
        }

        [Browsable(false)]
        public ListItemCollection Items
        {
            get { return InternalDropDownList.Items; }
        }

        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool AutoPostBack
        {
            get { return InternalDropDownList.AutoPostBack; }
            set { InternalDropDownList.AutoPostBack = value; }
        }
        
        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [Editor("System.Web.UI.Design.WebControls.TypeEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string ItemType
        {
            get { return InternalDropDownList.ItemType; }
            set { InternalDropDownList.ItemType = value; }
        }

        [Browsable(false)]
        public LookupFilterMethod FilterMethod { get; set; }

        [Browsable(false)]
        public string DefaultCssClass
        {
            get { return "k-dropdown"; }
        }

        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        public string CssClass
        {
            get; set;
        }        

        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        public bool Enabled
        {
            get { return InternalDropDownList.Enabled; }
            set { InternalDropDownList.Enabled = value; }
        }

        public string CompletedCssClass
        {
            get { return $"{DefaultCssClass} {CssClass}"; }
        }

        [Browsable(false)]
        public string LastAppliedFilter { get; set; }

        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        public bool Required
        {
            get;set;
        }

        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        public string EmptyItemText { get; set; } = "-- Selecione --";

        public int SelectedIndex
        {
            get { return InternalDropDownList.SelectedIndex; }
            set { InternalDropDownList.SelectedIndex = value; }
        }

        public string SelectedValue
        {
            get { return InternalDropDownList.SelectedValue; }
            set { InternalDropDownList.SelectedValue = value; }
        }

        public ListItem SelectedItem
        {
            get { return InternalDropDownList.SelectedItem; }
        }

        public event EventHandler SelectedIndexChanged;


        public Lookup()
        {
            var guid = Guid.NewGuid();
            Uid = guid.ToString();
            LookupType = LookupType.Server;
        }
        
                
        protected void Page_Load(object sender, EventArgs e)
        {
            InternalDropDownList.CssClass = CompletedCssClass;
        }


        private string ColumnsToJson()
        {
            if (this.Columns == null)
                return "[]";

            if (this.Columns.Count == 0)
                return "[]";

            var json = "[";

            foreach (BoundColumn item in this.Columns)
            {
                json += $"{{ field: '{item.DataField}', title: '{item.HeaderText}' ";

                if (item.ItemStyle.Width != default(Unit))
                {
                    json += $", width: {item.ItemStyle.Width.ToString()}";
                }

                if (!string.IsNullOrEmpty(item.DataFormatString))
                {
                    json += $", template: '<center> #= window.kendo.toString({item.DataField}, \"{item.DataFormatString}\") #</center>' ";
                }

                json += " },";
            }

            json = json.Remove(json.LastIndexOf(","), 1);
            json += "]";
            return json;
        }

        public override void DataBind()
        {
            InternalDropDownList.DataBind();

            if (!this.Required)
            {
                ListItem emptylist = new ListItem(EmptyItemText, "0");
                InternalDropDownList.Items.Insert(0, emptylist);
            }
        }

        public void Filter(string field, string operation, string value)
        {
            FilterMethod?.Invoke(field, operation, value);
            this.OnSelectedIndexChanged();
        }

        public virtual void OnSelectedIndexChanged()
        {
            this.SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
        }

        protected void InternalDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnSelectedIndexChanged();
        }
    }
}