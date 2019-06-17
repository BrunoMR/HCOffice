namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using DTOLayer;

    /// <summary>The Repository interface.</summary>
    /// <typeparam name="T">The Entity</typeparam>
    public interface IRepository<T> where T : IEntity
    {
        /// <summary>The get all.</summary>
        /// <returns>The <see cref="List"/>List of Entity</returns>
        List<T> GetAll();

        /// <summary>The get all.</summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="count">The count.</param>
        /// <returns>Returns a list of Entity</returns>
        List<T> GetAll(int pageNumber, int pageSize, out int count);

        /// <summary>The get.</summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Returns a list of Entity</returns>
        List<T> Get(Expression<Func<T, bool>> predicate);

        /// <summary>The get first or default.</summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Returns the Entity by predicate</returns>
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>The find by id.</summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns the entity by Id</returns>
        T FindById(int id);

        /// <summary>The add.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Returns the entity</returns>
        T Add(T entity);

        /// <summary>The update.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Returns the entity </returns>
        T Update(T entity);

        /// <summary>The remove by id</summary>
        /// <param name="id">The id.</param>
        void Remove(int id);

        /// <summary>The find by id async.</summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns the entity by Id</returns>
        Task<T> FindByIdAsync(int id);
    }
}
