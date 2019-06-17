using System;

namespace BusinessLayer
{
    using System.Collections.Generic;
    using System.Linq;
    using DataLayer;
    using DTOLayer;
    using Utils;

    public class TipoNaturezaNegocio : ITipoNaturezaNegocio
    {
        private static List<TipoNatureza> _tipoNaturezaList;
        
        /// <summary>
        /// Retorna se existe a natureza passada por parâmetro
        /// </summary>
        /// <param name="natureza">Tipo de Natureza que será pesquisada</param>
        /// <returns></returns>
        public bool ExistsTipoNatureza(string natureza)
        {
            return _tipoNaturezaList.Any(tipoNatureza => tipoNatureza.Descricao.RemoveDiacritics().Contains(natureza.RemoveDiacritics()));
        }

        public static void FindAllTipoNatureza()
        {
            ITipoNaturezaRepository tipoNaturezaRepository = new TipoNaturezaRepository();
            _tipoNaturezaList = tipoNaturezaRepository.GetAll();
        }

        public static int? FindByDescription(string natureza)
        {
            if (natureza == null)
                return null;
            var firstOrDefault = _tipoNaturezaList.FirstOrDefault(x => x.Descricao.RemoveDiacritics().Contains(natureza.RemoveDiacritics()));
            return firstOrDefault?.Id;
        }

        #region CRUD

        public List<TipoNatureza> GetAll()
        {
            try
            {
                ITipoNaturezaRepository tipoNaturezaRepository = new TipoNaturezaRepository();
                return tipoNaturezaRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public TipoNatureza FindById(int id)
        {
            try
            {
                ITipoNaturezaRepository tipoNaturezaRepository = new TipoNaturezaRepository();
                return tipoNaturezaRepository.FindById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private TipoNatureza Add(TipoNatureza tipoNatureza)
        {
            try
            {
                ITipoNaturezaRepository tipoNaturezaRepository = new TipoNaturezaRepository();
                return tipoNaturezaRepository.Add(tipoNatureza);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private TipoNatureza Update(TipoNatureza tipoNatureza)
        {
            try
            {
                ITipoNaturezaRepository tipoNaturezaRepository = new TipoNaturezaRepository();
                return tipoNaturezaRepository.Update(tipoNatureza);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public TipoNatureza Save(TipoNatureza tipoNatureza)
        {
            return tipoNatureza?.Id == null
                ? Add(tipoNatureza)
                : Update(tipoNatureza);
        }
        
        #endregion CRUD
    }
}
