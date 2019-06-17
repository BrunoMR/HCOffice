IF NOT EXISTS(
				SELECT
					*
				FROM 
					SYS.systypes
				WHERE
					name = 'CFE4TYPE'
			 )
BEGIN

	CREATE TYPE CFE4TYPE AS TABLE
	(
		[NUMERO_PROCESSO] [varchar] (20) PRIMARY KEY NONCLUSTERED NOT NULL,
		[CODIGO_CFE4] [char] (10) NOT NULL
	)
	WITH
    (MEMORY_OPTIMIZED = ON)
	
	PRINT 'O Type "CFE4TYPE" foi criado com sucesso'
END
ELSE
  PRINT 'O Type "CFE4TYPE" já existe!'
GO