using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer
{
    using System.Collections.Generic;
    using DataLayer;
    using DTOLayer;

    public class PaisNegocio : IPaisNegocio
    {
        readonly IPaisRepository _paisRepository = new PaisRepository();
        private static List<Pais> _paisList;

        public List<Pais> PaisList
        {
            get { return _paisList ?? (_paisList = GetAllAsync()); }
            set { _paisList = value; }
        }

        #region CRUD

        public List<Pais> GetAllAsync()
        {
            return _paisRepository.GetAllAsync();
        }

        public async Task<Pais> FindByIdAsync(int id)
        {
            return await _paisRepository.FindByIdAsync(id);
        }

        private Pais Add(Pais pais)
        {
            return _paisRepository.Add(pais);
        }

        private Pais Update(Pais pais)
        {
            return _paisRepository.Update(pais);
        }

        public Pais AddOrUpdate(Pais pais)
        {
            return pais?.Id == null
                ? Add(pais)
                : Update(pais);
        }

        public void Remove(int id)
        {
            _paisRepository.Remove(id);
        }

        #endregion CRUD

        /// <summary>The country exists.</summary>
        /// <param name="sigla">The sigla.</param>
        /// <returns>Returns if found country by Sigla.</returns>
        public bool CountryExists(string sigla)
        {
            return PaisList.Any(pais =>
            {
                var result = pais.Sigla?.Trim().Equals(sigla?.Trim());
                return result != null && (bool)result;
            });
        }
    }
}
