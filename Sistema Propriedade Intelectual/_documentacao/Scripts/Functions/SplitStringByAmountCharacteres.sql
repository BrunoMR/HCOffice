-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 05/03/2019
-- Description:	Esta função irá separar o termos com a quantidade de caracteres passada por parâmetro
-- =============================================
ALTER FUNCTION [dbo].[SplitStringByAmountCharacteres]
(
    @string VARCHAR(1000),
    @amountCharacteres smallint
)
RETURNS @output TABLE(Term NVARCHAR(20)
)
BEGIN
    DECLARE
      @count smallint,
      @timesToRepeat smallint

    SELECT
      @string = UPPER(LTRIM(RTRIM(@string)))

    SELECT
      @count = 1,
      @timesToRepeat = LEN(@string) - (@amountCharacteres - 1)

    WHILE @count <= @timesToRepeat
        BEGIN

            INSERT INTO @output
            VALUES(LTRIM(RTRIM(SUBSTRING(@string, @count, @amountCharacteres))))

            SET @count = @count + 1
        END

    RETURN
END
go