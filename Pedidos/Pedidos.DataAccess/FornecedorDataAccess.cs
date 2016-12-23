using Pedidos.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.DataAccess
{
    public class FornecedorDataAccess : GenericDataAccessObject<FORNECEDOR, int>
    {
        public override string GeneratorName
        {
            get
            {
                return "GEN_FORNECEDOR_ID";
            }
        }

        public FornecedorDataAccess(DbContext context) : base(context)
        {

        }
    }
}
