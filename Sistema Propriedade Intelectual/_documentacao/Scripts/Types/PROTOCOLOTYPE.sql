IF NOT EXISTS(
				SELECT
					*
				FROM
					SYS.systypes
				WHERE
					name = 'PROTOCOLOTYPE'
			 )
BEGIN

	CREATE TYPE PROTOCOLOTYPE AS TABLE
	(
		[NUMERO] [varchar] (20) PRIMARY KEY NONCLUSTERED,
		[DATA] [datetime2] NULL,
		[CODIGO_SERVICO] [varchar] (20) NULL,
		[NOME_RAZAO_SOCIAL] [varchar] (200),
		[PAIS] [varchar] (100),
		[UF] [char] (2)
	)
	WITH (MEMORY_OPTIMIZED = ON)

	PRINT 'O Type "PROTOCOLOTYPE" foi criado com sucesso'
END
ELSE
  PRINT 'O Type "PROTOCOLOTYPE" já existe!'
GO