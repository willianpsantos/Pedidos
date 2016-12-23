using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Util
{
    public class PageableData<TEntity>
    {
        public TEntity[] Data { get; set; }

        public int Count { get; set; }
    }
}
