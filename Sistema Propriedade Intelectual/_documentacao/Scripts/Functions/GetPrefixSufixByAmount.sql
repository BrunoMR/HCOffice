-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 19/07/2020
-- Description:	Esta função irá retornar o prefixo e sufixo indicado da palavra envianda
-- =============================================
CREATE FUNCTION [dbo].[GetPrefixSufixByAmount]
(
    @string VARCHAR(255),
    @amountCharacteres smallint
)
RETURNS TABLE
AS
RETURN
    select
        (LTRIM(RTRIM(LEFT(@string, @amountCharacteres)))) as PrefixSufix
    union
    select
        (LTRIM(RTRIM(RIGHT(@string, @amountCharacteres)))) as PrefixSufix
go

