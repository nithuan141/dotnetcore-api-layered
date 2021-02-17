using AssetServiceDataProvider.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserServiceAPI.DataAccess.Contracts;

namespace UserServiceAPI.DataAccess
{
    /// <summary>
    /// The abstract base class for the repository base.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// The DB context object of key service .
        /// </summary>
        protected AssetServiceContext RepositoryContext { get; set; }

        /// <summary>
        /// Repository base constructor
        /// </summary>
        /// <param name="repositoryContext"></param>
        public RepositoryBase(AssetServiceContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        /// <summary>
        /// Fetches and returns the full list of given entity.
        /// </summary>
        /// <returns>List of Entity.</returns>
        public IQueryable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>().AsNoTracking();
        }

        /// <summary>
        /// Fetches and returns the list of given entity based on the condition.
        /// </summary>
        /// <param name="expression">The cndition expresssion.</param>
        /// <returns>List of Entity.</returns>
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

        /// <summary>
        /// Create an async task for inserting the given object entity to the data source..
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The inserted object of the entity.</returns>
        public async Task<T> Create(T entity)
        {
           var result = await this.RepositoryContext.Set<T>().AddAsync(entity);
           return result.Entity;
        }

        /// <summary>
        /// Update the given entity.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        public T Update(T entity)
        {
            var result = this.RepositoryContext.Set<T>().Update(entity);
            return result.Entity;
        }

        /// <summary>
        /// Delete the given enity object from the data source.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Saving the context changes.
        /// </summary>
        public void Save()
        {
            this.RepositoryContext.SaveChanges();
        }
    }
}
