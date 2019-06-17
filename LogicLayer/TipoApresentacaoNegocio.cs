using System.Collections.Generic;
using System.Linq;
using DTOLayer;
using ExtratorDeDados.Repository;
using Utils;

namespace ExtratorDeDados.Negocio
{
    public class TipoApresentacaoNegocio
    {
        private static List<TipoApresentacao> _tipoApresentacaoList;

        private static List<TipoApresentacao> Search(TipoApresentacao model)
        {
            ARepositorySelect<TipoApresentacao> aRepositorySelect = new TipoApresentacaoRepository();

            return aRepositorySelect.Buscar(model);
        }

        /// <summary>
        /// Retorna se existe a apresentação passada por parâmetro
        /// </summary>
        /// <param name="apresentacao">Tipo de apresentação que será pesquisada</param>
        /// <returns></returns>
        public bool ExistsTipoApresentacao(string apresentacao)
        {
            return _tipoApresentacaoList.Any(tipoApresentacao => tipoApresentacao.Descricao.RemoveDiacritics().Contains(apresentacao.RemoveDiacritics()));
        }

        public static void FindAllTipoApresentacao()
        {
            _tipoApresentacaoList = Search(new TipoApresentacao());
        }

        public static int? FindByDescription(string apresentacao)
        {
            if (apresentacao == null)
                return null;
            var firstOrDefault = _tipoApresentacaoList.FirstOrDefault(x => x.Descricao.RemoveDiacritics().Contains(apresentacao.RemoveDiacritics()));
            return firstOrDefault?.Tipo;
        }
    }
}
