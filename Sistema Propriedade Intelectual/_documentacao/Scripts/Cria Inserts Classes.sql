DECLARE 
  @NR_CLASSE CHAR(5),
  @DS_CLASSE VARCHAR(1000),
  @comando VarChar(MAX),
  @classeNice CHAR(5)

DECLARE CURSOR_CLASSE CURSOR Fast_Forward
FOR

--SELECT
--    NR_CLASSE,
--    DS_CLASSE
--  FROM
--    [crisrio].[dbo].[TB_CLASSE]

SELECT
	NUMERO_CLASSE,
	DESCRICAO
FROM
	dbo.CLASSE

OPEN CURSOR_CLASSE

FETCH NEXT FROM CURSOR_CLASSE INTO @NR_CLASSE, @DS_CLASSE
WHILE (@@Fetch_Status <> -1)
BEGIN
          IF (@@Fetch_Status <> -2)
          BEGIN
		    SET @classeNice = NULL
			IF (CHARINDEX('N1', @NR_CLASSE) > 0)
				SET @classeNice = 'N10' + SUBSTRING(@NR_CLASSE, CHARINDEX('N', @NR_CLASSE) + 2, LEN(@NR_CLASSE))
        -- Executando INSERT
               set @comando = 'INSERT INTO [HCOFFICE].[dbo].[CLASSE] (NUMERO_CLASSE, DESCRICAO) VALUES('+''''+ COALESCE(@classeNice, @NR_CLASSE) +''''+','+''''+ @DS_CLASSE + ''')'
                         PRINT ''
				PRINT @comando

				SET @classeNice = NULL
          END
          FETCH NEXT FROM CURSOR_CLASSE INTO @NR_CLASSE, @DS_CLASSE
END

-- Fechando cursor
CLOSE CURSOR_CLASSE 
DEALLOCATE CURSOR_CLASSE