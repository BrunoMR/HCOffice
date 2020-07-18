-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 15/07/2020
-- Description:	Esta função irá retornar o ID da natureza usando a descrição como filtro
-- =============================================
CREATE FUNCTION GetNatureByDescription
(
	@descriptionNature VARCHAR(100)
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result INT = 1

	select
	  @Result = ID
	from
	  TIPO_NATUREZA
	where
	  DESCRICAO = @descriptionNature

	-- Return the result of the function
	RETURN @Result

END
go