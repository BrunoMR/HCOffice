-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 28/10/2017
-- Description:	Este procedimento irá criar os radicais substituindo 1 e 2 caracteres da palavra
-- =============================================
CREATE PROCEDURE [dbo].[INSERT_PROCESSO_RADICAL_BY_WORD]
  @processoNumero VARCHAR(20),
  @word NVARCHAR(MAX),
  @withPreffixAndSuffix BIT,
  @justMainTerm BIT,
  @theFullMainTerm BIT,
  @cutTerm BIT
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE
        @processoRadical dbo.PROCESSORADICALTYPE

  DECLARE
    @lengthWord INT = LEN(@word),
    @amountCharacters INT

  IF (@theFullMainTerm = 1)
  BEGIN
    -- Insert the full main term term as
    INSERT INTO @processoRadical
    (
      NUMERO_PROCESSO,
      RADICAL,
      LENGTH_RADICAL,
      ID_TIPO_PROCESSO_RADICAL
    )
    SELECT
      @processoNumero,
      @word,
      @lengthWord,
	  dbo.GetRadicalType('Completo')

  END
  ELSE
  BEGIN
  -- Insert the full term as main
	  INSERT INTO @processoRadical
	  (
		NUMERO_PROCESSO,
		RADICAL,
		LENGTH_RADICAL,
		ID_TIPO_PROCESSO_RADICAL
	  )
	  SELECT
		@processoNumero,
		@word,
		@lengthWord,
		dbo.GetRadicalType('Principais')
  ENd

  IF (@withPreffixAndSuffix = 1)
    BEGIN

      -- Make and insert the preffix and suffix
      SELECT
        @amountCharacters =
          CASE
			WHEN (@lengthWord <= 6)
			  THEN
			    3
            WHEN (@lengthWord > 6 AND @lengthWord <= 9)
              THEN
                5
            WHEN (@lengthWord >= 10 AND @lengthWord <= 11)
              THEN
                6
            WHEN (@lengthWord >= 12 AND @lengthWord <= 13)
              THEN
                7
			WHEN (@lengthWord >= 14 AND @lengthWord <= 15)
              THEN
                8
            WHEN (@lengthWord >= 16)
              THEN
                9
          END

      INSERT INTO @processoRadical
      (
        NUMERO_PROCESSO,
        RADICAL,
        LENGTH_RADICAL,
        ID_TIPO_PROCESSO_RADICAL
      )
      SELECT
        @processoNumero,
        LEFT(@word, @amountCharacters),
        @amountCharacters,
        dbo.GetRadicalType('Principais')
      UNION
        SELECT
        @processoNumero,
        RIGHT(@word, @amountCharacters),
        @amountCharacters,
        dbo.GetRadicalType('Principais')

      -- END Make and insert the preffix and suffix

    END

  IF (@justMainTerm = 0)
    BEGIN

      DECLARE
        @replaceExpression NVARCHAR(10),
        @radical NVARCHAR(2000),
        @count INT,
        @end INT,
        @length INT,

		    @currentLetter NVARCHAR(1),
		    @replaceLetterWithExpression NVARCHAR(11)


      -- IF (LEN(@word) >= 5 AND LEN(@word) <= 6)
      -- BEGIN
        -- With one underlines
        SET @replaceExpression = '_'
        SELECT
        @count = 1,
        @length = LEN(@replaceExpression)

        WHILE @count <= @lengthWord 
          BEGIN

          IF (LTRIM(RTRIM(STUFF(@word, @count, @length, @replaceExpression))) != '_')
            BEGIN

				-- Substituição de caracteres
				SET @radical = LTRIM(RTRIM(STUFF(@word, @count, @length, @replaceExpression)))

				INSERT INTO @processoRadical
				(
				  NUMERO_PROCESSO,
				  RADICAL,
				  LENGTH_RADICAL,
				  ID_TIPO_PROCESSO_RADICAL
				)
				SELECT
				  @processoNumero,
				  @radical,
				  LEN(@radical),
				  dbo.GetRadicalType('Modificados')

				SET @radical = NULL
				
				-- Fim Substituição de caracteres

				
				-- Increase of characteres
				
				IF (@count > 1)
				BEGIN

					SET @currentLetter = SUBSTRING(@word, @count, @length);
					SET @replaceLetterWithExpression = @replaceExpression + @currentLetter;

					SET @radical = STUFF(@word, @count, @length, @replaceLetterWithExpression)

					INSERT INTO @processoRadical
					(
					  NUMERO_PROCESSO,
					  RADICAL,
					  LENGTH_RADICAL,
					  ID_TIPO_PROCESSO_RADICAL
					)
					SELECT
					  @processoNumero,
					  @radical,
					  LEN(@radical),
					  dbo.GetRadicalType('Modificados')
				  
					SET @radical = NULL

				END
				
				-- FIM Inclusão de caracteres
            END

            SET @count = @count + 1

          END
		  -- Fim With one underlines
      --END

      IF (LEN(@word) > 6)
      BEGIN

        -- With two underlines
        SET @replaceExpression = '__'
        SELECT
          @count = 1,
          @length = LEN(@replaceExpression)

        SET @end = (LEN(@word) - @length) + 1

		WHILE @count <= @lengthWord
          BEGIN

            IF (LTRIM(RTRIM(STUFF(@word, @count, @length, @replaceExpression))) != '__')
              BEGIN
			    
				-- Substituição de caracteres
				IF (@count <= @end)
				BEGIN
					SET @radical = LTRIM(RTRIM(STUFF(@word, @count, @length, @replaceExpression)))
					INSERT INTO @processoRadical
					(
					  NUMERO_PROCESSO,
					  RADICAL,
					  LENGTH_RADICAL,
					  ID_TIPO_PROCESSO_RADICAL
					)
					SELECT
						@processoNumero,
						@radical,
						LEN(@radical),
						dbo.GetRadicalType('Modificados')

					SET @radical = NULL
				END
				
				-- FIM Substituição de caracteres
				
				-- Inclusão de caracteres

				IF (@count > 1)
				BEGIN

				  SET @currentLetter = SUBSTRING(@word, @count, @length);
				  SET @replaceLetterWithExpression = @replaceExpression + @currentLetter;
				  SET @radical = STUFF(@word, @count, 1, @replaceLetterWithExpression);
				
				  INSERT INTO @processoRadical
				  (
					NUMERO_PROCESSO,
					RADICAL,
					LENGTH_RADICAL,
					ID_TIPO_PROCESSO_RADICAL
				  )
				  SELECT
					@processoNumero,
					@radical,
					LEN(@radical),
					dbo.GetRadicalType('Modificados')

				  SET @radical = NULL

				END
				  
				-- FIM Inclusão de caracteres
				  
              END
			  
            SET @count = @count + 1

          END
		  
		-- Fim With two underlines

      END

    END

	IF (@cutTerm = 1)
	  BEGIN

	    INSERT INTO @processoRadical
        (
					NUMERO_PROCESSO,
					RADICAL,
					LENGTH_RADICAL,
					ID_TIPO_PROCESSO_RADICAL
        )
      SELECT
        @processoNumero,
        Term,
        LEN(Term),
        dbo.GetRadicalType('Cortados')
      FROM
        GetRadicalSizesByAmountCharacteres(@word)
        CROSS APPLY SplitStringByAmountCharacteres(@word, Size)
      WHERE
        Size = LEN(Term)

    END

  INSERT INTO PROCESSO_RADICAL
  (
    NUMERO_PROCESSO,
    RADICAL,
    LENGTH_RADICAL,
    ID_TIPO_PROCESSO_RADICAL
  )
  SELECT
    NUMERO_PROCESSO,
    RADICAL,
    LENGTH_RADICAL,
    ID_TIPO_PROCESSO_RADICAL
  FROM
    @processoRadical

END
GO