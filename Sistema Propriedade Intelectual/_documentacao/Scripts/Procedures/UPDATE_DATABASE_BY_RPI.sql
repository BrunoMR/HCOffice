-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 15/12/2017
-- Description:	Este procedimento irá atualizar a base local com com a rpi passada por parâmetro
-- =============================================
CREATE PROCEDURE [dbo].[UPDATE_DATABASE_BY_RPI]
  @rpi INT
AS
BEGIN
	SET NOCOUNT ON;

  DECLARE
    @processList PROCESSOTYPE,
    @processDespachoList PROCESSODESPACHOTYPE,
    @processCfe4List CFE4TYPE,
    @processProtocolList PROTOCOLOTYPE

  -- Add the RPI
  INSERT INTO RPI
  (
    NUMERO,
    DATA
  )
  SELECT
    *
  FROM
    MyAzureDb.HCOFFICE_FULL.dbo.RPI
  WHERE
    NUMERO = @rpi;

  -- Add the processes
  INSERT INTO @processList
  (
    NUMERO,
    NOME_TITULAR,
    CPF_CNPJ_INPI_TITULAR,
    PAIS_TITULAR,
    UF_TITULAR,
    NOME_PROCURADOR,
    MARCA,
    MARCA_ORTOGRAFADA,
    MARCA_NAO_ORTOGRAFADA,
    PRIORIDADE,
    DATA_PRIORIDADE,
    NOME_PAIS_PRIORIDADE,
    TIPO_APRESENTACAO,
    TIPO_NATUREZA,
    CLASSE_INTERNACIONAL,
    CLASSE_1,
    CLASSE_2,
    CLASSE_3,
    ESPECIFICACAO,
    APOSTILA,
    NUMERO_REFERENCIA,
    DATA_DEPOSITO,
    DATA_CONCESSAO,
    DATA_REGISTRO,
    DATA_VIGENCIA,
    MARCA_SEM_VOGAIS
  )
  SELECT
    PRO.NUMERO,
    PRO.NOME_TITULAR,
    PRO.CPF_CNPJ_INPI_TITULAR,
    PRO.PAIS_TITULAR,
    PRO.UF_TITULAR,
    PRO.NOME_PROCURADOR,
    PRO.MARCA,
    PRO.MARCA_ORTOGRAFADA,
    PRO.MARCA_NAO_ORTOGRAFADA,
    PRO.PRIORIDADE,
    PRO.DATA_PRIORIDADE,
    PRO.NOME_PAIS_PRIORIDADE,
    PRO.TIPO_APRESENTACAO,
    PRO.TIPO_NATUREZA,
    PRO.CLASSE_INTERNACIONAL,
    PRO.CLASSE_1,
    PRO.CLASSE_2,
    PRO.CLASSE_3,
    PRO.ESPECIFICACAO,
    PRO.APOSTILA,
    PRO.NUMERO_REFERENCIA,
    PRO.DATA_DEPOSITO,
    PRO.DATA_CONCESSAO,
    PRO.DATA_REGISTRO,
    PRO.DATA_VIGENCIA,
    PRO.MARCA_SEM_VOGAIS
  FROM
    MyAzureDb.HCOFFICE_FULL.dbo.PROCESSO                PRO
    JOIN MyAzureDb.HCOFFICE_FULL.dbo.PROCESSO_DESPACHO  PRD ON PRO.ID = PRD.ID_PROCESSO
                                   AND PRD.NUMERO_RPI = @rpi
  GROUP BY
    PRO.NUMERO,
    PRO.NOME_TITULAR,
    PRO.CPF_CNPJ_INPI_TITULAR,
    PRO.NOME_PROCURADOR,
    PRO.MARCA,
    PRO.MARCA_ORTOGRAFADA,
    PRO.PRIORIDADE,
    PRO.DATA_PRIORIDADE,
    PRO.NOME_PAIS_PRIORIDADE,
    PRO.TIPO_APRESENTACAO,
    PRO.TIPO_NATUREZA,
    PRO.CLASSE_INTERNACIONAL,
    PRO.CLASSE_1,
    PRO.CLASSE_2,
    PRO.CLASSE_3,
    PRO.ESPECIFICACAO,
    PRO.APOSTILA,
    PRO.NUMERO_REFERENCIA,
    PRO.DATA_DEPOSITO,
    PRO.DATA_CONCESSAO,
    PRO.DATA_REGISTRO,
    PRO.DATA_VIGENCIA,
    PRO.DATA_ORDINARIO_INICIAL,
    PRO.DATA_ORDINARIO_FINAL,
    PRO.DATA_EXTRA_ORDINARIO_INICIAL,
    PRO.DATA_EXTRA_ORDINARIO_FINAL,
    PRO.PAIS_TITULAR,
    PRO.UF_TITULAR,
    PRO.MARCA_NAO_ORTOGRAFADA,
    PRO.MARCA_SEM_VOGAIS

  EXECUTE UPDATE_PROCESSO
    @processList

  -- Save processes`s protocols
  INSERT INTO @processProtocolList
  (
    NUMERO,
    DATA,
    CODIGO_SERVICO,
    NOME_RAZAO_SOCIAL,
    PAIS,
    UF
  )
  SELECT
    PRT.NUMERO,
    PRT.DATA,
    PRT.CODIGO_SERVICO,
    PRT.NOME_RAZAO_SOCIAL,
    PRT.PAIS,
    PRT.UF
  FROM
    MyAzureDb.HCOFFICE_FULL.dbo.PROTOCOLO               PRT
    JOIN MyAzureDb.HCOFFICE_FULL.dbo.PROCESSO_DESPACHO  PRD ON PRD.ID_PROTOCOLO = PRT.ID
                                                              AND PRD.NUMERO_RPI = @rpi
  GROUP BY
    PRT.NUMERO,
    PRT.DATA,
    PRT.CODIGO_SERVICO,
    PRT.NOME_RAZAO_SOCIAL,
    PRT.PAIS,
    PRT.UF

  EXECUTE UPDATE_PROTOCOLO
    @processProtocolList

  -- Insert the processes`s despachos
  INSERT INTO @processDespachoList
  (
    NUMERO_PROCESSO,
    CODIGO_DESPACHO,
    NUMERO_RPI,
    DATA_DESPACHO,
    NUMERO_PROTOCOLO,
    COMPLEMENTO
  )

  SELECT
    PRO.NUMERO,
    DES.CODIGO,
    PRD.NUMERO_RPI,
    PRD.DATA_DESPACHO,
    PRT.NUMERO,
    PRD.COMPLEMENTO
  FROM
    MyAzureDb.HCOFFICE_FULL.dbo.PROCESSO                PRO
    JOIN MyAzureDb.HCOFFICE_FULL.dbo.PROCESSO_DESPACHO  PRD ON PRD.ID_PROCESSO = PRO.ID
                                                               AND PRD.NUMERO_RPI = @rpi
    JOIN MyAzureDb.HCOFFICE_FULL.dbo.DESPACHO           DES ON DES.ID = PRD.ID_DESPACHO
    LEFT JOIN MyAzureDb.HCOFFICE_FULL.dbo.PROTOCOLO          PRT ON PRT.ID = PRD.ID_PROTOCOLO

  EXECUTE INSERT_PROCESSO_DESPACHO
    @processDespachoList

  -- Save the processes`s cfe4
  INSERT INTO @processCfe4List
  (
    NUMERO_PROCESSO,
    CODIGO_CFE4
  )
  SELECT
    PRO.NUMERO,
    CFE.CODIGO_CFE4
  FROM
    MyAzureDb.HCOFFICE_FULL.dbo.PROCESSO_CFE4            PRC
    JOIN MyAzureDb.HCOFFICE_FULL.dbo.PROCESSO_DESPACHO   PRD ON PRD.ID_PROCESSO = PRC.ID_PROCESSO
                                                                AND PRD.NUMERO_RPI = @rpi
    JOIN MyAzureDb.HCOFFICE_FULL.dbo.PROCESSO            PRO ON PRO.ID = PRC.ID_PROCESSO
    JOIN MyAzureDb.HCOFFICE_FULL.dbo.CFE4                CFE ON CFE.ID = PRC.ID_CFE4
  GROUP BY
    PRO.NUMERO,
    CFE.CODIGO_CFE4

  EXECUTE UPDATE_PROCESSO_CFE4
    @processCfe4List

END

GO