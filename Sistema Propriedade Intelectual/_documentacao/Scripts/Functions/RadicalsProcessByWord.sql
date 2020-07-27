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
    @fullMainTerm BIT = 0,
    @cuttedTerm BIT = 0
)
RETURNS @output TABLE
    (
        NUMERO_PROCESSO             VARCHAR(20) NOT NULL,
        RADICAL                     NVARCHAR(255) NOT NULL,
        LENGTH_RADICAL              INT NOT NULL,
        MAIN                        BIT NOT NULL,
        ID_TIPO_PROCESSO_RADICAL    INT,
        BIGGER                      BIT DEFAULT 0
    )
AS
BEGIN

  DECLARE
    @lengthWord INT = LEN(@word),
    @amountCharacters INT,
    @radicalProcess PROCESSORADICALTYPE

  -- Insert the full term as main
  INSERT INTO @radicalProcess
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

        INSERT INTO @radicalProcess
          (
            NUMERO_PROCESSO,
            RADICAL,
            LENGTH_RADICAL,
            MAIN,
            ID_TIPO_PROCESSO_RADICAL,
            BIGGER
          )
        SELECT
            @processoNumero,
            PrefixSufix,
            Size,
            0,
            Type,
            case when Bigger = Size then 1 else 0 end
        FROM
            GetPrefixSufixSizes(@word)
            cross apply GetPrefixSufixByAmount(@word, Size)

      -- END Make and insert the preffix and suffix

    END

  IF (@cuttedTerm = 1)
    BEGIN

        INSERT INTO @radicalProcess
          (
            NUMERO_PROCESSO,
            RADICAL,
            LENGTH_RADICAL,
            MAIN,
            ID_TIPO_PROCESSO_RADICAL,
            BIGGER
          )
        SELECT
            @processoNumero,
            Term,
            Size,
            0,
            dbo.GetRadicalType('Termos Cortados'),
            case when Bigger = Size then 1 else 0 end
        FROM
            GetRadicalSizes(@word)
            cross apply SplitStringByAmountCharacteres(@word, Size)

      -- END Make and insert the preffix and suffix

    END

  IF ((@justMainTerm = 0) AND (@lengthWord = 4))
    BEGIN
        INSERT INTO @radicalProcess
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
                INSERT INTO @radicalProcess
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

                        INSERT INTO @radicalProcess
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
                    INSERT INTO @radicalProcess
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

				  INSERT INTO @radicalProcess
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
  (NUMERO_PROCESSO, RADICAL, LENGTH_RADICAL, MAIN, ID_TIPO_PROCESSO_RADICAL, BIGGER)
  SELECT
    DISTINCT
    NUMERO_PROCESSO,
    RADICAL,
    LENGTH_RADICAL,
    MAIN,
    ID_TIPO_PROCESSO_RADICAL,
    BIGGER
  FROM
    @radicalProcess

    RETURN

END
go

