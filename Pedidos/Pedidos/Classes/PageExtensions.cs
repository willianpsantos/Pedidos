using System.Web.UI;
using System.Linq;
using Pedidos.DataAccess;
using Pedidos.Util;
using System;

namespace Pedidos.Classes
{
    public static class PageExtensions
    {
        public static void SendMessage(this Page page, string message, string title = "Mensagem", MessageBoxIcon icon = MessageBoxIcon.SUCCESS, string handler = "")
        {
            var script = $"window.MessageBox.alert('{title}', '{message}', {{ icon: window.MessageBox.{icon.ToString()}, height: 120 ";

            if (!String.IsNullOrEmpty(handler) && !String.IsNullOrWhiteSpace(handler))
            {
                script += $", handler: function(e) {{ {handler} }} ";
            }

            script += " }); ";

            page.ClientScript.RegisterStartupScript(page.GetType(), "message", script, true);
        }

        public static void SendErrorMessage(this Page page, string title = "Erro Interno no Servidor")
        {
            var script = $"window.MessageBox.alert('{title}', 'Houve um erro interno ao tentar salvar o cliente.Por favor, tente mais tarde', {{ icon: window.MessageBox.{MessageBoxIcon.ERROR.ToString()}, height: 120 }}); ";

            page.ClientScript.RegisterStartupScript(page.GetType(), "error", script, true);
        }

        public static bool IsLookupRequest(this Page page)
        {
            var lookup = false;

            if (page.Request.QueryString.AllKeys.Contains(Global.QUERYSTRINGLOOKUP))
                lookup = !string.IsNullOrEmpty(page.Request.QueryString[Global.QUERYSTRINGLOOKUP]);

            return lookup;
        }

        public static bool IsLookupRequest(this MasterPage page)
        {
            var lookup = false;

            if (page.Request.QueryString.AllKeys.Contains(Global.QUERYSTRINGLOOKUP))
                lookup = !string.IsNullOrEmpty(page.Request.QueryString[Global.QUERYSTRINGLOOKUP]);

            return lookup;
        }

        public static bool IsLookupControlRequest(this Page page)
        {
            var target = page.Request.Params[Constantes.EventTargetParam];

            if (string.IsNullOrEmpty(target))
                return false;

            if (!target.Contains(Constantes.LookupControlIdentifier))
                return false;

            return true;
        }

        public static bool IsLookupControlClearRequest(this Page page)
        {
            var target = page.Request.Params[Constantes.EventTargetParam];

            if (string.IsNullOrEmpty(target))
                return false;

            if (!target.Contains(Constantes.LookupControlIdentifier))
                return false;

            var param = page.Request.Params[Constantes.EventArgumentParam];
            return param == "clear";
        }

        public static void FilterLookup(this Page page, Pedidos.UserControls.Lookup control)
        {            
            control.Items.Clear();
            control.SelectedIndex = -1;
            var reqParams = page.Request.Params[Constantes.EventArgumentParam];

            if (string.IsNullOrEmpty(reqParams))
            {               
                control.DataSource = null;
                control.DataBind();
                return;
            }

            var args = reqParams.Split(Constantes.ArgumentSeparator);            
            var field = args[0].Replace("field=", string.Empty);
            var operation = args[1].Replace("operation=", string.Empty);
            var value = args[2].Replace("value=", string.Empty);

            control.Filter(field, operation, value);
        }

        public static void ClearLookup(this Page page, Pedidos.UserControls.Lookup control)
        {
            control.Items.Clear();
            LoadLookupFirstTime(page, control);
        }

        public static void LoadLookupFirstTime(this Page page, Pedidos.UserControls.Lookup lookup)
        {
            lookup?.Filter(lookup.DataTextField, "contains", string.Empty);
        }

        public static Filter GetLookupLastFilter(this Page page, Type entityType)
        {
            if (!page.Request.QueryString.HasKeys())
                return null;

            string field = string.Empty;
            string operation = string.Empty;
            string value = string.Empty;

            if (page.Request.QueryString.HasKey("field"))
            {
                field = page.Request.QueryString["field"];
            }

            if (page.Request.QueryString.HasKey("operation"))
            {
                operation = page.Request.QueryString["operation"];
            }

            if (page.Request.QueryString.HasKey("value"))
            {
                value = page.Request.QueryString["value"];
            }

            return new Filter()
            {
                FilterBy = field,
                Operator = operation,
                Value = value,
                ItemType = entityType.GetPropertyType(field)
            };
        }
    }
}
