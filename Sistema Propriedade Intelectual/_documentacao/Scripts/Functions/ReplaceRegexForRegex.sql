-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 05/10/2019
-- Description:	Este procedimento irá substituir o padrão regex encontrado na palavra enviada por outro padrão regex
-- Ex:
-- SELECT dbo.ReplaceRegexForRegex('[aeiou]', 'cristiano', '[aeiou]')
-- =============================================
ALTER FUNCTION dbo.ReplaceRegexForRegex
(
  @regex                            VARCHAR(100),
  @word                             VARCHAR(255),
  @expressionToChange               VARCHAR(100)
)
RETURNS VARCHAR(255)
AS
BEGIN

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