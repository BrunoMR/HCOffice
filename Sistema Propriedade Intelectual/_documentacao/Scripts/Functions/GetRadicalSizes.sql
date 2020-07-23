-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 19/07/2020
-- Description:	Esta função irá retornar os tamanhos de radicais que a palavra deve criar
-- =============================================
CREATE FUNCTION [dbo].[GetRadicalSizes]
(
    @string VARCHAR(1000)
)
RETURNS @output TABLE(Size smallint, Bigger smallint)
BEGIN
    DECLARE
      @amountCharacteres smallint

    SELECT
      @string = LTRIM(RTRIM(@string))

    SELECT
      @amountCharacteres = LEN(@string)

        IF(@amountCharacteres >= 4 AND @amountCharacteres <= 5 OR(@amountCharacteres >= 4))
            INSERT INTO @output (Size) VALUES (4)

        IF(@amountCharacteres >= 6 AND @amountCharacteres <= 7 OR(@amountCharacteres >= 6))
            INSERT INTO @output (Size) VALUES (5)

        IF(@amountCharacteres >= 8 AND @amountCharacteres <= 9 OR(@amountCharacteres >= 8))
            INSERT INTO @output (Size) VALUES (6)

        IF(@amountCharacteres >= 10 AND @amountCharacteres <= 11 OR(@amountCharacteres >= 10))
            INSERT INTO @output (Size) VALUES (7)

        IF(@amountCharacteres >= 12 AND @amountCharacteres <= 13 OR(@amountCharacteres >= 12))
            INSERT INTO @output (Size) VALUES (8)

        IF(@amountCharacteres >= 14 AND @amountCharacteres <= 15 OR(@amountCharacteres >= 14))
            INSERT INTO @output (Size) VALUES (9)

        IF(@amountCharacteres >= 16)
            INSERT INTO @output (Size) VALUES (10)

    update
        @output
    set
        Bigger = (select max(Size) from @output)

    RETURN
END
go

