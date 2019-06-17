-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 20/10/2017
-- Description:	Esta função ortografar cada termo de uma possível marca composta e depois retornar os termos concatenados
CREATE FUNCTION [dbo].[OrtografarSplitString]
(
    @word NVARCHAR(MAX)
)
RETURNS NVARCHAR(MAX)
BEGIN
    DECLARE
      @result NVARCHAR(MAX)

    SELECT
      @result = COALESCE(@result + ' ', '') + dbo.ORTOGRAFAR(splitdata)
    FROM
        dbo.SplitString(@word, ' ')

  RETURN LTRIM(RTRIM(@result))
END
go

