ALTER PROCEDURE UPDATE_PROCESSO_CFE4
      @tableCfe4s CFE4TYPE READONLY
AS
BEGIN
	SET NOCOUNT ON;
	
	DELETE FROM PROCESSO_CFE4
	FROM
		PROCESSO_CFE4		OLD
		JOIN PROCESSO		PRO WITH (NOLOCK) ON PRO.ID = OLD.ID_PROCESSO
		JOIN @tableCfe4s	NEW ON NEW.NUMERO_PROCESSO = PRO.NUMERO

	INSERT PROCESSO_CFE4
	SELECT
	  PRO.ID,
	  CFE.ID
	FROM
	  @tableCfe4s			NEW
	  JOIN dbo.PROCESSO		PRO ON PRO.NUMERO = NEW.NUMERO_PROCESSO
	  JOIN dbo.CFE4			CFE ON CFE.CODIGO_CFE4 = NEW.CODIGO_CFE4

END
