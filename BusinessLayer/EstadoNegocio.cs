namespace BusinessLayer
{
    using System.Collections.Generic;
    using DataLayer;
    using DTOLayer;
    using System.Threading.Tasks;

    public class EstadoNegocio : IEstadoNegocio
    {
        readonly IEstadoRepository _estadoRepository = new EstadoRepository();
        
        #region CRUD

        public List<Uf> GetAllAsync()
        {
            return _estadoRepository.GetAllAsync();
        }

        public async Task<Uf> FindByIdAsync(int id)
        {
            return await _estadoRepository.FindByIdAsync(id);
        }

        private Uf Add(Uf uf)
        {
            return _estadoRepository.Add(uf);
        }

        private Uf Update(Uf uf)
        {
            return _estadoRepository.Update(uf);
        }

        public Uf Save(Uf uf)
        {
            return uf?.Id == null
                ? Add(uf)
                : Update(uf);
        }

        public void Remove(int id)
        {
            _estadoRepository.Remove(id);
        }

        #endregion CRUD
    }
}
