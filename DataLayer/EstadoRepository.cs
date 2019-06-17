namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using DTOLayer;
    using ServiceStack.OrmLite;
    using System.Threading.Tasks;

    public class EstadoRepository : ARepository, IEstadoRepository
    {
        #region CRUD

        public List<Uf> GetAllAsync()
        {
            return ConnectionOrmLite.OpenConnection().Select<Uf>();
        }

        public async Task<Uf> FindByIdAsync(int id)
        {
            return await ConnectionOrmLite.OpenConnection().SingleByIdAsync<Uf>(id);
        }

        public Uf Add(Uf uf)
        {
            uf.Id = (int)ConnectionOrmLite.OpenConnection().Insert(uf, true);
            return uf;
        }

        public Uf Update(Uf uf)
        {
            ConnectionOrmLite.OpenConnection().Update(uf);
            return uf;
        }

        public void Remove(int id)
        {
            ConnectionOrmLite.OpenConnection().DeleteById<Uf>(id);
        }

        #endregion CRUD
    }
}