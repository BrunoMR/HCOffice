namespace BusinessLayer
{
    using System.Collections.Generic;
    using System.Linq;
    using DataLayer;
    using DTOLayer;

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
            IConfiguracaoRepository aRepositorySelect = new ConfiguracaoRepository();

            return aRepositorySelect.Find(model);
        }

        #endregion Private Methods
    }
}