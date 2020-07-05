-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 17/09/2019
-- Description:	Este procedimento irá criar os radicais substituindo e incluíndo 1 e 2 caracteres da palavra
-- Ex:
-- select
--     *
-- from
--     dbo.RadicalsProcessByWord('123456789', 'bruno', 0, 0)
-- =============================================
CREATE FUNCTION dbo.RadicalsProcessByWord
(
    @processoNumero VARCHAR(20),
    @word NVARCHAR(255),
    @withPreffixAndSuffix BIT,
    @justMainTerm BIT,
    @fullMainTerm BIT = 0
)
RETURNS @output TABLE
    (
        NUMERO_PROCESSO             VARCHAR(20) NOT NULL,
        RADICAL                     NVARCHAR(255) NOT NULL,
        LENGTH_RADICAL              INT NOT NULL,
        MAIN                        BIT NOT NULL,
        ID_TIPO_PROCESSO_RADICAL    INT
    )
AS
BEGIN

  DECLARE
    @lengthWord INT = LEN(@word),
    @amountCharacters INT,
    @radicalProdcess PROCESSORADICALTYPE

  -- Insert the full term as main
  INSERT INTO @radicalProdcess
  (
    NUMERO_PROCESSO,
    RADICAL,
    LENGTH_RADICAL,
    MAIN,
    ID_TIPO_PROCESSO_RADICAL
  )
  SELECT
    @processoNumero,
    @word,
    @lengthWord,
    1,
    case @fullMainTerm
        when 1
            then
                dbo.GetRadicalType('Termos Completo Ortografado')
        else
            dbo.GetRadicalType('Termos Principais')
    end

  IF (@withPreffixAndSuffix = 1)
    BEGIN

      -- Make and insert the preffix and suffix
      SELECT
        @amountCharacters =
          CASE
            WHEN @lengthWord = 3
              THEN
                2
            WHEN (@lengthWord >= 4 AND @lengthWord <= 6)
              THEN
                3
            WHEN (@lengthWord >= 7 AND @lengthWord <= 9)
              THEN
                4
            WHEN (@lengthWord >= 10 AND @lengthWord <= 12)
              THEN
                4
            WHEN (@lengthWord >= 13 AND @lengthWord <= 16)
              THEN
                5
            WHEN (@lengthWord >= 17)
              THEN
                6
          END

      INSERT INTO @radicalProdcess
      (
        NUMERO_PROCESSO,
        RADICAL,
        LENGTH_RADICAL,
        MAIN
      )
      SELECT
        @processoNumero,
        LEFT(@word, @amountCharacters),
        @amountCharacters,
        1
      UNION
        SELECT
        @processoNumero,
        RIGHT(@word, @amountCharacters),
        @amountCharacters,
        1

      -- END Make and insert the preffix and suffix

    END

  IF ((@justMainTerm = 0) AND (@lengthWord = 4))
    BEGIN
        INSERT INTO @radicalProdcess
        (
          NUMERO_PROCESSO,
          RADICAL,
          LENGTH_RADICAL,
          MAIN,
          ID_TIPO_PROCESSO_RADICAL
        )
        SELECT
          @processoNumero,
          dbo.ReplaceRegexForRegex('[aeiou]', @word, '[aeiou]'),
          LEN(@word),
          0,
          dbo.GetRadicalType('Termos Modificados')
    END

  IF ((@justMainTerm = 0) AND (@lengthWord > 4))
    BEGIN

      DECLARE
        @replaceExpression              VARCHAR(100),
        @radical                        VARCHAR(255),
        @count                          INT,
        @end                            INT,
        @length                         INT,
        @currentLetter                  VARCHAR(1),
		@replaceLetterWithExpression    VARCHAR(11)

      -- With one underlines
      SET @replaceExpression = '_'
      SELECT
        @count = 1,
        @length = LEN(@replaceExpression)

      SET @end = (LEN(@word) - @length) + 1

        WHILE @count <= @end
          BEGIN

            IF (LTRIM(RTRIM(STUFF(@word, @count, @length, @replaceExpression))) != '_')
              BEGIN

				-- Substituição de caracteres
                SET @radical = LTRIM(RTRIM(STUFF(@word, @count, @length, @replaceExpression)))
                INSERT INTO @radicalProdcess
                (
                  NUMERO_PROCESSO,
                  RADICAL,
                  LENGTH_RADICAL,
                  MAIN,
                  ID_TIPO_PROCESSO_RADICAL
                )
                SELECT
                  @processoNumero,
                  @radical,
                  LEN(@radical),
                  0,
                  dbo.GetRadicalType('Termos Modificados')

                SET @radical = NULL

				-- Fim Substituição de caracteres


                -- Increase of characteres
                IF (@lengthWord > 4)
                BEGIN
                    IF (@count > 1)
                    BEGIN

                        SET @currentLetter = SUBSTRING(@word, @count, @length);
                        SET @replaceLetterWithExpression = @replaceExpression + @currentLetter;

                        SET @radical = STUFF(@word, @count, @length, @replaceLetterWithExpression)

                        INSERT INTO @radicalProdcess
                        (
                          NUMERO_PROCESSO,
                          RADICAL,
                          LENGTH_RADICAL,
                          MAIN,
                          ID_TIPO_PROCESSO_RADICAL
                        )
                        SELECT
                          @processoNumero,
                          @radical,
                          LEN(@radical),
                          0,
                          dbo.GetRadicalType('Termos Modificados')

                        SET @radical = NULL

                    END
                END


				-- END Increase of characteres
              END

            SET @count = @count + 1

          END
		  -- END With one underlines
      --END

      IF (LEN(@word) > 6) --OR LEN(@word) = 5)
      BEGIN

        -- With two underlines
        SET @replaceExpression = '__'
        SELECT
          @count = 1,
          @length = LEN(@replaceExpression)

        SET @end = LEN(@word) -- (LEN(@word) - @length) + 1

        WHILE @count <= @end
          BEGIN

            IF (LTRIM(RTRIM(STUFF(@word, @count, @length, @replaceExpression))) != '__')
              BEGIN

                --IF (LEN(@word) != 5)
                BEGIN
                    -- Replacemant of characteres
                    IF (@count <= @end)
                    BEGIN
                    SET @radical = LTRIM(RTRIM(STUFF(@word, @count, @length, @replaceExpression)))
                    INSERT INTO @radicalProdcess
                    (
                      NUMERO_PROCESSO,
                      RADICAL,
                      LENGTH_RADICAL,
                      MAIN,
                      ID_TIPO_PROCESSO_RADICAL
                    )
                    SELECT
                        @processoNumero,
                        @radical,
                        LEN(@radical),
                        0,
                        dbo.GetRadicalType('Termos Modificados')

                        SET @radical = NULL
                    END

                    -- END replacemant of characteres
                END

				-- Increase of characteres

				IF (@count > 1)
				BEGIN

				  SET @currentLetter = SUBSTRING(@word, @count, @length);
				  SET @replaceLetterWithExpression = @replaceExpression + @currentLetter;
				  SET @radical = STUFF(@word, @count, 1, @replaceLetterWithExpression);

				  INSERT INTO @radicalProdcess
				  (
					NUMERO_PROCESSO,
					RADICAL,
					LENGTH_RADICAL,
					MAIN,
				    ID_TIPO_PROCESSO_RADICAL
				  )
				  SELECT
					@processoNumero,
					@radical,
					LEN(@radical),
					0,
				    dbo.GetRadicalType('Termos Modificados')

                SET @radical = NULL

				END

				-- END Increase of characteres

              END

            SET @count = @count + 1

          END

		-- End With two underlines

      END

    END

  INSERT INTO @output
  (NUMERO_PROCESSO, RADICAL, LENGTH_RADICAL, MAIN, ID_TIPO_PROCESSO_RADICAL)
  SELECT
    DISTINCT
    NUMERO_PROCESSO,
    RADICAL,
    LENGTH_RADICAL,
    MAIN,
    ID_TIPO_PROCESSO_RADICAL
  FROM
    @radicalProdcess

    RETURN

END
go

