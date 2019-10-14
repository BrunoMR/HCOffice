-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 10/10/2019
-- Description:	Este procedimento irá remover as palavras de uso comum da marca
-- Ex:
-- SELECT dbo.RemoveCommonWords('bruno opa machado teste cabelo no sofa as vezes EITa opa', 'N0941')
-- =============================================
CREATE FUNCTION dbo.RemoveCommonWords
(
    @word   VARCHAR(255),
    @class  VARCHAR(10)
)
RETURNS VARCHAR(255)
AS
BEGIN

    select
       @word = ltrim(rtrim(string_agg(splitdata, ' ')))
    from
        dbo.SplitString(@word, ' ')  spl
    where
        not exists(  select
                        1
                    from
                        PALAVRA_USO_COMUM
                    where
                        Numero_Classe =  @class
                        AND Palavra = spl.splitdata
                )

  RETURN @word
END
go