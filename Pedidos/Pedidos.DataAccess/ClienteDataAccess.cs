using Pedidos.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.DataAccess
{
    public class ClienteDataAccess : GenericDataAccessObject<CLIENTES, int>
    {
        public override string GeneratorName
        {
            get
            {
                return "INC_CLIENTES";
            }
        }

        public ClienteDataAccess(DbContext context) : base(context)
        {

        }
    }
}
