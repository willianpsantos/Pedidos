using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pedidos.Classes;
using Pedidos.Model;
using Pedidos.UserControls;
using Pedidos.DataAccess;
using Pedidos.Util;

namespace Pedidos.Cadastros.Produto
{
    public partial class Listagem : PageList<PRODUTOS, int>
    {
        protected override DataGrid Grid { get { return gridDados; } }

        protected override FilterBar FilterBar { get { return filteBarProduto; } }

        protected override string DeleteMessage { get { return "Produto excluido com sucesso!"; } }

        protected override string FormUrl { get { return "~/Cadastros/Produto/Form"; } }


        public Listagem()
        {
            Dao = new ProdutoDataAccess(new PedidosDataContext());
        }

        public override Lookup GetLookupRequested(string clientId)
        {
            return null;
        }

        protected override PageableData<PRODUTOS> GetData(int pageIndex, Filter filter, bool recount = false, params object[] parameters)
        {
            if (filter != null && (filter.FilterBy == "GrupoNome" || filter.FilterBy == "SubgrupoNome"))
            {
                int count = 0;
                var data = (Dao as ProdutoDataAccess).FilterByGrupoOuSubGrupo(pageIndex, filter.FilterBy, filter.Operator, filter.Value, out count);

                return new PageableData<PRODUTOS>()
                {
                    Data = data,
                    Count = count
                };
            }

            return base.GetData(pageIndex, filter, recount, parameters);
        }
    }
}