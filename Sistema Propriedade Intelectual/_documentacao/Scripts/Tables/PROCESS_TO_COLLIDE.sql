IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'PROCESS_TO_COLLIDE' 
			 )
BEGIN
	create table PROCESS_TO_COLLIDE
	(
		ID_PROCESSO       int          not null,
		NUMERO            varchar(20)  not null,
		MARCA             varchar(1000),
		MARCA_ORTOGRAFADA varchar(1000),
		MARCA_SEM_VOGAIS  varchar(8000),
		NOME_TITULAR      varchar(500),
		DATA_DEPOSITO     varchar(30),
		DATA_CONCESSAO    varchar(30),
		CODIGO            varchar(100) not null,
		NOME_PROCURADOR   varchar(500),
		MarcaModificada   varchar(1000)
	)
	
	PRINT 'A Tabela "PROCESS_TO_COLLIDE" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "PROCESS_TO_COLLIDE" já existe!'
GO