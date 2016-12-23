using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Classes
{
    public class PageFormSaveEventArgs<TEntity> : EventArgs
    {
        public TEntity Entity { get; set; }
    }
}
