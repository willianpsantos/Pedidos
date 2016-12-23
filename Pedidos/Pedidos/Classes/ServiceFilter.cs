using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Classes
{
    public class ServiceFilter
    {
        public string Field { get; set; }

        public string Operator { get; set; }

        public string Value { get; set; }
    }
}
