using Pedidos.DataAccess;
using Pedidos.Model;
using System;
using Pedidos.Classes;
using System.Web.UI.WebControls;
using Pedidos.UserControls;
using Pedidos.Util;
using Pedidos.Model.Views;

namespace Pedidos.Cadastros.Pedidos
{
    public partial class Listagem : PageList<PEDIDOS, int>
    {
        protected override DataGrid Grid { get { return gridDados; } }

        protected override FilterBar FilterBar { get { return filteBarPedidos; } }
        
        protected override string DeleteMessage { get { return "Pedidos excluido com sucesso!"; } }

        protected override string FormUrl { get { return "~/Cadastros/Pedidos/Form"; } }


        public Listagem()
        {
            Dao = new PedidosDataAccess(new PedidosDataContext());
        }

        public override Lookup GetLookupRequested(string clientId)
        {
            return null;
        }

        protected override PageableData<PEDIDOS> GetData(int pageIndex, Filter filter, bool recount = false, params object[] parameters)
        {   
            if (filter != null)
            {
                int count = 0;
                PEDIDOS[] data = null;

                switch(filter.FilterBy)
                {
                    case "ClienteNome":
                        data = (Dao as PedidosDataAccess).FilterByClienteNome(filter.Operator, filter.Value, pageIndex, out count);
                        break;
                    case "PEDDATAEMISSAO":
                    case "PEDDATADEVOLUCAO":
                    case "PEDDATASAIDA":
                    case "PEDDATALOCACAO":
                        data = (Dao as PedidosDataAccess).FilterByDatas(filter.FilterBy, filter.Operator, filter.Value, pageIndex, out count);
                        break;
                    default:
                        return base.GetData(pageIndex, filter, recount, parameters);                        
                }

                return new PageableData<PEDIDOS>()
                {
                    Data = data,
                    Count = count
                };
            }

            return base.GetData(pageIndex, filter, recount, parameters);
        }

        protected void gridDados_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header ||
                 e.Item.ItemType == ListItemType.Footer ||
                   e.Item.ItemType == ListItemType.Pager ||
                     e.Item.ItemType == ListItemType.Separator)
                return;

            ImageButton img = e.Item.FindControl("btnPrint") as ImageButton;
            int id = Convert.ToInt32(e.Item.Cells[1].Text);
            string url = ResolveUrl($"~/Reports/RptPedidos.aspx?id={id}");
            img.OnClientClick = $" window.open('{url}', '_blank'); ";
        }
    }
}