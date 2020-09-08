-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 17/09/2019
-- Description:	Este procedimento quebra e ortografa cada termo da marca e cria os radicais para cada termo
-- =============================================
alter FUNCTION [dbo].[BuildBrandRadicalsByWord]
(
    @processNumber          VARCHAR(20),
    @brand                  NVARCHAR(1000),
    @withPreffixAndSuffix   BIT,
    @justMainTerm           BIT
)
RETURNS @output TABLE
    (
        NUMERO_PROCESSO             VARCHAR(20) NOT NULL,
        RADICAL                     NVARCHAR(100) NOT NULL,
        LENGTH_RADICAL              INT NOT NULL,
        MAIN                        BIT NOT NULL,
        ID_TIPO_PROCESSO_RADICAL    INT,
        BIGGER                      BIT DEFAULT 0
    )
AS
BEGIN

    SELECT
      @brand = REPLACE(@brand, '.', ' ')

    INSERT INTO @output
    (NUMERO_PROCESSO, RADICAL, LENGTH_RADICAL, MAIN, ID_TIPO_PROCESSO_RADICAL, BIGGER)
    SELECT
      DISTINCT
        RAD.NUMERO_PROCESSO,
        RAD.RADICAL,
        RAD.LENGTH_RADICAL,
        RAD.MAIN,
        RAD.ID_TIPO_PROCESSO_RADICAL,
        RAD.BIGGER
    FROM
        dbo.SplitString(@brand, ' ')  SPD
        CROSS APPLY dbo.RadicalsProcessByWord(@processNumber, dbo.ORTOGRAFAR(splitData), @withPreffixAndSuffix, @justMainTerm, 0, 0, null) RAD
    WHERE
      dbo.ORTOGRAFAR(splitData) IS NOT NULL
      AND LEN(dbo.ORTOGRAFAR(splitData)) >=4

    RETURN
END
go

