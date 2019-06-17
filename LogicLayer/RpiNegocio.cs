using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DTOLayer;
using ExtratorDeDados.Repository;
using ExtratorDeDados.Validation;
using FluentValidation.Results;
using Utils;

namespace ExtratorDeDados.Negocio
{
    public class RpiNegocio
    {
        public static RPI CurrentRpi { get; set; }

        public List<ValidationResult> ProcessRpi(List<RPI> rpis)
        {
            // Carrega os dados
            LoadData();
            
            var validationResults = ValidateRpis(rpis);

            // Verifica se aconteceu algum erro na validação dos arquivos
            if (validationResults.Any(x => x.IsValid == false))
            {
                ValidationErrors(validationResults);
            }
            else
            {
                InsertTransactions(rpis);
            }
            
            return validationResults;
        }

        private static void AddRpi(RPI rpi, SqlTransaction transaction)
        {
            var rpiRepository = new RpiRepository();
            rpiRepository.AddRpi(rpi, transaction);
        }

        private static void InsertTransactions(List<RPI> rpis)
        {
            rpis.ForEach(rpi =>
            {
                var conexao = Conexao.RetornaInstancia();
                var connection = conexao.AbreConexao();
                var transaction = connection.BeginTransaction(IsolationLevel.Serializable, "RPI "+ rpi.NumeroRpi);
                
                try
                {
                    AddRpi(rpi, transaction);
                    ProcessoNegocio.InsertOrUpdate(rpi.Processo, transaction);
                    ProtocoloNegocio.BulkInsert(rpi.Processo, transaction);
                    var processoDespachoList = ProcessoDespachoNegocio.BuildProcessoDespachos(rpi);
                    ProcessoDespachoNegocio.BulkInsert(processoDespachoList, transaction);
                    Cfe4Negocio.InsertOrUpdate(rpi.Processo, transaction);

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

        private static List<ValidationResult> ValidateRpis(IEnumerable<RPI> rpis)
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

        private static void ValidationErrors(IEnumerable<ValidationResult> validationResults)
        {
            var allErrors = new StringBuilder();
            validationResults
                .Where(x => x.IsValid == false)
                .ForEach(x => x.Errors.ForEach(e => { allErrors.AppendLine(e.ErrorMessage); }));
            // Escreve todos os erros
            WriteErrors(allErrors);
        }

        private static void WriteErrors(StringBuilder allErrors)
        {
            var path = ConfiguracaoNegocio.FindValueByDescription("ARQUIVO ERRO");
            System.IO.File.WriteAllText(@path +"Erros.txt", allErrors.ToString());
        }

        private static void LoadData()
        {
            Conexao conexao = Conexao.RetornaInstancia();
            conexao.AbreConexao();

            ConfiguracaoNegocio.FindAllClasses();
            ClasseNegocio.FindAllClasses();
            Cfe4Negocio.FindAllCfe4();
            DespachoNegocio.FindAllDespacho();
            TipoApresentacaoNegocio.FindAllTipoApresentacao();
            TipoNaturezaNegocio.FindAllTipoNatureza();

            conexao.FechaConexao();
        }
    }
}
