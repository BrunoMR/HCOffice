-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 30/06/2020
-- Description:	Este procedimento irá remover as palavras de uso comum da marca sem validar a classe
-- Ex:
-- SELECT dbo.RemoveCommonWordsWithoutClassVerified('bruno opa machado teste cabelo no sofa as vezes EITa opa MAIS UMA vez cabelo')
-- =============================================
CREATE FUNCTION [dbo].[RemoveCommonWordsWithoutClassVerified]
(
    @word   VARCHAR(255)
)
RETURNS VARCHAR(255)
AS
BEGIN

    declare
        @originalWords varchar(255) = @word

    DECLARE
        @words AS TABLE (Id INT IDENTITY(1,1), Word VARCHAR(255))

    set @word = dbo.OrtografarPalavraUsoComum(@word)

    INSERT INTO @words
    select
      ltrim(rtrim(splitdata))
    from
        dbo.SplitString(@word, ' ')  spl
    where
        not exists(  select
                        1
                    from
                        PALAVRA_USO_COMUM_EMPRESARIAL
                    where
                        Palavra = spl.splitdata
                )

        SET @word =
            (
                SELECT
                    distinct
                    STUFF((SELECT
                                ' ' + wrd2.Word
                           FROM
                                @words wrd2
                           ORDER BY
                                wrd2.Id
                           FOR XML PATH('')), 1, 1, '')
                FROM
                     @words wrd
                GROUP BY
                    wrd.Word
            )

    IF (@word is null OR LEN(@word) = 0)
        set @word = @originalWords

  RETURN UPPER(@word)
END