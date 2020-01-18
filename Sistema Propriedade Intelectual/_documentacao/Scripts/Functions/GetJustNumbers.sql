-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 09/12/2019
-- Description:	Este procedimento irá retornar somentes os números de um texto
-- Ex:
-- SELECT dbo.GetJustNumbers('018130041513 opa machado teste')
-- =============================================
CREATE FUNCTION dbo.GetJustNumbers
(
  @text VARCHAR(256)
)
RETURNS VARCHAR(256)
AS
BEGIN
  DECLARE @intAlpha INT
  SET @intAlpha = PATINDEX('%[^0-9]%', @text)
  BEGIN
    WHILE @intAlpha > 0
    BEGIN
      SET @text = STUFF(@text, @intAlpha, 1, '' )
      SET @intAlpha = PATINDEX('%[^0-9]%', @text )
    END
  END
  RETURN ISNULL(@text,0)
END
GO
