-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 19/07/2020
-- Description:	Esta função irá retornar o prefixo e sufixo indicado da palavra envianda
-- =============================================
create FUNCTION dbo.GetPrefixSufixByAmount
(
    @string VARCHAR(255),
    @amountCharacteres smallint,
    @originalBrand VARCHAR(255)
)
RETURNS @output TABLE
    (
        PrefixSufix     VARCHAR(255),
        Type            INT
    )
AS
BEGIN
    declare
        @word varchar(255)

    select
      @word = case
                when @amountCharacteres <= 4
                    then
                        @originalBrand
                else
                    @string
              end

    insert into @output
    select
        (LTRIM(RTRIM(LEFT(@word, @amountCharacteres)))) as PrefixSufix,
        dbo.GetRadicalType('Termos Prefixo') as Type
    union
    select
        (LTRIM(RTRIM(RIGHT(@word, @amountCharacteres)))) as PrefixSufix,
        dbo.GetRadicalType('Termos Sufixo') as Type

    RETURN
END
go

