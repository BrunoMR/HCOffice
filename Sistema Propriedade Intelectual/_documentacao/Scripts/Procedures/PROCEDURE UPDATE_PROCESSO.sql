CREATE PROCEDURE UPDATE_PROCESSO
		@tableProcessos PROCESSOTYPE READONLY
AS
BEGIN
	SET NOCOUNT ON;

	MERGE INTO dbo.PROCESSO PRO
	USING @tableProcessos NEWPRO ON NEWPRO.NUMERO = PRO.NUMERO
	WHEN MATCHED THEN
		UPDATE SET
			PRO.NOME_TITULAR = COALESCE(NEWPRO.NOME_TITULAR, PRO.NOME_TITULAR),
			PRO.CPF_CNPJ_INPI_TITULAR = COALESCE(NEWPRO.CPF_CNPJ_INPI_TITULAR, PRO.CPF_CNPJ_INPI_TITULAR),
			PRO.PAIS_TITULAR = COALESCE(NEWPRO.PAIS_TITULAR, PRO.PAIS_TITULAR),
			PRO.UF_TITULAR = COALESCE(NEWPRO.UF_TITULAR, PRO.UF_TITULAR),
			PRO.NOME_PROCURADOR = COALESCE(NEWPRO.NOME_PROCURADOR, PRO.NOME_PROCURADOR),
			PRO.MARCA = COALESCE(NEWPRO.MARCA, PRO.MARCA),
			PRO.MARCA_ORTOGRAFADA = COALESCE(NEWPRO.MARCA_ORTOGRAFADA, PRO.MARCA_ORTOGRAFADA),
			PRO.MARCA_NAO_ORTOGRAFADA = COALESCE(NEWPRO.MARCA_NAO_ORTOGRAFADA, PRO.MARCA_NAO_ORTOGRAFADA),
			PRO.MARCA_SEM_VOGAIS = COALESCE(NEWPRO.MARCA_SEM_VOGAIS, PRO.MARCA_SEM_VOGAIS),
			PRO.PRIORIDADE = COALESCE(NEWPRO.PRIORIDADE, PRO.PRIORIDADE),
			PRO.DATA_PRIORIDADE = COALESCE(NEWPRO.DATA_PRIORIDADE, PRO.DATA_PRIORIDADE),
			PRO.NOME_PAIS_PRIORIDADE = COALESCE(NEWPRO.NOME_PAIS_PRIORIDADE, PRO.NOME_PAIS_PRIORIDADE),
			PRO.TIPO_APRESENTACAO = COALESCE(NEWPRO.TIPO_APRESENTACAO, PRO.TIPO_APRESENTACAO),
			PRO.TIPO_NATUREZA = COALESCE(NEWPRO.TIPO_NATUREZA, PRO.TIPO_NATUREZA),
			PRO.CLASSE_INTERNACIONAL = COALESCE(NEWPRO.CLASSE_INTERNACIONAL, PRO.CLASSE_INTERNACIONAL),
			PRO.CLASSE_1 = COALESCE(NEWPRO.CLASSE_1, PRO.CLASSE_1),
			PRO.CLASSE_2 = COALESCE(NEWPRO.CLASSE_2, PRO.CLASSE_2),
			PRO.CLASSE_3 = COALESCE(NEWPRO.CLASSE_3, PRO.CLASSE_3),
			PRO.ESPECIFICACAO = COALESCE(NEWPRO.ESPECIFICACAO, PRO.ESPECIFICACAO),
			PRO.APOSTILA = COALESCE(NEWPRO.APOSTILA, PRO.APOSTILA),
			PRO.NUMERO_REFERENCIA = COALESCE(NEWPRO.NUMERO_REFERENCIA, PRO.NUMERO_REFERENCIA),
			PRO.DATA_DEPOSITO = COALESCE(NEWPRO.DATA_DEPOSITO, PRO.DATA_DEPOSITO),
			PRO.DATA_CONCESSAO = COALESCE(NEWPRO.DATA_CONCESSAO, PRO.DATA_CONCESSAO),
			PRO.DATA_REGISTRO = COALESCE(NEWPRO.DATA_REGISTRO, PRO.DATA_REGISTRO),
			PRO.DATA_VIGENCIA = COALESCE(NEWPRO.DATA_VIGENCIA, PRO.DATA_VIGENCIA),
			PRO.DATA_ORDINARIO_INICIAL = COALESCE(DATEADD(DAY, 1, DATEADD(YEAR, 10, DATEADD(YEAR, -1, NEWPRO.DATA_REGISTRO))), PRO.DATA_ORDINARIO_INICIAL),
			PRO.DATA_ORDINARIO_FINAL = COALESCE(DATEADD(YEAR, 10, NEWPRO.DATA_REGISTRO), PRO.DATA_ORDINARIO_FINAL),
			PRO.DATA_EXTRA_ORDINARIO_INICIAL = COALESCE(DATEADD(DAY, 1, DATEADD(YEAR, 10, NEWPRO.DATA_REGISTRO)), PRO.DATA_EXTRA_ORDINARIO_INICIAL),
			PRO.DATA_EXTRA_ORDINARIO_FINAL = COALESCE(DATEADD(MONTH, 6, DATEADD(YEAR, 10, NEWPRO.DATA_REGISTRO)), PRO.DATA_EXTRA_ORDINARIO_FINAL)

	WHEN NOT MATCHED THEN
		INSERT
		VALUES
		(
			NEWPRO.NUMERO,
			NEWPRO.NOME_TITULAR,
			NEWPRO.CPF_CNPJ_INPI_TITULAR,
			NEWPRO.NOME_PROCURADOR,
			NEWPRO.MARCA,
			NEWPRO.MARCA_ORTOGRAFADA,
			NEWPRO.PRIORIDADE,
			NEWPRO.DATA_PRIORIDADE,
			NEWPRO.NOME_PAIS_PRIORIDADE,
			NEWPRO.TIPO_APRESENTACAO,
			NEWPRO.TIPO_NATUREZA,
			NEWPRO.CLASSE_INTERNACIONAL,
			NEWPRO.CLASSE_1,
			NEWPRO.CLASSE_2,
			NEWPRO.CLASSE_3,
			NEWPRO.ESPECIFICACAO,
			NEWPRO.APOSTILA,
			NEWPRO.NUMERO_REFERENCIA,
			NEWPRO.DATA_DEPOSITO,
			NEWPRO.DATA_CONCESSAO,
			NEWPRO.DATA_REGISTRO,
			NEWPRO.DATA_VIGENCIA,
			DATEADD(DAY, 1, DATEADD(YEAR, 10, DATEADD(YEAR, -1, NEWPRO.DATA_REGISTRO))),
			DATEADD(YEAR, 10, NEWPRO.DATA_REGISTRO),
			DATEADD(DAY, 1, DATEADD(YEAR, 10, NEWPRO.DATA_REGISTRO)),
			DATEADD(DAY, 181, DATEADD(YEAR, 10, NEWPRO.DATA_REGISTRO)),
			NEWPRO.PAIS_TITULAR,
			NEWPRO.UF_TITULAR,
			NEWPRO.MARCA_NAO_ORTOGRAFADA,
			NEWPRO.MARCA_SEM_VOGAIS
		);
END
GO