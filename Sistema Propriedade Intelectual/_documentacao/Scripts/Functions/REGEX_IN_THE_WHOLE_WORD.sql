CREATE FUNCTION dbo.REGEX_IN_THE_WHOLE_WORD
(
  @regex VARCHAR(2000),
  @word VARCHAR(2000),
  @indexToChange INT,
  @lengthPattern INT,
  @expressionToChange VARCHAR(255),
  @quantityCharacterToChange INT
)
RETURNS VARCHAR(2000)
AS
BEGIN

  DECLARE  
    @string VARCHAR(2000),
    @count INT = 1 ,
    @lengthWord INT,
    @index INT = -1

  SET @string = UPPER(@word);
  SET @lengthWord = LEN(@string);
  
  IF (@quantityCharacterToChange IS NULL)
    SET @quantityCharacterToChange = LEN(@expressionToChange);

  WHILE (@count <= @lengthWord)
    BEGIN

      SET @index = PATINDEX('%'+ @regex +'%',@string);
      IF (@index > 0)
        BEGIN

          SET @string = STUFF(@string, @index + @indexToChange, @quantityCharacterToChange, @expressionToChange);

          SET @index = -1;
          SET @count += @lengthPattern;

        END
      ELSE
        SET @count += 1

    END

  RETURN @string
END
go

