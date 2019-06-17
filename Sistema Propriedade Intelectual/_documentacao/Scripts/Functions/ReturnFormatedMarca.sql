-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 27/05/2016
-- Description:	Esta função irá formatar a palavra que será salva em marca não ortografada
-- =============================================
CREATE FUNCTION [dbo].[ReturnFormatedMarca]
(
	@marca VARCHAR(1000)
)
RETURNS VARCHAR(1000)
AS
BEGIN

	DECLARE 
	  @delimitador VARCHAR(100), 
	  @value VARCHAR(1000),
	  @result VARCHAR(1000)

	-- SETANDO O DELIMITADOR
	SELECT @delimitador = ' '
 
	IF LEN(@marca) > 0 
	  SET @marca = @marca + @delimitador

	SET @result = ''
	WHILE LEN(@marca) > 0
	BEGIN
		SET	@value = LTRIM(SUBSTRING(@marca, 1, CHARINDEX(@delimitador, @marca) - 1))
		SET @result += dbo.ReturnFormatedWord(@value) + SPACE(1)

		SET	@marca = SUBSTRING(@marca, CHARINDEX(@delimitador, @marca) + 1, LEN(@marca))
	END

	IF (LEN(LTRIM(@Result)) <= 0)
		RETURN NULL

	RETURN LTRIM(@Result)
END
go

