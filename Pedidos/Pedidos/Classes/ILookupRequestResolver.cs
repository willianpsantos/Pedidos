using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Classes
{
    public interface ILookupRequestResolver
    {
        Pedidos.UserControls.Lookup GetLookupRequested();
        Pedidos.UserControls.Lookup GetLookupRequested(string clientId);
    }
}
