using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using DTOLayer;

namespace BusinessLayer.Implementations
{
    public class TitularBusiness : ITitularBusiness
    {
        private readonly ITitularRepository _titularRepository;

        public TitularBusiness(ITitularRepository titularRepository)
        {
            _titularRepository = titularRepository;
        }

        /// <summary>The remove second titular.</summary>
        /// <param name="rpi">The rpi.</param>
        public static void RemoveSecondTitular(RpiImported rpi)
        {
            rpi.Processo
                .AsParallel()
                .ForAll(pro =>
                    {
                        if (pro.Titulares != null)
                        {
                            var existsAndMoreThanOne = pro.Titulares.Titular.Any() && pro.Titulares.Titular.Count > 1;
                            if (existsAndMoreThanOne)
                            {
                                pro.Titulares.Titular.RemoveAll(x => string.IsNullOrWhiteSpace(x.Pais));
                            }
                        }
                    });
        }

        public void InsertOrUpdate(List<ProcessoImported> processos, SqlTransaction transaction)
        {
            _titularRepository.BulkUpsert(CreateDataTable(processos), transaction);
        }

        private static DataTable CreateDataTable(List<ProcessoImported> processos)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("NUMERO_PROCESSO", typeof(string));
            dataTable.Columns.Add("NOME_TITULAR", typeof(string));
            dataTable.Columns.Add("CPF_CNPJ_INPI_TITULAR", typeof(string));
            dataTable.Columns.Add("PAIS_TITULAR", typeof(string));
            dataTable.Columns.Add("UF_TITULAR", typeof(string));

            processos.ForEach(pro =>
            {
                pro.Titulares?.Titular.ForEach(tit =>
                {
                    dataTable.Rows.Add(pro.NumeroProcesso,
                        tit.Nome.Trim(),
                        tit.CpfCnpj,
                        tit.Pais,
                        tit.Uf);
                });
            });

            return dataTable;
        }
    }
}