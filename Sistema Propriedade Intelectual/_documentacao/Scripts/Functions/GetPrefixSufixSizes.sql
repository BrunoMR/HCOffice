-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 19/07/2020
-- Description:	Esta função irá retornar os tamanhos de prefixos e sufixos que a palavra deve criar
-- =============================================
ALTER FUNCTION [dbo].[GetPrefixSufixSizes]
(
    @string NVARCHAR(MAX)
)
RETURNS @output TABLE(Size smallint)
BEGIN
    DECLARE
      @amountCharacteres smallint

    SELECT
      @string = LTRIM(RTRIM(@string))

    SELECT
      @amountCharacteres = LEN(@string)

        IF(@amountCharacteres = 3)
            INSERT INTO @output (Size) VALUES (2)

        IF(@amountCharacteres >= 4 AND @amountCharacteres <= 7 OR(@amountCharacteres >= 4))
            INSERT INTO @output (Size) VALUES (3)

        IF(@amountCharacteres >= 8 AND @amountCharacteres <= 10 OR(@amountCharacteres >= 8))
            INSERT INTO @output (Size) VALUES (4)

        IF(@amountCharacteres >= 11 AND @amountCharacteres <= 13 OR(@amountCharacteres >= 11))
            INSERT INTO @output (Size) VALUES (5)

        IF(@amountCharacteres >= 14 AND @amountCharacteres <= 16 OR(@amountCharacteres >= 14))
            INSERT INTO @output (Size) VALUES (6)

        IF(@amountCharacteres >= 17)
            INSERT INTO @output (Size) VALUES (7)

    RETURN
END
go

