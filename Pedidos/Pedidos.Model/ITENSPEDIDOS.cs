//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pedidos.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ITENSPEDIDOS
    {
        public int IPCODIGO { get; set; }
        public Nullable<int> PEDCODIGO { get; set; }
        public Nullable<int> PROCODIGO { get; set; }
        public Nullable<int> IPQUANTIDADE { get; set; }
        public Nullable<decimal> IPPRECOUNITARIO { get; set; }
        public Nullable<decimal> IPTOTAL { get; set; }
        public Nullable<decimal> IPPRECOREPOSICAO { get; set; }
        public Nullable<decimal> IPTOTALREPOSICAO { get; set; }
        public string IPLOCADO { get; set; }
        public Nullable<System.DateTime> IPDATA { get; set; }
    }
}