-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 05/03/2019
-- Description:	Esta função irá separar o termos com a quantidade de caracteres passada por parâmetro
-- =============================================
ALTER FUNCTION [dbo].[SplitStringByAmountCharacteres]
(
    @string NVARCHAR(MAX),
    @amountCharacteres INT
)
RETURNS @output TABLE(Term NVARCHAR(20)
)
BEGIN
    DECLARE
      @start INT,
      @end INT,
	  @amountAuxiliar INT

    SELECT
      @string = UPPER(LTRIM(RTRIM(@string)))

    SELECT
      @start = 1,
      @end = 0

    WHILE LEN(@string) >= @start
      BEGIN
		SET @amountAuxiliar = LEN(LTRIM(RTRIM(SUBSTRING(@string, @start, @amountCharacteres))));

        IF (@end = 0 OR @amountAuxiliar >= @amountCharacteres)
            BEGIN
                INSERT INTO @output
                VALUES(LTRIM(RTRIM(SUBSTRING(@string, @start, @amountCharacteres))))
            END

        SET @end = LEN(@string) - (@amountCharacteres - 1)
        IF (@end <= 0)
          SET @end = LEN(@string)

        SET @string = LTRIM(RTRIM(SUBSTRING(@string, @amountCharacteres + 1, @end)))

      END
    RETURN
END
go