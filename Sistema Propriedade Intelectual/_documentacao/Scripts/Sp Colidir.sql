DECLARE
  @rpi INT,
  @fileOfClient VARCHAR(2000)

	SET NOCOUNT ON;

	SET @rpi = 2449
	SET @fileOfClient = 'D:\Arquivos\Guerra26122017.xlsx' --'D:\Guerra.xlsx'

  --BEGIN TRY

    -- If the rpi not yet exists in Colidir table
	IF NOT EXISTS(
		  							SELECT
		  								*
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
	  SELECT
		  CLI.[P/T]																                    AS ProprioTerceiro,
		  dbo.RemoveFirstCharacter(CLI.PROCESSO)										  AS Processo,
		  dbo.RemoveFirstCharacter(CLI.MARCA)											    AS Marca,
		  dbo.ORTOGRAFAR(dbo.RemoveFirstCharacter(CLI.MARCA))					AS MarcaOrtografada,
          REPLACE(dbo.REMOVER_VOGAIS(dbo.RemoveFirstCharacter(CLI.MARCA)), '' '', '''') AS MarcaSemVogais,
		  dbo.RemoveFirstCharacter(CLI.CLASSE)										    AS Classe,
		  --CONVERT(VARCHAR, CONVERT(DATE, dbo.RemoveFirstCharacter(CLI.[DEPÓSITO])), 103)			AS Deposito,
          --CONVERT(VARCHAR, CONVERT(DATE, dbo.RemoveFirstCharacter(CLI.[CONCESSÃO])), 103)			AS Concessao,
	      CONVERT(VARCHAR, dbo.RemoveFirstCharacter(CLI.[DEPÓSITO]), 103)			AS Deposito,
		  CONVERT(VARCHAR, dbo.RemoveFirstCharacter(CLI.[CONCESSÃO]), 103)			AS Concessao,
		  REPLACE(
        REPLACE(
            REPLACE(dbo.RemoveFirstCharacter(CLI.[ESPECIFICAÇÃO]),
            CHAR(13) + Char(10) ,'' ''),
        CHAR(10), '' ''),
      ''  '', '' '')							  AS Especificacao,
		  dbo.RemoveFirstCharacter(CLI.TITULAR)										    AS Titular,
		  dbo.RemoveFirstCharacter(CLI.PASTA)											    AS Pasta,
		  dbo.RemoveFirstCharacter(CLI.[RESPONSÁVEL])								  AS Responsavel,
		  dbo.RemoveFirstCharacter(CLI.[ADVOGADO/RESPONSÁVEL])			  AS Advogado
	  INTO
		  CLIENT_PROCESSES
	  FROM
		  OPENROWSET(''Microsoft.ACE.OLEDB.12.0'', ''Excel 12.0 XML;Database='+ @fileOfClient +';HDR=YES'', ''SELECT * FROM [COLIDIR$]'') CLI
	  WHERE
      CLI.PROCESSO != '''''

	  EXECUTE(@sqlExcel)

	SELECT
			PRC.ID_PROCESSO,
		  PRO.NUMERO,
		  PRO.MARCA,
		  PRO.MARCA_ORTOGRAFADA,
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
	  INTO
		  #PROCESS_TO_COLLIDE
	  FROM
		  PROCESSO_DESPACHO_COLIDI 	PRC
		  JOIN PROCESSO							PRO ON PRC.NUMERO_RPI = @rpi
                                       AND PRO.TIPO_APRESENTACAO != 3
                                        AND PRO.MARCA IS NOT NULL
                                        AND PRO.ID = PRC.ID_PROCESSO
      JOIN DESPACHO             DES ON DES.ID = PRC.ID_DESPACHO
	  WHERE
		  NOT EXISTS(
			  		SELECT
			  			*
			  		FROM
			  			CLIENT_PROCESSES
				  	WHERE
					  	Processo = PRO.NUMERO
				    )

    DELETE
    FROM
		  CLIENT_PROCESSES
    WHERE
      Marca IS NULL
			OR Marca = ''
			OR ProprioTerceiro = 'T'

	-- Build the Radicais of CLIENT PROCESSES
    DECLARE
      @processNumero VARCHAR(20),
      @marca NVARCHAR(2000),
      @marcaOrtografada NVARCHAR(2000)

    DECLARE procesesCursor CURSOR
      LOCAL FAST_FORWARD
    FOR SELECT
          Processo,
          Marca,
          MarcaOrtografada
        FROM
          CLIENT_PROCESSES

    OPEN procesesCursor

    FETCH procesesCursor INTO @processNumero, @marca, @marcaOrtografada;

    WHILE (@@FETCH_STATUS = 0)
      BEGIN

        -- Build and Insert term by term spelled
        EXECUTE BUILD_RADICAL_BY_WORD_OF_BRAND
          @processNumero,
          @marca,
          0,
          1

        -- Insert the word spelled with yours raddicals
        EXECUTE INSERT_PROCESSO_RADICAL_BY_WORD
          @processNumero,
          @marcaOrtografada,
          0,
          0

        FETCH procesesCursor INTO @processNumero, @marca, @marcaOrtografada;
      END

     CLOSE procesesCursor;
     DEALLOCATE procesesCursor;
    --END Build the Radicais of CLIENT PROCESSES


    -- Build the Radicais of RPI PROCESSES
    DECLARE
      @processNumero2 VARCHAR(20),
      @marca2 NVARCHAR(2000),
      @marcaOrtografada2 NVARCHAR(2000)

    DECLARE procesesCursor2 CURSOR
      LOCAL FAST_FORWARD
    FOR SELECT
          NUMERO,
          MARCA,
          MARCA_ORTOGRAFADA
        FROM
          #PROCESS_TO_COLLIDE

    OPEN procesesCursor2

    FETCH procesesCursor2 INTO @processNumero2, @marca2, @marcaOrtografada2;

    WHILE (@@FETCH_STATUS = 0)
      BEGIN

        EXECUTE BUILD_RADICAL_BY_WORD_OF_BRAND
          @processNumero2,
          @marca2,
          0,
          1

        -- Insert the word spelled with yours raddicals
        EXECUTE INSERT_PROCESSO_RADICAL_BY_WORD
          @processNumero2,
          @marcaOrtografada2,
          0,
          0

        FETCH procesesCursor2 INTO @processNumero2, @marca2, @marcaOrtografada2;
      END

    CLOSE procesesCursor2;
    DEALLOCATE procesesCursor2;
    --END Build the Radicais of RPI PROCESSES

    SELECT
      CLA.class,
      CLP.Processo
    INTO
      #CLIENT_PROCESSES_CLASS
    FROM
      CLIENT_PROCESSES CLP
      CROSS APPLY dbo.FormatClassesFromFile(CLP.Classe) AS CLA

	-- Do the Collidence

		SELECT
      PRC.ID_PROCESSO,
      1                                           AS [Tipo Colidência],
      PRC.MARCA										              	AS [Marca(RPI)],
      CLP.MARCA										              	AS [Marca(Cliente)],

      CLP.Classe                                  AS [Classe(Cliente)],
      CONVERT(VARCHAR, CLP.Deposito, 103)         AS [Data Depósito(Cliente)],
      CONVERT(VARCHAR, CLP.Concessao, 103)         AS [Data Concessão(Cliente)],
      CLP.Processo                                AS [Processo(Cliente)],
      CLP.Titular                                 AS [Titular(Cliente)],
      CLP.Especificacao                           AS [Especificação dos Produtos/Serviços(Cliente)],
      CLP.Pasta										              	AS [Referência/Pasta],
      CLP.Responsavel								          	  AS [Escritório Responsável],
      CLP.Advogado                                AS [Advogado Responsável],

      PRC.CODIGO                                  AS [Despacho],
      PRC.FORMAT_CLASSES                          AS [Classe],
      CONVERT(VARCHAR, PRC.DATA_DEPOSITO, 103)		AS [Data Depósito],
      CONVERT(VARCHAR, PRC.DATA_CONCESSAO, 103)		AS [Data Concessão],
      PRC.NUMERO									            		AS [Processo],
      PRC.NOME_TITULAR											      AS [Titular],
      PRC.NOME_PROCURADOR                         AS [Procurador],
      PRC.ESPECIFICACAO								        		AS [Especificação dos Produtos/Serviços]
--     INTO
--       #COLLIDED_PROCESS
    FROM
      #PROCESS_TO_COLLIDE			PRC
      JOIN CLIENT_PROCESSES		CLP ON CLP.MarcaOrtografada = PRC.MARCA_ORTOGRAFADA;

  -- Marcas sem vogais e afinidade de classes

  WITH CLI AS
    (
      SELECT
        DISTINCT

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
        CLPS.MarcaSemVogais AS MarcaSemVogais,
        LEN(CLPS.MarcaSemVogais) AS LenMarcaSemVogais
    FROM
      CLIENT_PROCESSES		          CLPS
      JOIN #CLIENT_PROCESSES_CLASS  CPC ON CPC.Processo = CLPS.Processo
    ),

    PRO AS
    (
      SELECT
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
        PRC.ESPECIFICACAO,
        PRC.MARCA_SEM_VOGAIS AS MARCA_SEM_VOGAIS,
        LEN(PRC.MARCA_SEM_VOGAIS) AS LEN_MARCA_SEM_VOGAIS
      FROM
        #PROCESS_TO_COLLIDE			      PRC
    )

--     INSERT INTO #COLLIDED_PROCESS
--       (
--        ID_PROCESSO,
--        [Tipo Colidência],
--        [Marca(RPI)],
--        [Marca(Cliente)],
--        [Classe(Cliente)],
--        [Data Depósito(Cliente)],
--        [Data Concessão(Cliente)],
--        [Processo(Cliente)],
--        [Titular(Cliente)],
--        [Especificação dos Produtos/Serviços(Cliente)],
--        [Referência/Pasta],
--        [Escritório Responsável],
--        [Advogado Responsável],
--
--        [Despacho],
--        [Classe],
--        [Data Depósito],
--        [Data Concessão],
--        [Processo],
--        [Titular],
--        [Procurador],
--        [Especificação dos Produtos/Serviços]
--       )
    SELECT
        DISTINCT
        PRO.ID_PROCESSO,
        4                          AS [Tipo Colidência],
        PRO.MARCA									 AS [Marca(RPI)],
        CLI.MARCA								   AS [Marca(Cliente)],

        CLI.Classe                 AS [Classe(Cliente)],
        CLI.Deposito               AS [Data Depósito(Cliente)],
        CLI.Concessao              AS [Data Concessão(Cliente)],
        CLI.Processo               AS [Processo(Cliente)],
        CLI.Titular                AS [Titular(Cliente)],
        CLI.Especificacao          AS [Especificação dos Produtos/Serviços(Cliente)],
        CLI.Pasta									 AS [Referência/Pasta],
        CLI.Responsavel						 AS [Escritório Responsável],
        CLI.Advogado               AS [Advogado Responsável],

        PRO.CODIGO                 AS [Despacho],
        PRO.FORMAT_CLASSES				 AS [Classe],
        PRO.DATA_DEPOSITO          AS [Data Depósito],
        PRO.DATA_CONCESSAO         AS [Data Depósito],
        PRO.NUMERO								 AS [Processo],
        PRO.NOME_TITULAR					 AS [Titular],
        PRO.NOME_PROCURADOR        AS [Procurador],
        PRO.ESPECIFICACAO					 AS [Especificação dos Produtos/Serviços]
      FROM
        PRO
        JOIN CLI ON CLI.LenMarcaSemVogais > 2 AND CLI.MarcaSemVogais = PRO.MARCA_SEM_VOGAIS
                    OR (
                          CLI.LenMarcaSemVogais > 2
                          AND CLI.LenMarcaSemVogais = PRO.LEN_MARCA_SEM_VOGAIS
                          AND CLI.MarcaSemVogais = PRO.MARCA_SEM_VOGAIS
                       )
        JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = CLI.class
                                   AND CLF.NUMERO_CLASSE_B = COALESCE(PRO.CLASSE_1, PRO.CLASSE_INTERNACIONAL);

    -- Radicais do cliente contidos no da RPI
    WITH CLI AS
    (
      SELECT
        DISTINCT
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
        CPC.class
    FROM
      CLIENT_PROCESSES		       CLPS
      JOIN PROCESSO_RADICAL      PRRS ON PRRS.MAIN = 1
                                           AND PRRS.LENGTH_RADICAL > 1
                                           AND PRRS.NUMERO_PROCESSO = CLPS.Processo
      JOIN #CLIENT_PROCESSES_CLASS  CPC ON CPC.Processo = CLPS.Processo
    ),

    PRO AS
    (
      SELECT
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
        PRC.ESPECIFICACAO
      FROM
        #PROCESS_TO_COLLIDE			      PRC
        JOIN PROCESSO_RADICAL         PRR ON PRR.MAIN = 1
                                             AND PRR.LENGTH_RADICAL > 1
                                             AND PRR.NUMERO_PROCESSO = PRC.NUMERO
    )

--     INSERT INTO #COLLIDED_PROCESS
--       (
--        ID_PROCESSO,
--        [Tipo Colidência],
--        [Marca(RPI)],
--        [Marca(Cliente)],
--        [Classe(Cliente)],
--        [Data Depósito(Cliente)],
--        [Data Concessão(Cliente)],
--        [Processo(Cliente)],
--        [Titular(Cliente)],
--        [Especificação dos Produtos/Serviços(Cliente)],
--        [Referência/Pasta],
--        [Escritório Responsável],
--        [Advogado Responsável],
--
--        [Despacho],
--        [Classe],
--        [Data Depósito],
--        [Data Concessão],
--        [Processo],
--        [Titular],
--        [Procurador],
--        [Especificação dos Produtos/Serviços]
--       )
    SELECT
        DISTINCT
        PRO.ID_PROCESSO,
        3                          AS [Tipo Colidência],
        PRO.MARCA									 AS [Marca(RPI)],
        CLI.MARCA								   AS [Marca(Cliente)],

        CLI.Classe                 AS [Classe(Cliente)],
        CLI.Deposito               AS [Data Depósito(Cliente)],
        CLI.Concessao              AS [Data Concessão(Cliente)],
        CLI.Processo               AS [Processo(Cliente)],
        CLI.Titular                AS [Titular(Cliente)],
        CLI.Especificacao          AS [Especificação dos Produtos/Serviços(Cliente)],
        CLI.Pasta									 AS [Referência/Pasta],
        CLI.Responsavel						 AS [Escritório Responsável],
        CLI.Advogado               AS [Advogado Responsável],

        PRO.CODIGO                 AS [Despacho],
        PRO.FORMAT_CLASSES				 AS [Classe],
        PRO.DATA_DEPOSITO          AS [Data Depósito],
        PRO.DATA_CONCESSAO         AS [Data Depósito],
        PRO.NUMERO								 AS [Processo],
        PRO.NOME_TITULAR					 AS [Titular],
        PRO.NOME_PROCURADOR        AS [Procurador],
        PRO.ESPECIFICACAO					 AS [Especificação dos Produtos/Serviços]
      FROM
        CLI
        JOIN PRO ON PRO.LENGTH_RADICAL >= CLI.LENGTH_RADICAL
                    AND
                    (
                       (
                         CLI.LENGTH_RADICAL = PRO.LENGTH_RADICAL
                       )
                       OR
                       (
                         CLI.LENGTH_RADICAL = 3 AND PRO.LENGTH_RADICAL <= 6
                       )
                       OR
                       (
                         CLI.LENGTH_RADICAL = 4 AND PRO.LENGTH_RADICAL <= 8
                       )
                       OR
                       (
                         CLI.LENGTH_RADICAL = 5 AND PRO.LENGTH_RADICAL <= 10
                       )
                       OR
                       (
                         CLI.LENGTH_RADICAL = 6 AND PRO.LENGTH_RADICAL <= 12
                       )
                       OR
                       (
                         CLI.LENGTH_RADICAL = 7 AND PRO.LENGTH_RADICAL <= 14
                       )
                       OR
                       (
                         CLI.LENGTH_RADICAL = 8 AND PRO.LENGTH_RADICAL <= 16
                       )
                       OR
                       (
                         CLI.LENGTH_RADICAL = 9 AND PRO.LENGTH_RADICAL <= 18
                       )
                    )
                    AND
                    (
                      PRO.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 = CLI.RADICAL
--                       OR
--                       CONTAINS(PRO.RADICAL, CLI.RADICAL)
--                       CONTAINS(PRO.RADICAL, '"' + CLI.RADICAL + '*"')
--                       OR
--                       CONTAINS(PRO.RADICAL, '"*' + CLI.RADICAL + '"')

--                       PRO.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE CLI.RADICAL + '%'
--                       OR
--                       PRO.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE '%' + CLI.RADICAL
                    )
        JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = CLI.class
                                   AND CLF.NUMERO_CLASSE_B = COALESCE(PRO.CLASSE_1, PRO.CLASSE_INTERNACIONAL);


    WITH CLI AS
    (
      SELECT
        DISTINCT
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
        CPC.class
    FROM
      CLIENT_PROCESSES		       CLPS
      JOIN PROCESSO_RADICAL      PRRS ON PRRS.MAIN = 0
                                           AND PRRS.LENGTH_RADICAL > 1
                                           AND PRRS.NUMERO_PROCESSO = CLPS.Processo
      JOIN #CLIENT_PROCESSES_CLASS  CPC ON CPC.Processo = CLPS.Processo
    ),

    PRO AS
    (
      SELECT
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
        PRC.ESPECIFICACAO
      FROM
        #PROCESS_TO_COLLIDE			      PRC
        JOIN PROCESSO_RADICAL         PRR ON PRR.MAIN = 0
                                             AND PRR.LENGTH_RADICAL > 1
                                             AND PRR.NUMERO_PROCESSO = PRC.NUMERO
    )

    INSERT INTO #COLLIDED_PROCESS
      (
       ID_PROCESSO,
       [Tipo Colidência],
       [Marca(RPI)],
       [Marca(Cliente)],
       [Classe(Cliente)],
       [Data Depósito(Cliente)],
       [Data Concessão(Cliente)],
       [Processo(Cliente)],
       [Titular(Cliente)],
       [Especificação dos Produtos/Serviços(Cliente)],
       [Referência/Pasta],
       [Escritório Responsável],
       [Advogado Responsável],

       [Despacho],
       [Classe],
       [Data Depósito],
       [Data Concessão],
       [Processo],
       [Titular],
       [Procurador],
       [Especificação dos Produtos/Serviços]
      )
    SELECT
        DISTINCT
        PRO.ID_PROCESSO,
        2                          AS [Tipo Colidência],
        PRO.MARCA									 AS [Marca(RPI)],
        CLI.MARCA								   AS [Marca(Cliente)],

        CLI.Classe                 AS [Classe(Cliente)],
        CLI.Deposito               AS [Data Depósito(Cliente)],
        CLI.Concessao              AS [Data Concessão(Cliente)],
        CLI.Processo               AS [Processo(Cliente)],
        CLI.Titular                AS [Titular(Cliente)],
        CLI.Especificacao          AS [Especificação dos Produtos/Serviços(Cliente)],
        CLI.Pasta									 AS [Referência/Pasta],
        CLI.Responsavel						 AS [Escritório Responsável],
        CLI.Advogado               AS [Advogado Responsável],

        PRO.CODIGO                 AS [Despacho],
        PRO.FORMAT_CLASSES				 AS [Classe],
        PRO.DATA_DEPOSITO          AS [Data Depósito],
        PRO.DATA_CONCESSAO         AS [Data Depósito],
        PRO.NUMERO								 AS [Processo],
        PRO.NOME_TITULAR					 AS [Titular],
        PRO.NOME_PROCURADOR        AS [Procurador],
        PRO.ESPECIFICACAO					 AS [Especificação dos Produtos/Serviços]
      FROM
        CLI
        JOIN PRO ON PRO.LENGTH_RADICAL >= CLI.LENGTH_RADICAL
                    AND
                    (
                       (
                         CLI.LENGTH_RADICAL = PRO.LENGTH_RADICAL
                       )
                       OR
                       (
                         CLI.LENGTH_RADICAL = 3 AND PRO.LENGTH_RADICAL <= 6
                       )
                       OR
                       (
                         CLI.LENGTH_RADICAL = 4 AND PRO.LENGTH_RADICAL <= 8
                       )
                       OR
                       (
                         CLI.LENGTH_RADICAL = 5 AND PRO.LENGTH_RADICAL <= 10
                       )
                       OR
                       (
                         CLI.LENGTH_RADICAL = 6 AND PRO.LENGTH_RADICAL <= 12
                       )
                       OR
                       (
                         CLI.LENGTH_RADICAL = 7 AND PRO.LENGTH_RADICAL <= 14
                       )
                       OR
                       (
                         CLI.LENGTH_RADICAL = 8 AND PRO.LENGTH_RADICAL <= 16
                       )
                       OR
                       (
                         CLI.LENGTH_RADICAL = 9 AND PRO.LENGTH_RADICAL <= 18
                       )
                    )
                    AND
                    (
                      (
                         CLI.LENGTH_RADICAL < 6
                         AND PRO.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE CLI.RADICAL
                       )
                       OR
                       (
                         CLI.LENGTH_RADICAL >= 6
                         AND PRO.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE '%' + CLI.RADICAL + '%'
                       )
                    )
        JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = CLI.class
                                   AND CLF.NUMERO_CLASSE_B = COALESCE(PRO.CLASSE_1, PRO.CLASSE_INTERNACIONAL);


    -- Radicais da RPI contidos no do cliente
    WITH PRO AS
    (
      SELECT
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
        PRC.ESPECIFICACAO
      FROM
        #PROCESS_TO_COLLIDE			      PRC
        JOIN PROCESSO_RADICAL         PRR ON PRR.MAIN = 1
                                             AND PRR.LENGTH_RADICAL > 1
                                             AND PRR.NUMERO_PROCESSO = PRC.NUMERO
    ),

    CLI AS
    (
      SELECT

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
      CPC.class
    FROM
      CLIENT_PROCESSES		       CLPS
      JOIN PROCESSO_RADICAL        PRRS ON PRRS.MAIN = 1
                                           AND PRRS.LENGTH_RADICAL > 1
                                           AND PRRS.NUMERO_PROCESSO = CLPS.Processo
      JOIN #CLIENT_PROCESSES_CLASS  CPC ON CPC.Processo = CLPS.Processo
    )

   INSERT INTO #COLLIDED_PROCESS
    (
     ID_PROCESSO,
     [Tipo Colidência],
     [Marca(RPI)],
     [Marca(Cliente)],
     [Classe(Cliente)],
     [Data Depósito(Cliente)],
     [Data Concessão(Cliente)],
     [Processo(Cliente)],
     [Titular(Cliente)],
     [Especificação dos Produtos/Serviços(Cliente)],
     [Referência/Pasta],
     [Escritório Responsável],
     [Advogado Responsável],

     [Despacho],
     [Classe],
     [Data Depósito],
     [Data Concessão],
     [Processo],
     [Titular],
     [Procurador],
     [Especificação dos Produtos/Serviços]
    )
  SELECT
      DISTINCT
      PRO.ID_PROCESSO,
      3                          AS [Tipo Colidência],
      PRO.MARCA									 AS [Marca(RPI)],
      CLI.MARCA								   AS [Marca(Cliente)],

      CLI.Classe                 AS [Classe(Cliente)],
      CLI.Deposito               AS [Data Depósito(Cliente)],
      CLI.Concessao              AS [Data Concessão(Cliente)],
      CLI.Processo               AS [Processo(Cliente)],
      CLI.Titular                AS [Titular(Cliente)],
      CLI.Especificacao          AS [Especificação dos Produtos/Serviços(Cliente)],
      CLI.Pasta									 AS [Referência/Pasta],
      CLI.Responsavel						 AS [Escritório Responsável],
      CLI.Advogado               AS [Advogado Responsável],

      PRO.CODIGO                 AS [Despacho],
      PRO.FORMAT_CLASSES				 AS [Classe],
      PRO.DATA_DEPOSITO          AS [Data Depósito],
      PRO.DATA_CONCESSAO         AS [Data Depósito],
      PRO.NUMERO								 AS [Processo],
      PRO.NOME_TITULAR					 AS [Titular],
      PRO.NOME_PROCURADOR        AS [Procurador],
      PRO.ESPECIFICACAO					 AS [Especificação dos Produtos/Serviços]
    FROM
      PRO
      JOIN CLI ON CLI.LENGTH_RADICAL >= PRO.LENGTH_RADICAL
                    AND
                    (
                       (
                         PRO.LENGTH_RADICAL = CLI.LENGTH_RADICAL
                       )
					             OR
                       (
                         PRO.LENGTH_RADICAL = 3 AND CLI.LENGTH_RADICAL <= 6
                       )
                       OR
                       (
                         PRO.LENGTH_RADICAL = 4 AND CLI.LENGTH_RADICAL <= 8
                       )
                       OR
                       (
                         PRO.LENGTH_RADICAL = 5 AND CLI.LENGTH_RADICAL <= 10
                       )
                       OR
                       (
                         PRO.LENGTH_RADICAL = 6 AND CLI.LENGTH_RADICAL <= 12
                       )
                       OR
                       (
                         PRO.LENGTH_RADICAL = 7 AND CLI.LENGTH_RADICAL <= 14
                       )
                       OR
                       (
                         PRO.LENGTH_RADICAL = 8 AND CLI.LENGTH_RADICAL <= 16
                       )
                       OR
                       (
                         PRO.LENGTH_RADICAL = 9 AND CLI.LENGTH_RADICAL <= 18
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
      JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = COALESCE(PRO.CLASSE_1, PRO.CLASSE_INTERNACIONAL)
                                   AND CLF.NUMERO_CLASSE_B = CLI.class;


 WITH PRO AS
  (
    SELECT
      DISTINCT
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
      PRC.ESPECIFICACAO
    FROM
      #PROCESS_TO_COLLIDE			      PRC
      JOIN PROCESSO_RADICAL         PRR ON PRR.MAIN = 0
                                           AND PRR.LENGTH_RADICAL > 1
                                           AND PRR.NUMERO_PROCESSO = PRC.NUMERO
  ),

  CLI AS
  (
    SELECT
      DISTINCT
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
	    CPC.class
	  FROM
	    CLIENT_PROCESSES		       CLPS
	    JOIN PROCESSO_RADICAL        PRRS ON PRRS.MAIN = 0
										                       AND PRRS.LENGTH_RADICAL > 1
										                       AND PRRS.NUMERO_PROCESSO = CLPS.Processo
	    JOIN #CLIENT_PROCESSES_CLASS  CPC ON CPC.Processo = CLPS.Processo
  )

	/*INSERT INTO #COLLIDED_PROCESS
	(
		ID_PROCESSO,
		[Tipo Colidência],
		[Marca(RPI)],
		[Marca(Cliente)],
		[Classe(Cliente)],
		[Data Depósito(Cliente)],
		[Data Concessão(Cliente)],
		[Processo(Cliente)],
		[Titular(Cliente)],
		[Especificação dos Produtos/Serviços(Cliente)],
		[Referência/Pasta],
		[Escritório Responsável],
		[Advogado Responsável],

		[Despacho],
		[Classe],
		[Data Depósito],
		[Data Concessão],
		[Processo],
		[Titular],
		[Procurador],
		[Especificação dos Produtos/Serviços]
	)*/
  SELECT
      DISTINCT
      PRO.ID_PROCESSO,
      2                           AS [Tipo Colidência],
      PRO.MARCA									  AS [Marca(RPI)],
      CLI.MARCA								    AS [Marca(Cliente)],

      CLI.Classe                  AS [Classe(Cliente)],
      CLI.Deposito                AS [Data Depósito(Cliente)],
      CLI.Concessao               AS [Data Concessão(Cliente)],
      CLI.Processo                AS [Processo(Cliente)],
      CLI.Titular                 AS [Titular(Cliente)],
      CLI.Especificacao           AS [Especificação dos Produtos/Serviços(Cliente)],
      CLI.Pasta									  AS [Referência/Pasta],
      CLI.Responsavel							AS [Escritório Responsável],
      CLI.Advogado                AS [Advogado Responsável],

      PRO.CODIGO                  AS [Despacho],
      PRO.FORMAT_CLASSES					AS [Classe],
      PRO.DATA_DEPOSITO           AS [Data Depósito],
      PRO.DATA_CONCESSAO          AS [Data Depósito],
      PRO.NUMERO								  AS [Processo],
      PRO.NOME_TITULAR						AS [Titular],
      PRO.NOME_PROCURADOR         AS [Procurador],
      PRO.ESPECIFICACAO						AS [Especificação dos Produtos/Serviços]
    FROM
      PRO
      JOIN CLI ON CLI.LENGTH_RADICAL >= PRO.LENGTH_RADICAL
                    AND
                    (
                       (
                         PRO.LENGTH_RADICAL = CLI.LENGTH_RADICAL
                       )
                       OR
                       (
                         PRO.LENGTH_RADICAL = 3 AND CLI.LENGTH_RADICAL <= 6
                       )
                       OR
                       (
                         PRO.LENGTH_RADICAL = 4 AND CLI.LENGTH_RADICAL <= 8
                       )
                       OR
                       (
                         PRO.LENGTH_RADICAL = 5 AND CLI.LENGTH_RADICAL <= 10
                       )
                       OR
                       (
                         PRO.LENGTH_RADICAL = 6 AND CLI.LENGTH_RADICAL <= 12
                       )
                       OR
                       (
                         PRO.LENGTH_RADICAL = 7 AND CLI.LENGTH_RADICAL <= 14
                       )
                       OR
                       (
                         PRO.LENGTH_RADICAL = 8 AND CLI.LENGTH_RADICAL <= 16
                       )
                       OR
                       (
                         PRO.LENGTH_RADICAL = 9 AND CLI.LENGTH_RADICAL <= 18
                       )
                    )
                    AND (
                            CLI.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE PRO.RADICAL
                           OR
                           (
                             PRO.LENGTH_RADICAL < 7
                             AND CLI.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE PRO.RADICAL
                           )
                           OR
                           (
                             CLI.LENGTH_RADICAL >= 7
                             AND CLI.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE '%' + PRO.RADICAL + '%'
                           )
                        )
      JOIN CLASSE_AFINIDADE CLF ON CLF.NUMERO_CLASSE_A = COALESCE(PRO.CLASSE_1, PRO.CLASSE_INTERNACIONAL)
                                   AND CLF.NUMERO_CLASSE_B = CLI.class;

    SELECT

      MIN([Tipo Colidência]) AS [Tipo Colidência],
      [Marca(RPI)],
      [Marca(Cliente)],

      [Classe(Cliente)],
	    [Data Depósito(Cliente)],
	    [Data Concessão(Cliente)],
      --CONVERT(VARCHAR, [Data Depósito(Cliente)]), 103 AS [Data Depósito(Cliente)],
      --CONVERT(VARCHAR, [Data Concessão(Cliente)]), 103 AS [Data Concessão(Cliente)],
      [Processo(Cliente)],
      [Titular(Cliente)],
      SUBSTRING([Especificação dos Produtos/Serviços(Cliente)], 1, 32750) AS [Especificação dos Produtos/Serviços(Cliente)],
      [Referência/Pasta],
      [Escritório Responsável],
      [Advogado Responsável],

      [Despacho],
      [Classe],
      CONVERT(VARCHAR, [Data Depósito], 103) AS [Data Depósito],
      CONVERT(VARCHAR, [Data Concessão], 103) AS [Data Concessão],
      [Processo],
      [Titular],
      [Procurador],
      SUBSTRING([Especificação dos Produtos/Serviços], 1, 32750) AS [Especificação dos Produtos/Serviços]
    FROM
      #COLLIDED_PROCESS
	GROUP BY
      [Marca(RPI)],
      [Marca(Cliente)],

      [Classe(Cliente)],
      [Data Depósito(Cliente)],
      [Data Concessão(Cliente)],
      [Processo(Cliente)],
      [Titular(Cliente)],
      [Especificação dos Produtos/Serviços(Cliente)],
      [Referência/Pasta],
      [Escritório Responsável],
      [Advogado Responsável],

      [Despacho],
      [Classe],
      [Data Depósito],
      [Data Concessão],
      [Processo],
      [Titular],
      [Procurador],
      [Especificação dos Produtos/Serviços]
    ORDER BY
	  [Tipo Colidência],
      [Marca(RPI)],
      [Marca(Cliente)]

  --END TRY
  --BEGIN CATCH

  --  DROP TABLE CLIENT_PROCESSES
  --  DROP TABLE #PROCESS_TO_COLLIDE
  --  DROP TABLE  #COLLIDED_PROCESS
  --  DROP TABLE #CLIENT_PROCESSES_CLASS
  --  DELETE from PROCESSO_DESPACHO_COLIDI
  --  DELETE FROM PROCESSO_RADICAL
  --END CATCH


select * from CLIENT_PROCESSES
select * from #PROCESS_TO_COLLIDE

select
  *
from
  PROCESSO_RADICAL;



select
  *
from
  sys.fulltext_stopwords
where
  [language_id] = 2070

ALTER FULLTEXT STOPLIST [Teste] ADD 'academia' LANGUAGE 'Portuguese';


CREATE TABLE [dbo].[PROCESSO_RADICAL](
  [ID] [int] IDENTITY(1,1) NOT NULL,
	[NUMERO_PROCESSO] [varchar](20) NOT NULL,
	[RADICAL] [nvarchar](2000) NOT NULL,
	[LENGTH_RADICAL] [int] NOT NULL,
	[MAIN] [bit] NULL,
	CONSTRAINT [PK_PROCESSO_RADICAL] PRIMARY KEY CLUSTERED
([ID] ASC)
)


select
  *
from
  sys.fulltext_stopwords

CREATE FULLTEXT CATALOG HcOffice AS DEFAULT


CREATE FULLTEXT INDEX ON PROCESSO
  (
      MARCA LANGUAGE Portuguese,
      MARCA_ORTOGRAFADA LANGUAGE Portuguese
  )
  KEY INDEX PK__PROCESSO__3214EC2601899666
  ON
  (
    --Definição do catálago full-text no qual o índice full-text será armazenado
    HcOffice
  )
  WITH
  (
  --O modelo de change tracking e qual arquivo de stoplist serão utilizados pelo índice full-text
       CHANGE_TRACKING = MANUAL,
       STOPLIST = Teste
  );
  GO

  --Habilitando o índice full-text criado na tabela EBook
  ALTER FULLTEXT INDEX
  ON PROCESSO ENABLE;
  GO


CREATE FULLTEXT INDEX ON PROCESSO_RADICAL
  (
     RADICAL LANGUAGE Portuguese
  )
  KEY INDEX PK_PROCESSO_RADICAL
  ON
  (
    --Definição do catálago full-text no qual o índice full-text será armazenado
    HcOffice
  )
  WITH
  (
  --O modelo de change tracking e qual arquivo de stoplist serão utilizados pelo índice full-text
       CHANGE_TRACKING = MANUAL,
       STOPLIST = Teste
  );
  GO

  --Habilitando o índice full-text criado na tabela EBook
  ALTER FULLTEXT INDEX
  ON PROCESSO_RADICAL ENABLE;
  GO


select
  *
from
  PROCESSO
where
  contains(MARCA, '"sede*"')
--   MARCA is not null;