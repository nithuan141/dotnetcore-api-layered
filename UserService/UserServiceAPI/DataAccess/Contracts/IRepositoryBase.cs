using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UserServiceAPI.DataAccess.Contracts
{
    /// <summary>
    /// Repository base intreace with CRUD.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T>
    {
        /// <summary>
        /// Fetches and returns the full list of given entity.
        /// </summary>
        /// <returns>List of Entity.</returns>
        IQueryable<T> FindAll();

        /// <summary>
        /// Fetches and returns the list of given entity based on the condition.
        /// </summary>
        /// <param name="expression">The cndition expresssion.</param>
        /// <returns>List of Entity.</returns>
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Create an async task for inserting the given object entity to the data source..
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The inserted object of the entity.</returns>
        Task<T> Create(T entity);

        /// <summary>
        /// Update the given entity.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The entity object.</returns>
        T Update(T entity);

        /// <summary>
        /// Delete the given enity object from the data source.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        void Delete(T entity);

        /// <summary>
        /// Saving the context changes.
        /// </summary>
        void Save();
    }
}
