-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 20/05/2016
-- Description:	Esta função irá retornar somente o número da classe
-- =============================================
CREATE FUNCTION GetJustClasse 
(
	@numeroClasse VARCHAR(5)
)
RETURNS VARCHAR(2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result VARCHAR(2)

	-- Add the T-SQL statements to compute the return value here
	SET @Result = (
				   CASE 
					WHEN (LEN(@numeroClasse) = 4) AND (LEFT(@numeroClasse, 1) != 'N')
					  THEN
						SUBSTRING(@numeroClasse, 1, 2)
					WHEN (LEN(@numeroClasse) = 5) AND (LEFT(@numeroClasse, 1) = 'N')
					  THEN
						SUBSTRING(@numeroClasse, 4, 2)
				  END
				 )

	-- Return the result of the function
	RETURN @Result

END
go

