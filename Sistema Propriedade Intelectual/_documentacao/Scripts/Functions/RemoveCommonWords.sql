-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 10/10/2019
-- Description:	Este procedimento irá remover as palavras de uso comum da marca
-- Ex:
-- SELECT dbo.RemoveCommonWords('bruno opa machado teste cabelo no sofa as vezes EITa opa MAIS UMA vez cabelo', 'N0941')
-- =============================================
ALTER FUNCTION dbo.RemoveCommonWords
(
    @word   VARCHAR(255),
    @class  VARCHAR(10)
)
RETURNS VARCHAR(255)
AS
BEGIN

    DECLARE
        @words AS TABLE (word VARCHAR(255))

    INSERT INTO @words
    select
      ltrim(rtrim(string_agg(splitdata, ' ')))
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
                            DISTINCT
                                ', ' + wrd2.word
                       FROM
                            @words wrd2
                      FOR XML PATH('')), 1, 2, '')
            FROM
                 @words wrd
            GROUP BY
                wrd.word
        )

  RETURN @word
END
go


DECLARE
        @words AS TABLE (word VARCHAR(255))

declare
    @word   VARCHAR(255) = 'bruno opa machado teste cabelo no sofa as vezes EITa opa'

    select
--        ltrim(rtrim(string_agg(splitdata, ' ')))
        splitdata
    from
        dbo.SplitString(@word, ' ')  spl
    where
        not exists(  select
                        1
                    from
                        PALAVRA_USO_COMUM
                    where
                        Palavra = spl.splitdata
                )





declare
    @word   VARCHAR(255) = 'bruno opa machado teste cabelo no sofa as vezes EITa opa MAIS UMA vez cabelo'


DECLARE
        @words AS TABLE (Id INT IDENTITY(1,1), Word VARCHAR(255))

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
--                         Numero_Classe =  @class
--                         AND
                          Palavra = spl.splitdata
                )

    select * from @words

    SET @word =
        (
            SELECT
                distinct
                STUFF((SELECT
                            --DISTINCT
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
--             ORDER BY
--                 wrd.Id
        )

select @word [@word]










SELECT
    distinct
    STUFF((SELECT
                DISTINCT
                    ', ' + spl2.splitdata
           FROM
                dbo.SplitString(@word, ' ')  spl2
          FOR XML PATH('')), 1, 2, '')
FROM
     dbo.SplitString(@word, ' ')  spl
GROUP BY
    spl.splitdata
