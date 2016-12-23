using Pedidos.DataAccess;
using Pedidos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pedidos.Services
{
    public class FornecedorController : LookupController<FORNECEDOR, int>
    {
        public FornecedorController()
        {
            this.Dao = new FornecedorDataAccess(new PedidosDataContext());
        }        
    }
}