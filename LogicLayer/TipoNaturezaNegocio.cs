using System.Collections.Generic;
using System.Linq;
using DTOLayer;
using ExtratorDeDados.Repository;
using Utils;

namespace ExtratorDeDados.Negocio
{
    public class TipoNaturezaNegocio
    {
        private static List<TipoNatureza> _tipoNaturezaList;

        private static List<TipoNatureza> Search(TipoNatureza model)
        {
            ARepositorySelect<TipoNatureza> aRepositorySelect = new TipoNaturezaRepository();

            return aRepositorySelect.Buscar(model);
        }

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
            _tipoNaturezaList = Search(new TipoNatureza());
        }

        public static int? FindByDescription(string natureza)
        {
            if (natureza == null)
                return null;
            var firstOrDefault = _tipoNaturezaList.FirstOrDefault(x => x.Descricao.RemoveDiacritics().Contains(natureza.RemoveDiacritics()));
            return firstOrDefault?.Tipo;
        }
    }
}
