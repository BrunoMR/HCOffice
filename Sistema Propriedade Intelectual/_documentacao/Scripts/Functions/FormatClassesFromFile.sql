-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 20/10/2017
-- Description:	Esta função irá encontrar e montar as classes encontradas
-- =============================================
CREATE FUNCTION dbo.FormatClassesFromFile
(
	@classe VARCHAR(30)
)
RETURNS @output TABLE(class NVARCHAR(5))
AS
BEGIN
	-- Declare the return variable here
	DECLARE
		@classeNumber VARCHAR(2)

	SELECT
	  @classe = dbo.RemoveFirstCharacter(@classe)

	IF (LEFT(@classe, 1) = 'N')
	  BEGIN

			DECLARE
				@edition VARCHAR(2),
				@firstBracket INT = CHARINDEX('(', @classe),
				@lastBracket INT = CHARINDEX(')', @classe)

			SELECT
				@edition = RIGHT('0' + SUBSTRING(@classe, @firstBracket + 1, (@lastBracket - @firstBracket) - 1), 2),
			  @classeNumber = LTRIM(RTRIM(SUBSTRING(@classe, @lastBracket + 1, LEN(@classe) - @lastBracket)))

			INSERT INTO @output
        (
          class
        )
      SELECT
        CONCAT('N', @edition, @classeNumber)

	  END
	ELSE
		BEGIN

      DECLARE
        @bracket INT = CHARINDEX('/', @classe)

      SET @classeNumber = LTRIM(RTRIM(SUBSTRING(@classe, 1, @bracket - 1)))
      SET @classe = SUBSTRING(@classe, @bracket + 1, LEN(@classe) - @bracket)

      INSERT INTO @output
        (
          class
        )
      SELECT
        @classeNumber + splitdata
      FROM
        dbo.SplitString(@classe, '.')

		END

	RETURN

END
go

