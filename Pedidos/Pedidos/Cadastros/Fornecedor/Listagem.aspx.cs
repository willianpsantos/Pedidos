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

namespace Pedidos.Cadastros.Fornecedor
{
    public partial class Listagem : PageList<FORNECEDOR, int>
    {
        protected override string PropertyId
        {
            get
            {
                return "FORCODG";
            }
        }

        protected override DataGrid Grid { get { return gridDados; } }

        protected override FilterBar FilterBar { get { return this.filteBarFornecedor; } }

        protected override string DeleteMessage { get { return "Fornecedor excluido com sucesso!"; } }

        protected override string FormUrl { get { return "~/Cadastros/Fornecedor/Form"; } }


        public Listagem()
        {
            Dao = new FornecedorDataAccess(new PedidosDataContext());
        }


        public override Lookup GetLookupRequested(string clientId)
        {
            return null;
        }
    }
}