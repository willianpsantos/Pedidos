using System;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Linq.Dynamic;
using Pedidos.Util;
using System.Linq.Expressions;
using System.Data.Common;
using System.Reflection;
using System.Data;


namespace Pedidos.DataAccess
{
    public class GenericDataAccessObject<TEntity, TId> : IDisposable, IDataAccessObject<TEntity, TId> where TEntity : class
    {        
        private int _Total;

        private IDbConnection _Connection;
        protected DbContextTransaction Transaction = null;
        protected readonly DbContext DataContext;

        public virtual string GeneratorName { get; }
        public bool AutoCloseConnection { get; set; }
        public bool AutoCommit { get; set; }

        public GenericDataAccessObject(DbContext dataContext)
        {
            this.DataContext = dataContext;
            this.AutoCloseConnection = true;
            this.AutoCommit = true;
        }

        protected IDbConnection GetConnection()
        {
            if (_Connection == null)
                _Connection = DataContext.Database.Connection;

            return _Connection;
        }

        public virtual DbContextTransaction GetTransaction()
        {
            return Transaction;
        }

        public virtual void SetTransaction(DbContextTransaction transaction)
        {
            Transaction = transaction;
        }

        public virtual void BeginTransaction()
        {
            Transaction = DataContext.Database.BeginTransaction();
        }
        
        public virtual TId GetIdValue(TEntity entity)
        {
            ObjectContext context = ((IObjectContextAdapter)DataContext).ObjectContext;
            ObjectSet<TEntity> set = context.CreateObjectSet<TEntity>();
            EdmMember keyProperty = set.EntitySet.ElementType.KeyMembers.ToArray().FirstOrDefault();

            var property = entity.GetType().GetProperty(keyProperty.Name);
            return (TId)property.GetValue(entity);
        }

        public virtual void SetIdValue(TEntity entity, TId id)
        {
            ObjectContext context = ((IObjectContextAdapter)DataContext).ObjectContext;
            ObjectSet<TEntity> set = context.CreateObjectSet<TEntity>();
            EdmMember keyProperty = set.EntitySet.ElementType.KeyMembers.ToArray().FirstOrDefault();

            var property = typeof(TEntity).GetProperty(keyProperty.Name);
            property.SetValue(entity, id);
        }

        protected virtual string GetIdName()
        {
            ObjectContext context = ((IObjectContextAdapter)DataContext).ObjectContext;
            ObjectSet<TEntity> set = context.CreateObjectSet<TEntity>();
            EdmMember keyProperty = set.EntitySet.ElementType.KeyMembers.ToArray().FirstOrDefault();

            var property = typeof(TEntity).GetProperty(keyProperty.Name);
            return property.Name;
        }


        public virtual void SaveChanges()
        {
            DataContext.SaveChanges();
        }

        public virtual void Commit()
        {
            if (AutoCommit)
                SaveChanges();

            Transaction.Commit();
        }

        public virtual void Rollback()
        {
            Transaction.Rollback();
        }
        
        public void CloseConnection()
        {
            if (_Connection != null)
            {
                if (_Connection.State == System.Data.ConnectionState.Open)
                    _Connection.Close();
            }
        }

        public virtual Expression<Func<TEntity, bool>> GetExpression(string expression, params object[] parameters)
        {
            var type = typeof(TEntity);
            var parameter = Expression.Parameter(type, Constantes.DEFAULT_ALIAS);
            var lambda = System.Linq.Dynamic.DynamicExpression.ParseLambda(new[] { parameter }, null, expression, parameters);
            return (Expression<Func<TEntity, bool>>)lambda;
        }

        public virtual Expression<Func<TType, bool>> GetExpression<TType>(string expression, params object[] parameters)
        {
            var type = typeof(TType);
            var parameter = Expression.Parameter(type, Constantes.DEFAULT_ALIAS);
            var lambda = System.Linq.Dynamic.DynamicExpression.ParseLambda(new[] { parameter }, null, expression, parameters);
            return (Expression<Func<TType, bool>>)lambda;
        }

        public virtual TId ConvertValue(string value)
        {
            var parseMethod = typeof(TId).GetMethod("Parse", new Type[] { typeof(string) }, new ParameterModifier[] { new ParameterModifier(1) });

            if (parseMethod == null)
                return default(TId);
            
            var result = parseMethod.Invoke(null, new object[] { value });
            return (TId)result;
        }

        public virtual TId GenerateId()
        {
            var connection = GetConnection();

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT GEN_ID("+GeneratorName+", 1) as ID FROM RDB$DATABASE";
                command.CommandType = System.Data.CommandType.Text;
                command.Transaction = this.Transaction.UnderlyingTransaction;
                command.Prepare();

                DbDataReader reader = (DbDataReader)command.ExecuteReader();
                TId id = default(TId);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var columnValue = reader.GetString(0);                        
                        id = ConvertValue(columnValue);
                    }
                }

                reader.Close();
                return id;
            }
            catch(Exception ex)
            {                
                throw;
            }
        }

        public virtual void Save(TEntity entity)
        {
            var connection = GetConnection();

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();              
                
                DbSet<TEntity> table = DataContext.Set<TEntity>();

                if (AutoCommit || Transaction == null)
                    BeginTransaction();

                var id = GetIdValue(entity);

                if (id.Equals(default(TId)))
                {
                    TId generatedId = GenerateId();
                    SetIdValue(entity, generatedId);
                    table.Add(entity);
                }
                else
                {
                    table.Attach(entity);
                    DataContext.Entry(entity).State = EntityState.Modified;                    
                    SaveChanges();
                }

                if (AutoCommit)
                    Commit();
            }
            catch(Exception ex)
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

        public virtual void Delete(TId id)
        {
            var connection = GetConnection();

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                
                DbSet<TEntity> table = DataContext.Set<TEntity>();
                BeginTransaction();

                var type = typeof(TEntity);
                var instance = (TEntity)type.Assembly.CreateInstance(type.FullName);
                SetIdValue(instance, id);
                                
                table.Attach(instance);
                DataContext.Entry<TEntity>(instance).State = EntityState.Deleted;

                if (AutoCommit)
                    Commit();
            }
            catch
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

        public virtual TEntity[] Filter(Expression<Func<TEntity, bool>> expression, int pageIndex = 0)
        {
            var idName = GetIdName();            

            var table = DataContext.Set<TEntity>()
                                   .Where(expression)
                                   .OrderBy($"{idName} desc")
                                   .Skip(pageIndex * Constantes.PAGE_SIZE)
                                   .Take(Constantes.PAGE_SIZE);

            return table.ToArray();
        }

        public virtual TEntity[] GetAll()
        {
            return DataContext.Set<TEntity>().ToArray();
        }

        public virtual TEntity[] GetAll(int pageIndex)
        {
            var idName = GetIdName();

            var table = DataContext.Set<TEntity>()
                                    .OrderBy($"{idName} desc")
                                    .Skip(pageIndex * Constantes.PAGE_SIZE)                                   
                                    .Take(Constantes.PAGE_SIZE);

            return table.ToArray();
        }

        public virtual TEntity GetById(TId id)
        {
            string idPropertyName = GetIdName();
            DbSet<TEntity> table = DataContext.Set<TEntity>();
            TEntity obj = table.Where(string.Format("{0} = {1}", idPropertyName, id)).FirstOrDefault();
            return obj;
        }

        public int Count(bool recount = false)
        {
            if (_Total == 0 || recount)
                _Total = DataContext.Set<TEntity>().Count();

            return _Total;
        }

        public int Count(Expression<Func<TEntity, bool>> expression)
        {
            return DataContext.Set<TEntity>().Where(expression).Count();
        }


        public void Dispose()
        {
            if (Transaction != null)
                Transaction.Dispose();

            if (DataContext != null)
                DataContext.Dispose();

            CloseConnection();
        }        
    }
}
