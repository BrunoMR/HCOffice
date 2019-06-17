-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 05/03/2019
-- Description:	Esta função irá retornar o maior tamanho de radicais que a palavra pode ter
-- =============================================
CREATE FUNCTION [dbo].[GetMaxSizeRadicalByAmountCharacteres]
(
    @string NVARCHAR(MAX)
)
RETURNS INT
BEGIN
    DECLARE
      @output INT,
      @amountCharacteres INT

    SELECT
      @string = LTRIM(RTRIM(@string))

    SELECT
      @amountCharacteres = LEN(@string)

    SELECT
      @output = CASE
                    WHEN @amountCharacteres >= 17
                        THEN
                            8
                    WHEN (@amountCharacteres >= 14 AND @amountCharacteres <= 16)
                        THEN
                            7
                    WHEN (@amountCharacteres >= 11 AND @amountCharacteres <= 13)
                        THEN
                            6
                    WHEN (@amountCharacteres >= 8 AND @amountCharacteres <= 10)
                        THEN
                            5
                    WHEN (@amountCharacteres >= 4 AND @amountCharacteres <= 7)
                        THEN
                            4
                    WHEN (@amountCharacteres >= 3)
                        THEN
                            3
                    WHEN (@amountCharacteres >= 2)
                        THEN
                            2
                    ELSE
                        1
                END
    RETURN @output
END
GO