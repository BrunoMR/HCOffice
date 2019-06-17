using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOLayer;
using ExtratorDeDados.Repository;

namespace ExtratorDeDados.Negocio
{
    public class Cfe4Negocio
    {
        #region Public Methods
        public static void InsertOrUpdate(List<Processo> processos, SqlTransaction transaction)
        {
            var cfe4Repository = new Cfe4Repository();
            cfe4Repository.InsertOrUpdate(CreateDataTable(processos), transaction);
        }

        /// <summary>
        /// Retorna se existe o código CFE4 passado por parâmetro
        /// </summary>
        /// <param name="codigo">Código CFE4</param>
        /// <returns></returns>
        public bool ExistsCfe4(string codigo)
        {
            return _cfe4List.Any(cfe4 => cfe4.Codigo_CFE4.Contains(codigo.Trim()));
        }

        public static void FindAllCfe4()
        {
            _cfe4List = Search(new CFE4());
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///  Método que irá criar a tabela com os dados que serão utilizados para inserir ou atualizar a tabela PROCESSO_CFE4
        /// </summary>
        /// <param name="processos"></param>
        /// <returns></returns>
        private static DataTable CreateDataTable(List<Processo> processos)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("NUMERO_PROCESSO", typeof(string));
            dataTable.Columns.Add("CODIGO_CFE4", typeof(string));

            processos.ForEach(pro =>
            {
                pro.ClasseVienna?.Cfe4S.ForEach(cfe4 =>
                {
                    dataTable.Rows.Add(pro.NumeroProcesso,
                        cfe4.Codigo_CFE4.Trim());
                });
            });

            return dataTable;
        }

        private static List<CFE4> _cfe4List; 
        private static List<CFE4> Search(CFE4 model)
        {
            ARepositorySelect<CFE4> aRepositorySelect = new Cfe4Repository();

            return aRepositorySelect.Buscar(model);
        }

        #endregion Private Methods
    }
}
