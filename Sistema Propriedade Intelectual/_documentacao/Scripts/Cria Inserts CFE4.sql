DECLARE 
  @CD_CFE4 CHAR(6),
  @DS_CFE4 VARCHAR(80),
  @comando VarChar(MAX)

DECLARE CURSOR_CFE4 CURSOR Fast_Forward
FOR 
 -- SELECT
	--CD_CFE4,
	--DS_CFE4
 -- FROM
 --   [crisrio].[dbo].[TB_CFE4]
 
	SELECT
		CODIGO_CF4,
		DESCRICAO
	FROM
		dbo.CF4

OPEN CURSOR_CFE4

FETCH NEXT FROM CURSOR_CFE4 INTO @CD_CFE4, @DS_CFE4
WHILE (@@Fetch_Status <> -1)
BEGIN
          IF (@@Fetch_Status <> -2)
          BEGIN
        -- Executando INSERT
               set @comando = 'INSERT INTO [HCOFFICE].[dbo].[CFE4] (CODIGO_CF4, DESCRICAO) VALUES('+''''+ @CD_CFE4 +''''+','+''''+ @DS_CFE4 + ''')'
                         PRINT ''
				PRINT @comando
          END
          FETCH NEXT FROM CURSOR_CFE4 INTO @CD_CFE4, @DS_CFE4
END

-- Fechando cursor
CLOSE CURSOR_CFE4 
DEALLOCATE CURSOR_CFE4
