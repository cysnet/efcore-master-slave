using Microsoft.Extensions.DependencyInjection;
using mysql.ms.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace mysql.ms.Repository
{
    public interface IRespository<T> where T : class
    {
        T Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        T Update(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);
        T Remove(T entity);
        IEnumerable<T> Remove(IEnumerable<T> entities);
        T FirstOrDefault(Expression<Func<T, bool>> predicate, bool useSlave = true);
        IQueryable<T> Queryable(bool isSlave = true);
    }


    public class BaseRespository<T> : IRespository<T> where T : class
    {
        Lazy<MySqlMasterDbContext> mySqlMasterDbContext;
        Lazy<MySqlSlaveDbContext> mySqlSlaveDbContext;
        public BaseRespository(IServiceProvider serviceProvider)
        {
            mySqlMasterDbContext = new Lazy<MySqlMasterDbContext>(() => serviceProvider.GetService<MySqlMasterDbContext>());
            mySqlSlaveDbContext = new Lazy<MySqlSlaveDbContext>(() => serviceProvider.GetService<MySqlSlaveDbContext>());
        }

        #region master
        public T Add(T entity)
        {
            var result = mySqlMasterDbContext.Value.Add(entity);
            return result.Entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            mySqlMasterDbContext.Value.AddRange(entities);
            return entities;
        }

        public T Update(T entity)
        {
            var data = mySqlMasterDbContext.Value.Update(entity);
            return data.Entity;
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            mySqlMasterDbContext.Value.UpdateRange(entities);
            return entities;
        }

        public T Remove(T entity)
        {
            var result = mySqlMasterDbContext.Value.Remove(entity);
            return result.Entity;
        }

        public IEnumerable<T> Remove(IEnumerable<T> entities)
        {
            mySqlMasterDbContext.Value.RemoveRange(entities);
            return entities;
        }
        #endregion

        #region slave
        public T FirstOrDefault(Expression<Func<T, bool>> predicate, bool useSlave = true)
        {
            return Queryable(useSlave).FirstOrDefault(predicate);
        }

        public IQueryable<T> Queryable(bool useSlave = true)
        {

            return useSlave ? mySqlSlaveDbContext.Value.Set<T>() : mySqlMasterDbContext.Value.Set<T>();
        }
        #endregion

    }
}
