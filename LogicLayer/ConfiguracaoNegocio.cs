using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOLayer;
using ExtratorDeDados.Repository;

namespace ExtratorDeDados.Negocio
{
    public class ConfiguracaoNegocio
    {
        private static List<Configuracao> _configuracaoList;

        #region Public Methods

        public static void FindAllClasses()
        {
            _configuracaoList = Search(new Configuracao());
        }

        public static string FindValueByDescription(string descricao)
        {
            return _configuracaoList?.FirstOrDefault(x => x.Descricao.Equals(descricao))?.Valor;
        }

        #endregion Public Methods

        #region Private Methods

        private static List<Configuracao> Search(Configuracao model)
        {
            ARepositorySelect<Configuracao> aRepositorySelect = new ConfiguracaoRepository();

            return aRepositorySelect.Buscar(model);
        }

        #endregion Private Methods
    }
}
