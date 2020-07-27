IF NOT EXISTS(
				SELECT
					*
				FROM
					SYS.systypes
				WHERE
					name = 'PROCESSORADICALTYPE'
			 )
BEGIN

	create type PROCESSORADICALTYPE as table
	(
		Id                       int identity(1,1)  not null,
		NUMERO_PROCESSO          varchar(20)        not null,
		RADICAL                  nvarchar(255)      not null,
		LENGTH_RADICAL           int                not null,
		ID_TIPO_PROCESSO_RADICAL int,
		MAIN                     bit,
		BIGGER                   bit default 0
		primary key nonclustered (Id)
	)
	go

	PRINT 'O Type "PROCESSORADICALTYPE" foi criado com sucesso'
END
ELSE
  PRINT 'O Type "PROCESSORADICALTYPE" já existe!'
GO