DECLARE 
  @DS_NATUREZA VARCHAR(80),
  @NO_ORDEM INT,
  @comando VarChar(MAX)

DECLARE CURSOR_NATUREZA CURSOR Fast_Forward
FOR 
  SELECT
	DS_NATUREZA,
    NO_ORDEM
  FROM
    [crisrio].[dbo].[TP_NATUREZA]

OPEN CURSOR_NATUREZA

FETCH NEXT FROM CURSOR_NATUREZA INTO @DS_NATUREZA, @NO_ORDEM
WHILE (@@Fetch_Status <> -1)
BEGIN
          IF (@@Fetch_Status <> -2)
          BEGIN
        -- Executando INSERT
               set @comando = 'INSERT INTO [HCOFFICE].[dbo].[TIPO_NATUREZA] (DESCRICAO, ORDEM) VALUES('+''''+ @DS_NATUREZA + ''''+','+''''+CONVERT(VARCHAR(20), @NO_ORDEM) + ''')'
                         PRINT ''
				PRINT @comando
          END
          FETCH NEXT FROM CURSOR_NATUREZA INTO @DS_NATUREZA, @NO_ORDEM
END

-- Fechando cursor
CLOSE CURSOR_NATUREZA 
DEALLOCATE CURSOR_NATUREZA
