namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Connections;
    using Dapper;
    using DTOLayer;
    using DTOLayer.Indexes;

    /// <summary>
    /// The sync elastic repository.
    /// </summary>
    public class SyncElasticRepository : ISyncElasticRepository
    {
        /// <summary>The get in range.</summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>The Processes sync List.</returns>
        public List<ProcessoSync> GetInRange(int start, int end)
        {
            var query = new StringBuilder();

            try
            {
                query.AppendLine("SELECT ");
                query.AppendLine("* ");
                query.AppendLine("FROM ");
                query.AppendLine("( ");
                query.AppendLine("SELECT ");
                query.AppendLine("ROW_NUMBER() OVER(ORDER BY PRO.ID) AS Line, ");
                query.AppendLine("PRO.ID AS Id, ");
                query.AppendLine("PRO.NUMERO AS Numero, ");
                query.AppendLine("PRO.NOME_TITULAR AS Titular, ");
                query.AppendLine("PRO.CPF_CNPJ_INPI_TITULAR AS CpfCnpjInpi, ");
                query.AppendLine("PRO.PAIS_TITULAR AS TitularPais, ");
                query.AppendLine("PAIS.NOME AS TitularPaisNome, ");
                query.AppendLine("PRO.UF_TITULAR AS TitularUf, ");
                query.AppendLine("UF.NOME AS TitularUfNome, ");
                query.AppendLine("PRO.NOME_PROCURADOR AS Procurador, ");
                query.AppendLine("PRO.MARCA AS Marca, ");
                query.AppendLine("PRO.MARCA_ORTOGRAFADA AS MarcaOrtografada, ");
                query.AppendLine("PRO.MARCA_NAO_ORTOGRAFADA AS MarcaNaoOrtografada, ");
                query.AppendLine("PRO.MARCA_SEM_VOGAIS AS MarcaSemVogais, ");
                query.AppendLine("PRO.PRIORIDADE AS Prioridade, ");
                query.AppendLine("PRO.DATA_PRIORIDADE AS PrioridadeData, ");
                query.AppendLine("PRO.NOME_PAIS_PRIORIDADE AS PrioridadePais, ");
                query.AppendLine("PRO.ESPECIFICACAO AS Especificacao, ");
                query.AppendLine("PRO.APOSTILA AS Apostila, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_DEPOSITO) AS DataDeposito, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_CONCESSAO) AS DataConcessao, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_REGISTRO) AS DataRegistro, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_VIGENCIA) AS DataVigencia, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_ORDINARIO_INICIAL) AS DataOrdinarioInicial, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_ORDINARIO_FINAL) AS DataOrdinarioFinal, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_EXTRA_ORDINARIO_INICIAL) AS DataExtraOrdinarioInicial, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_EXTRA_ORDINARIO_FINAL) AS DataExtraOrdinarioFinal, ");
                query.AppendLine("TPA.DESCRICAO AS Apresentacao, ");
                query.AppendLine("TPN.DESCRICAO AS Natureza, ");
                query.AppendLine("dbo.GetJustClasse(PRO.CLASSE_INTERNACIONAL) AS ClasseInternacional, ");
                query.AppendLine("dbo.GetJustEditionClasse(PRO.CLASSE_INTERNACIONAL) AS ClasseInternacionalEdicao, ");
                query.AppendLine("CLI.DESCRICAO AS ClasseInternacionalDescricao, ");
                query.AppendLine("dbo.GetJustClasse(PRO.CLASSE_1) AS Classe1, ");
                query.AppendLine("dbo.GetJustSubClasse(PRO.CLASSE_1) AS Classe1Sub, ");
                query.AppendLine("CL1.DESCRICAO AS Classe1SubDescricao, ");
                query.AppendLine("dbo.GetJustClasse(PRO.CLASSE_2) AS Classe2, ");
                query.AppendLine("dbo.GetJustSubClasse(PRO.CLASSE_2) AS Classe2Sub, ");
                query.AppendLine("CL2.DESCRICAO AS Classe2SubDescricao, ");
                query.AppendLine("dbo.GetJustClasse(PRO.CLASSE_3) AS Classe3, ");
                query.AppendLine("dbo.GetJustSubClasse(PRO.CLASSE_3) AS Classe3Sub, ");
                query.AppendLine("CL3.DESCRICAO AS Classe3SubDescricao, ");
                query.AppendLine("CFE.CODIGO_CFE4 AS Cfe4, ");
                query.AppendLine("CFE.DESCRICAO AS Cfe4Descricao, ");
                query.AppendLine("DEP.CODIGO AS DespachoCodigo, ");
                query.AppendLine("DEP.DESCRICAO AS DespachoDescricao, ");
                query.AppendLine("DEP.DESCRICAO_COMPLETA AS DespachoDescricaoCompleta, ");
                query.AppendLine("DSI.DESCRICAO AS DespachoSituacao, ");
                query.AppendLine("DTP.TIPO AS DespachoTipo, ");
                query.AppendLine("DTP.DESCRICAO AS DespachoTipoDescricao, ");
                query.AppendLine("PRD.NUMERO_RPI AS DespachoRpi, ");
                query.AppendLine("CONVERT(DATE, PRD.DATA_DESPACHO) AS DespachoData, ");
                query.AppendLine("PRD.COMPLEMENTO AS DespachoComplemento, ");
                query.AppendLine("PRT.NUMERO AS ProtocoloNumero, ");
                query.AppendLine("PRT.CODIGO_SERVICO AS ProtocoloCodigo, ");
                query.AppendLine("CONVERT(DATE, PRT.DATA) AS ProtocoloData, ");
                query.AppendLine("PRT.NOME_RAZAO_SOCIAL AS ProtocoloNomeRazaoSocial, ");
                query.AppendLine("PRT.PAIS AS ProtocoloPais, ");
                query.AppendLine("PRT.UF AS ProtocoloUf ");
                query.AppendLine("FROM ");
                query.AppendLine("PROCESSO                      PRO ");
                query.AppendLine("LEFT JOIN TIPO_APRESENTACAO   TPA ON TPA.ID = PRO.TIPO_APRESENTACAO ");
                query.AppendLine("LEFT JOIN TIPO_NATUREZA       TPN ON TPN.ID = PRO.TIPO_NATUREZA ");
                query.AppendLine("LEFT JOIN PROCESSO_CFE4       PR4 ON PR4.ID_PROCESSO = PRO.ID ");
                query.AppendLine("LEFT JOIN CFE4                CFE ON CFE.ID = PR4.ID_CFE4 ");
                query.AppendLine("LEFT JOIN PROCESSO_DESPACHO   PRD ON PRD.ID_PROCESSO = PRO.ID ");
                query.AppendLine("LEFT JOIN DESPACHO            DEP ON DEP.ID = PRD.ID_DESPACHO ");
                query.AppendLine("LEFT JOIN PROTOCOLO           PRT ON PRT.ID = PRD.ID_PROTOCOLO ");
                query.AppendLine("LEFT JOIN TIPO_SITUACAO       DSI ON DSI.TIPO = DEP.SITUACAO ");
                query.AppendLine("LEFT JOIN TIPO_DESPACHO       DTP ON DTP.TIPO = DEP.TIPO ");
                query.AppendLine("LEFT JOIN CLASSE CLI ON CLI.NUMERO_CLASSE = PRO.CLASSE_INTERNACIONAL ");
                query.AppendLine("LEFT JOIN CLASSE CL1 ON CL1.NUMERO_CLASSE = PRO.CLASSE_1 ");
                query.AppendLine("LEFT JOIN CLASSE CL2 ON CL2.NUMERO_CLASSE = PRO.CLASSE_2 ");
                query.AppendLine("LEFT JOIN CLASSE CL3 ON CL3.NUMERO_CLASSE = PRO.CLASSE_3 ");
                query.AppendLine("LEFT JOIN PAIS PAIS ON PAIS.SIGLA = PRO.PAIS_TITULAR ");
                query.AppendLine("LEFT JOIN UF UF ON UF.SIGLA = PRO.UF_TITULAR ");
                query.AppendLine(") RE ");
                query.AppendLine("WHERE ");
                query.AppendLine("RE.Line BETWEEN @start AND @end");

                var connection = ConnectionDapper.RetornaInstancia().AbreConexao();
                return connection.Query<ProcessoSync>(query.ToString(), new { start, end }, commandTimeout: 1800).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível Sincronizar " + query, ex.InnerException);
            }
        }

        /// <summary>The get count to sync.</summary>
        /// <returns>The count of processes to sync.</returns>
        public int GetCountToSync()
        {
            try
            {
                var query = new StringBuilder();
                query.AppendLine("SELECT ");
                query.AppendLine("COUNT(1) AS Total ");
                query.AppendLine("FROM ");
                query.AppendLine("PROCESSO                      PRO ");
                query.AppendLine("LEFT JOIN TIPO_APRESENTACAO   TPA ON TPA.ID = PRO.TIPO_APRESENTACAO ");
                query.AppendLine("LEFT JOIN TIPO_NATUREZA       TPN ON TPN.ID = PRO.TIPO_NATUREZA ");
                query.AppendLine("LEFT JOIN PROCESSO_CFE4       PR4 ON PR4.ID_PROCESSO = PRO.ID ");
                query.AppendLine("LEFT JOIN CFE4                CFE ON CFE.ID = PR4.ID_CFE4 ");
                query.AppendLine("LEFT JOIN PROCESSO_DESPACHO   PRD ON PRD.ID_PROCESSO = PRO.ID ");
                query.AppendLine("LEFT JOIN DESPACHO            DEP ON DEP.ID = PRD.ID_DESPACHO ");
                query.AppendLine("LEFT JOIN PROTOCOLO           PRT ON PRT.ID = PRD.ID_PROTOCOLO ");
                query.AppendLine("LEFT JOIN TIPO_SITUACAO       DSI ON DSI.TIPO = DEP.SITUACAO ");
                query.AppendLine("LEFT JOIN TIPO_DESPACHO       DTP ON DTP.TIPO = DEP.TIPO ");

                var connection = ConnectionDapper.RetornaInstancia().AbreConexao();
                return connection.Query<int>(query.ToString(), commandTimeout: 1200).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível Sincronizar ", ex.InnerException);
            }
        }

        /// <summary>
        /// The get all ids of processo.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<ProcessoIds> GetAllIdsOfProcesso()
        {
            var query = new StringBuilder();
            query.AppendLine("SELECT ");
            query.AppendLine("ID AS Id ");
            query.AppendLine("FROM ");
            query.AppendLine("PROCESSO ");
            query.AppendLine("ORDER BY ");
            query.AppendLine("ID ");

            var connection = ConnectionDapper.RetornaInstancia().AbreConexao();
            return connection.Query<ProcessoIds>(query.ToString(), commandTimeout: 1200).ToList();
        }

        /// <summary>
        /// The get full processos.
        /// </summary>
        /// <param name="modelDataTable">
        /// The model data table.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public IEnumerable<ProcessoSync> GetFullProcessos(DataTable modelDataTable)
        {
            try
            {
                return ConnectionDapper.RetornaInstancia().AbreConexao()
                        .Query<ProcessoSync>("GET_FULL_PROCESSOS", new
                        {
                            idProcesso = modelDataTable.AsTableValuedParameter("[dbo].[PROCESSOIDTYPE]")
                        },
                        commandType: CommandType.StoredProcedure, commandTimeout: 1200).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível retornar os Processos!", ex.InnerException);
            }
        }

        /// <summary>
        ///  Returns the processes using filter`s of rpi
        /// </summary>
        /// <param name="startRpi">Rpi start of filter</param>
        /// <param name="endRpi">Rpi end of filter</param>
        /// <returns>List of processes to sync</returns>
        public IEnumerable<ProcessoSync> GetFullProcessosByRpi(int startRpi, int endRpi)
        {
            try
            {
                var query = new StringBuilder();
                query.AppendLine("SELECT ");
                query.AppendLine("PRO.ID AS Id, ");
                query.AppendLine("PRO.NUMERO AS Numero, ");
                query.AppendLine("PRO.NOME_TITULAR AS Titular, ");
                query.AppendLine("PRO.CPF_CNPJ_INPI_TITULAR AS CpfCnpjInpi, ");
                query.AppendLine("PRO.PAIS_TITULAR AS TitularPais, ");
                query.AppendLine("PAIS.NOME AS TitularPaisNome, ");
                query.AppendLine("PRO.UF_TITULAR AS TitularUf, ");
                query.AppendLine("UF.NOME AS TitularUfNome, ");
                query.AppendLine("PRO.NOME_PROCURADOR AS Procurador, ");
                query.AppendLine("PRO.MARCA AS Marca, ");
                query.AppendLine("PRO.MARCA_ORTOGRAFADA AS MarcaOrtografada, ");
                query.AppendLine("PRO.MARCA_NAO_ORTOGRAFADA AS MarcaNaoOrtografada, ");
                query.AppendLine("PRO.MARCA_SEM_VOGAIS AS MarcaSemVogais, ");
                query.AppendLine("PRO.PRIORIDADE AS Prioridade, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_PRIORIDADE) AS PrioridadeData, ");
                query.AppendLine("PRO.NOME_PAIS_PRIORIDADE AS PrioridadePais, ");
                query.AppendLine("PRO.ESPECIFICACAO AS Especificacao, ");
                query.AppendLine("PRO.APOSTILA AS Apostila, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_DEPOSITO) AS DataDeposito, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_CONCESSAO) AS DataConcessao, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_REGISTRO) AS DataRegistro, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_VIGENCIA) AS DataVigencia, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_ORDINARIO_INICIAL) AS DataOrdinarioInicial, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_ORDINARIO_FINAL) AS DataOrdinarioFinal, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_EXTRA_ORDINARIO_INICIAL) AS DataExtraOrdinarioInicial, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_EXTRA_ORDINARIO_FINAL) AS DataExtraOrdinarioFinal, ");
                query.AppendLine("TPA.DESCRICAO AS Apresentacao, ");
                query.AppendLine("TPN.DESCRICAO AS Natureza, ");
                query.AppendLine("dbo.GetJustClasse(PRO.CLASSE_INTERNACIONAL) AS ClasseInternacional, ");
                query.AppendLine("dbo.GetJustEditionClasse(PRO.CLASSE_INTERNACIONAL) AS ClasseInternacionalEdicao, ");
                query.AppendLine("CLI.DESCRICAO AS ClasseInternacionalDescricao, ");
                query.AppendLine("dbo.GetJustClasse(PRO.CLASSE_1) AS Classe1, ");
                query.AppendLine("dbo.GetJustSubClasse(PRO.CLASSE_1) AS Classe1Sub, ");
                query.AppendLine("CL1.DESCRICAO AS Classe1SubDescricao, ");
                query.AppendLine("dbo.GetJustClasse(PRO.CLASSE_2) AS Classe2, ");
                query.AppendLine("dbo.GetJustSubClasse(PRO.CLASSE_2) AS Classe2Sub, ");
                query.AppendLine("CL2.DESCRICAO AS Classe2SubDescricao, ");
                query.AppendLine("dbo.GetJustClasse(PRO.CLASSE_3) AS Classe3, ");
                query.AppendLine("dbo.GetJustSubClasse(PRO.CLASSE_3) AS Classe3Sub, ");
                query.AppendLine("CL3.DESCRICAO AS Classe3SubDescricao, ");
                query.AppendLine("CFE.CODIGO_CFE4 AS Cfe4, ");
                query.AppendLine("CFE.DESCRICAO AS Cfe4Descricao, ");
                query.AppendLine("DEP.CODIGO AS DespachoCodigo, ");
                query.AppendLine("DEP.DESCRICAO AS DespachoDescricao, ");
                query.AppendLine("DEP.DESCRICAO_COMPLETA AS DespachoDescricaoCompleta, ");
                query.AppendLine("DSI.DESCRICAO AS DespachoSituacao, ");
                query.AppendLine("DTP.TIPO AS DespachoTipo, ");
                query.AppendLine("DTP.DESCRICAO AS DespachoTipoDescricao, ");
                query.AppendLine("PRD.NUMERO_RPI AS DespachoRpi, ");
                query.AppendLine("CONVERT(DATE, PRD.DATA_DESPACHO) AS DespachoData, ");
                query.AppendLine("PRD.COMPLEMENTO AS DespachoComplemento, ");
                query.AppendLine("PRT.NUMERO AS ProtocoloNumero, ");
                query.AppendLine("PRT.CODIGO_SERVICO AS ProtocoloCodigo, ");
                query.AppendLine("CONVERT(DATE, PRT.DATA) AS ProtocoloData, ");
                query.AppendLine("PRT.NOME_RAZAO_SOCIAL AS ProtocoloNomeRazaoSocial, ");
                query.AppendLine("PRT.PAIS AS ProtocoloPais, ");
                query.AppendLine("PRT.UF AS ProtocoloUf, ");

                query.AppendLine("dbo.GetJustClasse(PRC.NUMERO_CLASSE) AS Classe, ");
                query.AppendLine("dbo.GetJustEditionClasse(PRC.NUMERO_CLASSE) AS ClasseEdicao, ");
                query.AppendLine("CLN.DESCRICAO AS ClasseDescricao, ");
                query.AppendLine("TSC.DESCRICAO AS ClasseStatus, ");
                query.AppendLine("PRC.ESPECIFICAO AS EspecificacaoNova ");

                query.AppendLine("FROM ");
                query.AppendLine("PROCESSO_DESPACHO             PRI ");
                query.AppendLine("JOIN PROCESSO                 PRO ON PRO.ID = PRI.ID_PROCESSO");
                query.AppendLine("LEFT JOIN TIPO_APRESENTACAO   TPA ON TPA.ID = PRO.TIPO_APRESENTACAO ");
                query.AppendLine("LEFT JOIN TIPO_NATUREZA       TPN ON TPN.ID = PRO.TIPO_NATUREZA ");
                query.AppendLine("LEFT JOIN PROCESSO_CFE4       PR4 ON PR4.ID_PROCESSO = PRO.ID ");
                query.AppendLine("LEFT JOIN CFE4                CFE ON CFE.ID = PR4.ID_CFE4 ");
                query.AppendLine("LEFT JOIN PROCESSO_CLASSE     PRC ON PRC.ID_PROCESSO = PRO.ID ");
                query.AppendLine("LEFT JOIN CLASSE              CLN ON CLN.NUMERO_CLASSE = PRC.NUMERO_CLASSE ");
                query.AppendLine("LEFT JOIN TIPO_SITUACAO_CLASSE TSC ON TSC.TIPO = PRC.TIPO ");
                query.AppendLine("LEFT JOIN PROCESSO_DESPACHO   PRD ON PRD.ID_PROCESSO = PRO.ID ");
                query.AppendLine("LEFT JOIN DESPACHO            DEP ON DEP.ID = PRD.ID_DESPACHO ");
                query.AppendLine("LEFT JOIN PROTOCOLO           PRT ON PRT.ID = PRD.ID_PROTOCOLO ");
                query.AppendLine("LEFT JOIN TIPO_SITUACAO       DSI ON DSI.TIPO = DEP.SITUACAO ");
                query.AppendLine("LEFT JOIN TIPO_DESPACHO       DTP ON DTP.TIPO = DEP.TIPO ");
                query.AppendLine("LEFT JOIN CLASSE              CLI ON CLI.NUMERO_CLASSE = PRO.CLASSE_INTERNACIONAL ");
                query.AppendLine("LEFT JOIN CLASSE              CL1 ON CL1.NUMERO_CLASSE = PRO.CLASSE_1 ");
                query.AppendLine("LEFT JOIN CLASSE              CL2 ON CL2.NUMERO_CLASSE = PRO.CLASSE_2 ");
                query.AppendLine("LEFT JOIN CLASSE              CL3 ON CL3.NUMERO_CLASSE = PRO.CLASSE_3 ");
                query.AppendLine("LEFT JOIN PAIS                PAIS ON PAIS.SIGLA = PRO.PAIS_TITULAR ");
                query.AppendLine("LEFT JOIN UF                  UF ON UF.SIGLA = PRO.UF_TITULAR ");
                query.AppendLine("WHERE ");
                query.AppendLine("PRI.NUMERO_RPI BETWEEN @startRpi AND @endRpi ");
                //query.AppendLine("ORDER BY ");
                //query.AppendLine("Numero, ");
                //query.AppendLine("DespachoRpi, ");
                //query.AppendLine("DespachoCodigo ");

                var connection = ConnectionDapper.RetornaInstancia().AbreConexao();
                return connection.Query<ProcessoSync>(query.ToString(), new { startRpi, endRpi }, commandTimeout: 1800).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível Sincronizar ", ex.InnerException);
            }
        }

        public IEnumerable<ProcessoSync> GetFullProcessosByRpi2(int startRpi, int endRpi)
        {
            try
            {
                var query = new StringBuilder();
                query.AppendLine("SELECT ");
                query.AppendLine("PRO.ID AS Id, ");
                query.AppendLine("PRO.NUMERO AS Numero, ");
                query.AppendLine("PRO.NOME_TITULAR AS Titular, ");
                query.AppendLine("PRO.CPF_CNPJ_INPI_TITULAR AS CpfCnpjInpi, ");
                query.AppendLine("PRO.PAIS_TITULAR AS TitularPais, ");
                query.AppendLine("PAIS.NOME AS TitularPaisNome, ");
                query.AppendLine("PRO.UF_TITULAR AS TitularUf, ");
                query.AppendLine("UF.NOME AS TitularUfNome, ");
                query.AppendLine("PRO.NOME_PROCURADOR AS Procurador, ");
                query.AppendLine("PRO.MARCA AS Marca, ");
                query.AppendLine("PRO.MARCA_ORTOGRAFADA AS MarcaOrtografada, ");
                query.AppendLine("PRO.MARCA_NAO_ORTOGRAFADA AS MarcaNaoOrtografada, ");
                query.AppendLine("PRO.MARCA_SEM_VOGAIS AS MarcaSemVogais, ");
                query.AppendLine("PRO.PRIORIDADE AS Prioridade, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_PRIORIDADE) AS PrioridadeData, ");
                query.AppendLine("PRO.NOME_PAIS_PRIORIDADE AS PrioridadePais, ");
                query.AppendLine("PRO.ESPECIFICACAO AS Especificacao, ");
                query.AppendLine("PRO.APOSTILA AS Apostila, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_DEPOSITO) AS DataDeposito, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_CONCESSAO) AS DataConcessao, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_REGISTRO) AS DataRegistro, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_VIGENCIA) AS DataVigencia, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_ORDINARIO_INICIAL) AS DataOrdinarioInicial, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_ORDINARIO_FINAL) AS DataOrdinarioFinal, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_EXTRA_ORDINARIO_INICIAL) AS DataExtraOrdinarioInicial, ");
                query.AppendLine("CONVERT(DATE, PRO.DATA_EXTRA_ORDINARIO_FINAL) AS DataExtraOrdinarioFinal, ");
                query.AppendLine("TPA.DESCRICAO AS Apresentacao, ");
                query.AppendLine("TPN.DESCRICAO AS Natureza, ");
                query.AppendLine("dbo.GetJustClasse(PRO.CLASSE_INTERNACIONAL) AS ClasseInternacional, ");
                query.AppendLine("dbo.GetJustEditionClasse(PRO.CLASSE_INTERNACIONAL) AS ClasseInternacionalEdicao, ");
                query.AppendLine("CLI.DESCRICAO AS ClasseInternacionalDescricao, ");
                query.AppendLine("dbo.GetJustClasse(PRO.CLASSE_1) AS Classe1, ");
                query.AppendLine("dbo.GetJustSubClasse(PRO.CLASSE_1) AS Classe1Sub, ");
                query.AppendLine("CL1.DESCRICAO AS Classe1SubDescricao, ");
                query.AppendLine("dbo.GetJustClasse(PRO.CLASSE_2) AS Classe2, ");
                query.AppendLine("dbo.GetJustSubClasse(PRO.CLASSE_2) AS Classe2Sub, ");
                query.AppendLine("CL2.DESCRICAO AS Classe2SubDescricao, ");
                query.AppendLine("dbo.GetJustClasse(PRO.CLASSE_3) AS Classe3, ");
                query.AppendLine("dbo.GetJustSubClasse(PRO.CLASSE_3) AS Classe3Sub, ");
                query.AppendLine("CL3.DESCRICAO AS Classe3SubDescricao, ");
                query.AppendLine("CFE.CODIGO_CFE4 AS Cfe4, ");
                query.AppendLine("CFE.DESCRICAO AS Cfe4Descricao, ");
                query.AppendLine("DEP.CODIGO AS DespachoCodigo, ");
                query.AppendLine("DEP.DESCRICAO AS DespachoDescricao, ");
                query.AppendLine("DEP.DESCRICAO_COMPLETA AS DespachoDescricaoCompleta, ");
                query.AppendLine("DSI.DESCRICAO AS DespachoSituacao, ");
                query.AppendLine("DTP.TIPO AS DespachoTipo, ");
                query.AppendLine("DTP.DESCRICAO AS DespachoTipoDescricao, ");
                query.AppendLine("PRD.NUMERO_RPI AS DespachoRpi, ");
                query.AppendLine("CONVERT(DATE, PRD.DATA_DESPACHO) AS DespachoData, ");
                query.AppendLine("PRD.COMPLEMENTO AS DespachoComplemento, ");
                query.AppendLine("PRT.NUMERO AS ProtocoloNumero, ");
                query.AppendLine("PRT.CODIGO_SERVICO AS ProtocoloCodigo, ");
                query.AppendLine("CONVERT(DATE, PRT.DATA) AS ProtocoloData, ");
                query.AppendLine("PRT.NOME_RAZAO_SOCIAL AS ProtocoloNomeRazaoSocial, ");
                query.AppendLine("PRT.PAIS AS ProtocoloPais, ");
                query.AppendLine("PRT.UF AS ProtocoloUf, ");

                query.AppendLine("dbo.GetJustClasse(PRC.NUMERO_CLASSE) AS Classe, ");
                query.AppendLine("dbo.GetJustEditionClasse(PRC.NUMERO_CLASSE) AS ClasseEdicao, ");
                query.AppendLine("CLN.DESCRICAO AS ClasseDescricao, ");
                query.AppendLine("TSC.DESCRICAO AS ClasseStatus, ");
                query.AppendLine("PRC.ESPECIFICAO AS EspecificacaoNova ");

                query.AppendLine("FROM ");
                query.AppendLine("PROCESSO_DESPACHO             PRI ");
                query.AppendLine("JOIN PROCESSO                 PRO ON PRO.ID = PRI.ID_PROCESSO");
                query.AppendLine("LEFT JOIN TIPO_APRESENTACAO   TPA ON TPA.ID = PRO.TIPO_APRESENTACAO ");
                query.AppendLine("LEFT JOIN TIPO_NATUREZA       TPN ON TPN.ID = PRO.TIPO_NATUREZA ");
                query.AppendLine("LEFT JOIN PROCESSO_CFE4       PR4 ON PR4.ID_PROCESSO = PRO.ID ");
                query.AppendLine("LEFT JOIN CFE4                CFE ON CFE.ID = PR4.ID_CFE4 ");
                query.AppendLine("LEFT JOIN PROCESSO_CLASSE     PRC ON PRC.ID_PROCESSO = PRO.ID ");
                query.AppendLine("LEFT JOIN CLASSE              CLN ON CLN.NUMERO_CLASSE = PRC.NUMERO_CLASSE ");
                query.AppendLine("LEFT JOIN TIPO_SITUACAO_CLASSE TSC ON TSC.TIPO = PRC.TIPO ");
                query.AppendLine("LEFT JOIN PROCESSO_DESPACHO   PRD ON PRD.ID_PROCESSO = PRO.ID ");
                query.AppendLine("LEFT JOIN DESPACHO            DEP ON DEP.ID = PRD.ID_DESPACHO ");
                query.AppendLine("LEFT JOIN PROTOCOLO           PRT ON PRT.ID = PRD.ID_PROTOCOLO ");
                query.AppendLine("LEFT JOIN TIPO_SITUACAO       DSI ON DSI.TIPO = DEP.SITUACAO ");
                query.AppendLine("LEFT JOIN TIPO_DESPACHO       DTP ON DTP.TIPO = DEP.TIPO ");
                query.AppendLine("LEFT JOIN CLASSE              CLI ON CLI.NUMERO_CLASSE = PRO.CLASSE_INTERNACIONAL ");
                query.AppendLine("LEFT JOIN CLASSE              CL1 ON CL1.NUMERO_CLASSE = PRO.CLASSE_1 ");
                query.AppendLine("LEFT JOIN CLASSE              CL2 ON CL2.NUMERO_CLASSE = PRO.CLASSE_2 ");
                query.AppendLine("LEFT JOIN CLASSE              CL3 ON CL3.NUMERO_CLASSE = PRO.CLASSE_3 ");
                query.AppendLine("LEFT JOIN PAIS                PAIS ON PAIS.SIGLA = PRO.PAIS_TITULAR ");
                query.AppendLine("LEFT JOIN UF                  UF ON UF.SIGLA = PRO.UF_TITULAR ");
                query.AppendLine("WHERE ");
                query.AppendLine("PRI.NUMERO_RPI BETWEEN @startRpi AND @endRpi ");
                //query.AppendLine("ORDER BY ");
                //query.AppendLine("Numero, ");
                //query.AppendLine("DespachoRpi, ");
                //query.AppendLine("DespachoCodigo ");

                var connection = ConnectionDapper.RetornaInstancia().AbreConexao();
                return connection.Query<ProcessoSync>(query.ToString(), new { startRpi, endRpi }, commandTimeout: 1800).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível Sincronizar ", ex.InnerException);
            }
        }


        /// <summary>
        /// The get imported processos.
        /// </summary>
        /// <param name="modelDataTable">
        /// The model data table.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public IEnumerable<ProcessoSync> GetImportedProcessos(DataTable modelDataTable)
        {
            try
            {
                return ConnectionDapper.RetornaInstancia().AbreConexao()
                        .Query<ProcessoSync>("GET_IMPORTED_PROCESSOS", new
                        {
                            numeroProcesso = modelDataTable.AsTableValuedParameter("[dbo].[PROCESSONUMEROTYPE]")
                        },
                        commandType: CommandType.StoredProcedure, commandTimeout: 180).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível retornar os Processos Importados ", ex.InnerException);
            }
        }

        /// <summary>
        /// The get all procuradores.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<ProcuradorIndex> GetAllProcuradores()
        {
            var query = new StringBuilder();
            query.AppendLine("SELECT ");
            query.AppendLine("DISTINCT ");
            query.AppendLine("LTRIM(NOME_PROCURADOR) AS Nome ");
            query.AppendLine("FROM ");
            query.AppendLine("PROCESSO ");
            query.AppendLine("WHERE ");
            query.AppendLine("NOME_PROCURADOR IS NOT NULL ");
            query.AppendLine("AND LEN(LTRIM(NOME_PROCURADOR)) > 0 ");
            query.AppendLine("ORDER BY ");
            query.AppendLine("Nome ");

            var connection = ConnectionDapper.RetornaInstancia().AbreConexao();
            return connection.Query<ProcuradorIndex>(query.ToString(), commandTimeout: 1200).ToList();
        }

        /// <summary>
        /// The get all titulares.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<TitularIndex> GetAllTitulares()
        {
            var query = new StringBuilder();
            query.AppendLine("SELECT ");
            query.AppendLine("DISTINCT ");
            query.AppendLine("LTRIM(NOME_TITULAR) AS Nome ");
            query.AppendLine("FROM ");
            query.AppendLine("PROCESSO ");
            query.AppendLine("WHERE ");
            query.AppendLine("NOME_TITULAR IS NOT NULL ");
            query.AppendLine("AND LEN(LTRIM(NOME_TITULAR)) > 0 ");
            query.AppendLine("ORDER BY ");
            query.AppendLine("Nome ");

            var connection = ConnectionDapper.RetornaInstancia().AbreConexao();
            return connection.Query<TitularIndex>(query.ToString(), commandTimeout: 1200).ToList();
        }

        /// <summary>
        /// The get all classes.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<ClasseIndex> GetAllClasses()
        {
            var query = new StringBuilder();
            query.AppendLine("SELECT ");
            query.AppendLine("dbo.GetJustClasse(NUMERO_CLASSE) AS Classe, ");
            query.AppendLine("dbo.GetJustSubClasse(NUMERO_CLASSE) AS SubClasse, ");
            query.AppendLine("dbo.GetJustEditionClasse(NUMERO_CLASSE) AS Edicao ");
            query.AppendLine("FROM ");
            query.AppendLine("CLASSE ");
            query.AppendLine("ORDER BY ");
            query.AppendLine("NUMERO_CLASSE ");

            var connection = ConnectionDapper.RetornaInstancia().AbreConexao();
            return connection.Query<ClasseIndex>(query.ToString(), commandTimeout: 1200).ToList();
        }

        /// <summary>
        /// The get all cfe 4 s.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<Cfe4Index> GetAllCfe4S()
        {
            var query = new StringBuilder();
            query.AppendLine("SELECT ");
            query.AppendLine("CODIGO_CFE4 AS Codigo ");
            query.AppendLine("FROM ");
            query.AppendLine("CFE4 ");
            query.AppendLine("ORDER BY ");
            query.AppendLine("Codigo ");

            var connection = ConnectionDapper.RetornaInstancia().AbreConexao();
            return connection.Query<Cfe4Index>(query.ToString(), commandTimeout: 1200).ToList();
        }

        /// <summary>
        /// The get all despachos.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<DespachoIndex> GetAllDespachos()
        {
            var query = new StringBuilder();
            query.AppendLine("SELECT ");
            query.AppendLine("CODIGO AS Codigo ");
            query.AppendLine("FROM ");
            query.AppendLine("DESPACHO ");
            query.AppendLine("ORDER BY ");
            query.AppendLine("Codigo ");

            var connection = ConnectionDapper.RetornaInstancia().AbreConexao();
            return connection.Query<DespachoIndex>(query.ToString(), commandTimeout: 1200).ToList();
        }

        /// <summary>
        /// The get all class similarities.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<ClasseAfinidadeSync> GetAllClassSimilarities()
        {
            var query = new StringBuilder();
            query.AppendLine("SELECT ");
            query.AppendLine("DISTINCT ");
            query.AppendLine("dbo.GetJustClasse(CAF.NUMERO_CLASSE_A) Classe, ");
            query.AppendLine("dbo.GetJustClasse(CAF.NUMERO_CLASSE_B) ClasseAfinidade ");
            query.AppendLine("FROM ");
            query.AppendLine("CLASSE_AFINIDADE CAF ");
            query.AppendLine("JOIN CLASSE       CL1 ON CL1.NUMERO_CLASSE = CAF.NUMERO_CLASSE_A ");
            query.AppendLine("JOIN CLASSE       CL2 ON CL2.NUMERO_CLASSE = CAF.NUMERO_CLASSE_B");
            query.AppendLine("ORDER BY ");
            query.AppendLine("Classe, ");
            query.AppendLine("ClasseAfinidade");

            var connection = ConnectionDapper.RetornaInstancia().AbreConexao();
            return connection.Query<ClasseAfinidadeSync>(query.ToString(), commandTimeout: 1200).ToList();
        }

        /// <summary>
        /// The get all presentations.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<ApresentacaoIndex> GetAllPresentations()
        {
            var query = new StringBuilder();
            query.AppendLine("SELECT ");
            query.AppendLine("DESCRICAO AS ApresentacaoDescricao ");
            query.AppendLine("FROM ");
            query.AppendLine("TIPO_APRESENTACAO ");

            var connection = ConnectionDapper.RetornaInstancia().AbreConexao();
            return connection.Query<ApresentacaoIndex>(query.ToString(), commandTimeout: 1200).ToList();
        }

        /// <summary>
        /// The get all countries.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<PaisIndex> GetAllCountries()
        {
            var query = new StringBuilder();
            query.AppendLine("SELECT ");
            query.AppendLine("SIGLA AS PaisSigla, ");
            query.AppendLine("NOME AS PaisNome ");
            query.AppendLine("FROM ");
            query.AppendLine("PAIS ");

            var connection = ConnectionDapper.RetornaInstancia().AbreConexao();
            return connection.Query<PaisIndex>(query.ToString(), commandTimeout: 1200).ToList();
        }

        /// <summary>
        /// The get all states.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<EstadoIndex> GetAllStates()
        {
            var query = new StringBuilder();
            query.AppendLine("SELECT ");
            query.AppendLine("SIGLA AS EstadoSigla, ");
            query.AppendLine("NOME AS EstadoNome ");
            query.AppendLine("FROM ");
            query.AppendLine("UF ");

            var connection = ConnectionDapper.RetornaInstancia().AbreConexao();
            return connection.Query<EstadoIndex>(query.ToString(), commandTimeout: 1200).ToList();
        }

        /// <summary>
        /// The get all athributes.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<AtributoIndex> GetAllAthributes()
        {
            var query = new StringBuilder();
            query.AppendLine("SELECT ");
            query.AppendLine("CODIGO AS CodigoAtributo, ");
            query.AppendLine("DESCRICAO AS DescricaoAtributo ");
            query.AppendLine("FROM ");
            query.AppendLine("ATRIBUTO ");

            var connection = ConnectionDapper.RetornaInstancia().AbreConexao();
            return connection.Query<AtributoIndex>(query.ToString(), commandTimeout: 1200).ToList();
        }
    }
}