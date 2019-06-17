-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 27/05/2016
-- Description:	Esta função irá retirar os caracteres duplicados e setar a palavra para caixa alta 
-- =============================================
CREATE FUNCTION [dbo].[ReturnFormatedWord]
(
	@word VARCHAR(1000)
)
RETURNS VARCHAR(1000)
AS
BEGIN
	DECLARE
	  @totalCaracters INT,
	  @countCaracters INT,
	  @charToSearch CHAR(1),
	  @char CHAR(1),
	  @charIndex INT,
	  @repeatChar CHAR(1),
	  @repeatCharIndex INT,
	  @Result VARCHAR(1000)

	SET @totalCaracters = LEN(@word)

	SET @charToSearch = SUBSTRING(@word, 1, 1)

	SET @charIndex = CHARINDEX(@charToSearch, @word)
	SET @char = SUBSTRING(@word, @charIndex, 1)

	SET @repeatCharIndex = CHARINDEX(@charToSearch, @word, @charIndex)
	SET @repeatChar = SUBSTRING(@word, @repeatCharIndex, 1)

	SET @Result = ''
	SET @countCaracters = 1
	WHILE @countCaracters <= @totalCaracters
		BEGIN  
			SET @charToSearch = SUBSTRING(@word, @countCaracters, 1)

			SET @charIndex = CHARINDEX(@charToSearch, @word, @countCaracters)
			SET @char = SUBSTRING(@word, @charIndex, 1)

			SET @repeatCharIndex = CHARINDEX(@charToSearch, @word, @charIndex + 1)
			SET @repeatChar = SUBSTRING(@word, @repeatCharIndex, 1)

			IF ((@repeatCharIndex > 0) AND ((@repeatCharIndex - @charIndex) = 1) AND (ISNUMERIC(@repeatChar) = 0))
				BEGIN
				SET @Result += UPPER(@char)
				SET @countCaracters += 1
				END
			ELSE
				SET @Result += UPPER(@char)

			SET @countCaracters += 1
		END;

	-- Return the result of the function
	RETURN @Result
END
go

