using Pedidos.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Pedidos.Model.Views;
using Pedidos.Util;

namespace Pedidos.DataAccess
{
    public class PedidosDataAccess : GenericDataAccessObject<PEDIDOS, int>
    {
        private const string ClienteNomeProperty = "ClienteNome";

        public override string GeneratorName
        {
            get
            {
                return "INC_PEDIDOS";
            }
        }

        private IQueryable<PEDIDOSView> _CustomQuery;

        public PedidosDataAccess(DbContext context) : base(context)
        {
            _CustomQuery = from p in context.Set<PEDIDOS>()
                           join c in context.Set<CLIENTES>() on p.CLICODIGO equals c.CLICODIGO
                           select new PEDIDOSView()
                           {
                               PEDCODIGO = p.PEDCODIGO,
                               ORCCODIGO = p.ORCCODIGO,
                               PEDDATAEMISSAO = p.PEDDATAEMISSAO,
                               PEDDATALOCACAO = p.PEDDATALOCACAO,
                               PEDDATASAIDA = p.PEDDATASAIDA,
                               PEDDATADEVOLUCAO = p.PEDDATADEVOLUCAO,
                               CLICODIGO = p.CLICODIGO,
                               FORCODIGO = p.FORCODIGO,
                               PEDOBS = p.PEDOBS,
                               PEDDIARIA = p.PEDDIARIA,
                               PEDOBS2 = p.PEDOBS2,
                               PEDTIPOENTREGA = p.PEDTIPOENTREGA,
                               PEDATENDENTE = p.PEDATENDENTE,
                               PEDVALORPAGO = p.PEDVALORPAGO,
                               PEDDESCONTO = p.PEDDESCONTO,
                               PEDSTATUS = p.PEDSTATUS,
                               PEDSTATUSPGTO = p.PEDSTATUSPGTO,
                               DATAIMPRESSAO = p.DATAIMPRESSAO,
                               ULTIMOACESSO = p.ULTIMOACESSO,
                               ULTIMOACESSODATA = p.ULTIMOACESSODATA,
                               EMITIDOPOR = p.EMITIDOPOR,
                               EMITIDOPORDATA = p.EMITIDOPORDATA,
                               ULTIMOIMPRESSAO = p.ULTIMOIMPRESSAO,
                               PEDDESCONTOP = p.PEDDESCONTOP,
                               ClienteNome = c.CLIRAZAOSOCIAL
                           };
        }
        
        public override PEDIDOS[] GetAll()
        {
            return _CustomQuery.ToArray();
        }

        public override PEDIDOS[] GetAll(int pageIndex)
        {
            var table = _CustomQuery.OrderBy(x => x.PEDCODIGO)
                                    .Skip(pageIndex * Constantes.PAGE_SIZE)
                                    .Take(Constantes.PAGE_SIZE);

            return table.ToArray();
        }

        public PEDIDOS[] FilterByClienteNome(string operation, string value, int pageIndex, out int count)
        {
            IQueryable<PEDIDOSView> query = null;

            switch (operation)
            {
                case "contains":
                    query = (from p in this.DataContext.Set<PEDIDOS>()
                             join c in this.DataContext.Set<CLIENTES>() on p.CLICODIGO equals c.CLICODIGO
                             where c.CLIRAZAOSOCIAL.Contains(value)                                                 
                             select new PEDIDOSView()
                             {
                                 PEDCODIGO = p.PEDCODIGO,
                                 ORCCODIGO = p.ORCCODIGO,
                                 PEDDATAEMISSAO = p.PEDDATAEMISSAO,
                                 PEDDATALOCACAO = p.PEDDATALOCACAO,
                                 PEDDATASAIDA = p.PEDDATASAIDA,
                                 PEDDATADEVOLUCAO = p.PEDDATADEVOLUCAO,
                                 CLICODIGO = p.CLICODIGO,
                                 FORCODIGO = p.FORCODIGO,
                                 PEDOBS = p.PEDOBS,
                                 PEDDIARIA = p.PEDDIARIA,
                                 PEDOBS2 = p.PEDOBS2,
                                 PEDTIPOENTREGA = p.PEDTIPOENTREGA,
                                 PEDATENDENTE = p.PEDATENDENTE,
                                 PEDVALORPAGO = p.PEDVALORPAGO,
                                 PEDDESCONTO = p.PEDDESCONTO,
                                 PEDSTATUS = p.PEDSTATUS,
                                 PEDSTATUSPGTO = p.PEDSTATUSPGTO,
                                 DATAIMPRESSAO = p.DATAIMPRESSAO,
                                 ULTIMOACESSO = p.ULTIMOACESSO,
                                 ULTIMOACESSODATA = p.ULTIMOACESSODATA,
                                 EMITIDOPOR = p.EMITIDOPOR,
                                 EMITIDOPORDATA = p.EMITIDOPORDATA,
                                 ULTIMOIMPRESSAO = p.ULTIMOIMPRESSAO,
                                 PEDDESCONTOP = p.PEDDESCONTOP,
                                 ClienteNome = c.CLIRAZAOSOCIAL
                             });
                    break;

                case "startswith":
                    query = (from p in this.DataContext.Set<PEDIDOS>()
                            join c in this.DataContext.Set<CLIENTES>() on p.CLICODIGO equals c.CLICODIGO
                            where c.CLIRAZAOSOCIAL.StartsWith(value)                             
                            select new PEDIDOSView()
                            {
                                PEDCODIGO = p.PEDCODIGO,
                                ORCCODIGO = p.ORCCODIGO,
                                PEDDATAEMISSAO = p.PEDDATAEMISSAO,
                                PEDDATALOCACAO = p.PEDDATALOCACAO,
                                PEDDATASAIDA = p.PEDDATASAIDA,
                                PEDDATADEVOLUCAO = p.PEDDATADEVOLUCAO,
                                CLICODIGO = p.CLICODIGO,
                                FORCODIGO = p.FORCODIGO,
                                PEDOBS = p.PEDOBS,
                                PEDDIARIA = p.PEDDIARIA,
                                PEDOBS2 = p.PEDOBS2,
                                PEDTIPOENTREGA = p.PEDTIPOENTREGA,
                                PEDATENDENTE = p.PEDATENDENTE,
                                PEDVALORPAGO = p.PEDVALORPAGO,
                                PEDDESCONTO = p.PEDDESCONTO,
                                PEDSTATUS = p.PEDSTATUS,
                                PEDSTATUSPGTO = p.PEDSTATUSPGTO,
                                DATAIMPRESSAO = p.DATAIMPRESSAO,
                                ULTIMOACESSO = p.ULTIMOACESSO,
                                ULTIMOACESSODATA = p.ULTIMOACESSODATA,
                                EMITIDOPOR = p.EMITIDOPOR,
                                EMITIDOPORDATA = p.EMITIDOPORDATA,
                                ULTIMOIMPRESSAO = p.ULTIMOIMPRESSAO,
                                PEDDESCONTOP = p.PEDDESCONTOP,
                                ClienteNome = c.CLIRAZAOSOCIAL
                            });
                    break;

                case "endswith":
                    query = (from p in this.DataContext.Set<PEDIDOS>()
                             join c in this.DataContext.Set<CLIENTES>() on p.CLICODIGO equals c.CLICODIGO
                             where c.CLIRAZAOSOCIAL.EndsWith(value)
                             select new PEDIDOSView()
                             {
                                 PEDCODIGO = p.PEDCODIGO,
                                 ORCCODIGO = p.ORCCODIGO,
                                 PEDDATAEMISSAO = p.PEDDATAEMISSAO,
                                 PEDDATALOCACAO = p.PEDDATALOCACAO,
                                 PEDDATASAIDA = p.PEDDATASAIDA,
                                 PEDDATADEVOLUCAO = p.PEDDATADEVOLUCAO,
                                 CLICODIGO = p.CLICODIGO,
                                 FORCODIGO = p.FORCODIGO,
                                 PEDOBS = p.PEDOBS,
                                 PEDDIARIA = p.PEDDIARIA,
                                 PEDOBS2 = p.PEDOBS2,
                                 PEDTIPOENTREGA = p.PEDTIPOENTREGA,
                                 PEDATENDENTE = p.PEDATENDENTE,
                                 PEDVALORPAGO = p.PEDVALORPAGO,
                                 PEDDESCONTO = p.PEDDESCONTO,
                                 PEDSTATUS = p.PEDSTATUS,
                                 PEDSTATUSPGTO = p.PEDSTATUSPGTO,
                                 DATAIMPRESSAO = p.DATAIMPRESSAO,
                                 ULTIMOACESSO = p.ULTIMOACESSO,
                                 ULTIMOACESSODATA = p.ULTIMOACESSODATA,
                                 EMITIDOPOR = p.EMITIDOPOR,
                                 EMITIDOPORDATA = p.EMITIDOPORDATA,
                                 ULTIMOIMPRESSAO = p.ULTIMOIMPRESSAO,
                                 PEDDESCONTOP = p.PEDDESCONTOP,
                                 ClienteNome = c.CLIRAZAOSOCIAL
                             });
                    break;

                default:
                    query = (from p in this.DataContext.Set<PEDIDOS>()
                            join c in this.DataContext.Set<CLIENTES>() on p.CLICODIGO equals c.CLICODIGO
                            where c.CLIRAZAOSOCIAL.ToLower() == value.ToLower()
                            select new PEDIDOSView()
                            {
                                PEDCODIGO = p.PEDCODIGO,
                                ORCCODIGO = p.ORCCODIGO,
                                PEDDATAEMISSAO = p.PEDDATAEMISSAO,
                                PEDDATALOCACAO = p.PEDDATALOCACAO,
                                PEDDATASAIDA = p.PEDDATASAIDA,
                                PEDDATADEVOLUCAO = p.PEDDATADEVOLUCAO,
                                CLICODIGO = p.CLICODIGO,
                                FORCODIGO = p.FORCODIGO,
                                PEDOBS = p.PEDOBS,
                                PEDDIARIA = p.PEDDIARIA,
                                PEDOBS2 = p.PEDOBS2,
                                PEDTIPOENTREGA = p.PEDTIPOENTREGA,
                                PEDATENDENTE = p.PEDATENDENTE,
                                PEDVALORPAGO = p.PEDVALORPAGO,
                                PEDDESCONTO = p.PEDDESCONTO,
                                PEDSTATUS = p.PEDSTATUS,
                                PEDSTATUSPGTO = p.PEDSTATUSPGTO,
                                DATAIMPRESSAO = p.DATAIMPRESSAO,
                                ULTIMOACESSO = p.ULTIMOACESSO,
                                ULTIMOACESSODATA = p.ULTIMOACESSODATA,
                                EMITIDOPOR = p.EMITIDOPOR,
                                EMITIDOPORDATA = p.EMITIDOPORDATA,
                                ULTIMOIMPRESSAO = p.ULTIMOIMPRESSAO,
                                PEDDESCONTOP = p.PEDDESCONTOP,
                                ClienteNome = c.CLIRAZAOSOCIAL
                            });
                    break;
            }

            count = query.Count();

            return query.OrderByDescending(x => x.PEDCODIGO)
                        .Skip(pageIndex * Constantes.PAGE_SIZE)
                        .Take(Constantes.PAGE_SIZE)                        
                        .ToArray();
        }

        public PEDIDOS[] FilterByDatas(string nomePropriedadeData, string operation, string value, int pageIndex, out int count)
        {
            IQueryable<PEDIDOSView> query = null;
            DateTime data = DateTime.Now;
            bool success = DateTime.TryParse(value, out data);

            if (!success)
            {
                count = 0;
                return new PEDIDOS[] { };
            }

            switch (operation)
            {
                case "==":
                    query = (from p in this.DataContext.Set<PEDIDOS>()
                             join c in this.DataContext.Set<CLIENTES>() on p.CLICODIGO equals c.CLICODIGO
                             where (nomePropriedadeData == "PEDDATAEMISSAO" && p.PEDDATAEMISSAO != null && p.PEDDATAEMISSAO.Value == data) 
                                || (nomePropriedadeData == "PEDDATADEVOLUCAO" && p.PEDDATADEVOLUCAO != null && p.PEDDATADEVOLUCAO.Value == data)
                                || (nomePropriedadeData == "PEDDATALOCACAO" && p.PEDDATALOCACAO != null && p.PEDDATALOCACAO.Value == data)
                                || (nomePropriedadeData == "PEDDATASAIDA" && p.PEDDATASAIDA != null && p.PEDDATASAIDA.Value == data)
                             select new PEDIDOSView()
                             {
                                 PEDCODIGO = p.PEDCODIGO,
                                 ORCCODIGO = p.ORCCODIGO,
                                 PEDDATAEMISSAO = p.PEDDATAEMISSAO,
                                 PEDDATALOCACAO = p.PEDDATALOCACAO,
                                 PEDDATASAIDA = p.PEDDATASAIDA,
                                 PEDDATADEVOLUCAO = p.PEDDATADEVOLUCAO,
                                 CLICODIGO = p.CLICODIGO,
                                 FORCODIGO = p.FORCODIGO,
                                 PEDOBS = p.PEDOBS,
                                 PEDDIARIA = p.PEDDIARIA,
                                 PEDOBS2 = p.PEDOBS2,
                                 PEDTIPOENTREGA = p.PEDTIPOENTREGA,
                                 PEDATENDENTE = p.PEDATENDENTE,
                                 PEDVALORPAGO = p.PEDVALORPAGO,
                                 PEDDESCONTO = p.PEDDESCONTO,
                                 PEDSTATUS = p.PEDSTATUS,
                                 PEDSTATUSPGTO = p.PEDSTATUSPGTO,
                                 DATAIMPRESSAO = p.DATAIMPRESSAO,
                                 ULTIMOACESSO = p.ULTIMOACESSO,
                                 ULTIMOACESSODATA = p.ULTIMOACESSODATA,
                                 EMITIDOPOR = p.EMITIDOPOR,
                                 EMITIDOPORDATA = p.EMITIDOPORDATA,
                                 ULTIMOIMPRESSAO = p.ULTIMOIMPRESSAO,
                                 PEDDESCONTOP = p.PEDDESCONTOP,
                                 ClienteNome = c.CLIRAZAOSOCIAL
                             });
                    break;
                case "!=":
                    query = (from p in this.DataContext.Set<PEDIDOS>()
                             join c in this.DataContext.Set<CLIENTES>() on p.CLICODIGO equals c.CLICODIGO
                             where (nomePropriedadeData == "PEDDATAEMISSAO" && p.PEDDATAEMISSAO != null && p.PEDDATAEMISSAO.Value != data)
                                || (nomePropriedadeData == "PEDDATADEVOLUCAO" && p.PEDDATADEVOLUCAO != null && p.PEDDATADEVOLUCAO.Value != data)
                                || (nomePropriedadeData == "PEDDATALOCACAO" && p.PEDDATALOCACAO != null && p.PEDDATALOCACAO.Value != data)
                                || (nomePropriedadeData == "PEDDATASAIDA" && p.PEDDATASAIDA != null && p.PEDDATASAIDA.Value != data)
                             select new PEDIDOSView()
                             {
                                 PEDCODIGO = p.PEDCODIGO,
                                 ORCCODIGO = p.ORCCODIGO,
                                 PEDDATAEMISSAO = p.PEDDATAEMISSAO,
                                 PEDDATALOCACAO = p.PEDDATALOCACAO,
                                 PEDDATASAIDA = p.PEDDATASAIDA,
                                 PEDDATADEVOLUCAO = p.PEDDATADEVOLUCAO,
                                 CLICODIGO = p.CLICODIGO,
                                 FORCODIGO = p.FORCODIGO,
                                 PEDOBS = p.PEDOBS,
                                 PEDDIARIA = p.PEDDIARIA,
                                 PEDOBS2 = p.PEDOBS2,
                                 PEDTIPOENTREGA = p.PEDTIPOENTREGA,
                                 PEDATENDENTE = p.PEDATENDENTE,
                                 PEDVALORPAGO = p.PEDVALORPAGO,
                                 PEDDESCONTO = p.PEDDESCONTO,
                                 PEDSTATUS = p.PEDSTATUS,
                                 PEDSTATUSPGTO = p.PEDSTATUSPGTO,
                                 DATAIMPRESSAO = p.DATAIMPRESSAO,
                                 ULTIMOACESSO = p.ULTIMOACESSO,
                                 ULTIMOACESSODATA = p.ULTIMOACESSODATA,
                                 EMITIDOPOR = p.EMITIDOPOR,
                                 EMITIDOPORDATA = p.EMITIDOPORDATA,
                                 ULTIMOIMPRESSAO = p.ULTIMOIMPRESSAO,
                                 PEDDESCONTOP = p.PEDDESCONTOP,
                                 ClienteNome = c.CLIRAZAOSOCIAL
                             });
                    break;
                case ">":
                    query = (from p in this.DataContext.Set<PEDIDOS>()
                             join c in this.DataContext.Set<CLIENTES>() on p.CLICODIGO equals c.CLICODIGO
                             where (nomePropriedadeData == "PEDDATAEMISSAO" && p.PEDDATAEMISSAO != null && p.PEDDATAEMISSAO.Value > data)
                                || (nomePropriedadeData == "PEDDATADEVOLUCAO" && p.PEDDATADEVOLUCAO != null && p.PEDDATADEVOLUCAO.Value > data)
                                || (nomePropriedadeData == "PEDDATALOCACAO" && p.PEDDATALOCACAO != null && p.PEDDATALOCACAO.Value > data)
                                || (nomePropriedadeData == "PEDDATASAIDA" && p.PEDDATASAIDA != null && p.PEDDATASAIDA.Value > data)
                             select new PEDIDOSView()
                             {
                                 PEDCODIGO = p.PEDCODIGO,
                                 ORCCODIGO = p.ORCCODIGO,
                                 PEDDATAEMISSAO = p.PEDDATAEMISSAO,
                                 PEDDATALOCACAO = p.PEDDATALOCACAO,
                                 PEDDATASAIDA = p.PEDDATASAIDA,
                                 PEDDATADEVOLUCAO = p.PEDDATADEVOLUCAO,
                                 CLICODIGO = p.CLICODIGO,
                                 FORCODIGO = p.FORCODIGO,
                                 PEDOBS = p.PEDOBS,
                                 PEDDIARIA = p.PEDDIARIA,
                                 PEDOBS2 = p.PEDOBS2,
                                 PEDTIPOENTREGA = p.PEDTIPOENTREGA,
                                 PEDATENDENTE = p.PEDATENDENTE,
                                 PEDVALORPAGO = p.PEDVALORPAGO,
                                 PEDDESCONTO = p.PEDDESCONTO,
                                 PEDSTATUS = p.PEDSTATUS,
                                 PEDSTATUSPGTO = p.PEDSTATUSPGTO,
                                 DATAIMPRESSAO = p.DATAIMPRESSAO,
                                 ULTIMOACESSO = p.ULTIMOACESSO,
                                 ULTIMOACESSODATA = p.ULTIMOACESSODATA,
                                 EMITIDOPOR = p.EMITIDOPOR,
                                 EMITIDOPORDATA = p.EMITIDOPORDATA,
                                 ULTIMOIMPRESSAO = p.ULTIMOIMPRESSAO,
                                 PEDDESCONTOP = p.PEDDESCONTOP,
                                 ClienteNome = c.CLIRAZAOSOCIAL
                             });
                    break;
                case "<":
                    query = (from p in this.DataContext.Set<PEDIDOS>()
                             join c in this.DataContext.Set<CLIENTES>() on p.CLICODIGO equals c.CLICODIGO
                             where (nomePropriedadeData == "PEDDATAEMISSAO" && p.PEDDATAEMISSAO != null && p.PEDDATAEMISSAO.Value < data)
                                || (nomePropriedadeData == "PEDDATADEVOLUCAO" && p.PEDDATADEVOLUCAO != null && p.PEDDATADEVOLUCAO.Value < data)
                                || (nomePropriedadeData == "PEDDATALOCACAO" && p.PEDDATALOCACAO != null && p.PEDDATALOCACAO.Value < data)
                                || (nomePropriedadeData == "PEDDATASAIDA" && p.PEDDATASAIDA != null && p.PEDDATASAIDA.Value < data)
                             select new PEDIDOSView()
                             {
                                 PEDCODIGO = p.PEDCODIGO,
                                 ORCCODIGO = p.ORCCODIGO,
                                 PEDDATAEMISSAO = p.PEDDATAEMISSAO,
                                 PEDDATALOCACAO = p.PEDDATALOCACAO,
                                 PEDDATASAIDA = p.PEDDATASAIDA,
                                 PEDDATADEVOLUCAO = p.PEDDATADEVOLUCAO,
                                 CLICODIGO = p.CLICODIGO,
                                 FORCODIGO = p.FORCODIGO,
                                 PEDOBS = p.PEDOBS,
                                 PEDDIARIA = p.PEDDIARIA,
                                 PEDOBS2 = p.PEDOBS2,
                                 PEDTIPOENTREGA = p.PEDTIPOENTREGA,
                                 PEDATENDENTE = p.PEDATENDENTE,
                                 PEDVALORPAGO = p.PEDVALORPAGO,
                                 PEDDESCONTO = p.PEDDESCONTO,
                                 PEDSTATUS = p.PEDSTATUS,
                                 PEDSTATUSPGTO = p.PEDSTATUSPGTO,
                                 DATAIMPRESSAO = p.DATAIMPRESSAO,
                                 ULTIMOACESSO = p.ULTIMOACESSO,
                                 ULTIMOACESSODATA = p.ULTIMOACESSODATA,
                                 EMITIDOPOR = p.EMITIDOPOR,
                                 EMITIDOPORDATA = p.EMITIDOPORDATA,
                                 ULTIMOIMPRESSAO = p.ULTIMOIMPRESSAO,
                                 PEDDESCONTOP = p.PEDDESCONTOP,
                                 ClienteNome = c.CLIRAZAOSOCIAL
                             });
                    break;
                case ">=":
                    query = (from p in this.DataContext.Set<PEDIDOS>()
                             join c in this.DataContext.Set<CLIENTES>() on p.CLICODIGO equals c.CLICODIGO
                             where (nomePropriedadeData == "PEDDATAEMISSAO" && p.PEDDATAEMISSAO != null && p.PEDDATAEMISSAO.Value >= data)
                                || (nomePropriedadeData == "PEDDATADEVOLUCAO" && p.PEDDATADEVOLUCAO != null && p.PEDDATADEVOLUCAO.Value >= data)
                                || (nomePropriedadeData == "PEDDATALOCACAO" && p.PEDDATALOCACAO != null && p.PEDDATALOCACAO.Value >= data)
                                || (nomePropriedadeData == "PEDDATASAIDA" && p.PEDDATASAIDA != null && p.PEDDATASAIDA.Value >= data)
                             select new PEDIDOSView()
                             {
                                 PEDCODIGO = p.PEDCODIGO,
                                 ORCCODIGO = p.ORCCODIGO,
                                 PEDDATAEMISSAO = p.PEDDATAEMISSAO,
                                 PEDDATALOCACAO = p.PEDDATALOCACAO,
                                 PEDDATASAIDA = p.PEDDATASAIDA,
                                 PEDDATADEVOLUCAO = p.PEDDATADEVOLUCAO,
                                 CLICODIGO = p.CLICODIGO,
                                 FORCODIGO = p.FORCODIGO,
                                 PEDOBS = p.PEDOBS,
                                 PEDDIARIA = p.PEDDIARIA,
                                 PEDOBS2 = p.PEDOBS2,
                                 PEDTIPOENTREGA = p.PEDTIPOENTREGA,
                                 PEDATENDENTE = p.PEDATENDENTE,
                                 PEDVALORPAGO = p.PEDVALORPAGO,
                                 PEDDESCONTO = p.PEDDESCONTO,
                                 PEDSTATUS = p.PEDSTATUS,
                                 PEDSTATUSPGTO = p.PEDSTATUSPGTO,
                                 DATAIMPRESSAO = p.DATAIMPRESSAO,
                                 ULTIMOACESSO = p.ULTIMOACESSO,
                                 ULTIMOACESSODATA = p.ULTIMOACESSODATA,
                                 EMITIDOPOR = p.EMITIDOPOR,
                                 EMITIDOPORDATA = p.EMITIDOPORDATA,
                                 ULTIMOIMPRESSAO = p.ULTIMOIMPRESSAO,
                                 PEDDESCONTOP = p.PEDDESCONTOP,
                                 ClienteNome = c.CLIRAZAOSOCIAL
                             });
                    break;
                case "<=":
                    query = (from p in this.DataContext.Set<PEDIDOS>()
                             join c in this.DataContext.Set<CLIENTES>() on p.CLICODIGO equals c.CLICODIGO
                             where (nomePropriedadeData == "PEDDATAEMISSAO" && p.PEDDATAEMISSAO != null && p.PEDDATAEMISSAO.Value <= data)
                                || (nomePropriedadeData == "PEDDATADEVOLUCAO" && p.PEDDATADEVOLUCAO != null && p.PEDDATADEVOLUCAO.Value <= data)
                                || (nomePropriedadeData == "PEDDATALOCACAO" && p.PEDDATALOCACAO != null && p.PEDDATALOCACAO.Value <= data)
                                || (nomePropriedadeData == "PEDDATASAIDA" && p.PEDDATASAIDA != null && p.PEDDATASAIDA.Value <= data)
                             select new PEDIDOSView()
                             {
                                 PEDCODIGO = p.PEDCODIGO,
                                 ORCCODIGO = p.ORCCODIGO,
                                 PEDDATAEMISSAO = p.PEDDATAEMISSAO,
                                 PEDDATALOCACAO = p.PEDDATALOCACAO,
                                 PEDDATASAIDA = p.PEDDATASAIDA,
                                 PEDDATADEVOLUCAO = p.PEDDATADEVOLUCAO,
                                 CLICODIGO = p.CLICODIGO,
                                 FORCODIGO = p.FORCODIGO,
                                 PEDOBS = p.PEDOBS,
                                 PEDDIARIA = p.PEDDIARIA,
                                 PEDOBS2 = p.PEDOBS2,
                                 PEDTIPOENTREGA = p.PEDTIPOENTREGA,
                                 PEDATENDENTE = p.PEDATENDENTE,
                                 PEDVALORPAGO = p.PEDVALORPAGO,
                                 PEDDESCONTO = p.PEDDESCONTO,
                                 PEDSTATUS = p.PEDSTATUS,
                                 PEDSTATUSPGTO = p.PEDSTATUSPGTO,
                                 DATAIMPRESSAO = p.DATAIMPRESSAO,
                                 ULTIMOACESSO = p.ULTIMOACESSO,
                                 ULTIMOACESSODATA = p.ULTIMOACESSODATA,
                                 EMITIDOPOR = p.EMITIDOPOR,
                                 EMITIDOPORDATA = p.EMITIDOPORDATA,
                                 ULTIMOIMPRESSAO = p.ULTIMOIMPRESSAO,
                                 PEDDESCONTOP = p.PEDDESCONTOP,
                                 ClienteNome = c.CLIRAZAOSOCIAL
                             });
                    break;
                default:
                    count = 0;
                    return new PEDIDOS[] { };
            }

            count = query.Count();

            return query.OrderByDescending(x => x.PEDCODIGO)
                        .Skip(pageIndex * Constantes.PAGE_SIZE)
                        .Take(Constantes.PAGE_SIZE)
                        .ToArray();
        }

        public override PEDIDOS[] Filter(Expression<Func<PEDIDOS, bool>> expression, int pageIndex)
        {           
            return _CustomQuery.Where(expression)
                               .OrderByDescending(x => x.PEDCODIGO)
                               .Skip(pageIndex * Constantes.PAGE_SIZE)
                               .Take(Constantes.PAGE_SIZE)
                               .ToArray();
        }

        public override PEDIDOS GetById(int id)
        {
            return _CustomQuery.Where(x => x.PEDCODIGO == id).FirstOrDefault();
        }
        
    }
}
