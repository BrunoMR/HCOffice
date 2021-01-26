using DataLayer.Implementations;
using DataLayer.Interfaces;

namespace BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using DataLayer;
    using DTOLayer;

    public class Cfe4Negocio : ICfe4Negocio
    {
        /// <summary>
        /// The cfe 4 repository.
        /// </summary>
        private readonly ICfe4Repository _cfe4Repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cfe4Negocio"/> class.
        /// </summary>
        /// <param name="cfe4Repository">
        /// The cfe 4 repository.
        /// </param>
        public Cfe4Negocio(ICfe4Repository cfe4Repository)
        {
            _cfe4Repository = cfe4Repository;
        }

        #region Public Methods

        #region CRUD

        public List<CFE4> GetAll(int pageNumber, int pageSize, out int count)
        {
            return this._cfe4Repository.GetAll(pageNumber, pageSize, out count);
        }

        public CFE4 FindById(int id)
        {
            try
            {
                return this._cfe4Repository.FindById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private CFE4 Add(CFE4 cfe4)
        {
            try
            {
                return this._cfe4Repository.Add(cfe4);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private CFE4 Update(CFE4 cfe4)
        {
            try
            {
                return this._cfe4Repository.Update(cfe4);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public CFE4 AddOrUpdate(CFE4 cfe4)
        {
            return cfe4?.Id == null
                ? Add(cfe4)
                : Update(cfe4);
        }

        #endregion CRUD

        /// <summary>The insert or update.</summary>
        /// <param name="processos">The processos.</param>
        /// <param name="transaction">The transaction.</param>
        //public void InsertOrUpdate(List<ProcessoImported> processos, SqlTransaction transaction)
        //{
        //    this._cfe4Repository.BulkUpsert(CreateDataTable(processos), transaction);
        //}
		public void InsertOrUpdate(List<ProcessoImported> processos, SqlTransaction transaction)
		{
			ICfe4Repository cfe4Repository = new Cfe4Repository();
			cfe4Repository.BulkUpsert(CreateDataTable(processos), transaction);
		}

		/// <summary>
		/// Retorna se existe o código CFE4 passado por parâmetro
		/// </summary>
		/// <param name="codigo">Código CFE4</param>
		/// <returns></returns>
		public static bool ExistsCfe4(string codigo)
        {
            return _cfe4List.Any(cfe4 => cfe4.CodigoCfe4.Contains(codigo.Trim()));
        }

        /// <summary>
        ///  O método irá popular o campo "_cfe4List" com todos os códigos CFE4 encontrados no banco
        /// </summary>
        public static void FindAllCfe4()
        {
            ICfe4Repository aRepositorySelect = new Cfe4Repository();
            _cfe4List = aRepositorySelect.GetAll();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///  Método que irá criar a tabela com os dados que serão utilizados para inserir ou atualizar a tabela PROCESSO_CFE4
        /// </summary>
        /// <param name="processos"></param>
        /// <returns></returns>
        private static DataTable CreateDataTable(List<ProcessoImported> processos)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("NUMERO_PROCESSO", typeof(string));
            dataTable.Columns.Add("CODIGO_CFE4", typeof(string));

            processos.ForEach(pro =>
            {
                pro.ClasseVienna?.Cfe4S.ForEach(cfe4 =>
                {
                    dataTable.Rows.Add(pro.NumeroProcesso,
                        cfe4.CodigoCfe4.Trim());
                });
            });

            return dataTable;
        }

        private static List<CFE4> _cfe4List;

        #endregion Private Methods
    }
}
