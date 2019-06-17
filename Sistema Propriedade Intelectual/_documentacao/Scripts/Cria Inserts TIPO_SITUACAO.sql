DECLARE 
  @DS_SITUACAO VARCHAR(80),
  @comando VarChar(MAX)

DECLARE CURSOR_SITUACAO CURSOR Fast_Forward
FOR 
  SELECT
	DS_SITUACAO
  FROM
    [crisrio].[dbo].[TP_SITUACAO]

OPEN CURSOR_SITUACAO

FETCH NEXT FROM CURSOR_SITUACAO INTO @DS_SITUACAO
WHILE (@@Fetch_Status <> -1)
BEGIN
          IF (@@Fetch_Status <> -2)
          BEGIN
        -- Executando INSERT
               set @comando = 'INSERT INTO [HCOFFICE].[dbo].[TIPO_SITUACAO] (DESCRICAO) VALUES('+''''+ @DS_SITUACAO + ''')'
                         PRINT ''
				PRINT @comando
          END
          FETCH NEXT FROM CURSOR_SITUACAO INTO @DS_SITUACAO
END

-- Fechando cursor
CLOSE CURSOR_SITUACAO 
DEALLOCATE CURSOR_SITUACAO
