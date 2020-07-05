IF NOT EXISTS(
				SELECT
					*
				FROM 
					SYS.systypes
				WHERE
					name = 'PROCESSOTYPE'
			 )
BEGIN

	CREATE TYPE PROCESSOTYPE AS TABLE
	(
		NUMERO                varchar(20) not null,
		NOME_TITULAR          varchar(500),
		CPF_CNPJ_INPI_TITULAR varchar(100),
		PAIS_TITULAR          varchar(50),
		UF_TITULAR            char(2),
		NOME_PROCURADOR       varchar(500),
		MARCA                 varchar(1000),
		MARCA_ORTOGRAFADA     varchar(1000),
		MARCA_NAO_ORTOGRAFADA varchar(1000),
		PRIORIDADE            varchar(100),
		DATA_PRIORIDADE       date,
		NOME_PAIS_PRIORIDADE  char(2),
		TIPO_APRESENTACAO     int,
		TIPO_NATUREZA         int,
		CLASSE_INTERNACIONAL  char(5),
		CLASSE_1              char(5),
		CLASSE_2              char(5),
		CLASSE_3              char(5),
		ESPECIFICACAO         varchar(max),
		APOSTILA              varchar(max),
		NUMERO_REFERENCIA     varchar(255),
		DATA_DEPOSITO         date,
		DATA_CONCESSAO        date,
		DATA_REGISTRO         date,
		DATA_VIGENCIA         date,
		MARCA_SEM_VOGAIS      varchar(1000),
		NUMERO_INSCRICAO_INTERNACIONAL varchar(20),
		DATA_RECEBIMENTO_INPI date
	)
    
PRINT 'O Type "PROCESSOTYPE" foi criado com sucesso'
END
ELSE
  PRINT 'O Type "PROCESSOTYPE" já existe!'
GO

