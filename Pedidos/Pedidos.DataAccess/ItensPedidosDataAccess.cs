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
    public class ItensPedidosDataAccess: GenericDataAccessObject<ITENSPEDIDOS, int>
    {
        public override string GeneratorName
        {
            get
            {
                return "INC_ITENSPEDIDOS";
            }
        }

        private IQueryable<ITENSPEDIDOSView> _CustomQuery;

        public ItensPedidosDataAccess(DbContext context) : base(context)
        {
            _CustomQuery = from p in context.Set<ITENSPEDIDOS>()
                           join c in context.Set<PRODUTOS>() on p.PROCODIGO equals c.PROCODIGO
                           select new ITENSPEDIDOSView()
                           {
                               IPCODIGO = p.IPCODIGO,
                               PEDCODIGO = p.PEDCODIGO,
                               PROCODIGO = p.PROCODIGO,
                               IPQUANTIDADE = p.IPQUANTIDADE,
                               IPPRECOUNITARIO = p.IPPRECOUNITARIO,
                               IPTOTAL = p.IPTOTAL,
                               IPPRECOREPOSICAO = p.IPPRECOREPOSICAO,
                               IPTOTALREPOSICAO = p.IPTOTALREPOSICAO,
                               IPLOCADO = p.IPLOCADO,
                               IPDATA = p.IPDATA,
                               ProdutoNome = c.PRODESCRICAO
                           };
        }

        public override ITENSPEDIDOS[] GetAll()
        {
            return _CustomQuery.ToArray();
        }

        public override ITENSPEDIDOS[] GetAll(int pageIndex)
        {
            var table = _CustomQuery.OrderBy(x => x.IPCODIGO)
                              .Skip(pageIndex * Constantes.PAGE_SIZE)
                              .Take(Constantes.PAGE_SIZE);

            return table.ToArray();
        }

        public override ITENSPEDIDOS[] Filter(Expression<Func<ITENSPEDIDOS, bool>> expression, int pageIndex)
        {
            return _CustomQuery.Where(expression)
                               .OrderByDescending(x => x.IPCODIGO)
                               .Skip(pageIndex * Constantes.PAGE_SIZE)
                               .Take(Constantes.PAGE_SIZE)
                               .ToArray();
        }

        public override ITENSPEDIDOS GetById(int id)
        {
            return _CustomQuery.Where(x => x.IPCODIGO == id).FirstOrDefault();
        }

        public void DeleteAll(int pedidoId)
        {
            var connection = this.GetConnection();
            var command = connection.CreateCommand();
            
            command.CommandText = $"DELETE FROM ITENSPEDIDOS WHERE PEDCODIGO = {pedidoId}";
            command.CommandType = System.Data.CommandType.Text;
            command.Connection = connection;

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                BeginTransaction();

                command.Transaction = Transaction.UnderlyingTransaction;
                command.ExecuteNonQuery();

                if (AutoCommit)
                    Commit();
            }
            catch (Exception ex)
            {
                Rollback();
                throw;
            }
            finally
            {
                if (AutoCloseConnection)
                    CloseConnection();
            }
        }
    }
}
