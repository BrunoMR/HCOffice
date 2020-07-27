IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'PROCESS_TO_COLLIDE_FULL' 
			 )
BEGIN
	create table PROCESS_TO_COLLIDE_FULL
	(
		ID                    int identity
			primary key nonclustered,
		Main                  bit,
		LengthRadical         int,
		Radical               nvarchar(255),
		IdProcesso            int not null,
		Marca                 varchar(1000),
		MarcaOrtografada      varchar(1000),
		Codigo                varchar(100),
		DataDeposito          varchar(30),
		DataConcessao         varchar(30),
		Numero                varchar(20),
		NomeTitular           varchar(500),
		NomeProcurador        varchar(500),
		MarcaSemVogais        varchar(100),
		LenMarcaSemVogais     int,
		Class                 varchar(5),
		ClassFormated         varchar(1000),
		Specification         varchar(max),
		IdTipoProcessoRadical int.
		BrandSize             int,
		Bigger                bit,
		MarcaModificada       varchar(1000)
	)
	
	PRINT 'A Tabela "PROCESS_TO_COLLIDE_FULL" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "PROCESS_TO_COLLIDE_FULL" já existe!'
GO