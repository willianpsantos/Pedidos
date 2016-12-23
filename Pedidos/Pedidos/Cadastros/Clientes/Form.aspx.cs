using Pedidos.DataAccess;
using Pedidos.Model;
using System;
using System.Web.ModelBinding;
using Pedidos.Classes;
using System.Web.UI.WebControls;
using Pedidos.UserControls;

namespace Pedidos.Cadastros.Clientes
{
    public partial class Form : PageForm<CLIENTES, int>     
    {
        protected override string ListUrl { get { return "~/Cadastros/Clientes/Listagem"; } }

        protected override string InsertMessage { get { return "Cliente inserido com sucesso!" ; } }

        protected override string UpdateMessage { get { return "Cliente atualizado com sucesso!"; } }

        protected override string EntityNotFoundMessage { get { return "Cliente não encontrado!"; } }

        protected override FormView FormView { get { return frmCadastroCliente; } }

        protected override Button SaveButton { get { return btnSalvar; } }
        

        public Form()
        {
            Dao = new ClienteDataAccess(new PedidosDataContext());
        }

        public override Lookup GetLookupRequested(string clientId)
        {
            return null;
        }
    }
}