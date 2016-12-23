using Pedidos.DataAccess;
using Pedidos.Model;
using System;
using Pedidos.Classes;
using System.Web.UI.WebControls;
using Pedidos.UserControls;

namespace Pedidos.Cadastros.Clientes
{
    public partial class Listagem : PageList<CLIENTES, int>
    {
        protected override string PropertyId
        {
            get
            {
                return "CLICODIGO";
            }
        }

        protected override DataGrid Grid { get { return gridDados; } }
        
        protected override FilterBar FilterBar { get { return this.filteBarCliente; } }

        protected override string DeleteMessage { get { return "Cliente excluido com sucesso!"; } }

        protected override string FormUrl { get { return "~/Cadastros/Clientes/Form"; } }


        public Listagem()
        {
            Dao = new ClienteDataAccess(new PedidosDataContext());
        }


        public override Lookup GetLookupRequested(string clientId)
        {
            return null;
        }
    }
}