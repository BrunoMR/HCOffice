DECLARE
  @sqlExcel NVARCHAR(MAX) = '',
  @fileOfCompanys VARCHAR(2000),
  @fileOfClient VARCHAR(2000),
  @collidenceClientIntoCompany BIT = 1,
  @collidenceCompanyIntoClient BIT = 0

  SET @fileOfCompanys = 'D:\Development\Tests\EmpresaGeraLtESTE.xlsx'
  SET @fileOfClient = 'D:\Development\Tests\TesteNE.xlsx'

   SET @sqlExcel = '
  INSERT INTO Empresa
    (
      Id,
      CONSTITUICAO,
      EMPRESA,
      EMPRESA_ORIGINAL,
      OBJETO,
      PUBLICACAO,
      NIREPROTOCOLO,
      UF,
      ORTOGRAFIA
    )
    SELECT
     CAST(ROW_NUMBER() OVER(ORDER BY EMPRESA ASC) AS VARCHAR(20)) AS Id,
     dbo.RemoveFirstCharacter(CONSTITUICAO) AS CONSTITUICAO,
     dbo.RemoveCommonWordsWithoutClassVerified(EMPRESA),
     EMPRESA,
     OBJETO,
     dbo.RemoveFirstCharacter(PUBLICACAO) AS PUBLICACAO,
     dbo.RemoveFirstCharacter(NIREPROTOCOLO) AS NIREPROTOCOLO,
     UF,
     dbo.ORTOGRAFAR(dbo.RemoveCommonWordsWithoutClassVerified(EMPRESA)) AS ORTOGRAFIA
    FROM
     OPENROWSET(''Microsoft.ACE.OLEDB.12.0'', ''Excel 12.0 XML;Database='+ @fileOfCompanys +';HDR=YES'', ''SELECT * FROM   [EMPRESA$]'') EMP
    WHERE
     EMP.EMPRESA IS NOT NULL
     AND EMP.EMPRESA != '''''

--    print @sqlExcel
   EXECUTE(@sqlExcel)


SET @sqlExcel = N'
insert into CLIENT_COMPANY
(
    Processo,
    [MarcaModificada],
    [Marca(Cliente)],
    MarcaOrtografada,
    Classe,
    [Data Depósito],
    Titular,
    Especificacao,
    [Referência/Pasta],
    [Escritório Responsável],
    [Advogado Responsável]
 )
Select
	dbo.RemoveFirstCharacter(Processo)								AS [Processo],
  dbo.RemoveCommonWordsWithoutClassVerified(Marca)                      AS [MarcaModificada],
  Marca										                        AS [Marca(Cliente)],
  dbo.ORTOGRAFAR(dbo.RemoveCommonWordsWithoutClassVerified(Marca))  AS MarcaOrtografada,
	dbo.RemoveFirstCharacter(Classe)                                AS [Classe],
	CONVERT(VARCHAR, dbo.RemoveFirstCharacter([DEPÓSITO]), 103)		AS [Data Depósito],
	Titular										                    AS [Titular],
	REPLACE(
        REPLACE(
            REPLACE(dbo.RemoveFirstCharacter([Especificação]),
            CHAR(13) + Char(10) ,'' ''),
        CHAR(10), '' ''),
      ''  '', '' '')							                    AS Especificacao,
	Pasta										                    AS [Referência/Pasta],
	[Responsável]								                    AS [Escritório Responsável],
	[Advogado/Responsável]						                    AS [Advogado Responsável]
FROM
	OPENROWSET(''Microsoft.ACE.OLEDB.12.0'', ''Excel 12.0 XML;Database='+ @fileOfClient +';HDR=YES'', ''SELECT * FROM   [COLIDIR$]'') EMP'


EXECUTE(@sqlExcel)


    INSERT into PROCESSO_RADICAL
    (NUMERO_PROCESSO, RADICAL, LENGTH_RADICAL, MAIN, ID_TIPO_PROCESSO_RADICAL)

    -- Build the Radicais of COMPANY PROCESSES
    SELECT
        RAD.NUMERO_PROCESSO,
        RAD.RADICAL,
        RAD.LENGTH_RADICAL,
        RAD.MAIN,
        RAD.ID_TIPO_PROCESSO_RADICAL
    FROM
        Empresa    EMP
        cross apply dbo.BuildBrandRadicalsByWord(EMP.Id, EMP.EMPRESA, 0, 1) RAD

    UNION

    -- Build the Radicais of CLIENT PROCESSES
    SELECT
        RAD.NUMERO_PROCESSO,
        RAD.RADICAL,
        RAD.LENGTH_RADICAL,
        RAD.MAIN,
        RAD.ID_TIPO_PROCESSO_RADICAL
    FROM
        CLIENT_COMPANY    CLI
        cross apply dbo.BuildBrandRadicalsByWord(CLI.Processo, CLI.MarcaModificada, 0, 1) RAD


    SELECT
      CLC.[Marca(Cliente)],
      CLC.[Classe],
      CLC.[Data Depósito],
      CLC.[Processo],
      CLC.[Titular],
      CLC.[Especificacao],
      CLC.[Referência/Pasta],
      CLC.[Escritório Responsável],
      CLC.[Advogado Responsável],

      EMP.EMPRESA_ORIGINAL AS EMPRESA,
      EMP.CONSTITUICAO,
      EMP.OBJETO,
      EMP.PUBLICACAO,
      EMP.NIREPROTOCOLO,
      EMP.UF
    INTO
      #COLLIDED_PROCESS
    FROM
      Empresa EMP
      JOIN CLIENT_COMPANY CLC ON CLC.MarcaOrtografada = EMP.ORTOGRAFIA
    ORDER BY
      CLC.Processo,
      EMP.ID;


    WITH EMP AS
    (
      SELECT
        DISTINCT
        PRR.LENGTH_RADICAL,
        PRR.RADICAL,
        PRR.NUMERO_PROCESSO,

        EMP.EMPRESA_ORIGINAL AS EMPRESA,
        EMP.CONSTITUICAO,
        EMP.OBJETO,
        EMP.PUBLICACAO,
        EMP.NIREPROTOCOLO,
        EMP.UF
    FROM
      Empresa EMP
      JOIN PROCESSO_RADICAL PRR ON PRR.ID_TIPO_PROCESSO_RADICAL IN (1)
                                    AND PRR.NUMERO_PROCESSO = EMP.Id
                                    AND PRR.LENGTH_RADICAL > 1
    ),

    CLI AS
    (
      SELECT
        PRR.LENGTH_RADICAL,
        PRR.RADICAL,

        CLC.[Marca(Cliente)],
        CLC.[Classe],
        CLC.[Data Depósito],
        CLC.[Processo],
        CLC.[Titular],
        CLC.[Especificacao],
        CLC.[Referência/Pasta],
        CLC.[Escritório Responsável],
        CLC.[Advogado Responsável]
      FROM
        CLIENT_COMPANY			  CLC
        JOIN PROCESSO_RADICAL     PRR ON PRR.ID_TIPO_PROCESSO_RADICAL IN (1)
                                            AND PRR.NUMERO_PROCESSO = CLC.Processo
                                            AND PRR.LENGTH_RADICAL > 1
    )

--     Radicais da empresa contidos no do Cliente
      INSERT INTO #COLLIDED_PROCESS
        (
          [Marca(Cliente)],
          Classe,
          [Data Depósito],
          Processo,
          Titular,
          Especificacao,
          [Referência/Pasta],
          [Escritório Responsável],
          [Advogado Responsável],
          EMPRESA,
          CONSTITUICAO,
          OBJETO,
          PUBLICACAO,
          NIREPROTOCOLO,
          UF
        )

      SELECT
        CLI.[Marca(Cliente)],
        CLI.[Classe],
        CLI.[Data Depósito],
        CLI.[Processo],
        CLI.[Titular],
        CLI.[Especificacao],
        CLI.[Referência/Pasta],
        CLI.[Escritório Responsável],
        CLI.[Advogado Responsável],

        EMP.EMPRESA,
        EMP.CONSTITUICAO,
        EMP.OBJETO,
        EMP.PUBLICACAO,
        EMP.NIREPROTOCOLO,
        EMP.UF
      FROM
        EMP
        JOIN CLI ON CLI.LENGTH_RADICAL >= EMP.LENGTH_RADICAL
                      AND
                      (
                         (
                           EMP.LENGTH_RADICAL = CLI.LENGTH_RADICAL
                         )
                         OR
                         (
                           EMP.LENGTH_RADICAL = 3 AND CLI.LENGTH_RADICAL <= 100
                         )
                         OR
                         (
                           EMP.LENGTH_RADICAL = 4 AND CLI.LENGTH_RADICAL <= 100
                         )
                         OR
                         (
                           EMP.LENGTH_RADICAL = 5 AND CLI.LENGTH_RADICAL <= 100
                         )
                         OR
                         (
                           EMP.LENGTH_RADICAL = 6 AND CLI.LENGTH_RADICAL <= 100
                         )
                         OR
                         (
                           EMP.LENGTH_RADICAL = 7 AND CLI.LENGTH_RADICAL <= 100
                         )
                         OR
                         (
                           EMP.LENGTH_RADICAL = 8 AND CLI.LENGTH_RADICAL <= 100
                         )
                         OR
                         (
                           EMP.LENGTH_RADICAL = 9 AND CLI.LENGTH_RADICAL <= 100
                         )
                      )
                      AND
                      (
                        CLI.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 = EMP.RADICAL
                        OR
                        CLI.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE EMP.RADICAL + '%'
                        OR
                        CLI.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE '%' + EMP.RADICAL
                      )
    UNION

    -- Radicais da Cliente contidos no da Empresa
    SELECT
        CLI.[Marca(Cliente)],
        CLI.[Classe],
        CLI.[Data Depósito],
        CLI.[Processo],
        CLI.[Titular],
        CLI.[Especificacao],
        CLI.[Referência/Pasta],
        CLI.[Escritório Responsável],
        CLI.[Advogado Responsável],

        EMP.EMPRESA,
        EMP.CONSTITUICAO,
        EMP.OBJETO,
        EMP.PUBLICACAO,
        EMP.NIREPROTOCOLO,
        EMP.UF
    FROM
        CLI
        JOIN EMP ON EMP.LENGTH_RADICAL >= CLI.LENGTH_RADICAL
                  AND
                  (
                     (
                       CLI.LENGTH_RADICAL = EMP.LENGTH_RADICAL
                     )
                     OR
                     (
                       CLI.LENGTH_RADICAL = 3 AND EMP.LENGTH_RADICAL <= 100
                     )
                     OR
                     (
                       CLI.LENGTH_RADICAL = 4 AND EMP.LENGTH_RADICAL <= 100
                     )
                     OR
                     (
                       CLI.LENGTH_RADICAL = 5 AND EMP.LENGTH_RADICAL <= 100
                     )
                     OR
                     (
                       CLI.LENGTH_RADICAL = 6 AND EMP.LENGTH_RADICAL <= 100
                     )
                     OR
                     (
                       CLI.LENGTH_RADICAL = 7 AND EMP.LENGTH_RADICAL <= 100
                     )
                     OR
                     (
                       CLI.LENGTH_RADICAL = 8 AND EMP.LENGTH_RADICAL <= 100
                     )
                     OR
                     (
                       CLI.LENGTH_RADICAL = 9 AND EMP.LENGTH_RADICAL <= 100
                     )
                  )
                  AND
                  (
                    EMP.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 = CLI.RADICAL
                    OR
                    EMP.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE CLI.RADICAL + '%'
                    OR
                    EMP.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE '%' + CLI.RADICAL
                  );

    SELECT
      DISTINCT
      *
    FROM
      #COLLIDED_PROCESS

    DELETE FROM PROCESSO_RADICAL
    DELETE CLIENT_COMPANY
    DELETE Empresa
    DROP TABLE #COLLIDED_PROCESS



