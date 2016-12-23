using System;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Pedidos.DataAccess
{
    public interface IDataAccessObject<TEntity, TId>
    {
        bool AutoCloseConnection { get; set; }
        bool AutoCommit { get; set; }
        string GeneratorName { get; }        
        void Commit();
        void Rollback();
        void CloseConnection();
        DbContextTransaction GetTransaction();
        void SetTransaction(DbContextTransaction transaction);

        TId ConvertValue(string value);
        TId GenerateId();
        TId GetIdValue(TEntity entity);
        void SetIdValue(TEntity entity, TId id);

        int Count(bool recount = false);
        int Count(Expression<Func<TEntity, bool>> expression);

        Expression<Func<TEntity, bool>> GetExpression(string expression, params object[] parameters);
        Expression<Func<TType, bool>> GetExpression<TType>(string expression, params object[] parameters);

        TEntity[] GetAll();
        TEntity[] GetAll(int pageIndex);
        TEntity GetById(TId id);

        TEntity[] Filter(Expression<Func<TEntity, bool>> expression, int pageIndex = 0);

        void Save(TEntity entity);
        void Delete(TId id);
    }
}
