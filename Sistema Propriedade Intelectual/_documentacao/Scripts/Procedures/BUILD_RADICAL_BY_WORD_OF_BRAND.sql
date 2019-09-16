-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 01/11/2017
-- Description:	Este procedimento quebra e ortografa cada termo da marca e cria os radicais para cada termo
-- =============================================
CREATE PROCEDURE [dbo].[BUILD_RADICAL_BY_WORD_OF_BRAND]
  @processoNumero VARCHAR(20),
  @brand NVARCHAR(MAX),
  @withPreffixAndSuffix BIT,
  @justMainTerm BIT
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE
      @marca NVARCHAR(2000)

    SELECT
      @brand = REPLACE(@brand, '.', ' ')

    DECLARE procesesCursor CURSOR
      LOCAL FAST_FORWARD
    FOR SELECT
          DISTINCT
          dbo.ORTOGRAFAR(splitData) as term
        FROM
           dbo.SplitString(@brand, ' ')
        WHERE
          dbo.ORTOGRAFAR(splitData) IS NOT NULL
          AND LEN(dbo.ORTOGRAFAR(splitData)) >= 5

    OPEN procesesCursor

    FETCH procesesCursor INTO @marca;

    WHILE (@@FETCH_STATUS = 0)
      BEGIN

        EXECUTE INSERT_PROCESSO_RADICAL_BY_WORD
          @processoNumero,
          @marca,
          @withPreffixAndSuffix,
          @justMainTerm

        FETCH procesesCursor INTO @marca;
      END

    CLOSE procesesCursor;
    DEALLOCATE procesesCursor;

END

GO


