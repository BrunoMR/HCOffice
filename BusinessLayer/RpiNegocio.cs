namespace BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using DataLayer;
    using DataLayer.Connections;
    using DTOLayer;
    using FluentValidation.Results;
    using Validation;

    public class RpiNegocio : IRpiNegocio
    {
        /// <summary>
        /// Gets or sets the current rpi.
        /// </summary>
        public static RpiImported CurrentRpi { get; set; }

        /// <summary>
        /// The cfe 4 repository.
        /// </summary>
        private readonly ICfe4Negocio _cfe4Negocio;

        public RpiNegocio(ICfe4Negocio cfe4Negocio)
        {
            _cfe4Negocio = cfe4Negocio;
        }
        
        #region Public Methods

        /// <summary>The get all.</summary>
        /// <returns>Returns the list of rpi</returns>
        public List<Rpi> GetAll()
        {
            IRpiRepository rpiRepository = new RpiRepository();
            return rpiRepository.GetAll();
        }

        /// <summary>
        /// The process rpi.
        /// </summary>
        /// <param name="rpis">
        /// The rpis.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<ValidationResult> ProcessRpi(List<RpiImported> rpis)
        {
            // Carrega os dados de Despacho, Classe, Cfe4 do banco de dados 
            // que serão utilizados para validar a integridade dos dados importados
            LoadData();

            // Limpa os Logs
            LogProcess.ClearLogs();

            var validationResults = ValidateRpis(rpis);

            // Verifica se aconteceu algum erro na validação dos arquivos
            if (validationResults.Any(x => x.IsValid == false))
            {
                ValidationErrors(validationResults);
            }
            else
            {
                LogProcess.ClearLogs();
                TransactionInserts(rpis);
            }

            return validationResults;
        }

        #endregion Public Methods

        #region Private Methods

        private void AddRpi(RpiImported rpi, SqlTransaction transaction)
        {
            IRpiRepository rpiRepository = new RpiRepository();
            rpiRepository.Add(rpi, transaction);
        }

        /// <summary>
        /// The transaction inserts.
        /// </summary>
        /// <param name="rpis">
        /// The rpis.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        private void TransactionInserts(List<RpiImported> rpis)
        {
            rpis.ForEach(rpi =>
            {
                var conexao = ConnectionDapper.RetornaInstancia();
                var connection = conexao.AbreConexao();
                var transaction = connection.BeginTransaction(IsolationLevel.Serializable, "RPI " + rpi.NumeroRpi);

                try
                {
                    AddRpi(rpi, transaction);
                    
                    SqlConnection.ClearAllPools();
                    ProcessoNegocio.BulkInsertOrUpdate(rpi.Processo, transaction);

                    ProtocoloNegocio.BulkInsert(rpi.Processo, transaction);
                    var processoDespachoList = ProcessoDespachoNegocio.BuildProcessoDespachos(rpi);
                    ProcessoDespachoNegocio.BulkInsert(processoDespachoList, transaction);
                    _cfe4Negocio.InsertOrUpdate(rpi.Processo, transaction);
                    ClasseNegocio.InsertOrUpdate(rpi.NumeroRpi, rpi.Processo, transaction);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    conexao.FechaConexao();
                }

            });

        }

        private List<ValidationResult> ValidateRpis(IEnumerable<RpiImported> rpis)
        {
            var rpiValidation = new RpiValidation();
            var validationResults = new List<ValidationResult>();

            // Valida todos Processso de Cada RPI em Ordem Crescente de RPI
            rpis.OrderBy(x => x.NumeroRpi)
                .ToList()
                .ForEach(rpi =>
                {
                    CurrentRpi = rpi;
                    validationResults.Add(rpiValidation.Validate(rpi));
                });
            return validationResults;
        }

        private void ValidationErrors(IEnumerable<ValidationResult> validationResults)
        {
            var allErrors = new StringBuilder();
            validationResults
                .Where(x => x.IsValid == false)
                .ToList()
                .AsParallel()
                .ForAll(x => x.Errors
                            .ToList()
                            .ForEach(e => { allErrors.AppendLine(e.ErrorMessage); }));
            WriteErrors(allErrors);
        }

        private void WriteErrors(StringBuilder allErrors)
        {
            var path = ConfiguracaoNegocio.FindValueByDescription("ARQUIVO ERRO");
            System.IO.File.WriteAllText(@path + "Erros.txt", allErrors.ToString());
        }

        private void LoadData()
        {
            ConnectionDapper connectionDapper = ConnectionDapper.RetornaInstancia();
            connectionDapper.AbreConexao();

            ConfiguracaoNegocio.FindAllClasses();
            ClasseNegocio.FindAllClasses();
            Cfe4Negocio.FindAllCfe4();
            DespachoNegocio.FindAllDespacho();
            TipoApresentacaoNegocio.FindAllTipoApresentacao();
            TipoNaturezaNegocio.FindAllTipoNatureza();

            var paisNegocio = new PaisNegocio { PaisList = null };

            connectionDapper.FechaConexao();
        }

        #endregion Private Methods
    }
}
