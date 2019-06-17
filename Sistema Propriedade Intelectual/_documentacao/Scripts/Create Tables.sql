
IF NOT EXISTS (
			SELECT 
			  * 
			FROM 
			  sys.databases 
			WHERE 
			  name = 'HCOFFICE'
		    )
BEGIN
   CREATE DATABASE HCOFFICE

   PRINT 'O banco HCOFFICE foi criado com sucesso'

END

GO


-- CLASSE
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'CLASSE' 
			 )
BEGIN
	CREATE TABLE CLASSE
	(
		NUMERO_CLASSE CHAR(5) NOT NULL PRIMARY KEY CLUSTERED,
		DESCRICAO VARCHAR (2000) NULL,
		DESCRICAO_INGLES VARCHAR(2000) NULL
	)

	PRINT 'A Tabela "CLASSE" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "CLASSE" já existe!'
GO

-- CLASSE_AFINIDADE
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'CLASSE_AFINIDADE' 
			 )
BEGIN
	CREATE TABLE CLASSE_AFINIDADE
	(
		NUMERO_CLASSE_A CHAR(5) NOT NULL,
		NUMERO_CLASSE_B CHAR(5) NOT NULL,
		CONSTRAINT UNIQUE_CLASSE_AFINIDADE UNIQUE (NUMERO_CLASSE_A, NUMERO_CLASSE_B)
	)
	
	ALTER TABLE CLASSE_AFINIDADE  WITH CHECK ADD  CONSTRAINT [FK_CLASSE_AFINIDADE_1] FOREIGN KEY(NUMERO_CLASSE_A)
	REFERENCES CLASSE (NUMERO_CLASSE)
	
	ALTER TABLE CLASSE_AFINIDADE  WITH CHECK ADD  CONSTRAINT [FK_CLASSE_AFINIDADE_2] FOREIGN KEY(NUMERO_CLASSE_B)
	REFERENCES CLASSE (NUMERO_CLASSE)

	PRINT 'A Tabela "CLASSE_AFINIDADE" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "CLASSE_AFINIDADE" já existe!'
GO


-- TIPO_SITUACAO
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'TIPO_SITUACAO' 
			 )
BEGIN
	CREATE TABLE TIPO_SITUACAO
	(
		TIPO INT IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
		DESCRICAO VARCHAR(100) NOT NULL
	)

	PRINT 'A Tabela "TIPO_SITUACAO" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "TIPO_SITUACAO" já existe!'
GO


-- TIPO_DESPACHO
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'TIPO_DESPACHO' 
			 )
BEGIN
	CREATE TABLE TIPO_DESPACHO
	(
		TIPO CHAR(1) NOT NULL PRIMARY KEY CLUSTERED,
		DESCRICAO VARCHAR(100) NOT NULL
	)

	PRINT 'A Tabela "TIPO_DESPACHO" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "TIPO_DESPACHO" já existe!'
GO

-- TIPO_PESSOA
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'TIPO_PESSOA' 
			 )
BEGIN
	CREATE TABLE TIPO_PESSOA
	(
		TIPO CHAR(1) NOT NULL PRIMARY KEY CLUSTERED,
		DESCRICAO VARCHAR(100) NOT NULL,
		ORDEM INT NULL
	)

	PRINT 'A Tabela "TIPO_PESSOA" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "TIPO_PESSOA" já existe!'
GO


-- TIPO_NATUREZA
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'TIPO_NATUREZA' 
			 )
BEGIN
	CREATE TABLE TIPO_NATUREZA
	(
		ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
		DESCRICAO VARCHAR (100) NOT NULL,
		ORDEM INT NULL,
		DESCRICAO_INGLES VARCHAR(100) NULL
	)
	
	ALTER TABLE TIPO_NATUREZA ADD CONSTRAINT TIPO_NATUREZA_UNIQUE UNIQUE (DESCRICAO)
	
	PRINT 'A Tabela "TIPO_NATUREZA" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "TIPO_NATUREZA" já existe!'
GO


-- TIPO_APRESENTACAO
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'TIPO_APRESENTACAO' 
			 )
BEGIN
	CREATE TABLE TIPO_APRESENTACAO
	(
		ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
		DESCRICAO VARCHAR (100) NOT NULL,
		ORDEM INT NULL,
		DESCRICAO_INGLES VARCHAR(100) NULL
	)
	
	ALTER TABLE TIPO_APRESENTACAO ADD CONSTRAINT TIPO_NATUREZA_UNIQUE UNIQUE (DESCRICAO)
	
	PRINT 'A Tabela "TIPO_APRESENTACAO" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "TIPO_APRESENTACAO" já existe!'
GO


-- RPI
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'RPI' 
			 )
BEGIN
	CREATE TABLE RPI
	(
		NUMERO INT NOT NULL PRIMARY KEY CLUSTERED,
		DATA DATETIME2 NOT NULL
	)

	PRINT 'A Tabela "RPI" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "RPI" já existe!'
GO


-- CFE4
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'CFE4' 
			 )
BEGIN
	CREATE TABLE CFE4
	(
		ID INT IDENTITY(1,1) PRIMARY KEY NONCLUSTERED NOT NULL,
		CODIGO_CFE4 CHAR(10) NOT NULL ,
		DESCRICAO VARCHAR (200) NULL,
		DESCRICAO_INGLES VARCHAR(200) NULL
	)

	CREATE CLUSTERED INDEX INDEX_CFE4
    ON CFE4 (CODIGO_CFE4)
	
	ALTER TABLE CFE4 ADD CONSTRAINT CFE4_UNIQUE UNIQUE (CODIGO_CFE4)

	PRINT 'A Tabela "CFE4" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "CFE4" já existe!'
GO


-- DESPACHO
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'DESPACHO' 
			 )
BEGIN
	CREATE TABLE DESPACHO
	(
		ID INT IDENTITY(1,1) PRIMARY KEY NONCLUSTERED NOT NULL,
		CODIGO VARCHAR(100) NOT NULL,
		DESCRICAO VARCHAR(2000) NULL,
		DESCRICAO_COMPLETA VARCHAR(MAX) NULL,
		TIPO CHAR(1) NULL,
		SITUACAO INT NULL,
		PRAZO_1_EM_DIAS INT NULL,
		PRAZO_2_EM_DIAS INT NULL,
		DESCRICAO_INGLES VARCHAR(2000) NULL,
		DESCRICAO_COMPLETA_INGLES VARCHAR(MAX) NULL
	)

	CREATE CLUSTERED INDEX INDEX_DESPACHO
    ON DESPACHO (CODIGO); 
	
	ALTER TABLE DESPACHO  WITH CHECK ADD  CONSTRAINT [FK_DESPACHO_TIPO] FOREIGN KEY(TIPO)
	REFERENCES TIPO_DESPACHO (TIPO)
	
	ALTER TABLE DESPACHO  WITH CHECK ADD  CONSTRAINT [FK_DESPACHO_SITUACAO] FOREIGN KEY(SITUACAO)
	REFERENCES TIPO_SITUACAO (TIPO)
	
	ALTER TABLE DESPACHO ADD CONSTRAINT DESPACHO_UNIQUE UNIQUE (CODIGO)

	PRINT 'A Tabela "DESPACHO" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "DESPACHO" já existe!'
GO


-- PROCESSO
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'PROCESSO' 
			 )
BEGIN
	CREATE TABLE PROCESSO
	(
		ID INT IDENTITY(1,1) PRIMARY KEY NONCLUSTERED NOT NULL,
		NUMERO VARCHAR(20) NOT NULL,
		NOME_TITULAR VARCHAR (500) NULL,
		CPF_CNPJ_INPI_TITULAR VARCHAR(50) NULL,
		PAIS_TITULAR VARCHAR(2) NULL,
		UF_TITULAR CHAR(2) NULL,
		NOME_PROCURADOR VARCHAR (500) NULL,
		MARCA VARCHAR(1000)NULL,
		MARCA_ORTOGRAFADA VARCHAR(1000)NULL,
		PRIORIDADE VARCHAR(100) NULL,
		DATA_PRIORIDADE DATETIME2 NULL,
		NOME_PAIS_PRIORIDADE CHAR(2) NULL,
		TIPO_APRESENTACAO INT NULL,
		TIPO_NATUREZA INT NULL,
		CLASSE_INTERNACIONAL CHAR(5) NULL,
		CLASSE_1 CHAR(5) NULL,
		CLASSE_2 CHAR(5) NULL,
		CLASSE_3 CHAR(5) NULL,
		ESPECIFICACAO VARCHAR(MAX) NULL,
		APOSTILA VARCHAR(MAX) NULL,
		NUMERO_REFERENCIA VARCHAR(255) NULL,
		DATA_DEPOSITO DATE NULL,
		DATA_CONCESSAO DATE NULL,
		DATA_REGISTRO DATE NULL,
		DATA_VIGENCIA DATE NULL,
		DATA_ORDINARIO_INICIAL DATE NULL,
		DATA_ORDINARIO_FINAL DATE NULL,
		DATA_EXTRA_ORDINARIO_INICIAL DATE NULL,
		DATA_EXTRA_ORDINARIO_FINAL DATE NULL
	)

	CREATE CLUSTERED INDEX INDEX_PROCESSO
	ON PROCESSO (NUMERO)
	
	ALTER TABLE PROCESSO  WITH CHECK ADD  CONSTRAINT [FK_PROCESSO_APRESENTACAO] FOREIGN KEY(TIPO_APRESENTACAO)
	REFERENCES TIPO_APRESENTACAO (TIPO)
	
	ALTER TABLE PROCESSO  WITH CHECK ADD  CONSTRAINT [FK_PROCESSO_NATUREZA] FOREIGN KEY(TIPO_NATUREZA)
	REFERENCES TIPO_NATUREZA (TIPO)
	
	ALTER TABLE PROCESSO  WITH CHECK ADD  CONSTRAINT [FK_PROCESSO_CLASSE_INT] FOREIGN KEY(CLASSE_INTERNACIONAL)
	REFERENCES CLASSE (NUMERO_CLASSE)
	
	ALTER TABLE PROCESSO  WITH CHECK ADD  CONSTRAINT [FK_PROCESSO_CLASSE_1] FOREIGN KEY(CLASSE_1)
	REFERENCES CLASSE (NUMERO_CLASSE)
	
	ALTER TABLE PROCESSO  WITH CHECK ADD  CONSTRAINT [FK_PROCESSO_CLASSE_2] FOREIGN KEY(CLASSE_2)
	REFERENCES CLASSE (NUMERO_CLASSE)
	
	ALTER TABLE PROCESSO  WITH CHECK ADD  CONSTRAINT [FK_PROCESSO_CLASSE_3] FOREIGN KEY(CLASSE_3)
	REFERENCES CLASSE (NUMERO_CLASSE)
	
	ALTER TABLE PROCESSO  WITH CHECK ADD  CONSTRAINT [FK_PROCESSO_PAIS_TITULAR] FOREIGN KEY(PAIS_TITULAR)
	REFERENCES PAIS (SIGLA)
	
	ALTER TABLE PROCESSO  WITH CHECK ADD  CONSTRAINT [FK_PROCESSO_UF_TITULAR] FOREIGN KEY(UF_TITULAR)
	REFERENCES UF (SIGLA)

	PRINT 'A Tabela "PROCESSO" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "PROCESSO" já existe!'
GO



-- PROCESSO_CFE4
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'PROCESSO_CFE4' 
			 )
BEGIN
	CREATE TABLE PROCESSO_CFE4
	(
		ID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
		ID_PROCESSO INT NOT NULL,
		ID_CFE4 INT NOT NULL
	)
	
	ALTER TABLE PROCESSO_CFE4  WITH CHECK ADD  CONSTRAINT [FK_PROCESSO_CFE4_PROCESSO] FOREIGN KEY(ID_PROCESSO)
	REFERENCES PROCESSO (ID)
	
	ALTER TABLE PROCESSO_CFE4  WITH CHECK ADD  CONSTRAINT [FK_PROCESSO_CFE4_CFE4] FOREIGN KEY(ID_CFE4)
	REFERENCES CFE4 (ID)

	PRINT 'A Tabela "PROCESSO_CFE4" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "PROCESSO_CFE4" já existe!'
GO

-- PROTOCOLO
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'PROTOCOLO' 
			 )
BEGIN
	CREATE TABLE PROTOCOLO
	(
		ID INT IDENTITY(1,1) PRIMARY KEY NONCLUSTERED NOT NULL,
		NUMERO VARCHAR(20) NOT NULL,
		DATA DATETIME2 NULL,
		CODIGO_SERVICO VARCHAR(20) NULL,
		NOME_RAZAO_SOCIAL VARCHAR(200) NULL,
		PAIS VARCHAR(100) NULL,
		UF CHAR(2) NULL
	)

	CREATE CLUSTERED INDEX INDEX_PROTOCOLO
	ON PROTOCOLO (NUMERO)
	
	ALTER TABLE PROTOCOLO ADD CONSTRAINT PROTOCOLO_UNIQUE UNIQUE (NUMERO)
	
	PRINT 'A Tabela "PROTOCOLO" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "PROTOCOLO" já existe!'
GO


-- PROCESSO_DESPACHO
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'PROCESSO_DESPACHO' 
			 )
BEGIN
	CREATE TABLE PROCESSO_DESPACHO
	(
		ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
		ID_PROCESSO INT NOT NULL,
		ID_DESPACHO INT NOT NULL,
		NUMERO_RPI INT NOT NULL,
		DATA_DESPACHO DATETIME2 NOT NULL,
		ID_PROTOCOLO INT NULL,
		COMPLEMENTO VARCHAR(MAX) NULL
	)
	
	CREATE NONCLUSTERED INDEX INDEX_PROCESSO_DESPACHO
	ON PROCESSO_DESPACHO (ID_PROCESSO, ID_DESPACHO, NUMERO_RPI)
	
	ALTER TABLE PROCESSO_DESPACHO  WITH CHECK ADD  CONSTRAINT [FK_PROCESSO_DESPACHO_PROCESSO] FOREIGN KEY(ID_PROCESSO)
	REFERENCES PROCESSO (ID)
	
	ALTER TABLE PROCESSO_DESPACHO  WITH CHECK ADD  CONSTRAINT [FK_PROCESSO_DESPACHO_COD_DESPACHO] FOREIGN KEY(ID_DESPACHO)
	REFERENCES DESPACHO (ID)
	
	ALTER TABLE PROCESSO_DESPACHO  WITH CHECK ADD  CONSTRAINT [FK_PROCESSO_DESPACHO_RPI] FOREIGN KEY(NUMERO_RPI)
	REFERENCES RPI (NUMERO)
	
	ALTER TABLE PROCESSO_DESPACHO  WITH CHECK ADD  CONSTRAINT [FK_PROCESSO_DESPACHO_PROTOCOLO] FOREIGN KEY(ID_PROTOCOLO)
	REFERENCES PROTOCOLO (ID)
	
	CREATE NONCLUSTERED INDEX [INDEX_PROCESSO_DESPACHO_PROCESSO] ON [dbo].[PROCESSO_DESPACHO] (ID_PROCESSO)
	CREATE NONCLUSTERED INDEX [INDEX_PROCESSO_DESPACHO_DESPACHO] ON [dbo].[PROCESSO_DESPACHO] (ID_DESPACHO)
	CREATE NONCLUSTERED INDEX [INDEX_PROCESSO_DESPACHO_RPI] ON [dbo].[PROCESSO_DESPACHO] (NUMERO_RPI)
	CREATE NONCLUSTERED INDEX [INDEX_PROCESSO_DESPACHO_PROTOCOLO] ON [dbo].[PROCESSO_DESPACHO] (ID_PROTOCOLO)

	PRINT 'A Tabela "PROCESSO_DESPACHO" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "PROCESSO_DESPACHO" já existe!'
GO



-- CLIENTE
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'CLIENTE' 
			 )
BEGIN
	CREATE TABLE CLIENTE
	(
		ID  INT IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
		TIPO_PESSOA CHAR(1) NOT NULL,
		NOME VARCHAR(100) NOT NULL,
		NOME_FANTASIA VARCHAR(100) NOT NULL,
		CPF_CNPJ VARCHAR(20) NULL,
		RG VARCHAR(20) NULL,
		INSCRICAO_MUNICIPAL VARCHAR(20) NULL,
		ENDERECO_ELETRONICO VARCHAR(100) NULL,
		EMAIL VARCHAR(100) NULL,
		NOME_CONTATO VARCHAR(100) NULL,
		TELEFONE_CONTATO VARCHAR(20) NULL,
		TELEFONE_EMPRESA VARCHAR(20) NULL,
		TELEFONE_FAX VARCHAR(20) NULL,
		ENDERECO VARCHAR(200) NULL,
		BAIRRO VARCHAR(100) NULL,
		CEP VARCHAR(20) NULL,
		CIDADE VARCHAR(100) NULL,
		UF CHAR(2) NULL,
		OBSERVACAO VARCHAR(MAX) NULL
	)
	
	ALTER TABLE CLIENTE  WITH CHECK ADD  CONSTRAINT [FK_CLIENTE_TIPO_PESSOA] FOREIGN KEY(TIPO_PESSOA)
	REFERENCES TIPO_PESSOA (TIPO)
	
	PRINT 'A Tabela "CLIENTE" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "CLIENTE" já existe!'
GO



-- CONFIGURAO
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'CONFIGURAO' 
			 )
BEGIN
	CREATE TABLE CONFIGURAO
	(
		ID INT IDENTITY(1,1) PRIMARY KEY CLUSTERED NOT NULL,
		DESCRICAO VARCHAR(200) NOT NULL,
		VALOR VARCHAR(MAX)
	)
	
	PRINT 'A Tabela "CONFIGURAO" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "CONFIGURAO" já existe!'
GO


-- ATRIBUTO
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'ATRIBUTO' 
			 )
BEGIN
	CREATE TABLE ATRIBUTO
	(
		ID INT IDENTITY(1,1) PRIMARY KEY CLUSTERED NOT NULL,
		CODIGO VARCHAR(10) NOT NULL,
		DESCRICAO VARCHAR(MAX)
	)
	
	ALTER TABLE ATRIBUTO ADD CONSTRAINT ATRIBUTO_UNIQUE UNIQUE (CODIGO)
	
	PRINT 'A Tabela "ATRIBUTO" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "ATRIBUTO" já existe!'
GO

-- PAIS
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'PAIS' 
			 )
BEGIN
	CREATE TABLE PAIS
	(
		ID INT IDENTITY(1,1) PRIMARY KEY CLUSTERED NOT NULL,
		SIGLA CHAR(2) NOT NULL,
		NOME VARCHAR(1000) NULL,
		NOME_INGLES VARCHAR(1000) NULL
	)
	
	ALTER TABLE PAIS ADD CONSTRAINT PAIS_UNIQUE UNIQUE (SIGLA)
	
	CREATE NONCLUSTERED INDEX [INDEX_PAIS_SIGLA] ON [dbo].[PAIS] (SIGLA)
	
	PRINT 'A Tabela "PAIS" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "PAIS" já existe!'
GO


-- UF
USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'UF' 
			 )
BEGIN
	CREATE TABLE UF
	(
		ID INT IDENTITY(1,1) PRIMARY KEY CLUSTERED NOT NULL,
		SIGLA CHAR(2) NOT NULL,
		NOME VARCHAR(1000) NULL,
		NOME_INGLES VARCHAR(1000) NULL
	)
	
	ALTER TABLE UF ADD CONSTRAINT UF_UNIQUE UNIQUE (SIGLA)
	
	CREATE NONCLUSTERED INDEX [INDEX_UF_SIGLA] ON [dbo].[UF] (SIGLA)
	
	PRINT 'A Tabela "UF" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "UF" já existe!'
GO


USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'TIPO_PROCESSO_RADICAL'
			 )
BEGIN
	CREATE TABLE TIPO_PROCESSO_RADICAL
	(
	  ID int
		constraint PK_TIPO_PROCESSO_RADICAL
		primary key ,
	  DESCRIPTION varchar(255)    not null
	)
	
	
	PRINT 'A Tabela "TIPO_PROCESSO_RADICAL" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "TIPO_PROCESSO_RADICAL" já existe!'
GO


USE HCOFFICE
GO
IF NOT EXISTS (
				SELECT
				 *
				FROM 
				  SYSOBJECTS
				WHERE 
				  XTYPE = 'U'
				  AND NAME = 'PROCESSO_RADICAL' 
			 )
BEGIN
	CREATE TABLE PROCESSO_RADICAL
	(
	  ID              int identity
		constraint PK_PROCESSO_RADICAL
		primary key ,
	  NUMERO_PROCESSO varchar(20)    not null ,
	  RADICAL         nvarchar(2000) not null ,
	  LENGTH_RADICAL  int            not null ,
	  ID_TIPO_PROCESSO_RADICAL            INT NOT NULL
	)
	
	ALTER TABLE PROCESSO_RADICAL  WITH CHECK ADD  CONSTRAINT [FK_PROCESSO_RADICAL_TIPO] FOREIGN KEY(ID_TIPO_PROCESSO_RADICAL)
	REFERENCES TIPO_PROCESSO_RADICAL (ID)
	
	CREATE NONCLUSTERED INDEX [INDEX_PROCESSO_RADICAL] ON [dbo].[PROCESSO_DESPACHO] (ID_PROCESSO)
	CREATE NONCLUSTERED INDEX [INDEX_PROCESSO_RADICAL] ON [dbo].[PROCESSO_DESPACHO] (ID_DESPACHO)
	
	PRINT 'A Tabela "PROCESSO_RADICAL" foi criada com sucesso'
END
ELSE
  PRINT 'A Tabela "PROCESSO_RADICAL" já existe!'
GO