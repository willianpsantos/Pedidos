using Pedidos.Model;
using Pedidos.Model.Views;
using Pedidos.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.DataAccess
{
    public class ProdutoDataAccess : GenericDataAccessObject<PRODUTOS, int>
    {
        public override string GeneratorName
        {
            get
            {
                return "INC_PRODUTOS";
            }
        }

        private IQueryable<PRODUTOSView> _CustomQuery;

        public ProdutoDataAccess(DbContext context) : base(context)
        {
            _CustomQuery = from p in context.Set<PRODUTOS>()
                           join g in context.Set<GRUPOPRODUTOS>() on p.GRUCODIGO equals g.GRUCODIGO into g1
                           join s in context.Set<SUBGRUPOPRODUTOS>() on p.SUBGCODIGO equals s.SUBGCODIGO into s1
                           from g2 in g1.DefaultIfEmpty()
                           from s2 in s1.DefaultIfEmpty()
                           select new PRODUTOSView()
                           {
                               PROCODIGO = p.PROCODIGO,
                               GRUCODIGO = p.GRUCODIGO,
                               SUBGCODIGO = p.SUBGCODIGO,
                               PRODESCRICAO = p.PRODESCRICAO,
                               PROPRECOUNITARIO = p.PROPRECOUNITARIO,
                               PROREPOSICAO = p.PROREPOSICAO,
                               PROUNIDADE = p.PROUNIDADE,
                               PROESTOQUE = p.PROESTOQUE,
                               ULTIMOACESSO = p.ULTIMOACESSO,
                               ULTIMOACESSODATA = p.ULTIMOACESSODATA,
                               EMITIDOPOR = p.EMITIDOPOR,
                               EMITIDOPORDATA = p.EMITIDOPORDATA,
                               GrupoNome = g2 != null ? g2.GRUDESCRICAO : string.Empty,
                               SubgrupoNome = s2 != null ? s2.SUBGDESCRICAO : string.Empty
                           };
        }

        public override PRODUTOS[] GetAll()
        {
            return _CustomQuery.ToArray();
        }

        public override PRODUTOS[] GetAll(int pageIndex)
        {
            var table = _CustomQuery.OrderBy(x => x.PROCODIGO)
                              .Skip(pageIndex * Constantes.PAGE_SIZE)
                              .Take(Constantes.PAGE_SIZE);

            return table.ToArray();
        }

        public PRODUTOS[] FilterByGrupoOuSubGrupo(int pageIndex, string filterBy, string operation, string value, out int count)
        {
            IQueryable<PRODUTOS> query = null;

            switch (operation)
            {
                case "contains":
                    query = from p in DataContext.Set<PRODUTOS>()
                            join g in DataContext.Set<GRUPOPRODUTOS>() on p.GRUCODIGO equals g.GRUCODIGO into g1
                            join s in DataContext.Set<SUBGRUPOPRODUTOS>() on p.SUBGCODIGO equals s.SUBGCODIGO into s1
                            from g2 in g1.DefaultIfEmpty()
                            from s2 in s1.DefaultIfEmpty()
                            where ((filterBy == "GrupoNome" && (g2 != null && g2.GRUDESCRICAO.Contains(value))) ||
                                   (filterBy == "SubgrupoNome" && (s2 != null && s2.SUBGDESCRICAO.Contains(value))))
                            select new PRODUTOSView()
                            {
                                PROCODIGO = p.PROCODIGO,
                                GRUCODIGO = p.GRUCODIGO,
                                SUBGCODIGO = p.SUBGCODIGO,
                                PRODESCRICAO = p.PRODESCRICAO,
                                PROPRECOUNITARIO = p.PROPRECOUNITARIO,
                                PROREPOSICAO = p.PROREPOSICAO,
                                PROUNIDADE = p.PROUNIDADE,
                                PROESTOQUE = p.PROESTOQUE,
                                ULTIMOACESSO = p.ULTIMOACESSO,
                                ULTIMOACESSODATA = p.ULTIMOACESSODATA,
                                EMITIDOPOR = p.EMITIDOPOR,
                                EMITIDOPORDATA = p.EMITIDOPORDATA,
                                GrupoNome = g2 != null ? g2.GRUDESCRICAO : string.Empty,
                                SubgrupoNome = s2 != null ? s2.SUBGDESCRICAO : string.Empty
                            };
                    break;
                case "endswith":
                    query = from p in DataContext.Set<PRODUTOS>()
                            join g in DataContext.Set<GRUPOPRODUTOS>() on p.GRUCODIGO equals g.GRUCODIGO into g1
                            join s in DataContext.Set<SUBGRUPOPRODUTOS>() on p.SUBGCODIGO equals s.SUBGCODIGO into s1
                            from g2 in g1.DefaultIfEmpty()
                            from s2 in s1.DefaultIfEmpty()
                            where ((filterBy == "GrupoNome" && (g2 != null && g2.GRUDESCRICAO.EndsWith(value))) ||
                                   (filterBy == "SubgrupoNome" && (s2 != null && s2.SUBGDESCRICAO.EndsWith(value))))
                            select new PRODUTOSView()
                            {
                                PROCODIGO = p.PROCODIGO,
                                GRUCODIGO = p.GRUCODIGO,
                                SUBGCODIGO = p.SUBGCODIGO,
                                PRODESCRICAO = p.PRODESCRICAO,
                                PROPRECOUNITARIO = p.PROPRECOUNITARIO,
                                PROREPOSICAO = p.PROREPOSICAO,
                                PROUNIDADE = p.PROUNIDADE,
                                PROESTOQUE = p.PROESTOQUE,
                                ULTIMOACESSO = p.ULTIMOACESSO,
                                ULTIMOACESSODATA = p.ULTIMOACESSODATA,
                                EMITIDOPOR = p.EMITIDOPOR,
                                EMITIDOPORDATA = p.EMITIDOPORDATA,
                                GrupoNome = g2 != null ? g2.GRUDESCRICAO : string.Empty,
                                SubgrupoNome = s2 != null ? s2.SUBGDESCRICAO : string.Empty
                            };
                    break;
                case "startswith":
                    query = from p in DataContext.Set<PRODUTOS>()
                            join g in DataContext.Set<GRUPOPRODUTOS>() on p.GRUCODIGO equals g.GRUCODIGO into g1
                            join s in DataContext.Set<SUBGRUPOPRODUTOS>() on p.SUBGCODIGO equals s.SUBGCODIGO into s1
                            from g2 in g1.DefaultIfEmpty()
                            from s2 in s1.DefaultIfEmpty()
                            where ((filterBy == "GrupoNome" && (g2 != null && g2.GRUDESCRICAO.StartsWith(value))) ||
                                   (filterBy == "SubgrupoNome" && (s2 != null && s2.SUBGDESCRICAO.StartsWith(value))))
                            select new PRODUTOSView()
                            {
                                PROCODIGO = p.PROCODIGO,
                                GRUCODIGO = p.GRUCODIGO,
                                SUBGCODIGO = p.SUBGCODIGO,
                                PRODESCRICAO = p.PRODESCRICAO,
                                PROPRECOUNITARIO = p.PROPRECOUNITARIO,
                                PROREPOSICAO = p.PROREPOSICAO,
                                PROUNIDADE = p.PROUNIDADE,
                                PROESTOQUE = p.PROESTOQUE,
                                ULTIMOACESSO = p.ULTIMOACESSO,
                                ULTIMOACESSODATA = p.ULTIMOACESSODATA,
                                EMITIDOPOR = p.EMITIDOPOR,
                                EMITIDOPORDATA = p.EMITIDOPORDATA,
                                GrupoNome = g2 != null ? g2.GRUDESCRICAO : string.Empty,
                                SubgrupoNome = s2 != null ? s2.SUBGDESCRICAO : string.Empty
                            };
                    break;
                default:
                    query = from p in DataContext.Set<PRODUTOS>()
                            join g in DataContext.Set<GRUPOPRODUTOS>() on p.GRUCODIGO equals g.GRUCODIGO into g1
                            join s in DataContext.Set<SUBGRUPOPRODUTOS>() on p.SUBGCODIGO equals s.SUBGCODIGO into s1
                            from g2 in g1.DefaultIfEmpty()
                            from s2 in s1.DefaultIfEmpty()
                            where ((filterBy == "GrupoNome" && (g2 != null && g2.GRUDESCRICAO == value)) ||
                                   (filterBy == "SubgrupoNome" && (s2 != null && s2.SUBGDESCRICAO == value)))
                            select new PRODUTOSView()
                            {
                                PROCODIGO = p.PROCODIGO,
                                GRUCODIGO = p.GRUCODIGO,
                                SUBGCODIGO = p.SUBGCODIGO,
                                PRODESCRICAO = p.PRODESCRICAO,
                                PROPRECOUNITARIO = p.PROPRECOUNITARIO,
                                PROREPOSICAO = p.PROREPOSICAO,
                                PROUNIDADE = p.PROUNIDADE,
                                PROESTOQUE = p.PROESTOQUE,
                                ULTIMOACESSO = p.ULTIMOACESSO,
                                ULTIMOACESSODATA = p.ULTIMOACESSODATA,
                                EMITIDOPOR = p.EMITIDOPOR,
                                EMITIDOPORDATA = p.EMITIDOPORDATA,
                                GrupoNome = g2 != null ? g2.GRUDESCRICAO : string.Empty,
                                SubgrupoNome = s2 != null ? s2.SUBGDESCRICAO : string.Empty
                            };
                    break;
            }

            count = query.Count();

            return query.OrderByDescending(x => x.PROCODIGO)
                        .Skip(pageIndex * Constantes.PAGE_SIZE)
                        .Take(Constantes.PAGE_SIZE)
                        .ToArray();
        }

        public override PRODUTOS[] Filter(Expression<Func<PRODUTOS, bool>> expression, int pageIndex = 0)
        {
            return _CustomQuery.Where(expression)
                               .OrderByDescending(x => x.PROCODIGO)
                               .Skip(pageIndex * Constantes.PAGE_SIZE)
                               .Take(Constantes.PAGE_SIZE)
                               .ToArray();
        }

        public override PRODUTOS GetById(int id)
        {
            return _CustomQuery.Where(x => x.PROCODIGO == id).FirstOrDefault();
        }
    }
}
