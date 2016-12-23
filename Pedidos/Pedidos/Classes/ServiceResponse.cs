using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Classes
{
    public class ServiceResponse<TEntity>
    {
        public int Total { get; set; }

        public TEntity[] Data { get; set; }
    }
}
