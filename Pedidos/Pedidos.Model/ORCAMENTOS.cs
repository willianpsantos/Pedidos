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
    
    public partial class ORCAMENTOS
    {
        public int ORCCODIGO { get; set; }
        public Nullable<System.DateTime> ORCDATAEMISSAO { get; set; }
        public Nullable<System.DateTime> ORCDATALOCACAO { get; set; }
        public Nullable<System.DateTime> ORCDATASAIDA { get; set; }
        public Nullable<System.DateTime> ORCDATADEVOLUCAO { get; set; }
        public int CLICODIGO { get; set; }
        public Nullable<int> FORCODIGO { get; set; }
        public byte[] ORCOBS { get; set; }
        public Nullable<float> ORCDIARIA { get; set; }
        public Nullable<float> ORCDESCONTO { get; set; }
        public Nullable<System.DateTime> DATAIMPRESSAO { get; set; }
        public string ULTIMOACESSO { get; set; }
        public Nullable<System.DateTime> ULTIMOACESSODATA { get; set; }
        public string EMITIDOPOR { get; set; }
        public Nullable<System.DateTime> EMITIDOPORDATA { get; set; }
        public string ULTIMOIMPRESSAO { get; set; }
        public Nullable<float> ORCDESCONTOP { get; set; }
    }
}
