-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 10/10/2019
-- Description:	Este procedimento irá remover as palavras de uso comum da marca
-- Ex:
-- SELECT dbo.RemoveCommonWords('bruno opa machado teste cabelo no sofa as vezes EITa opa MAIS UMA vez cabelo', 'N0941')
-- =============================================
CREATE FUNCTION dbo.RemoveCommonWords
(
    @word   VARCHAR(255),
    @class  VARCHAR(10)
)
RETURNS VARCHAR(255)
AS
BEGIN

    declare
        @originalWords varchar(255) = @word

    DECLARE
        @words AS TABLE (Id INT IDENTITY(1,1), Word VARCHAR(255))

    set @word = dbo.OrtografarPalavraUsoComum(@word)

    IF (@class IS NOT NULL)
    begin
        INSERT INTO @words
        select
          ltrim(rtrim(splitdata))
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
    end

    IF (@word is null OR LEN(@word) = 0)
        set @word = @originalWords

  RETURN UPPER(@word)
END
go

