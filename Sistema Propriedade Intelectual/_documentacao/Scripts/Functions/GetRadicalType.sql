-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 25/08/2018
-- Description:	Esta função irá retornar o Id do tipo de processo desejado, se não encontrar irá retornar o ID do tipo de termos modificados
-- =============================================
ALTER FUNCTION GetRadicalType
(
	@descriptionToSearch VARCHAR(50)
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE 
	  @Result INT

  SELECT
    TOP 1
    @Result = ID
  FROM
    TIPO_PROCESSO_RADICAL
  WHERE
    DESCRIPTION LIKE '%' + @descriptionToSearch  +'%'
  
  IF (@Result IS NULL)
  BEGIN

	  SELECT
		TOP 1
		@Result = ID
	  FROM
		TIPO_PROCESSO_RADICAL
	  WHERE
		DESCRIPTION LIKE '%Modificados%'
  END

	-- Return the result of the function
	RETURN @Result

END
GO