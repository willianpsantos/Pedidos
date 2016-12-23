using Pedidos.Util;
using System;

namespace Pedidos.Classes
{
    public class FilterBarSearchEventArgs : EventArgs
    {
        public Filter Filter { get; set; }
    }
}