CREATE PROCEDURE UPDATE_PROTOCOLO
      @tableProtocolos PROTOCOLOTYPE READONLY
AS
BEGIN
	SET NOCOUNT ON;

	MERGE INTO PROTOCOLO PRT
	USING @tableProtocolos NEWPRT ON NEWPRT.NUMERO = PRT.NUMERO
	WHEN MATCHED THEN
		UPDATE SET
			PRT.DATA = COALESCE(NEWPRT.DATA, PRT.DATA),
			PRT.CODIGO_SERVICO = COALESCE(NEWPRT.CODIGO_SERVICO, PRT.CODIGO_SERVICO),
			PRT.NOME_RAZAO_SOCIAL = COALESCE(NEWPRT.NOME_RAZAO_SOCIAL, PRT.NOME_RAZAO_SOCIAL),
			PRT.PAIS = COALESCE(NEWPRT.PAIS, PRT.PAIS),
			PRT.UF = COALESCE(NEWPRT.UF, PRT.UF)
	WHEN NOT MATCHED THEN
		INSERT
		VALUES
		(
			NEWPRT.NUMERO,
			NEWPRT.DATA,
			NEWPRT.CODIGO_SERVICO,
			NEWPRT.NOME_RAZAO_SOCIAL,
			NEWPRT.PAIS,
			NEWPRT.UF
		);

END
go

