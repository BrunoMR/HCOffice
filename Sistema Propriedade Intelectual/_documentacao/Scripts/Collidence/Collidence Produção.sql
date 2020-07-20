DECLARE
  @rpi INT,
  @fileOfClient VARCHAR(2000)

	SET NOCOUNT ON;

	SET @rpi = 2575
	SET @fileOfClient = 'D:\Development\Tests\_Processos do Cliente Multi-classes.xlsx' --'D:\Guerra.xlsx'

  --BEGIN TRY
  -- Query Executa processamento da colidência RPI Azure_New - Regra LONGA

    -- If the rpi not yet exists in Colidir table
	IF NOT EXISTS(
                    SELECT
                        1
                    FROM
                        PROCESSO_DESPACHO_COLIDI
                    WHERE
                        NUMERO_RPI = @rpi
                )
		  BEGIN

			  INSERT INTO PROCESSO_DESPACHO_COLIDI
			  (
				  ID_PROCESSO,
				  ID_DESPACHO,
				  NUMERO_RPI,
				  DATA_DESPACHO,
				  ID_PROTOCOLO,
				  COMPLEMENTO
			  )
			  SELECT
				  PRD.ID_PROCESSO,
				  PRD.ID_DESPACHO,
				  PRD.NUMERO_RPI,
				  PRD.DATA_DESPACHO,
				  PRD.ID_PROTOCOLO,
				  PRD.COMPLEMENTO
			  FROM
				  DESPACHO                DES
				  JOIN PROCESSO_DESPACHO  PRD ON PRD.ID_DESPACHO = DES.ID
				  AND DES.CODIGO IN ('IPAS009', 'IPAS135', 'IPAS158', 'IPAS421', 'IPAS756', 'IPAS757')
			  WHERE
					  PRD.NUMERO_RPI = @rpi
		  END

	  -- Insert rows of client`s file into temporary table
	  DECLARE
		  @sqlExcel NVARCHAR(MAX)

	  SET @sqlExcel = '
      insert into CLIENT_PROCESSES
      (ProprioTerceiro, Processo, MarcaOriginal, MarcaOrtografadaOriginal, MarcaSemVogaisOriginal, Classe, Deposito, Concessao, Especificacao, Titular, Pasta, Responsavel, Advogado)
	  SELECT
		  CLI.[P/T]																                    AS ProprioTerceiro,
		  dbo.RemoveFirstCharacter(CLI.PROCESSO)										            AS Processo,
		  dbo.RemoveFirstCharacter(CLI.MARCA)											            AS MarcaOriginal,
		  dbo.ORTOGRAFAR(dbo.RemoveFirstCharacter(CLI.MARCA))					                    AS MarcaOrtografadaOriginal,
          REPLACE(dbo.REMOVER_VOGAIS(dbo.RemoveFirstCharacter(CLI.MARCA)), '' '', '''')             AS MarcaSemVogaisOriginal,
		  dbo.RemoveFirstCharacter(CLI.CLASSE)										                AS Classe,
		  --CONVERT(VARCHAR, CONVERT(DATE, dbo.RemoveFirstCharacter(CLI.[DEPÓSITO])), 103)			AS Deposito,
          --CONVERT(VARCHAR, CONVERT(DATE, dbo.RemoveFirstCharacter(CLI.[CONCESSÃO])), 103)			AS Concessao,
	      CONVERT(VARCHAR, dbo.RemoveFirstCharacter(CLI.[DEPÓSITO]), 103)			                AS Deposito,
		  CONVERT(VARCHAR, dbo.RemoveFirstCharacter(CLI.[CONCESSÃO]), 103)			                AS Concessao,
		  REPLACE(
                REPLACE(
                    REPLACE(CONVERT(VARCHAR(max), CLI.[ESPECIFICAÇÃO]),
                        CHAR(13) + Char(10) ,'' ''),
                    CHAR(10), '' ''),
            ''  '', '' '')							                                                AS Especificacao,
		  dbo.RemoveFirstCharacter(CLI.TITULAR)										                AS Titular,
		  dbo.RemoveFirstCharacter(CLI.PASTA)											            AS Pasta,
		  dbo.RemoveFirstCharacter(CLI.[RESPONSÁVEL])								                AS Responsavel,
		  dbo.RemoveFirstCharacter(CLI.[ADVOGADO/RESPONSÁVEL])			                            AS Advogado
	  FROM
		  OPENROWSET(''Microsoft.ACE.OLEDB.12.0'', ''Excel 12.0 XML;Database='+ @fileOfClient +';HDR=YES'', ''SELECT * FROM [COLIDIR$]'') CLI
	  WHERE
      CLI.PROCESSO != ''''
	  OR CLI.MARCA != '''''

	  EXECUTE(@sqlExcel)

    insert into PROCESS_TO_COLLIDE
    (ID_PROCESSO, NUMERO, MARCA, MARCA_ORTOGRAFADA, MARCA_SEM_VOGAIS,
     NOME_TITULAR, DATA_DEPOSITO, DATA_CONCESSAO, CODIGO, NOME_PROCURADOR)
	SELECT
        PRC.ID_PROCESSO,
        PRO.NUMERO,
        PRO.MARCA,
        dbo.ORTOGRAFAR(PRO.MARCA) AS MARCA_ORTOGRAFADA,
        replace(PRO.MARCA_SEM_VOGAIS, ' ', '') AS MARCA_SEM_VOGAIS,
        PRO.NOME_TITULAR,
        CONVERT(VARCHAR, PRO.DATA_DEPOSITO, 103) AS DATA_DEPOSITO,
        CONVERT(VARCHAR, PRO.DATA_CONCESSAO, 103) AS DATA_CONCESSAO,
        DES.CODIGO,
        PRO.NOME_PROCURADOR
	FROM
		PROCESSO_DESPACHO_COLIDI 	PRC
        JOIN PROCESSO				PRO ON PRC.NUMERO_RPI = @rpi
                                            AND PRO.TIPO_APRESENTACAO != 3
                                            AND PRO.MARCA IS NOT NULL
                                            AND PRO.ID = PRC.ID_PROCESSO
        JOIN DESPACHO               DES ON DES.ID = PRC.ID_DESPACHO

	  WHERE
		NOT EXISTS(
			  		SELECT
			  			1
			  		FROM
			  			CLIENT_PROCESSES
				  	WHERE
					  	Processo = PRO.NUMERO
				    )

    DELETE FROM
        CLIENT_PROCESSES
    WHERE
        MarcaOriginal IS NULL
        OR MarcaOriginal = ''
        OR ProprioTerceiro = 'T'

	-- Join client processes and their classes
	SELECT
		CLA.class,
		CLP.Processo
	INTO
		#CLIENT_PROCESSES_CLASS
	FROM
		CLIENT_PROCESSES CLP
		CROSS APPLY dbo.GetProcessAndClassesFromFile(CLP.Processo, CLP.Classe) AS CLA
	-- CROSS APPLY dbo.FormatClassesFromFile(CLP.Classe) AS CLA
	-- END Join client processes and their classes

	-- Remove common words of client processes
	update
		CLP
	set
        CLP.Marca = dbo.RemoveCommonWords(CLP.MarcaOriginal, CLC.class),
        CLP.MarcaOrtografada = dbo.ORTOGRAFAR(dbo.RemoveCommonWords(CLP.MarcaOriginal, CLC.class)),
        CLP.MarcaSemVogais = dbo.REMOVER_VOGAIS(dbo.RemoveCommonWords(CLP.MarcaOriginal, CLC.class))
	from
		CLIENT_PROCESSES CLP
		join #CLIENT_PROCESSES_CLASS CLC on CLC.Processo = CLP.Processo
	-- END Remove common words of client processes

	-- Build the Radicais of CLIENT PROCESSES
    -- Build the Radicais of RPI PROCESSES

    INSERT into PROCESSO_RADICAL
    (NUMERO_PROCESSO, RADICAL, LENGTH_RADICAL, MAIN, ID_TIPO_PROCESSO_RADICAL)
    SELECT
        RAD.NUMERO_PROCESSO,
        RAD.RADICAL,
        RAD.LENGTH_RADICAL,
        RAD.MAIN,
        RAD.ID_TIPO_PROCESSO_RADICAL
    FROM
        CLIENT_PROCESSES    CLI
        cross apply dbo.RadicalsProcessByWord(Processo, MarcaOrtografada, 1, 0, 1) RAD
    UNION

    SELECT
        RAD.NUMERO_PROCESSO,
        RAD.RADICAL,
        RAD.LENGTH_RADICAL,
        RAD.MAIN,
        RAD.ID_TIPO_PROCESSO_RADICAL
    FROM
        CLIENT_PROCESSES    CLI
        cross apply dbo.BuildBrandRadicalsByWord(Processo, Marca, 0, 1) RAD

    UNION

    SELECT
        RAD.NUMERO_PROCESSO,
        RAD.RADICAL,
        RAD.LENGTH_RADICAL,
        RAD.MAIN,
        RAD.ID_TIPO_PROCESSO_RADICAL
    FROM
        PROCESS_TO_COLLIDE PRO
        cross apply dbo.RadicalsProcessByWord(PRO.NUMERO, PRO.MARCA_ORTOGRAFADA, 1, 0, 1) RAD

    UNION


    SELECT
        RAD.NUMERO_PROCESSO,
        RAD.RADICAL,
        RAD.LENGTH_RADICAL,
        RAD.MAIN,
        RAD.ID_TIPO_PROCESSO_RADICAL
    FROM
        PROCESS_TO_COLLIDE PRO
        cross apply dbo.BuildBrandRadicalsByWord(PRO.NUMERO, PRO.MARCA, 0, 1) RAD

    --END Build the Radicais of CLIENT PROCESSES
    --END Build the Radicais of RPI PROCESSES

    -- Build the client to collide

    INSERT INTO CLIENT_TO_COLLIDE
    (Main, LengthRadical, Radical, NumeroProcesso, Marca, MarcaOriginal, Classe, Deposito, Concessao, Processo, Titular, Pasta, Responsavel,
     Advogado, Class, MarcaSemVogais, LenMarcaSemVogais, IdTipoProcessoRadical)
     SELECT
        DISTINCT
        PRRS.MAIN,
        PRRS.LENGTH_RADICAL,
        PRRS.RADICAL,
        PRRS.NUMERO_PROCESSO,

        CLPS.Marca,
        CLPS.MarcaOriginal,
        CLPS.Classe,
        CLPS.Deposito,
        CLPS.Concessao,
        CLPS.Processo,
        CLPS.Titular,
        CLPS.Pasta,
        CLPS.Responsavel,
        CLPS.Advogado,
        CPC.class,
        CLPS.MarcaSemVogais,
        LEN(CLPS.MarcaSemVogais) AS LenMarcaSemVogais,
        PRRS.ID_TIPO_PROCESSO_RADICAL
    FROM
      CLIENT_PROCESSES		       CLPS
      JOIN PROCESSO_RADICAL      PRRS ON PRRS.NUMERO_PROCESSO = CLPS.Processo
      LEFT JOIN #CLIENT_PROCESSES_CLASS  CPC ON CPC.Processo = CLPS.Processo

    -- END Build the client to collide


    -- Build the processes to collide full

    insert into PROCESS_TO_COLLIDE_FULL
    (Main, LengthRadical, Radical, IdProcesso, Marca, MarcaOrtografada , Codigo, DataDeposito,
     DataConcessao, Numero, NomeTitular, NomeProcurador, MarcaSemVogais, LenMarcaSemVogais, ClassFormated, Class, Specification, IdTipoProcessoRadical)
    SELECT
        PRR.MAIN,
        PRR.LENGTH_RADICAL,
        PRR.RADICAL,

        PRC.ID_PROCESSO,
        PRC.MARCA,
        PRC.MARCA_ORTOGRAFADA,

        PRC.CODIGO,
        PRC.DATA_DEPOSITO,
        PRC.DATA_CONCESSAO,
        PRC.NUMERO,
        PRC.NOME_TITULAR,
        PRC.NOME_PROCURADOR,
        PRC.MARCA_SEM_VOGAIS,
        LEN(PRC.MARCA_SEM_VOGAIS),
        dbo.FormatClassesAfterUnified(PCL.NUMERO_CLASSE),
        PCL.NUMERO_CLASSE,
        PCL.ESPECIFICAO,
        PRR.ID_TIPO_PROCESSO_RADICAL
      FROM
        PROCESS_TO_COLLIDE			  PRC
        JOIN PROCESSO_RADICAL         PRR ON PRR.NUMERO_PROCESSO = PRC.NUMERO
        join PROCESSO_CLASSE          PCL on PCL.ID_PROCESSO = PRC.ID_PROCESSO

    -- END Build the processes to collide full

	-- Do the Collidence

	SELECT
      PRC.IdProcesso                              AS ID_PROCESSO,
      1                                           AS [Tipo Colidência],
      PRC.MARCA									  AS [Marca(RPI)],
      CLP.MarcaOriginal						      AS [Marca(Cliente)],

      CLP.Classe                                  AS [Classe(Cliente)],
      CONVERT(VARCHAR, CLP.Deposito, 103)         AS [Data Depósito(Cliente)],
      CONVERT(VARCHAR, CLP.Concessao, 103)        AS [Data Concessão(Cliente)],
      CLP.Processo                                AS [Processo(Cliente)],
      CLP.Titular                                 AS [Titular(Cliente)],
      CLP.Pasta									  AS [Referência/Pasta],
      CLP.Responsavel							  AS [Escritório Responsável],
      CLP.Advogado                                AS [Advogado Responsável],

      PRC.CODIGO                                  AS [Despacho],
      PRC.Class                                   AS [Class],
	  PRC.ClassFormated                           AS [Classe],
      PRC.DataDeposito	                          AS [Data Depósito],
      PRC.DataConcessao	                          AS [Data Concessão],
      PRC.NUMERO								  AS [Processo],
      PRC.NomeTitular							  AS [Titular],
      PRC.NomeProcurador                          AS [Procurador]
    INTO
      #COLLIDED_PROCESS
    FROM
        PROCESS_TO_COLLIDE_FULL		PRC
        JOIN CLIENT_PROCESSES		CLP ON CLP.MarcaOrtografada = PRC.MarcaOrtografada

    -- Marcas sem vogais e afinidade de classes teste

    INSERT INTO #COLLIDED_PROCESS
    (ID_PROCESSO, [Tipo Colidência], [Marca(RPI)], [Marca(Cliente)], [Classe(Cliente)], [Data Depósito(Cliente)], [Data Concessão(Cliente)], [Processo(Cliente)], [Titular(Cliente)],
    [Referência/Pasta], [Escritório Responsável], [Advogado Responsável], Despacho, Classe, Class, [Data Depósito], [Data Concessão], Processo, Titular,
     Procurador)
    SELECT
        DISTINCT
        PRO.IdProcesso,
        4                          AS [Tipo Colidência],
        PRO.MARCA				   AS [Marca(RPI)],
        CLI.MarcaOriginal		   AS [Marca(Cliente)],

        CLI.Classe                 AS [Classe(Cliente)],
        CLI.Deposito               AS [Data Depósito(Cliente)],
        CLI.Concessao              AS [Data Concessão(Cliente)],
        CLI.Processo               AS [Processo(Cliente)],
        CLI.Titular                AS [Titular(Cliente)],
        CLI.Pasta				   AS [Referência/Pasta],
        CLI.Responsavel			   AS [Escritório Responsável],
        CLI.Advogado               AS [Advogado Responsável],

        PRO.CODIGO                 AS [Despacho],
        PRO.ClassFormated		   AS [Classe],
        PRO.Class		           AS [Class],
        PRO.DataDeposito           AS [Data Depósito],
        PRO.DataConcessao          AS [Data Depósito],
        PRO.NUMERO				   AS [Processo],
        PRO.NomeTitular		       AS [Titular],
        PRO.NomeProcurador         AS [Procurador]
      FROM
        PROCESS_TO_COLLIDE_FULL    PRO
        JOIN CLIENT_TO_COLLIDE     CLI ON CLI.LenMarcaSemVogais > 2 AND CLI.MarcaSemVogais = PRO.MarcaSemVogais
                    OR (
                          CLI.LenMarcaSemVogais > 2
                          AND CLI.LenMarcaSemVogais = PRO.LenMarcaSemVogais
                          AND CLI.MarcaSemVogais = PRO.MarcaSemVogais
                       )
        JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = PRO.class
                                   AND CLF.NUMERO_CLASSE_B = CLI.Class



    -- Radicais do cliente contidos no da RPI

    INSERT INTO #COLLIDED_PROCESS
    (ID_PROCESSO, [Tipo Colidência], [Marca(RPI)], [Marca(Cliente)], [Classe(Cliente)], [Data Depósito(Cliente)], [Data Concessão(Cliente)], [Processo(Cliente)], [Titular(Cliente)],
     [Referência/Pasta], [Escritório Responsável], [Advogado Responsável], Despacho, Classe, Class, [Data Depósito], [Data Concessão], Processo, Titular,
     Procurador)
    SELECT
        DISTINCT
        PRO.IdProcesso,
        3                          AS [Tipo Colidência],
        PRO.MARCA				   AS [Marca(RPI)],
        CLI.MarcaOriginal		   AS [Marca(Cliente)],

        CLI.Classe                 AS [Classe(Cliente)],
        CLI.Deposito               AS [Data Depósito(Cliente)],
        CLI.Concessao              AS [Data Concessão(Cliente)],
        CLI.Processo               AS [Processo(Cliente)],
        CLI.Titular                AS [Titular(Cliente)],
        CLI.Pasta				   AS [Referência/Pasta],
        CLI.Responsavel			   AS [Escritório Responsável],
        CLI.Advogado               AS [Advogado Responsável],

        PRO.CODIGO                 AS [Despacho],
        PRO.ClassFormated		   AS [Classe],
        PRO.Class		           AS [Class],
        PRO.DataDeposito           AS [Data Depósito],
        PRO.DataConcessao          AS [Data Depósito],
        PRO.NUMERO				   AS [Processo],
        PRO.NomeTitular		       AS [Titular],
        PRO.NomeProcurador         AS [Procurador]
      FROM
        CLIENT_TO_COLLIDE               CLI
        JOIN PROCESS_TO_COLLIDE_FULL    PRO ON PRO.LengthRadical >= CLI.LengthRadical
                    AND
                    (
                       (
                         CLI.LengthRadical = PRO.LengthRadical
                       )
                       OR
                       (
                         --CLI.LengthRadical >= 3 AND PRO.LengthRadical <= 9
                         CLI.LengthRadical = 3 AND PRO.LengthRadical <= 12
                         --CLI.LengthRadical >= 3 AND PRO.LengthRadical <= 6
                       )
                       OR
                       (
                         --CLI.LengthRadical >= 4 AND PRO.LengthRadical <= 12
                         CLI.LengthRadical = 4 AND PRO.LengthRadical <= 16
                         --CLI.LengthRadical >= 4 AND PRO.LengthRadical <= 8
                       )
                       OR
                       (
                         --CLI.LengthRadical = 5 AND PRO.LengthRadical <= 15
                         CLI.LengthRadical = 5 AND PRO.LengthRadical <= 20
                         --CLI.LengthRadical = 5 AND PRO.LengthRadical <= 10
                       )
                       OR
                       (
                         --CLI.LengthRadical = 6 AND PRO.LengthRadical <= 18
                         CLI.LengthRadical = 6 AND PRO.LengthRadical <= 24
                         --CLI.LengthRadical = 6 AND PRO.LengthRadical <= 12
                       )
                       OR
                       (
                         --CLI.LengthRadical = 7 AND PRO.LengthRadical <= 21
                         CLI.LengthRadical = 7 AND PRO.LengthRadical <= 30
                         --CLI.LengthRadical = 7 AND PRO.LengthRadical <= 14
                       )
                       OR
                       (
                         --CLI.LengthRadical = 8 AND PRO.LengthRadical <= 24
                         CLI.LengthRadical = 8 AND PRO.LengthRadical <= 32
                         --CLI.LengthRadical = 8 AND PRO.LengthRadical <= 16
                       )
                       OR
                       (
                         --CLI.LengthRadical = 9 AND PRO.LengthRadical <= 27
                         CLI.LengthRadical = 9 AND PRO.LengthRadical <= 36
                         --CLI.LengthRadical = 9 AND PRO.LengthRadical <= 18
                       )
                    )
                    AND
                    (
                      PRO.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 = CLI.RADICAL
                      OR
                      PRO.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE CLI.RADICAL + '%'
                      OR
                      PRO.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE '%' + CLI.RADICAL
                    )
        JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = CLI.class
                                   AND CLF.NUMERO_CLASSE_B = PRO.Class
    where
        CLI.IdTipoProcessoRadical in (1, 3)
        AND CLI.LengthRadical > 1
        AND PRO.IdTipoProcessoRadical in (1, 3)
        AND PRO.LengthRadical > 1

    INSERT INTO #COLLIDED_PROCESS
    (ID_PROCESSO, [Tipo Colidência], [Marca(RPI)], [Marca(Cliente)], [Classe(Cliente)], [Data Depósito(Cliente)], [Data Concessão(Cliente)], [Processo(Cliente)], [Titular(Cliente)],
     [Referência/Pasta], [Escritório Responsável], [Advogado Responsável], Despacho, Classe, Class, [Data Depósito], [Data Concessão], Processo, Titular,
     Procurador)
    SELECT
        DISTINCT
        PRO.IdProcesso,
        2                          AS [Tipo Colidência],
        PRO.MARCA				   AS [Marca(RPI)],
        CLI.MarcaOriginal		   AS [Marca(Cliente)],

        CLI.Classe                 AS [Classe(Cliente)],
        CLI.Deposito               AS [Data Depósito(Cliente)],
        CLI.Concessao              AS [Data Concessão(Cliente)],
        CLI.Processo               AS [Processo(Cliente)],
        CLI.Titular                AS [Titular(Cliente)],
        CLI.Pasta				   AS [Referência/Pasta],
        CLI.Responsavel			   AS [Escritório Responsável],
        CLI.Advogado               AS [Advogado Responsável],

        PRO.CODIGO                 AS [Despacho],
        PRO.ClassFormated          as Classe,
        PRO.Class                  as Class,
        PRO.DataDeposito           AS [Data Depósito],
        PRO.DataConcessao          AS [Data Depósito],
        PRO.NUMERO				   AS [Processo],
        PRO.NomeTitular		       AS [Titular],
        PRO.NomeProcurador         AS [Procurador]
      FROM
        CLIENT_TO_COLLIDE               CLI
        JOIN PROCESS_TO_COLLIDE_FULL    PRO ON PRO.LengthRadical >= CLI.LengthRadical
                    AND
                    (
                       (
                         CLI.LengthRadical = PRO.LengthRadical
                       )
                       OR
                       (
                         --CLI.LengthRadical = 3 AND PRO.LengthRadical <= 9
                         CLI.LengthRadical = 3 AND PRO.LengthRadical <= 12
                         --CLI.LengthRadical = 3 AND PRO.LengthRadical <= 6
                       )
                       OR
                       (
                         --CLI.LengthRadical = 4 AND PRO.LengthRadical <= 12
                         CLI.LengthRadical = 4 AND PRO.LengthRadical <= 16
                         --CLI.LengthRadical = 4 AND PRO.LengthRadical <= 8
                       )
                       OR
                       (
                         --CLI.LengthRadical = 5 AND PRO.LengthRadical <= 15
                         CLI.LengthRadical = 5 AND PRO.LengthRadical <= 20
                         --CLI.LengthRadical = 5 AND PRO.LengthRadical <= 10
                       )
                       OR
                       (
                         --CLI.LengthRadical = 6 AND PRO.LengthRadical <= 18
                         CLI.LengthRadical = 6 AND PRO.LengthRadical <= 24
                         --CLI.LengthRadical = 6 AND PRO.LengthRadical <= 12
                       )
                       OR
                       (
                         --CLI.LengthRadical = 7 AND PRO.LengthRadical <= 21
                         CLI.LengthRadical = 7 AND PRO.LengthRadical <= 30
                         --CLI.LengthRadical = 7 AND PRO.LengthRadical <= 14
                       )
                       OR
                       (
                         --CLI.LengthRadical = 8 AND PRO.LengthRadical <= 24
                         CLI.LengthRadical = 8 AND PRO.LengthRadical <= 32
                         --CLI.LengthRadical = 8 AND PRO.LengthRadical <= 16
                       )
                       OR
                       (
                         --CLI.LengthRadical = 9 AND PRO.LengthRadical <= 27
                         CLI.LengthRadical = 9 AND PRO.LengthRadical <= 36
                         --CLI.LengthRadical = 9 AND PRO.LengthRadical <= 18
                       )
                    )
                    AND
                    (
                      (
                         CLI.LengthRadical < 6
                         AND PRO.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE CLI.RADICAL
                       )
                       OR
                       (
                         CLI.LengthRadical >= 6
                         AND PRO.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE '%' + CLI.RADICAL + '%'
                       )
                    )
--         JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = CLI.class
--                                    AND CLF.NUMERO_CLASSE_B = COALESCE(PRO.Classe1, PRO.ClasseInternacional)
            join CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = CLI.class
                                        AND CLF.NUMERO_CLASSE_B = PRO.Class
    WHERE
        CLI.IdTipoProcessoRadical = 2
        AND CLI.LengthRadical > 1
        AND PRO.IdTipoProcessoRadical in (1, 3)
        AND PRO.LengthRadical > 1

    INSERT INTO #COLLIDED_PROCESS
    (ID_PROCESSO, [Tipo Colidência], [Marca(RPI)], [Marca(Cliente)], [Classe(Cliente)], [Data Depósito(Cliente)], [Data Concessão(Cliente)], [Processo(Cliente)], [Titular(Cliente)],
     [Referência/Pasta], [Escritório Responsável], [Advogado Responsável], Despacho, Classe, Class, [Data Depósito], [Data Concessão], Processo, Titular,
     Procurador)
    SELECT
        DISTINCT
        PRO.IdProcesso,
        5                          AS [Tipo Colidência],
        PRO.MARCA				   AS [Marca(RPI)],
        CLI.MarcaOriginal		   AS [Marca(Cliente)],

        CLI.Classe                 AS [Classe(Cliente)],
        CLI.Deposito               AS [Data Depósito(Cliente)],
        CLI.Concessao              AS [Data Concessão(Cliente)],
        CLI.Processo               AS [Processo(Cliente)],
        CLI.Titular                AS [Titular(Cliente)],
        CLI.Pasta				   AS [Referência/Pasta],
        CLI.Responsavel			   AS [Escritório Responsável],
        CLI.Advogado               AS [Advogado Responsável],

        PRO.CODIGO                 AS [Despacho],
        PRO.ClassFormated		   AS [Classe],
        PRO.Class		           AS [Class],
        PRO.DataDeposito           AS [Data Depósito],
        PRO.DataConcessao          AS [Data Depósito],
        PRO.NUMERO				   AS [Processo],
        PRO.NomeTitular		       AS [Titular],
        PRO.NomeProcurador         AS [Procurador]
      FROM
        CLIENT_TO_COLLIDE               CLI
        JOIN PROCESS_TO_COLLIDE_FULL    PRO ON PRO.IdTipoProcessoRadical = 3
                                                AND CLI.IdTipoProcessoRadical = 3
                                                AND CLI.LengthRadical between 2 and 4
                                                AND PRO.LengthRadical >= CLI.LengthRadical
                    AND
                    (
                      PRO.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE CLI.RADICAL + '%'
                      OR
                      PRO.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE '%' + CLI.RADICAL
                    )
        JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = CLI.class
                                   AND CLF.NUMERO_CLASSE_B = PRO.Class

    INSERT INTO #COLLIDED_PROCESS
    (ID_PROCESSO, [Tipo Colidência], [Marca(RPI)], [Marca(Cliente)], [Classe(Cliente)], [Data Depósito(Cliente)], [Data Concessão(Cliente)], [Processo(Cliente)], [Titular(Cliente)],
     [Referência/Pasta], [Escritório Responsável], [Advogado Responsável], Despacho, Classe, Class, [Data Depósito], [Data Concessão], Processo, Titular,
     Procurador)
    SELECT
        DISTINCT
        PRO.IdProcesso,
        6                          AS [Tipo Colidência],
        PRO.MARCA				   AS [Marca(RPI)],
        CLI.MarcaOriginal		   AS [Marca(Cliente)],

        CLI.Classe                 AS [Classe(Cliente)],
        CLI.Deposito               AS [Data Depósito(Cliente)],
        CLI.Concessao              AS [Data Concessão(Cliente)],
        CLI.Processo               AS [Processo(Cliente)],
        CLI.Titular                AS [Titular(Cliente)],
        CLI.Pasta				   AS [Referência/Pasta],
        CLI.Responsavel			   AS [Escritório Responsável],
        CLI.Advogado               AS [Advogado Responsável],

        PRO.CODIGO                 AS [Despacho],
        PRO.ClassFormated		   AS [Classe],
        PRO.Class		           AS [Class],
        PRO.DataDeposito           AS [Data Depósito],
        PRO.DataConcessao          AS [Data Depósito],
        PRO.NUMERO				   AS [Processo],
        PRO.NomeTitular		       AS [Titular],
        PRO.NomeProcurador         AS [Procurador]
      FROM
        CLIENT_TO_COLLIDE               CLI
        JOIN PROCESS_TO_COLLIDE_FULL    PRO ON PRO.IdTipoProcessoRadical = 3
                                                AND CLI.IdTipoProcessoRadical = 5
                                                AND PRO.LengthRadical >= CLI.LengthRadical
                    AND
                    (
                      PRO.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 = CLI.RADICAL
                    )
        JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = CLI.class
                                   AND CLF.NUMERO_CLASSE_B = PRO.Class


    -- Radicais da RPI contidos no do cliente

    INSERT INTO #COLLIDED_PROCESS
    (ID_PROCESSO, [Tipo Colidência], [Marca(RPI)], [Marca(Cliente)], [Classe(Cliente)], [Data Depósito(Cliente)], [Data Concessão(Cliente)], [Processo(Cliente)], [Titular(Cliente)],
     [Referência/Pasta], [Escritório Responsável], [Advogado Responsável], Despacho, Classe, Class, [Data Depósito], [Data Concessão], Processo, Titular,
     Procurador)
    SELECT
        DISTINCT
        PRO.IdProcesso,
        3                          AS [Tipo Colidência],
        PRO.MARCA				   AS [Marca(RPI)],
        CLI.MarcaOriginal		   AS [Marca(Cliente)],

        CLI.Classe                 AS [Classe(Cliente)],
        CLI.Deposito               AS [Data Depósito(Cliente)],
        CLI.Concessao              AS [Data Concessão(Cliente)],
        CLI.Processo               AS [Processo(Cliente)],
        CLI.Titular                AS [Titular(Cliente)],
        CLI.Pasta				   AS [Referência/Pasta],
        CLI.Responsavel			   AS [Escritório Responsável],
        CLI.Advogado               AS [Advogado Responsável],

        PRO.CODIGO                 AS [Despacho],
        PRO.ClassFormated		   AS [Classe],
        PRO.Class		           AS [Class],
        PRO.DataDeposito           AS [Data Depósito],
        PRO.DataConcessao          AS [Data Depósito],
        PRO.NUMERO				   AS [Processo],
        PRO.NomeTitular		       AS [Titular],
        PRO.NomeProcurador         AS [Procurador]
    FROM
      PROCESS_TO_COLLIDE_FULL   PRO
      JOIN CLIENT_TO_COLLIDE    CLI ON CLI.LengthRadical >= PRO.LengthRadical
                    AND
                    (
                       (
                         PRO.LengthRadical = CLI.LengthRadical
                       )
					             OR
                       (
                          --PRO.LengthRadical = 3 AND CLI.LengthRadical <= 9
                         PRO.LengthRadical = 3 AND CLI.LengthRadical <= 12
                         --PRO.LengthRadical = 3 AND CLI.LengthRadical <= 6
                       )
                       OR
                       (
                         --PRO.LengthRadical = 4 AND CLI.LengthRadical <= 12
                         PRO.LengthRadical = 4 AND CLI.LengthRadical <= 16
                         --PRO.LengthRadical = 4 AND CLI.LengthRadical <= 8
                       )
                       OR
                       (
                         --PRO.LengthRadical = 5 AND CLI.LengthRadical <= 15
                         PRO.LengthRadical = 5 AND CLI.LengthRadical <= 20
                         --PRO.LengthRadical = 5 AND CLI.LengthRadical <= 10
                       )
                       OR
                       (
                         --PRO.LengthRadical = 6 AND CLI.LengthRadical <= 18
                         PRO.LengthRadical = 6 AND CLI.LengthRadical <= 24
                         --PRO.LengthRadical = 6 AND CLI.LengthRadical <= 12
                       )
                       OR
                       (
                         --PRO.LengthRadical = 7 AND CLI.LengthRadical <= 21
                         PRO.LengthRadical = 7 AND CLI.LengthRadical <= 30
                         --PRO.LengthRadical = 7 AND CLI.LengthRadical <= 14
                       )
                       OR
                       (
                         --PRO.LengthRadical = 8 AND CLI.LengthRadical <= 24
                         PRO.LengthRadical = 8 AND CLI.LengthRadical <= 32
                         --PRO.LengthRadical = 8 AND CLI.LengthRadical <= 16
                       )
                       OR
                       (
                         --PRO.LengthRadical = 9 AND CLI.LengthRadical <= 27
                         PRO.LengthRadical = 9 AND CLI.LengthRadical <= 36
                         --PRO.LengthRadical = 9 AND CLI.LengthRadical <= 18
                       )
                    )
                    AND
                    (
                      CLI.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 = PRO.RADICAL
                      OR
                      CLI.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE PRO.RADICAL + '%'
                      OR
                      CLI.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE '%' + PRO.RADICAL
                    )
      JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = PRO.Class
                                   AND CLF.NUMERO_CLASSE_B = CLI.class
    where
        PRO.IdTipoProcessoRadical in (1, 3)
        AND PRO.LengthRadical > 1
        AND CLI.IdTipoProcessoRadical in (1, 3)
        AND CLI.LengthRadical > 1

    INSERT INTO #COLLIDED_PROCESS
    (ID_PROCESSO, [Tipo Colidência], [Marca(RPI)], [Marca(Cliente)], [Classe(Cliente)], [Data Depósito(Cliente)], [Data Concessão(Cliente)], [Processo(Cliente)], [Titular(Cliente)],
     [Referência/Pasta], [Escritório Responsável], [Advogado Responsável], Despacho, Classe, Class, [Data Depósito], [Data Concessão], Processo, Titular,
     Procurador)
    SELECT
        DISTINCT
        PRO.IdProcesso,
        2                          AS [Tipo Colidência],
        PRO.MARCA				   AS [Marca(RPI)],
        CLI.MarcaOriginal		   AS [Marca(Cliente)],

        CLI.Classe                 AS [Classe(Cliente)],
        CLI.Deposito               AS [Data Depósito(Cliente)],
        CLI.Concessao              AS [Data Concessão(Cliente)],
        CLI.Processo               AS [Processo(Cliente)],
        CLI.Titular                AS [Titular(Cliente)],
        CLI.Pasta				   AS [Referência/Pasta],
        CLI.Responsavel			   AS [Escritório Responsável],
        CLI.Advogado               AS [Advogado Responsável],

        PRO.CODIGO                 AS [Despacho],
        PRO.ClassFormated		   AS [Classe],
        PRO.Class		           AS [Class],
        PRO.DataDeposito           AS [Data Depósito],
        PRO.DataConcessao          AS [Data Depósito],
        PRO.NUMERO				   AS [Processo],
        PRO.NomeTitular		       AS [Titular],
        PRO.NomeProcurador         AS [Procurador]
    FROM
      PROCESS_TO_COLLIDE_FULL   PRO
      JOIN CLIENT_TO_COLLIDE    CLI ON CLI.LengthRadical >= PRO.LengthRadical
                    AND
                    (
                       (
                         PRO.LengthRadical = CLI.LengthRadical
                       )
                       OR
                       (
                         --PRO.LengthRadical = 3 AND CLI.LengthRadical <= 9
                         PRO.LengthRadical = 3 AND CLI.LengthRadical <= 12
                         --PRO.LengthRadical = 3 AND CLI.LengthRadical <= 6
                       )
                       OR
                       (
                         --PRO.LengthRadical = 4 AND CLI.LengthRadical <= 12
                         PRO.LengthRadical = 4 AND CLI.LengthRadical <= 16
                         --PRO.LengthRadical = 4 AND CLI.LengthRadical <= 8
                       )
                       OR
                       (
                         --PRO.LengthRadical = 5 AND CLI.LengthRadical <= 15
                         PRO.LengthRadical = 5 AND CLI.LengthRadical <= 20
                         --PRO.LengthRadical = 5 AND CLI.LengthRadical <= 10
                       )
                       OR
                       (
                         --PRO.LengthRadical = 6 AND CLI.LengthRadical <= 18
                         PRO.LengthRadical = 6 AND CLI.LengthRadical <= 24
                         --PRO.LengthRadical = 6 AND CLI.LengthRadical <= 12
                       )
                       OR
                       (
                         --PRO.LengthRadical = 7 AND CLI.LengthRadical <= 21
                         PRO.LengthRadical = 7 AND CLI.LengthRadical <= 30
                         --PRO.LengthRadical = 7 AND CLI.LengthRadical <= 14
                       )
                       OR
                       (
                         --PRO.LengthRadical = 8 AND CLI.LengthRadical <= 24
                         PRO.LengthRadical = 8 AND CLI.LengthRadical <= 32
                         --PRO.LengthRadical = 8 AND CLI.LengthRadical <= 16
                       )
                       OR
                       (
                         --PRO.LengthRadical = 9 AND CLI.LengthRadical <= 27
                         PRO.LengthRadical = 9 AND CLI.LengthRadical <= 36
                         --PRO.LengthRadical = 9 AND CLI.LengthRadical <= 18
                       )
                    )
                    AND (
                            CLI.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE PRO.RADICAL
                           OR
                           (
                             PRO.LengthRadical < 7
                             AND CLI.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE PRO.RADICAL
                           )
                           OR
                           (
                             PRO.LengthRadical >= 7
                             AND CLI.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE '%' + PRO.RADICAL + '%'
                           )
                        )
      JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = PRO.Class
                                   AND CLF.NUMERO_CLASSE_B = CLI.class
    where
        PRO.IdTipoProcessoRadical = 2
        AND PRO.LengthRadical > 1
        AND CLI.IdTipoProcessoRadical in (1, 3)
        AND CLI.LengthRadical > 1


    INSERT INTO #COLLIDED_PROCESS
    (ID_PROCESSO, [Tipo Colidência], [Marca(RPI)], [Marca(Cliente)], [Classe(Cliente)], [Data Depósito(Cliente)], [Data Concessão(Cliente)], [Processo(Cliente)], [Titular(Cliente)],
     [Referência/Pasta], [Escritório Responsável], [Advogado Responsável], Despacho, Classe, Class, [Data Depósito], [Data Concessão], Processo, Titular,
     Procurador)
    SELECT
        DISTINCT
        PRO.IdProcesso,
        5                          AS [Tipo Colidência],
        PRO.MARCA				   AS [Marca(RPI)],
        CLI.MarcaOriginal		   AS [Marca(Cliente)],

        CLI.Classe                 AS [Classe(Cliente)],
        CLI.Deposito               AS [Data Depósito(Cliente)],
        CLI.Concessao              AS [Data Concessão(Cliente)],
        CLI.Processo               AS [Processo(Cliente)],
        CLI.Titular                AS [Titular(Cliente)],
        CLI.Pasta				   AS [Referência/Pasta],
        CLI.Responsavel			   AS [Escritório Responsável],
        CLI.Advogado               AS [Advogado Responsável],

        PRO.CODIGO                 AS [Despacho],
        PRO.ClassFormated		   AS [Classe],
        PRO.Class		           AS [Class],
        PRO.DataDeposito           AS [Data Depósito],
        PRO.DataConcessao          AS [Data Depósito],
        PRO.NUMERO				   AS [Processo],
        PRO.NomeTitular		       AS [Titular],
        PRO.NomeProcurador         AS [Procurador]
    FROM
      PROCESS_TO_COLLIDE_FULL   PRO
      JOIN CLIENT_TO_COLLIDE    CLI ON CLI.IdTipoProcessoRadical = 3
                                        AND PRO.IdTipoProcessoRadical = 3
                                        AND PRO.LengthRadical between 2 and 4
                                        AND CLI.LengthRadical >= PRO.LengthRadical
                    AND
                    (
                      CLI.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE PRO.RADICAL + '%'
                      OR
                      CLI.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE '%' + PRO.RADICAL
                    )
      JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = PRO.Class
                                   AND CLF.NUMERO_CLASSE_B = CLI.class

    INSERT INTO #COLLIDED_PROCESS
    (ID_PROCESSO, [Tipo Colidência], [Marca(RPI)], [Marca(Cliente)], [Classe(Cliente)], [Data Depósito(Cliente)], [Data Concessão(Cliente)], [Processo(Cliente)], [Titular(Cliente)],
     [Referência/Pasta], [Escritório Responsável], [Advogado Responsável], Despacho, Classe, Class, [Data Depósito], [Data Concessão], Processo, Titular,
     Procurador)
    SELECT
        DISTINCT
        PRO.IdProcesso,
        6                          AS [Tipo Colidência],
        PRO.MARCA				   AS [Marca(RPI)],
        CLI.MarcaOriginal		   AS [Marca(Cliente)],

        CLI.Classe                 AS [Classe(Cliente)],
        CLI.Deposito               AS [Data Depósito(Cliente)],
        CLI.Concessao              AS [Data Concessão(Cliente)],
        CLI.Processo               AS [Processo(Cliente)],
        CLI.Titular                AS [Titular(Cliente)],
        CLI.Pasta				   AS [Referência/Pasta],
        CLI.Responsavel			   AS [Escritório Responsável],
        CLI.Advogado               AS [Advogado Responsável],

        PRO.CODIGO                 AS [Despacho],
        PRO.ClassFormated		   AS [Classe],
        PRO.Class		           AS [Class],
        PRO.DataDeposito           AS [Data Depósito],
        PRO.DataConcessao          AS [Data Depósito],
        PRO.NUMERO				   AS [Processo],
        PRO.NomeTitular		       AS [Titular],
        PRO.NomeProcurador         AS [Procurador]
    FROM
      PROCESS_TO_COLLIDE_FULL   PRO
      JOIN CLIENT_TO_COLLIDE    CLI ON CLI.IdTipoProcessoRadical = 3
                                        AND PRO.IdTipoProcessoRadical = 5
                                        AND CLI.LengthRadical >= PRO.LengthRadical
                    AND
                    (
                      CLI.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 = PRO.RADICAL
                    )
      JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = PRO.Class
                                   AND CLF.NUMERO_CLASSE_B = CLI.class

    SELECT

      MIN([Tipo Colidência]) AS [Tipo Colidência],
      [Marca(RPI)],
      [Marca(Cliente)],
      [Classe(Cliente)],
	  [Data Depósito(Cliente)],
      [Processo(Cliente)],
	  [Data Concessão(Cliente)],
	  [Titular(Cliente)],
      SUBSTRING(CPS.Especificacao, 1, 31500) AS [Especificação dos Produtos/Serviços(Cliente)],
      SUBSTRING(CPS.Especificacao, 31500, 31500) AS [Especificação dos Produtos/Serviços(Cliente) 2],
      SUBSTRING(CPS.Especificacao, 63000, 31500) AS [Especificação dos Produtos/Serviços(Cliente) 3],
      [Referência/Pasta],
      [Escritório Responsável],
      [Advogado Responsável],

      --CPC.[Classe],
	  CPC.Classe as [Classe],
      CONVERT(VARCHAR, [Data Depósito], 103) AS [Data Depósito],
      CPC.[Processo],
      CONVERT(VARCHAR, [Data Concessão], 103) AS [Data Concessão],
      CPC.[Titular],
      SUBSTRING(PCL.Specification, 1, 31500) AS [Especificação dos Produtos/Serviços],
      SUBSTRING(PCL.Specification, 31500, 31500) AS [Especificação dos Produtos/Serviços 2],
      SUBSTRING(PCL.Specification, 63000, 31500) AS [Especificação dos Produtos/Serviços 3],
	  [Despacho],
      [Procurador]
    FROM
      #COLLIDED_PROCESS                     CPC
      LEFT JOIN CLIENT_PROCESSES            CPS ON CPS.Processo = CPC.[Processo(Cliente)]
      LEFT JOIN PROCESS_TO_COLLIDE_FULL     PCL ON PCL.IdProcesso  = CPC.ID_PROCESSO and PCL.Class = CPC.Class
	GROUP BY
      [Marca(RPI)],
      [Marca(Cliente)],

      [Classe(Cliente)],
      [Data Depósito(Cliente)],
      [Data Concessão(Cliente)],
      [Processo(Cliente)],
      [Titular(Cliente)],
      CPS.Especificacao,
      [Referência/Pasta],
      [Escritório Responsável],
      [Advogado Responsável],

	  CPC.Classe,
      [Data Depósito],
      CPC.[Processo],
      [Data Concessão],
      CPC.[Titular],
	  PCL.Specification,
      [Despacho],
      [Procurador]
    ORDER BY
	  [Tipo Colidência],
      [Marca(RPI)],
      [Marca(Cliente)]


--        delete from PROCESSO_RADICAL
--        delete from CLIENT_PROCESSES
--        delete from CLIENT_TO_COLLIDE
--        delete from PROCESS_TO_COLLIDE_FULL
--        DROP TABLE #CLIENT_PROCESSES_CLASS
--        DROP TABLE  #COLLIDED_PROCESS
--        delete from PROCESS_TO_COLLIDE


	 --CTRL + K + U
	 --CTRL + K + C