-- CREATE FUNCTION dbo.ReplaceRegexForRegex
-- (
declare
  @regex                VARCHAR(100) = '[aeiou]',
  @word                 VARCHAR(255) = 'bruno',
  @expressionToChange   VARCHAR(100) = '[aeiou]'
-- )
-- RETURNS VARCHAR(255)
-- AS
-- BEGIN

    DECLARE
        @indexToChange INT = PATINDEX('%' + @regex + '%', @word),
        @isSame BIT = 0

    IF (@regex = @expressionToChange)
        SET @isSame = 1

    select @isSame [@isSame]

    DECLARE
        @index AS TABLE (Id INT IDENTITY(1, 1), IndexRegex INT)

   select @indexToChange [@indexToChange]

    WHILE @indexToChange > 0
    BEGIN
        SET @word = STUFF(@word, @indexToChange, 1, @expressionToChange)
        SET @indexToChange = PATINDEX(@regex, @word)

--         select
--            @word [@word],
--            @indexToChange [@indexToChange]
    END

    select @word [@word]
--   RETURN @word
-- END
-- go