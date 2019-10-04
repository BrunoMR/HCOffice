DECLARE
  @rpi INT,
  @fileOfClient VARCHAR(2000)

	SET NOCOUNT ON;

	SET @rpi = 2533
	SET @fileOfClient = 'C:\Development\Arquivos\Teste_PERFORMANCE Pequeno.xlsx' --'D:\Guerra.xlsx'

  --BEGIN TRY
  -- Query Executa processamento da colidência RPI Azure_New - Regra MÉDIA

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
																				AND DES.CODIGO IN ('IPAS009', 'IPAS135', 'IPAS158', 'IPAS421')
			  WHERE
					  PRD.NUMERO_RPI = @rpi
		  END

	  -- Insert rows of client`s file into temporary table
	  DECLARE
		  @sqlExcel NVARCHAR(MAX)

	  SET @sqlExcel = '
      insert into CLIENT_PROCESSES
      (ProprioTerceiro, Processo, Marca, MarcaOrtografada, MarcaSemVogais, Classe, Deposito, Concessao, Especificacao, Titular, Pasta, Responsavel, Advogado)
	  SELECT
		  CLI.[P/T]																                    AS ProprioTerceiro,
		  dbo.RemoveFirstCharacter(CLI.PROCESSO)										            AS Processo,
		  dbo.RemoveFirstCharacter(CLI.MARCA)											            AS Marca,
		  dbo.ORTOGRAFAR(dbo.RemoveFirstCharacter(CLI.MARCA))					                    AS MarcaOrtografada,
          REPLACE(dbo.REMOVER_VOGAIS(dbo.RemoveFirstCharacter(CLI.MARCA)), '' '', '''')             AS MarcaSemVogais,
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
    (ID_PROCESSO, NUMERO, MARCA, MARCA_ORTOGRAFADA, MARCA_SEM_VOGAIS, FORMAT_CLASSES, CLASSE_1, CLASSE_INTERNACIONAL, NOME_TITULAR, ESPECIFICACAO, DATA_DEPOSITO, DATA_CONCESSAO, CODIGO, NOME_PROCURADOR)
	SELECT
        PRC.ID_PROCESSO,
        PRO.NUMERO,
        PRO.MARCA,
        dbo.ORTOGRAFAR(PRO.MARCA) AS MARCA_ORTOGRAFADA,
        replace(PRO.MARCA_SEM_VOGAIS, ' ', '') AS MARCA_SEM_VOGAIS,
        dbo.FormatClasses(PRO.CLASSE_1, PRO.CLASSE_2, PRO.CLASSE_3, PRO.CLASSE_INTERNACIONAL) AS FORMAT_CLASSES,
        PRO.CLASSE_1,
        PRO.CLASSE_INTERNACIONAL,
        PRO.NOME_TITULAR,
        PRO.ESPECIFICACAO,
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
        Marca IS NULL
        OR Marca = ''
        OR ProprioTerceiro = 'T'

	-- Build the Radicais of CLIENT PROCESSES
    -- Build the Radicais of RPI PROCESSES

    INSERT into PROCESSO_RADICAL
    (NUMERO_PROCESSO, RADICAL, LENGTH_RADICAL, MAIN)
    SELECT
        RAD.NUMERO_PROCESSO,
        RAD.RADICAL,
        RAD.LENGTH_RADICAL,
        RAD.MAIN
    FROM
        CLIENT_PROCESSES    CLI
        cross apply dbo.RadicalsProcessByWord(Processo, MarcaOrtografada, 0, 0) RAD
    UNION

    SELECT
        RAD.NUMERO_PROCESSO,
        RAD.RADICAL,
        RAD.LENGTH_RADICAL,
        RAD.MAIN
    FROM
        CLIENT_PROCESSES    CLI
        cross apply dbo.BuildBrandRadicalsByWord(Processo, Marca, 0, 1) RAD

    UNION

    SELECT
        RAD.NUMERO_PROCESSO,
        RAD.RADICAL,
        RAD.LENGTH_RADICAL,
        RAD.MAIN
    FROM
        PROCESS_TO_COLLIDE PRO
        cross apply dbo.RadicalsProcessByWord(PRO.NUMERO, PRO.MARCA_ORTOGRAFADA, 0, 0) RAD

    UNION

    SELECT
        RAD.NUMERO_PROCESSO,
        RAD.RADICAL,
        RAD.LENGTH_RADICAL,
        RAD.MAIN
    FROM
        PROCESS_TO_COLLIDE PRO
        cross apply dbo.BuildBrandRadicalsByWord(PRO.NUMERO, PRO.MARCA, 0, 1) RAD

    --END Build the Radicais of CLIENT PROCESSES
    --END Build the Radicais of RPI PROCESSES

    SELECT
      CLA.class,
      CLP.Processo
    INTO
        #CLIENT_PROCESSES_CLASS
    FROM
      CLIENT_PROCESSES CLP
      CROSS APPLY dbo.FormatClassesFromFile(CLP.Classe) AS CLA

    -- Build the client to collide

    INSERT INTO CLIENT_TO_COLLIDE
    (Main, LengthRadical, Radical, NumeroProcesso, Marca, Classe, Deposito, Concessao, Processo, Titular, Especificacao, Pasta, Responsavel, Advogado, Class, MarcaSemVogais, LenMarcaSemVogais)
     SELECT
        DISTINCT
        PRRS.MAIN,
        PRRS.LENGTH_RADICAL,
        PRRS.RADICAL,
        PRRS.NUMERO_PROCESSO,

        CLPS.Marca,
        CLPS.Classe,
        CLPS.Deposito,
        CLPS.Concessao,
        CLPS.Processo,
        CLPS.Titular,
        CLPS.Especificacao,
        CLPS.Pasta,
        CLPS.Responsavel,
        CLPS.Advogado,
        CPC.class,
        CLPS.MarcaSemVogais,
        LEN(CLPS.MarcaSemVogais) AS LenMarcaSemVogais
    FROM
      CLIENT_PROCESSES		       CLPS
      JOIN PROCESSO_RADICAL      PRRS ON PRRS.NUMERO_PROCESSO = CLPS.Processo
      LEFT JOIN #CLIENT_PROCESSES_CLASS  CPC ON CPC.Processo = CLPS.Processo

    -- END Build the client to collide


    -- Build the processes to collide full

    insert into PROCESS_TO_COLLIDE_FULL
    (Main, LengthRadical, Radical, IdProcesso, Marca, Codigo, FormatClasses, Classe1, ClasseInternacional, DataDeposito, DataConcessao, Numero, NomeTitular, NomeProcurador, MarcaSemVogais, LenMarcaSemVogais)
    SELECT
        PRR.MAIN,
        PRR.LENGTH_RADICAL,
        PRR.RADICAL,

        PRC.ID_PROCESSO,
        PRC.MARCA,

        PRC.CODIGO,
        PRC.FORMAT_CLASSES,
        PRC.CLASSE_1,
        PRC.CLASSE_INTERNACIONAL,
        PRC.DATA_DEPOSITO,
        PRC.DATA_CONCESSAO,
        PRC.NUMERO,
        PRC.NOME_TITULAR,
        PRC.NOME_PROCURADOR,
        PRC.MARCA_SEM_VOGAIS,
        LEN(PRC.MARCA_SEM_VOGAIS)
      FROM
        PROCESS_TO_COLLIDE			  PRC
        JOIN PROCESSO_RADICAL         PRR ON PRR.NUMERO_PROCESSO = PRC.NUMERO

    -- END Build the processes to collide full


	-- Do the Collidence

	SELECT
      PRC.ID_PROCESSO,
      1                                           AS [Tipo Colidência],
      PRC.MARCA									  AS [Marca(RPI)],
      CLP.MARCA									  AS [Marca(Cliente)],

      CLP.Classe                                  AS [Classe(Cliente)],
      CONVERT(VARCHAR, CLP.Deposito, 103)         AS [Data Depósito(Cliente)],
      CONVERT(VARCHAR, CLP.Concessao, 103)        AS [Data Concessão(Cliente)],
      CLP.Processo                                AS [Processo(Cliente)],
      CLP.Titular                                 AS [Titular(Cliente)],
      CLP.Especificacao                           AS [Especificação dos Produtos/Serviços(Cliente)],
      CLP.Pasta									  AS [Referência/Pasta],
      CLP.Responsavel							  AS [Escritório Responsável],
      CLP.Advogado                                AS [Advogado Responsável],

      PRC.CODIGO                                  AS [Despacho],
      PRC.FORMAT_CLASSES                          AS [Classe],
      CONVERT(VARCHAR, PRC.DATA_DEPOSITO, 103)	  AS [Data Depósito],
      CONVERT(VARCHAR, PRC.DATA_CONCESSAO, 103)	  AS [Data Concessão],
      PRC.NUMERO								  AS [Processo],
      PRC.NOME_TITULAR							  AS [Titular],
      PRC.NOME_PROCURADOR                         AS [Procurador],
      PRC.ESPECIFICACAO							  AS [Especificação dos Produtos/Serviços]
    INTO
      #COLLIDED_PROCESS
    FROM
        PROCESS_TO_COLLIDE			PRC
        JOIN CLIENT_PROCESSES		CLP ON CLP.MarcaOrtografada = PRC.MARCA_ORTOGRAFADA;


    -- Marcas sem vogais e afinidade de classes


    INSERT INTO #COLLIDED_PROCESS
    (ID_PROCESSO, [Tipo Colidência], [Marca(RPI)], [Marca(Cliente)], [Classe(Cliente)], [Data Depósito(Cliente)], [Data Concessão(Cliente)], [Processo(Cliente)], [Titular(Cliente)],
     [Especificação dos Produtos/Serviços(Cliente)], [Referência/Pasta], [Escritório Responsável], [Advogado Responsável], Despacho, Classe, [Data Depósito], [Data Concessão], Processo, Titular,
     Procurador)
    SELECT
        DISTINCT
        PRO.IdProcesso,
        4                          AS [Tipo Colidência],
        PRO.MARCA				   AS [Marca(RPI)],
        CLI.MARCA				   AS [Marca(Cliente)],

        CLI.Classe                 AS [Classe(Cliente)],
        CLI.Deposito               AS [Data Depósito(Cliente)],
        CLI.Concessao              AS [Data Concessão(Cliente)],
        CLI.Processo               AS [Processo(Cliente)],
        CLI.Titular                AS [Titular(Cliente)],
        CLI.Especificacao          AS [Especificação dos Produtos/Serviços(Cliente)],
        CLI.Pasta				   AS [Referência/Pasta],
        CLI.Responsavel			   AS [Escritório Responsável],
        CLI.Advogado               AS [Advogado Responsável],

        PRO.CODIGO                 AS [Despacho],
        PRO.FormatClasses		   AS [Classe],
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
        JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = CLI.class
                                   AND CLF.NUMERO_CLASSE_B = COALESCE(PRO.Classe1, PRO.ClasseInternacional);


    -- Radicais do cliente contidos no da RPI

    INSERT INTO #COLLIDED_PROCESS
    (ID_PROCESSO, [Tipo Colidência], [Marca(RPI)], [Marca(Cliente)], [Classe(Cliente)], [Data Depósito(Cliente)], [Data Concessão(Cliente)], [Processo(Cliente)], [Titular(Cliente)],
     [Especificação dos Produtos/Serviços(Cliente)], [Referência/Pasta], [Escritório Responsável], [Advogado Responsável], Despacho, Classe, [Data Depósito], [Data Concessão], Processo, Titular,
     Procurador)
    SELECT
        DISTINCT
        PRO.IdProcesso,
        3                          AS [Tipo Colidência],
        PRO.MARCA				   AS [Marca(RPI)],
        CLI.MARCA				   AS [Marca(Cliente)],

        CLI.Classe                 AS [Classe(Cliente)],
        CLI.Deposito               AS [Data Depósito(Cliente)],
        CLI.Concessao              AS [Data Concessão(Cliente)],
        CLI.Processo               AS [Processo(Cliente)],
        CLI.Titular                AS [Titular(Cliente)],
        CLI.Especificacao          AS [Especificação dos Produtos/Serviços(Cliente)],
        CLI.Pasta				   AS [Referência/Pasta],
        CLI.Responsavel			   AS [Escritório Responsável],
        CLI.Advogado               AS [Advogado Responsável],

        PRO.CODIGO                 AS [Despacho],
        PRO.FormatClasses		   AS [Classe],
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
                         CLI.LengthRadical = 3 AND PRO.LengthRadical <= 9
                         --CLI.LengthRadical = 3 AND PRO.LENGTH_RADICAL <= 12
                         --CLI.LengthRadical = 3 AND PRO.LENGTH_RADICAL <= 6
                       )
                       OR
                       (
                         CLI.LengthRadical = 4 AND PRO.LengthRadical <= 12
                         --CLI.LengthRadical = 4 AND PRO.LENGTH_RADICAL <= 16
                         --CLI.LengthRadical = 4 AND PRO.LENGTH_RADICAL <= 8
                       )
                       OR
                       (
                         CLI.LengthRadical = 5 AND PRO.LengthRadical <= 15
                         --CLI.LengthRadical = 5 AND PRO.LENGTH_RADICAL <= 20
                         --CLI.LengthRadical = 5 AND PRO.LENGTH_RADICAL <= 10
                       )
                       OR
                       (
                         CLI.LengthRadical = 6 AND PRO.LengthRadical <= 18
                         --CLI.LengthRadical = 6 AND PRO.LENGTH_RADICAL <= 24
                         --CLI.LengthRadical = 6 AND PRO.LENGTH_RADICAL <= 12
                       )
                       OR
                       (
                         CLI.LengthRadical = 7 AND PRO.LengthRadical <= 21
                         --CLI.LengthRadical = 7 AND PRO.LENGTH_RADICAL <= 30
                         --CLI.LengthRadical = 7 AND PRO.LENGTH_RADICAL <= 14
                       )
                       OR
                       (
                         CLI.LengthRadical = 8 AND PRO.LengthRadical <= 24
                         --CLI.LengthRadical = 8 AND PRO.LENGTH_RADICAL <= 32
                         --CLI.LengthRadical = 8 AND PRO.LENGTH_RADICAL <= 16
                       )
                       OR
                       (
                         CLI.LengthRadical = 9 AND PRO.LengthRadical <= 27
                         --CLI.LengthRadical = 9 AND PRO.LENGTH_RADICAL <= 36
                         --CLI.LengthRadical = 9 AND PRO.LENGTH_RADICAL <= 18
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
                                   AND CLF.NUMERO_CLASSE_B = COALESCE(PRO.Classe1, PRO.ClasseInternacional)
    where
        CLI.Main = 1
        AND CLI.LengthRadical > 1
        AND PRO.Main = 1
        AND PRO.LengthRadical > 1

    UNION

    SELECT
        DISTINCT
        PRO.IdProcesso,
        2                          AS [Tipo Colidência],
        PRO.MARCA				   AS [Marca(RPI)],
        CLI.MARCA				   AS [Marca(Cliente)],

        CLI.Classe                 AS [Classe(Cliente)],
        CLI.Deposito               AS [Data Depósito(Cliente)],
        CLI.Concessao              AS [Data Concessão(Cliente)],
        CLI.Processo               AS [Processo(Cliente)],
        CLI.Titular                AS [Titular(Cliente)],
        CLI.Especificacao          AS [Especificação dos Produtos/Serviços(Cliente)],
        CLI.Pasta				   AS [Referência/Pasta],
        CLI.Responsavel			   AS [Escritório Responsável],
        CLI.Advogado               AS [Advogado Responsável],

        PRO.CODIGO                 AS [Despacho],
        PRO.FormatClasses		   AS [Classe],
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
                         CLI.LengthRadical = 3 AND PRO.LengthRadical <= 9
                         --CLI.LengthRadical = 3 AND PRO.LengthRadical <= 12
                         --CLI.LengthRadical = 3 AND PRO.LengthRadical <= 6
                       )
                       OR
                       (
                         CLI.LengthRadical = 4 AND PRO.LengthRadical <= 12
                         --CLI.LengthRadical = 4 AND PRO.LengthRadical <= 16
                         --CLI.LengthRadical = 4 AND PRO.LengthRadical <= 8
                       )
                       OR
                       (
                         CLI.LengthRadical = 5 AND PRO.LengthRadical <= 15
                         --CLI.LengthRadical = 5 AND PRO.LengthRadical <= 20
                         --CLI.LengthRadical = 5 AND PRO.LengthRadical <= 10
                       )
                       OR
                       (
                         CLI.LengthRadical = 6 AND PRO.LengthRadical <= 18
                         --CLI.LengthRadical = 6 AND PRO.LengthRadical <= 24
                         --CLI.LengthRadical = 6 AND PRO.LengthRadical <= 12
                       )
                       OR
                       (
                         CLI.LengthRadical = 7 AND PRO.LengthRadical <= 21
                         --CLI.LengthRadical = 7 AND PRO.LengthRadical <= 30
                         --CLI.LengthRadical = 7 AND PRO.LengthRadical <= 14
                       )
                       OR
                       (
                         CLI.LengthRadical = 8 AND PRO.LengthRadical <= 24
                         --CLI.LengthRadical = 8 AND PRO.LengthRadical <= 32
                         --CLI.LengthRadical = 8 AND PRO.LengthRadical <= 16
                       )
                       OR
                       (
                         CLI.LengthRadical = 9 AND PRO.LengthRadical <= 27
                         --CLI.LengthRadical = 9 AND PRO.LengthRadical <= 36
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
        JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = CLI.class
                                   AND CLF.NUMERO_CLASSE_B = COALESCE(PRO.Classe1, PRO.ClasseInternacional)
    WHERE
        CLI.Main = 0
        AND CLI.LengthRadical > 1
        AND PRO.Main = 1
        AND PRO.LengthRadical > 1


    -- Radicais da RPI contidos no do cliente

    UNION

    SELECT
        DISTINCT
        PRO.IdProcesso,
        3                          AS [Tipo Colidência],
        PRO.MARCA				   AS [Marca(RPI)],
        CLI.MARCA				   AS [Marca(Cliente)],

        CLI.Classe                 AS [Classe(Cliente)],
        CLI.Deposito               AS [Data Depósito(Cliente)],
        CLI.Concessao              AS [Data Concessão(Cliente)],
        CLI.Processo               AS [Processo(Cliente)],
        CLI.Titular                AS [Titular(Cliente)],
        CLI.Especificacao          AS [Especificação dos Produtos/Serviços(Cliente)],
        CLI.Pasta				   AS [Referência/Pasta],
        CLI.Responsavel			   AS [Escritório Responsável],
        CLI.Advogado               AS [Advogado Responsável],

        PRO.CODIGO                 AS [Despacho],
        PRO.FormatClasses		   AS [Classe],
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
                         PRO.LengthRadical = 3 AND CLI.LengthRadical <= 9
                         --PRO.LengthRadical = 3 AND CLI.LengthRadical <= 12
                         --PRO.LengthRadical = 3 AND CLI.LengthRadical <= 6
                       )
                       OR
                       (
                         PRO.LengthRadical = 4 AND CLI.LengthRadical <= 12
                         --PRO.LengthRadical = 4 AND CLI.LengthRadical <= 16
                         --PRO.LengthRadical = 4 AND CLI.LengthRadical <= 8
                       )
                       OR
                       (
                         PRO.LengthRadical = 5 AND CLI.LengthRadical <= 15
                         --PRO.LengthRadical = 5 AND CLI.LengthRadical <= 20
                         --PRO.LengthRadical = 5 AND CLI.LengthRadical <= 10
                       )
                       OR
                       (
                         PRO.LengthRadical = 6 AND CLI.LengthRadical <= 18
                         --PRO.LengthRadical = 6 AND CLI.LengthRadical <= 24
                         --PRO.LengthRadical = 6 AND CLI.LengthRadical <= 12
                       )
                       OR
                       (
                         PRO.LengthRadical = 7 AND CLI.LengthRadical <= 21
                         --PRO.LengthRadical = 7 AND CLI.LengthRadical <= 30
                         --PRO.LengthRadical = 7 AND CLI.LengthRadical <= 14
                       )
                       OR
                       (
                         PRO.LengthRadical = 8 AND CLI.LengthRadical <= 24
                         --PRO.LengthRadical = 8 AND CLI.LengthRadical <= 32
                         --PRO.LengthRadical = 8 AND CLI.LengthRadical <= 16
                       )
                       OR
                       (
                         PRO.LengthRadical = 9 AND CLI.LengthRadical <= 27
                         --PRO.LengthRadical = 9 AND CLI.LengthRadical <= 36
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
      JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = COALESCE(PRO.Classe1, PRO.ClasseInternacional)
                                   AND CLF.NUMERO_CLASSE_B = CLI.class
    where
        PRO.Main = 1
        AND PRO.LengthRadical > 1
        AND CLI.Main = 1
        AND CLI.LengthRadical > 1

    UNION

    SELECT
        DISTINCT
        PRO.IdProcesso,
        2                          AS [Tipo Colidência],
        PRO.MARCA				   AS [Marca(RPI)],
        CLI.MARCA				   AS [Marca(Cliente)],

        CLI.Classe                 AS [Classe(Cliente)],
        CLI.Deposito               AS [Data Depósito(Cliente)],
        CLI.Concessao              AS [Data Concessão(Cliente)],
        CLI.Processo               AS [Processo(Cliente)],
        CLI.Titular                AS [Titular(Cliente)],
        CLI.Especificacao          AS [Especificação dos Produtos/Serviços(Cliente)],
        CLI.Pasta				   AS [Referência/Pasta],
        CLI.Responsavel			   AS [Escritório Responsável],
        CLI.Advogado               AS [Advogado Responsável],

        PRO.CODIGO                 AS [Despacho],
        PRO.FormatClasses		   AS [Classe],
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
                         PRO.LengthRadical = 3 AND CLI.LengthRadical <= 9
                         --PRO.LengthRadical = 3 AND CLI.LengthRadical <= 12
                         --PRO.LengthRadical = 3 AND CLI.LengthRadical <= 6
                       )
                       OR
                       (
                         PRO.LengthRadical = 4 AND CLI.LengthRadical <= 12
                         --PRO.LengthRadical = 4 AND CLI.LengthRadical <= 16
                         --PRO.LengthRadical = 4 AND CLI.LengthRadical <= 8
                       )
                       OR
                       (
                         PRO.LengthRadical = 5 AND CLI.LengthRadical <= 15
                         --PRO.LengthRadical = 5 AND CLI.LengthRadical <= 20
                         --PRO.LengthRadical = 5 AND CLI.LengthRadical <= 10
                       )
                       OR
                       (
                         PRO.LengthRadical = 6 AND CLI.LengthRadical <= 18
                         --PRO.LengthRadical = 6 AND CLI.LengthRadical <= 24
                         --PRO.LengthRadical = 6 AND CLI.LengthRadical <= 12
                       )
                       OR
                       (
                         PRO.LengthRadical = 7 AND CLI.LengthRadical <= 21
                         --PRO.LengthRadical = 7 AND CLI.LengthRadical <= 30
                         --PRO.LengthRadical = 7 AND CLI.LengthRadical <= 14
                       )
                       OR
                       (
                         PRO.LengthRadical = 8 AND CLI.LengthRadical <= 24
                         --PRO.LengthRadical = 8 AND CLI.LengthRadical <= 32
                         --PRO.LengthRadical = 8 AND CLI.LengthRadical <= 16
                       )
                       OR
                       (
                         PRO.LengthRadical = 9 AND CLI.LengthRadical <= 27
                         --PRO.LengthRadical = 9 AND CLI.LengthRadical <= 36
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
                             CLI.LengthRadical >= 7
                             AND CLI.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE '%' + PRO.RADICAL + '%'
                           )
                        )
      JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = COALESCE(PRO.Classe1, PRO.ClasseInternacional)
                                   AND CLF.NUMERO_CLASSE_B = CLI.class
    where
        PRO.Main = 0
        AND PRO.LengthRadical > 1
        AND CLI.Main = 1
        AND CLI.LengthRadical > 1


    SELECT

      MIN([Tipo Colidência]) AS [Tipo Colidência],
      [Marca(RPI)],
      [Marca(Cliente)],
      [Classe(Cliente)],
	  [Data Depósito(Cliente)],
	  [Data Concessão(Cliente)],
      [Processo(Cliente)],
      [Titular(Cliente)],
      SUBSTRING(CPS.Especificacao, 1, 31500) AS [Especificação dos Produtos/Serviços(Cliente)],
      SUBSTRING(CPS.Especificacao, 31500, 31500) AS [Especificação dos Produtos/Serviços(Cliente) 2],
      SUBSTRING(CPS.Especificacao, 63000, 31500) AS [Especificação dos Produtos/Serviços(Cliente) 3],
      [Referência/Pasta],
      [Escritório Responsável],
      [Advogado Responsável],

      [Despacho],
      CPC.[Classe],
      CONVERT(VARCHAR, [Data Depósito], 103) AS [Data Depósito],
      CONVERT(VARCHAR, [Data Concessão], 103) AS [Data Concessão],
      CPC.[Processo],
      CPC.[Titular],
      [Procurador],
      SUBSTRING(PRO.ESPECIFICACAO, 1, 31500) AS [Especificação dos Produtos/Serviços],
      SUBSTRING(PRO.ESPECIFICACAO, 31500, 31500) AS [Especificação dos Produtos/Serviços 2],
      SUBSTRING(PRO.ESPECIFICACAO, 63000, 31500) AS [Especificação dos Produtos/Serviços 3]
    FROM
      #COLLIDED_PROCESS                 CPC
      LEFT JOIN CLIENT_PROCESSES        CPS ON CPS.Processo = CPC.[Processo(Cliente)]
      LEFT JOIN PROCESSO                PRO ON PRO.NUMERO = CPC.Processo
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

      [Despacho],
      CPC.[Classe],
      [Data Depósito],
      [Data Concessão],
      CPC.[Processo],
      CPC.[Titular],
      [Procurador],
      PRO.ESPECIFICACAO
    ORDER BY
	  [Tipo Colidência],
      [Marca(RPI)],
      [Marca(Cliente)]


--      delete from PROCESSO_RADICAL
--      delete from CLIENT_PROCESSES
--      delete from CLIENT_TO_COLLIDE
--      delete from PROCESS_TO_COLLIDE_FULL
--      DROP TABLE #CLIENT_PROCESSES_CLASS
--      DROP TABLE  #COLLIDED_PROCESS
--      delete from PROCESS_TO_COLLIDE


	 --CTRL + K + U
	 --CTRL + K + C





SELECT
    RAD.NUMERO_PROCESSO,
    RAD.RADICAL,
    RAD.LENGTH_RADICAL,
    RAD.MAIN
FROM
    PROCESS_TO_COLLIDE PRO
    cross apply dbo.BuildBrandRadicalsByWord(PRO.NUMERO, PRO.MARCA, 0, 1) RAD


SELECT
--     RAD.NUMERO_PROCESSO,
--     RAD.RADICAL,
--     RAD.LENGTH_RADICAL,
--     RAD.MAIN
PRO.NUMERO,
PRO.MARCA, *
FROM
    PROCESS_TO_COLLIDE PRO
--     cross apply dbo.BuildBrandRadicalsByWord(PRO.NUMERO, PRO.MARCA, 0, 1) RAD
where
   len(pro.MARCA) > 30
order by
    CLASSE_INTERNACIONAL,
    len(PRO.MARCA) desc


SELECT
--     RAD.NUMERO_PROCESSO,
--     RAD.RADICAL,
--     RAD.LENGTH_RADICAL,
--     RAD.MAIN
PRO.NUMERO,
PRO.MARCA, *
FROM
    PROCESS_TO_COLLIDE PRO
--     cross apply dbo.BuildBrandRadicalsByWord(PRO.NUMERO, PRO.MARCA, 0, 1) RAD
where
   pro.MARCA like '%cosm[aeiou]t%'

