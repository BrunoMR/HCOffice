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
	'Física',
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
	'Jurídica',
	1
  )
GO