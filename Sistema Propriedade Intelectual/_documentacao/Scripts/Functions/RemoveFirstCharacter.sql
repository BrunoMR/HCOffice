-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 20/10/2017
-- Description:	Esta função irá retirar o primeiro caracter do termo enviado por parâmetro
CREATE FUNCTION [dbo].[RemoveFirstCharacter]
(
  @word VARCHAR(2000)-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 20/10/2017
-- Description:	Esta função irá retirar o primeiro caracter do termo enviado por parâmetro
ALTER FUNCTION [dbo].[RemoveFirstCharacter]
(
  @word VARCHAR(255)
)
RETURNS VARCHAR(255)
AS
BEGIN

  IF (LEN(@word) <= 0)
    RETURN NULL;

  IF (LEFT(@word, 1) = '''')
  BEGIN
    RETURN SUBSTRING(@word, 2, LEN(@word) - 1)
  END

  RETURN @word
END
go


)
RETURNS VARCHAR(2000)
AS
BEGIN

  IF (LEN(@word) <= 0)
    RETURN NULL;

  IF (LEFT(@word, 1) = '''')
  BEGIN
    RETURN SUBSTRING(@word, 2, LEN(@word) - 1)
  END

  RETURN @word
END
go

