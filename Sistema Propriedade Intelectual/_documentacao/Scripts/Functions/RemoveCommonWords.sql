-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 10/10/2019
-- Description:	Este procedimento irá remover as palavras de uso comum da marca
-- Ex:
-- SELECT dbo.ReplaceRegexForRegex('[aeiou]', 'cristiano', '[aeiou]')
-- =============================================
CREATE FUNCTION dbo.RemoveCommonWords
(
    @word   VARCHAR(255),
    @class  VARCHAR(10)
)
RETURNS VARCHAR(255)
AS
BEGIN

    SELECT
      *
    FROM
        PALAVRA_USO_COMUM

    DECLARE
        @expressionToChangeIfIsTheSame  VARCHAR(100) = '*',
        @indexToChange                  INT = PATINDEX('%' + @regex + '%', @word),
        @isSame                         BIT = 0,
        @count                          INT = 1,
        @amountIndex                    INT = 0,
        @lengthToChange                 INT = (LEN(@expressionToChange) - 1)

    IF (@regex = @expressionToChange)
        SET @isSame = 1

    DECLARE
        @index AS TABLE (Id INT IDENTITY(1, 1), RegexIndex INT)

    WHILE @indexToChange > 0
    BEGIN
        insert into @index values (@indexToChange)

        IF(@isSame = 1)
            SET @word = STUFF(@word, @indexToChange, 1, @expressionToChangeIfIsTheSame)
        ELSE
            SET @word = STUFF(@word, @indexToChange, 1, @expressionToChange)

        SET @indexToChange = PATINDEX('%' + @regex + '%', @word)

    END

    IF exists(select 1 from @index)
    BEGIN
        SET @amountIndex = (select COUNT(1) from @index)
        SET @indexToChange = 0

        WHILE @count <= @amountIndex
        BEGIN
            select
                @indexToChange = case Id
                                    when 1
                                        then
                                            RegexIndex
                                    else
                                        (@lengthToChange * (Id -1) + RegexIndex)
                                end
            from
                @index
            where
                Id = @count

            SET @word = STUFF(@word, @indexToChange, 1, @expressionToChange)
            SET @count = @count + 1

        END
    END

  RETURN @word
END
go