USE [HCOFFICE]
GO

INSERT INTO 
  [dbo].[TIPO_PESSOA]
  (
    [TIPO]
    ,[DESCRICAO]
	,[ORDEM]
  )
  VALUES
  (
    'F',
	'F�sica',
	2
  )
GO

INSERT INTO 
  [dbo].[TIPO_PESSOA]
  (
    [TIPO]
    ,[DESCRICAO]
	,[ORDEM]
  )
  VALUES
  (
    'J',
	'Jur�dica',
	1
  )
GO