﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PedidosDataContext : DbContext
    {
        public PedidosDataContext()
            : base("name=PedidosDataContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CLIENTES> CLIENTES { get; set; }
        public virtual DbSet<PEDIDOS> PEDIDOS { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }
        public virtual DbSet<ITENSPEDIDOS> ITENSPEDIDOS { get; set; }
        public virtual DbSet<CHEQUES> CHEQUES { get; set; }
        public virtual DbSet<CONFSISTEMA> CONFSISTEMA { get; set; }
        public virtual DbSet<FORMAPAGAMENTO> FORMAPAGAMENTO { get; set; }
        public virtual DbSet<FORNECEDOR> FORNECEDOR { get; set; }
        public virtual DbSet<FUNCIONARIOS> FUNCIONARIOS { get; set; }
        public virtual DbSet<GRUPOPRODUTOS> GRUPOPRODUTOS { get; set; }
        public virtual DbSet<IMGENDERECO> IMGENDERECO { get; set; }
        public virtual DbSet<ITENSORCAMENTOS> ITENSORCAMENTOS { get; set; }
        public virtual DbSet<LOGNF> LOGNF { get; set; }
        public virtual DbSet<LOGPEDIDOS> LOGPEDIDOS { get; set; }
        public virtual DbSet<NF> NF { get; set; }
        public virtual DbSet<ORCAMENTOS> ORCAMENTOS { get; set; }
        public virtual DbSet<PRODUTOS> PRODUTOS { get; set; }
        public virtual DbSet<SUBGRUPOPRODUTOS> SUBGRUPOPRODUTOS { get; set; }
    }
}
