using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DTOLayer;
using ExtratorDeDados.Repository;
using Utils;

namespace ExtratorDeDados.Negocio
{
    public class ClasseNegocio
    {
        private static List<Classe> _classeList;

        #region Public Methods

        /// <summary>
        /// Retorna se existe a classe passada por parâmetro
        /// </summary>
        /// <param name="numeroClasse">Número da Classe a ser pesquisado</param>
        /// <returns></returns>
        public bool ExistsClasse(string numeroClasse)
        {
            return _classeList.Any(classe => classe.Numero_Classe.Contains(numeroClasse));
        }

        /// <summary>
        /// Busca o valor NCL da classe Internacional e concatena ao valor da classe
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="numeroRpi"></param>
        /// <param name="numeroProcesso"></param>
        /// <returns></returns>
        public static string BuildCodeClasseNice(string codigo, string numeroProcesso)
        {
            var numeroRpi = RpiNegocio.CurrentRpi?.NumeroRpi.ToString();
            var nclEdition = FindNiceInDirectory(numeroRpi, numeroProcesso);
            
            var codeClasseNice = string.Format("N{0}{1}", 
                nclEdition, 
                codigo);

            return codeClasseNice;
        }

        /// <summary>
        /// Método irá buscar no arquivo criado com NCL de Cada RPI, caso ele tenha sido importado por XML, já que os arquivos
        /// importados por TXT já tenham o número NCL 
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="numeroProcesso"></param>
        /// <returns></returns>
        public static string RetrieveCodeClasseNiceIfFromXml(string codigo, string numeroProcesso)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return null;
            return codigo.ToUpper().StartsWith("N") && !(RegularExpressions.CodeClasseNiceFromTxt.IsMatch(codigo))
                ? BuildCodeClasseNice(codigo, numeroProcesso) 
                : codigo;
        }

        public static string BuildCodeClasseNice(GroupCollection values)
        {
            var nclEdition = values[2].Length < 2 ? "0" + values[2].Value : values[2].Value;
            var codigo = values[3].Value;

            var codeClasseNice = string.Format("N{0}{1}",
                nclEdition,
                codigo);
            
            return codeClasseNice;
        }

        public static string BuildCodeClasseNiceOldTxt(GroupCollection values)
        {
            var nclEdition = values[1].Length < 2 ? "0" + values[1].Value : values[1].Value;
            var codigo = values[2].Value;

            var codeClasseNice = string.Format("N{0}{1}",
                nclEdition,
                codigo);

            return codeClasseNice;
        }

        /// <summary>
        /// Retorna todas classes cadastradas para a propriedade _classeList
        /// </summary>
        public static void FindAllClasses()
        {
            _classeList = Search(new Classe());
        }

        /// <summary>
        /// Concatena o valor da Classe Nacional com o valor da SubClasse referenciada por parametro
        /// </summary>
        /// <param name="processo"></param>
        /// <param name="subClasse"></param>
        /// <returns></returns>
        public static string BuildClasseNacionalAndSubClasse(Processo processo, int subClasse)
        {
            return processo.ClasseNacional?.SubClassesNacional?.SubClasseNacionais.Count > subClasse
                ? (processo.ClasseNacional.Codigo +
                  processo.ClasseNacional.SubClassesNacional.SubClasseNacionais[subClasse].Codigo).Trim()
                : null;
        }

        public static string FindNiceInDirectory(string numeroRpi, string numeroProcesso)
        {
            var path = ConfiguracaoNegocio.FindValueByDescription("BUSCA NCL");
            if (path == null)
                throw new Exception("Caminho NCL não encontrado");

            var fileRpi = Directory.GetFiles(path, "*.txt", SearchOption.TopDirectoryOnly)
                .Where(x => x.Contains("-" + numeroRpi + "."));

            if (!fileRpi.Any())
                throw new Exception(string.Format("Não foi encontrado arquivo NCL da RPI '{0}'", numeroRpi));

            string nclEdition = null;
            fileRpi.ToList().ForEach(x =>
            {
                var openText = File.OpenText(x);
                var values = openText.ReadToEnd();

                int indexProcesso = values.IndexOf(numeroProcesso, StringComparison.Ordinal);

                if (indexProcesso > 0)
                {
                    int indexProximaVirgula = values.IndexOf(';', indexProcesso) != 0 ? values.IndexOf(';', indexProcesso) + 1 : 0;
                    nclEdition = values.Substring(indexProximaVirgula, 2);
                }
                
                openText.Close();
            });
            
            return nclEdition;
        }

        #endregion Public Methods

        #region Private Methods

        private static List<Classe> Search(Classe model)
        {
            ARepositorySelect<Classe> aRepositorySelect = new ClasseRepository();

            return aRepositorySelect.Buscar(model);
        }



        #endregion Private Methods

    }
}
