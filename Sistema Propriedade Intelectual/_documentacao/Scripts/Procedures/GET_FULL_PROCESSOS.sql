CREATE PROCEDURE [dbo].[GET_FULL_PROCESSOS]
		@idProcesso PROCESSOIDTYPE READONLY
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		PRO.ID AS Id, 
		PRO.NUMERO AS Numero, 
		PRO.NOME_TITULAR AS Titular,
		PRO.CPF_CNPJ_INPI_TITULAR AS CpfCnpjInpi, 
		PRO.PAIS_TITULAR AS TitularPais,
		PAIS.NOME AS TitularPaisNome,
		PRO.UF_TITULAR AS TitularUf,
		UF.NOME AS TitularUfNome,
		PRO.NOME_PROCURADOR AS Procurador, 
		PRO.MARCA AS Marca, 
		PRO.MARCA_ORTOGRAFADA AS MarcaOrtografada,
		PRO.MARCA_NAO_ORTOGRAFADA AS MarcaNaoOrtografada,
		PRO.MARCA_SEM_VOGAIS AS MarcaSemVogais,	
		PRO.PRIORIDADE AS Prioridade, 
		PRO.DATA_PRIORIDADE AS PrioridadeData, 
		PRO.NOME_PAIS_PRIORIDADE AS PrioridadePais, 
		PRO.ESPECIFICACAO AS Especificacao, 
		PRO.APOSTILA AS Apostila, 
		CONVERT(DATE, PRO.DATA_DEPOSITO) AS DataDeposito, 
		CONVERT(DATE, PRO.DATA_CONCESSAO) AS DataConcessao, 
		CONVERT(DATE, PRO.DATA_REGISTRO) AS DataRegistro, 
		CONVERT(DATE, PRO.DATA_VIGENCIA) AS DataVigencia, 
		CONVERT(DATE, PRO.DATA_ORDINARIO_INICIAL) AS DataOrdinarioInicial, 
		CONVERT(DATE, PRO.DATA_ORDINARIO_FINAL) AS DataOrdinarioFinal, 
		CONVERT(DATE, PRO.DATA_EXTRA_ORDINARIO_INICIAL) AS DataExtraOrdinarioInicial, 
		CONVERT(DATE, PRO.DATA_EXTRA_ORDINARIO_FINAL) AS DataExtraOrdinarioFinal, 
		TPA.DESCRICAO AS Apresentacao, 
		TPN.DESCRICAO AS Natureza, 
		dbo.GetJustClasse(PRO.CLASSE_INTERNACIONAL) AS ClasseInternacional, 
		dbo.GetJustEditionClasse(PRO.CLASSE_INTERNACIONAL) AS ClasseInternacionalEdicao, 
		CLI.DESCRICAO AS ClasseInternacionalDescricao,
		dbo.GetJustClasse(PRO.CLASSE_1) AS Classe1, 
		dbo.GetJustSubClasse(PRO.CLASSE_1) AS Classe1Sub,
		CL1.DESCRICAO AS Classe1SubDescricao,
		dbo.GetJustClasse(PRO.CLASSE_2) AS Classe2, 
		dbo.GetJustSubClasse(PRO.CLASSE_2) AS Classe2Sub, 
		CL2.DESCRICAO AS Classe2SubDescricao,
		dbo.GetJustClasse(PRO.CLASSE_3) AS Classe3, 
		dbo.GetJustSubClasse(PRO.CLASSE_3) AS Classe3Sub, 
		CL3.DESCRICAO AS Classe3SubDescricao,
		CFE.CODIGO_CFE4 AS Cfe4, 
		CFE.DESCRICAO AS Cfe4Descricao, 
		DEP.CODIGO AS DespachoCodigo, 
		DEP.DESCRICAO AS DespachoDescricao, 
		DEP.DESCRICAO_COMPLETA AS DespachoDescricaoCompleta, 
		DSI.DESCRICAO AS DespachoSituacao, 
		DTP.TIPO AS DespachoTipo, 
		DTP.DESCRICAO AS DespachoTipoDescricao, 
		PRD.NUMERO_RPI AS DespachoRpi, 
		CONVERT(DATE, PRD.DATA_DESPACHO) AS DespachoData, 
		PRD.COMPLEMENTO AS DespachoComplemento, 
		PRT.NUMERO AS ProtocoloNumero, 
		PRT.CODIGO_SERVICO AS ProtocoloCodigo, 
		CONVERT(DATE, PRT.DATA) AS ProtocoloData, 
		PRT.NOME_RAZAO_SOCIAL AS ProtocoloNomeRazaoSocial, 
		PRT.PAIS AS ProtocoloPais, 
		PRT.UF AS ProtocoloUf 
	FROM
	    @idProcesso					  SYNC
		JOIN PROCESSO                 PRO ON PRO.ID = SYNC.ID_PROCESSO
		LEFT JOIN TIPO_APRESENTACAO   TPA ON TPA.ID = PRO.TIPO_APRESENTACAO 
		LEFT JOIN TIPO_NATUREZA       TPN ON TPN.ID = PRO.TIPO_NATUREZA 
		LEFT JOIN PROCESSO_CFE4       PR4 ON PR4.ID_PROCESSO = PRO.ID 
		LEFT JOIN CFE4                CFE ON CFE.ID = PR4.ID_CFE4 
		LEFT JOIN PROCESSO_DESPACHO   PRD ON PRD.ID_PROCESSO = PRO.ID 
		LEFT JOIN DESPACHO            DEP ON DEP.ID = PRD.ID_DESPACHO 
		LEFT JOIN PROTOCOLO           PRT ON PRT.ID = PRD.ID_PROTOCOLO 
		LEFT JOIN TIPO_SITUACAO       DSI ON DSI.TIPO = DEP.SITUACAO 
		LEFT JOIN TIPO_DESPACHO       DTP ON DTP.TIPO = DEP.TIPO
		LEFT JOIN CLASSE			  CLI ON CLI.NUMERO_CLASSE = PRO.CLASSE_INTERNACIONAL
		LEFT JOIN CLASSE			  CL1 ON CL1.NUMERO_CLASSE = PRO.CLASSE_1
		LEFT JOIN CLASSE			  CL2 ON CL2.NUMERO_CLASSE = PRO.CLASSE_2
		LEFT JOIN CLASSE			  CL3 ON CL3.NUMERO_CLASSE = PRO.CLASSE_3
		LEFT JOIN PAIS				  PAIS ON PAIS.SIGLA = PRO.PAIS_TITULAR
		LEFT JOIN UF				  UF ON UF.SIGLA = PRO.UF_TITULAR
	ORDER BY
	  PRO.NUMERO,
	  PRD.NUMERO_RPI
END