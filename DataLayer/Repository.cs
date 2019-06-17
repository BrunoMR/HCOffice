namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Connections;
    using DTOLayer;
    using Extensions;
    using ServiceStack.OrmLite;

    /// <summary>The repository.</summary>
    /// <typeparam name="T">The Entity</typeparam>
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        /// <summary>
        /// The connection dapper.
        /// </summary>
        protected readonly ConnectionDapper ConnectionDapper = ConnectionDapper.RetornaInstancia();

        /// <summary>
        /// The connection orm lite.
        /// </summary>
        private readonly Connections.OrmLiteConnection connectionOrmLite = Connections.OrmLiteConnection.GetInstance();

        /// <inheritdoc />
        /// <summary>The get all.</summary>
        /// <returns>Returns a list of Entity</returns>
        public List<T> GetAll()
        {
            return connectionOrmLite.OpenConnection().Select<T>();
        }

        /// <inheritdoc />
        /// <summary>The get all.</summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="count">The count.</param>
        /// <returns>Returns a list of Entity</returns>
        public List<T> GetAll(int pageNumber, int pageSize, out int count)
        {
            var connection = connectionOrmLite.OpenConnection();
            count = (int)connection.Count<T>();
            return connection.Select<T>(x => x.Page(pageNumber, pageSize));
        }

        /// <inheritdoc />
        /// <summary>The get.</summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Returns a list of Entity</returns>
        public List<T> Get(Expression<Func<T, bool>> predicate)
        {
            return connectionOrmLite.OpenConnection().Select(predicate);
        }

        /// <inheritdoc />
        /// <summary>The get first or default.</summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Returns the Entity by predicate</returns>
        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return connectionOrmLite.OpenConnection().Single(predicate);
        }

        /// <inheritdoc />
        /// <summary>The find by id.</summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns the entity by Id</returns>
        public T FindById(int id)
        {
            return connectionOrmLite.OpenConnection().SingleById<T>(id);
        }

        /// <inheritdoc />
        /// <summary>The add.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Returns the entity</returns>
        public T Add(T entity)
        {
            entity.Id = (int)connectionOrmLite.OpenConnection().Insert(entity, true);
            return entity;
        }

        /// <inheritdoc />
        /// <summary>The update.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Returns the entity </returns>
        public T Update(T entity)
        {
            connectionOrmLite.OpenConnection().Update(entity);
            return entity;
        }

        /// <inheritdoc />
        /// <summary>The remove by id</summary>
        /// <param name="id">The id.</param>
        public void Remove(int id)
        {
            this.connectionOrmLite.OpenConnection().DeleteById<T>(id);
        }

        /// <summary>The find by id async.</summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns the entity by Id</returns>
        public async Task<T> FindByIdAsync(int id)
        {
            return await connectionOrmLite.OpenConnection().SingleByIdAsync<T>(id);
        }
    }
}
