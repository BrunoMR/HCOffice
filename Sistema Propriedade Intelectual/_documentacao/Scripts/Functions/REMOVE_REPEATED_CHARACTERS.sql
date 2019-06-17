create FUNCTION dbo.REMOVE_REPEATED_CHARACTERS
(
  @word VARCHAR(2000)
)
RETURNS VARCHAR(2000)
AS
BEGIN

  DECLARE
    @string VARCHAR(2000),
    @count INT = 1,
    @countCharacters INT = 1,
    @result VARCHAR(2000),
    @char CHAR(1),
    @indexChar INT,
    @nextChar CHAR(1),
    @indexNextChar INT,
    @lengthWord INT

  SET @string = UPPER(@word);
  SET @lengthWord = LEN(@string);

  WHILE (@count <= @lengthWord)
  BEGIN

    IF (@result IS NULL)
    BEGIN
      SET @result = ''
    END

    -- Pega um caracter por vez
    SET @char = SUBSTRING(@string, @count, 1);
    SET @indexChar = CHARINDEX(@char, @string, @count);

    SET @result += @char;

    WHILE (@countCharacters <= @lengthWord)
      BEGIN
        -- Pega o próximo caracter
        SET @nextChar = SUBSTRING(@string, @indexChar + 1, 1);
        SET @indexNextChar = CHARINDEX(@nextChar, @string, @indexChar);

        IF (@char = @nextChar)
          BEGIN

            SET @string = STUFF(@string, @indexNextChar, 1, '');
            SET @lengthWord -= 1;
            
          END

        SET @countCharacters += 1

      END

    SET @count += 1
    SET @countCharacters = 1;

    END

  RETURN @result
  END
go

