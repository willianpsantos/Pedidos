using Pedidos.Classes;
using Pedidos.DataAccess;
using Pedidos.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Pedidos.Services
{
    public class LookupController<TEntity, TId> : ApiController
    {
        protected virtual IDataAccessObject<TEntity, TId> Dao { get; set; }

        public LookupController()
        {

        }

        public ServiceResponse<TEntity> Get(int take, int skip, int page, int pageSize, [ModelBinder] ServiceFilter filter)
        {
            TEntity[] data = null;
            int count = 0;

            if (filter == null || string.IsNullOrEmpty(filter.Field))
            {
                data = Dao.GetAll(page - 1);
                count = Dao.Count(true);

                return new ServiceResponse<TEntity>()
                {
                    Total = count,
                    Data = data
                };
            }

            Filter args = new Filter();
            args.FilterBy = filter.Field;
            args.Operator = filter.Operator;
            args.Value = filter.Value;
            args.HasParameter = false;

            string strExpression = args.ToString();
            var expression = Dao.GetExpression(strExpression);

            data = Dao.Filter(expression, page - 1);
            count = Dao.Count(expression);

            return new ServiceResponse<TEntity>()
            {
                Total = count,
                Data = data
            };
        }

        public TEntity[] Get(string field, string operation, string value)
        {
            Filter args = new Filter();
            args.ItemType = typeof(TEntity);
            args.FilterBy = field;
            args.Operator = operation;
            args.Value = value;
            args.HasParameter = false;

            string strExpression = args.ToString();
            var expression = Dao.GetExpression(strExpression);

            var data = Dao.Filter(expression, 0);
            return data;
        }
    }
}