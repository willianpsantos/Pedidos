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
    
    public partial class CLIENTES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLIENTES()
        {
            this.IMGENDERECO = new HashSet<IMGENDERECO>();
        }
    
        public int CLICODIGO { get; set; }
        public string CLIRAZAOSOCIAL { get; set; }
        public string CLICNPJ { get; set; }
        public string CLICPF { get; set; }
        public string CLITELFONE { get; set; }
        public string CLICELULAR { get; set; }
        public string CLITELEFONECOMERCIAL { get; set; }
        public string CLIENDERECO { get; set; }
        public string CLIPONTOREFERENCIA { get; set; }
        public string CLIBAIRRO { get; set; }
        public string CLICIDADEUF { get; set; }
        public string CLICONTATO { get; set; }
        public string CLIEMAIL { get; set; }
        public Nullable<System.DateTime> CLIDATACADASTRO { get; set; }
        public string CLICLASSIFICACAO { get; set; }
        public byte[] CLIOBS { get; set; }
        public string CLICEP { get; set; }
        public string CLIRGIE { get; set; }
        public string CLIINSCRESTADUAL { get; set; }
        public string CLIINSCRMUNICIPAL { get; set; }
        public string ULTIMOACESSO { get; set; }
        public Nullable<System.DateTime> ULTIMOACESSODATA { get; set; }
        public string EMITIDOPOR { get; set; }
        public Nullable<System.DateTime> EMITIDOPORDATA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IMGENDERECO> IMGENDERECO { get; set; }
    }
}
