-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 19/07/2020
-- Description:	Esta função irá retornar os tamanhos de prefixos e sufixos que a palavra deve criar
-- =============================================
ALTER FUNCTION [dbo].[GetPrefixSufixSizes]
(
    @string NVARCHAR(MAX)
)
RETURNS @output TABLE(Size smallint, Bigger smallint)
BEGIN

    declare
        @aux TABLE(Size smallint, Bigger smallint)

    DECLARE
      @amountCharacteres smallint

    SELECT
      @string = LTRIM(RTRIM(@string))

    SELECT
      @amountCharacteres = LEN(@string)

        IF(@amountCharacteres = 3)
            INSERT INTO @aux (Size) VALUES (2)

        IF(@amountCharacteres >= 4 AND @amountCharacteres <= 5 OR(@amountCharacteres >= 4))
            INSERT INTO @aux (Size) VALUES (3)

        IF(@amountCharacteres >= 6 AND @amountCharacteres <= 7 OR(@amountCharacteres >= 6))
            INSERT INTO @aux (Size) VALUES (3)

        IF(@amountCharacteres >= 8 AND @amountCharacteres <= 9 OR(@amountCharacteres >= 8))
            INSERT INTO @aux (Size) VALUES (4)

        IF(@amountCharacteres >= 10 AND @amountCharacteres <= 11 OR(@amountCharacteres >= 10))
            INSERT INTO @aux (Size) VALUES (5)

        IF(@amountCharacteres >= 12 AND @amountCharacteres <= 13 OR(@amountCharacteres >= 12))
            INSERT INTO @aux (Size) VALUES (5)

        IF(@amountCharacteres >= 14 AND @amountCharacteres <= 15 OR(@amountCharacteres >= 14))
            INSERT INTO @aux (Size) VALUES (6)

        IF(@amountCharacteres >= 16)
            INSERT INTO @aux (Size) VALUES (7)

    INSERT INTO @output
        (Size, Bigger)
    select
        distinct
        Size,
        (select max(Size) from @aux)
    from
        @aux

    RETURN
END
