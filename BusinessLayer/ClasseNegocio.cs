using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using DataLayer;
    using DTOLayer;
    using Utils;

    public class ClasseNegocio : IClasseNegocio
    {
        private static List<Classe> _classeList;

        #region Public Methods

        #region CRUD

        public List<Classe> GetAll()
        {
            try
            {
                IClasseRepository classeRepository = new ClasseRepository();
                return classeRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Classe FindByCode(string code)
        {
            try
            {
                IClasseRepository classeRepository = new ClasseRepository();
                return classeRepository.FindByCodeClasse(code);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private Classe Add(Classe classe)
        {
            try
            {
                IClasseRepository classeRepository = new ClasseRepository();
                classeRepository.Add(classe);
                return classe;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private Classe Update(Classe classe)
        {
            try
            {
                IClasseRepository classeRepository = new ClasseRepository();
                return classeRepository.Update(classe);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Classe Save(Classe classe)
        {
            return classe.IsNew
                ? Add(classe)
                : Update(classe);
        }

        #endregion CRUD

        /// <summary>
        /// Retorna se existe a classe passada por parâmetro
        /// </summary>
        /// <param name="numeroClasse">Número da Classe a ser pesquisado</param>
        /// <returns></returns>
        public bool ExistsClasse(string numeroClasse)
        {
            try
            {
                return _classeList.Any(classe => classe.NumeroClasse.Trim().Equals(numeroClasse.Trim()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
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
            fileRpi
                .ToList()
                .ForEach(x =>
                {
                    var openText = File.OpenText(x);
                    var values = openText.ReadToEnd();

                    int indexProcesso = values.IndexOf(numeroProcesso, StringComparison.Ordinal);

                    if (indexProcesso > -1)
                    {
                        int indexProximaVirgula = values.IndexOf(';', indexProcesso) != 0 ? values.IndexOf(';', indexProcesso) + 1 : 0;
                        nclEdition = values.Substring(indexProximaVirgula, 2);
                    }

                    openText.Close();
                });

            return nclEdition;
        }

        /// <summary>
        /// Concatena o valor da Classe Nacional com o valor da SubClasse referenciada por parametro
        /// </summary>
        /// <param name="processo"></param>
        /// <param name="subClasse"></param>
        /// <returns></returns>
        public static string BuildClasseNacionalAndSubClasse(ProcessoImported processo, int subClasse)
        {
            return processo.ClasseNacional?.SubClassesNacional?.SubClasseNacionais.Count > subClasse
                ? (processo.ClasseNacional.Codigo +
                  processo.ClasseNacional.SubClassesNacional.SubClasseNacionais[subClasse].Codigo).Trim()
                : null;
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
            return !codigo.ToUpper().StartsWith("N") && !(RegularExpressions.CodeClasseNiceFromTxt.IsMatch(codigo))
                ? BuildCodeClasseNice(codigo, numeroProcesso)
                : codigo;
        }

        /// <summary>
        /// Retorna todas classes cadastradas para a propriedade _classeList
        /// </summary>
        public static void FindAllClasses()
        {
            IClasseRepository classeRepository = new ClasseRepository();
            _classeList = classeRepository.GetAll();
        }

        public static void InsertOrUpdate(List<ProcessoImported> processos, SqlTransaction transaction)
        {
            IClasseRepository cfe4Repository = new ClasseRepository();
            cfe4Repository.BulkUpsert(CreateDataTable(processos), transaction);
        }

        #endregion Public Methods

        private static DataTable CreateDataTable(List<ProcessoImported> processos)
        {
            try
            {
                var dataTable = new DataTable();
                dataTable.Columns.Add("NUMERO_PROCESSO", typeof(string));
                dataTable.Columns.Add("NUMERO_CLASSE", typeof(string));
                dataTable.Columns.Add("TIPO_DESCRICAO", typeof(string));
                dataTable.Columns.Add("ESPECIFICACAO", typeof(string));

                processos.ForEach(pro =>
                {
                    pro.ListaClasseNice?.ClassesNice?.ForEach(cla =>
                    {
                        dataTable.Rows.Add(pro.NumeroProcesso,
                            RetrieveCodeClasseNiceIfFromXml(cla.Codigo, pro.NumeroProcesso),
                            cla.Status,
                            cla.Descricao);
                    });
                });

                return dataTable;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}