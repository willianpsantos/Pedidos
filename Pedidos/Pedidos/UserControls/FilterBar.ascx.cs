using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
using Pedidos.Classes;
using Pedidos.Util;
using System.Security;

namespace Pedidos.UserControls
{

    public delegate void FilterBarSearchEventHandler(object sender, FilterBarSearchEventArgs e);
        
    public partial class FilterBar : System.Web.UI.UserControl
    {
        [DefaultValue(null)]
        [Editor("System.Web.UI.Design.WebControls.ListItemsCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ListItemCollection FilterByItems { get; set; }

        [DefaultValue(null)]
        [Editor("System.Web.UI.Design.WebControls.ListItemsCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ListItemCollection OperatorItems { get; set; }

        [DefaultValue(null)]
        [Editor("System.Web.UI.Design.WebControls.TypeEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public Type ItemType { get; set; }

        public event FilterBarSearchEventHandler Search;

        public event EventHandler Clear;
                

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFilterByItems(this.FilterByItems);

                if (this.OperatorItems == null || this.OperatorItems.Count == 0)
                    this.OperatorItems = DefaultOperators();

                LoadOperatorItems(this.OperatorItems);
            }
        }


        protected virtual ListItemCollection DefaultOperators()
        {
            var items = new ListItemCollection();

            items.Add(new ListItem("Igual", "=="));
            items.Add(new ListItem("Diferente de", "!="));
            items.Add(new ListItem("Maior que", ">"));
            items.Add(new ListItem("Menor que", "<"));
            items.Add(new ListItem("Maior ou igual que", ">="));
            items.Add(new ListItem("Menor ou igual que", "<="));
            items.Add(new ListItem("Começa com", "startswith"));
            items.Add(new ListItem("Termina com", "endswith"));
            items.Add(new ListItem("Contem", "contains"));

            return items;
        }

        protected virtual void LoadFilterByItems(ListItemCollection items)
        {
            ddlFilterBy.Items.Clear();

            if (items == null)
                return;

            var enumerator = items.GetEnumerator();

            while (enumerator.MoveNext())
                ddlFilterBy.Items.Add(enumerator.Current as ListItem);            
        }

        protected virtual void LoadOperatorItems(ListItemCollection items)
        {
            ddlOperator.Items.Clear();

            if (items == null)
                return;

            var enumerator = items.GetEnumerator();

            while (enumerator.MoveNext())
                ddlOperator.Items.Add(enumerator.Current as ListItem);
        }

        protected string GetFilterBy()
        {
            var selected = ddlFilterBy.SelectedItem;

            if (selected == null)
                return string.Empty;

            return selected.Value;
        }

        protected string GetOperator()
        {
            var selected = ddlOperator.SelectedItem;

            if (selected == null)
                return string.Empty;

            return selected.Value;
        }

        protected string GetValue()
        {
            var selected = txtValue.Text;

            if (selected == null)
                return string.Empty;

            return selected;
        }
        
        public void ExecuteSearch()
        {
            var filterBy = GetFilterBy();

            var filter = new Filter()
            {                
                FilterBy = filterBy,
                Operator = GetOperator(),
                Value = GetValue(),
                ItemType = ItemType.GetPropertyType(filterBy)
            };

            OnSearch(new FilterBarSearchEventArgs() { Filter = filter });
        }

        public void ExecuteClear()
        {
            OnClear(EventArgs.Empty);
        }


        protected void OnSearch(FilterBarSearchEventArgs e)
        {
            Search?.Invoke(this, e);
        }

        protected void OnClear(EventArgs e)
        {
            Clear?.Invoke(this, e);
        }

        protected void buttonSearch_ServerClick(object sender, EventArgs e)
        {
            if (this.Search == null)
                return;

            var filterBy = GetFilterBy();

            var filter = new Filter()
            {                
                FilterBy = filterBy,
                Operator = GetOperator(),
                Value = GetValue(),
                ItemType = ItemType.GetPropertyType(filterBy)
            };

            OnSearch(new FilterBarSearchEventArgs() { Filter = filter });
        }

        protected void buttonClear_ServerClick(object sender, EventArgs e)
        {
            ddlFilterBy.SelectedIndex = 0;
            ddlOperator.SelectedIndex = 0;
            txtValue.Text = "";
            OnClear(EventArgs.Empty);
        }
    }
}