DECLARE 
  @NR_CLASSEA CHAR(4),
  @NR_CLASSEB CHAR(4),
  @comando VarChar(MAX),
  @classeNiceA CHAR(5),
  @classeNiceB CHAR(5)

DECLARE CURSOR_CLASSE_AFINIDADE CURSOR Fast_Forward
FOR 
 -- SELECT
	--NR_CLASSEA,
	--NR_CLASSEB
 -- FROM
 --   [crisrio].[dbo].[TB_AFICLS]

 SELECT
	NUMERO_CLASSE_A,
	NUMERO_CLASSE_B
 FROM
	CLASSE_AFINIDADE

OPEN CURSOR_CLASSE_AFINIDADE

FETCH NEXT FROM CURSOR_CLASSE_AFINIDADE INTO @NR_CLASSEA, @NR_CLASSEB
WHILE (@@Fetch_Status <> -1)
BEGIN
          IF (@@Fetch_Status <> -2)
          BEGIN

			  SET @classeNiceA = NULL
				IF (CHARINDEX('N1', @NR_CLASSEA) > 0)
					SET @classeNiceA = 'N10' + SUBSTRING(@NR_CLASSEA, CHARINDEX('N', @NR_CLASSEA) + 2, LEN(@NR_CLASSEA))

			  SET @classeNiceB = NULL
				IF (CHARINDEX('N1', @NR_CLASSEB) > 0)
					SET @classeNiceB = 'N10' + SUBSTRING(@NR_CLASSEB, CHARINDEX('N', @NR_CLASSEB) + 2, LEN(@NR_CLASSEB))

        -- Executando INSERT
               set @comando = 'INSERT INTO [HCOFFICE].[dbo].[CLASSE_AFINIDADE] (NUMERO_CLASSE_A, NUMERO_CLASSE_B) VALUES('+''''+ COALESCE(@classeNiceA, @NR_CLASSEA) +''''+','+''''+ COALESCE(@classeNiceB, @NR_CLASSEB) + ''')'
                         PRINT ''
				PRINT @comando

				SET @classeNiceA = NULL
				SET @classeNiceB = NULL
          END
          FETCH NEXT FROM CURSOR_CLASSE_AFINIDADE INTO @NR_CLASSEA, @NR_CLASSEB
END

-- Fechando cursor
CLOSE CURSOR_CLASSE_AFINIDADE 
DEALLOCATE CURSOR_CLASSE_AFINIDADE
