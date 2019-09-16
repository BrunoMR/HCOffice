-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 20/10/2017
-- Description:	Este procedimento irá realizar todo fluxo necessário para colidência de marcas
-- =============================================
CREATE PROCEDURE [dbo].[COLIDIR_PROCESSO]
  @rpi INT,
	@fileOfClient VARCHAR(2000)
AS
BEGIN
	SET NOCOUNT ON;

  BEGIN TRY

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
		  dbo.REMOVER_VOGAIS(dbo.RemoveFirstCharacter(CLI.MARCA))			AS MarcaSemVogais,
		  dbo.RemoveFirstCharacter(CLI.CLASSE)										    AS Classe,
		  dbo.RemoveFirstCharacter(CLI.[DEPÓSITO])									  AS Deposito,
		  dbo.RemoveFirstCharacter(CLI.[ESPECIFICAÇÃO])							  AS Especificacao,
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
		  PRO.MARCA_SEM_VOGAIS,
		  PRO.CLASSE_1,
		  PRO.CLASSE_2,
		  PRO.CLASSE_3,
		  PRO.CLASSE_INTERNACIONAL,
		  PRO.NOME_TITULAR,
		  PRO.ESPECIFICACAO,
		  PRO.DATA_DEPOSITO
	  INTO
		  #PROCESS_TO_COLLIDE
	  FROM
		  PROCESSO_DESPACHO_COLIDI 	PRC
		  JOIN PROCESSO							PRO ON PRO.ID = PRC.ID_PROCESSO
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


--   CREATE INDEX INDEX_CLIENT_PROCESSES_MARCA
--                 ON CLIENT_PROCESSES (MARCA)
--   GO
--
--   CREATE INDEX INDEX_CLIENT_PROCESSES_MARCA_ORTOGRAFADA
--                 ON CLIENT_PROCESSES (MarcaOrtografada)
--   GO
--
--   CREATE INDEX INDEX_CLIENT_PROCESSES_MARCA_SEM_VOGAIS
--                 ON CLIENT_PROCESSES (MarcaSemVogais)
--   GO
--
--   CREATE INDEX INDEX_PROCESS_TO_COLLIDE_MARCA_ORTOGRAFADA
--                 ON #PROCESS_TO_COLLIDE (MARCA_ORTOGRAFADA)
--   GO
--
--   CREATE INDEX INDEX_PROCESS_TO_COLLIDE_MARCA_SEM_VOGAIS
--                 ON #PROCESS_TO_COLLIDE (MARCA_SEM_VOGAIS)
--   GO


	-- Build the Radicais of CLIENT PROCESSES
    DECLARE
      @processNumero VARCHAR(20),
      @marca NVARCHAR(2000)

    DECLARE procesesCursor CURSOR
      LOCAL FAST_FORWARD
    FOR SELECT
          Processo,
          Marca
        FROM
          CLIENT_PROCESSES

    OPEN procesesCursor

    FETCH procesesCursor INTO @processNumero, @marca;

    WHILE (@@FETCH_STATUS = 0)
      BEGIN

        EXECUTE BUILD_RADICAL_BY_WORD_OF_BRAND
          @processNumero,
          @marca

        FETCH procesesCursor INTO @processNumero, @marca;
      END

     CLOSE procesesCursor;
     DEALLOCATE procesesCursor;
    --END Build the Radicais of CLIENT PROCESSES


    -- Build the Radicais of RPI PROCESSES
    DECLARE
      @processNumero2 VARCHAR(20),
      @marca2 NVARCHAR(2000)

    DECLARE procesesCursor2 CURSOR
      LOCAL FAST_FORWARD
    FOR SELECT
          NUMERO,
          MARCA
        FROM
          #PROCESS_TO_COLLIDE

    OPEN procesesCursor2

    FETCH procesesCursor2 INTO @processNumero2, @marca2;

    WHILE (@@FETCH_STATUS = 0)
      BEGIN

        EXECUTE BUILD_RADICAL_BY_WORD_OF_BRAND
          @processNumero2,
          @marca2

        FETCH procesesCursor2 INTO @processNumero2, @marca2;
      END

    CLOSE procesesCursor2;
    DEALLOCATE procesesCursor2;
    --END Build the Radicais of RPI PROCESSES


  DELETE
      FROM
        PROCESSO_RADICAL
      WHERE
        RADICAL = '_'
        OR RADICAL = '__'
    
    
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
      PRC.MARCA										              	AS [Marca(RPI)],
      CLP.MARCA										              	AS [Marca(Cliente)],

      CLP.Classe                                  AS [Classe(Cliente)],
      CLP.Deposito                                AS [Data Depósito(Cliente)],
      CLP.Processo                                AS [Processo(Cliente)],
      CLP.Titular                                 AS [Titular(Cliente)],
      CLP.Especificacao                           AS [Especificação dos Produtos/Serviços(Cliente)],
      CLP.Pasta										              	AS [Referência/Pasta],
      CLP.Responsavel								          	  AS [Escritório Responsável],
      CLP.Advogado                                AS [Advogado Responsável],

      PRC.CLASSE_1										          	AS [Classe],
      PRC.DATA_DEPOSITO                           AS [Data Depósito],
      PRC.NUMERO									            		AS [Processo],
      PRC.NOME_TITULAR											      AS [Titular],
      PRC.ESPECIFICACAO								        		AS [Especificação dos Produtos/Serviços]
    INTO
      #COLLIDED_PROCESS
    FROM
      #PROCESS_TO_COLLIDE			PRC
      JOIN CLIENT_PROCESSES		CLP ON CLP.MarcaOrtografada = PRC.MARCA_ORTOGRAFADA
                                   OR (LEN(CLP.MarcaSemVogais) > 2 AND CLP.MarcaSemVogais = PRC.MARCA_SEM_VOGAIS)
                                   OR (
                                        LEN(CLP.MarcaSemVogais) > 2
                                        AND LEN(CLP.MarcaSemVogais) = LEN(PRC.MARCA_SEM_VOGAIS)
                                        AND CLP.MarcaSemVogais = PRC.MARCA_SEM_VOGAIS
                                     )

    -- Radicais do cliente contidos no da RPI
    INSERT INTO #COLLIDED_PROCESS
    (
      ID_PROCESSO,
     [Marca(RPI)],
     [Marca(Cliente)],
     [Classe(Cliente)],
     [Data Depósito(Cliente)],
     [Processo(Cliente)],
     [Titular(Cliente)],
     [Especificação dos Produtos/Serviços(Cliente)],
     [Referência/Pasta],
     [Escritório Responsável],
     [Advogado Responsável],

      Classe,
     [Data Depósito],
     Processo,
     Titular,
     [Especificação dos Produtos/Serviços]
    )
	  SELECT
      DISTINCT
      PRC.ID_PROCESSO,
      PRC.MARCA										              	AS [Marca(RPI)],
      CLP.MARCA										              	AS [Marca(Cliente)],

      CLP.Classe                                  AS [Classe(Cliente)],
      CLP.Deposito                                AS [Data Depósito(Cliente)],
      CLP.Processo                                AS [Processo(Cliente)],
      CLP.Titular                                 AS [Titular(Cliente)],
      CLP.Especificacao                           AS [Especificação dos Produtos/Serviços(Cliente)],
      CLP.Pasta										              	AS [Referência/Pasta],
      CLP.Responsavel								          	  AS [Escritório Responsável],
      CLP.Advogado                                AS [Advogado Responsável],

      PRC.CLASSE_1										          	AS [Classe],
      PRC.DATA_DEPOSITO                           AS [Data Depósito],
      PRC.NUMERO									            		AS [Processo],
      PRC.NOME_TITULAR											      AS [Titular],
      PRC.ESPECIFICACAO								        		AS [Especificação dos Produtos/Serviços]
    FROM
      CLIENT_PROCESSES		          CLP
      JOIN PROCESSO_RADICAL         PRR ON PRR.MAIN = 1
                                           AND PRR.LENGTH_RADICAL > 3
                                           AND PRR.NUMERO_PROCESSO = CLP.Processo
      JOIN #CLIENT_PROCESSES_CLASS  CPC ON CPC.Processo = CLP.Processo
      JOIN (
             SELECT
               PRRS.LENGTH_RADICAL,
               PRRS.RADICAL,
               PRRS.NUMERO_PROCESSO,

               PRCS.ID_PROCESSO,
               PRCS.MARCA,

               dbo.GetJustClasse(PRCS.CLASSE_1) AS CLASSE_1,
               PRCS.CLASSE_2,
               PRCS.CLASSE_3,
               dbo.GetJustClasse(PRCS.CLASSE_INTERNACIONAL) AS CLASSE_INTERNACIONAL,

               PRCS.DATA_DEPOSITO,
               PRCS.NUMERO,
               PRCS.NOME_TITULAR,
               PRCS.ESPECIFICACAO
             FROM
               #PROCESS_TO_COLLIDE			      PRCS
               JOIN PROCESSO_RADICAL          PRRS ON PRRS.MAIN = 1
                                                      AND PRRS.LENGTH_RADICAL > 3
                                                      AND PRRS.NUMERO_PROCESSO = PRCS.NUMERO
           ) PRC ON PRC.LENGTH_RADICAL > PRR.LENGTH_RADICAL
                    AND
                    (
                       (
                         PRR.LENGTH_RADICAL = 4 AND PRC.LENGTH_RADICAL <= 7
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 5 AND PRC.LENGTH_RADICAL <= 9
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 6 AND PRC.LENGTH_RADICAL <= 11
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 7 AND PRC.LENGTH_RADICAL <= 13
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 8 AND PRC.LENGTH_RADICAL <= 15
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 9 AND PRC.LENGTH_RADICAL <= 17
                       )
                    )
                    AND PRC.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE '%' + PRR.RADICAL + '%'
      JOIN CLASSE_AFINIDADE CLF ON dbo.GetJustClasse(CLF.NUMERO_CLASSE_A) = dbo.GetJustClasse(CPC.class)
                                   AND dbo.GetJustClasse(CLF.NUMERO_CLASSE_B) = COALESCE(PRC.CLASSE_1, PRC.CLASSE_INTERNACIONAL)

    INSERT INTO #COLLIDED_PROCESS
    (
      ID_PROCESSO,
     [Marca(RPI)],
     [Marca(Cliente)],
     [Classe(Cliente)],
     [Data Depósito(Cliente)],
     [Processo(Cliente)],
     [Titular(Cliente)],
     [Especificação dos Produtos/Serviços(Cliente)],
     [Referência/Pasta],
     [Escritório Responsável],
     [Advogado Responsável],

      Classe,
     [Data Depósito],
     Processo,
     Titular,
     [Especificação dos Produtos/Serviços]
    )
	  SELECT
      DISTINCT
      PRC.ID_PROCESSO,
      PRC.MARCA										              	AS [Marca(RPI)],
      CLP.MARCA										              	AS [Marca(Cliente)],

      CLP.Classe                                  AS [Classe(Cliente)],
      CLP.Deposito                                AS [Data Depósito(Cliente)],
      CLP.Processo                                AS [Processo(Cliente)],
      CLP.Titular                                 AS [Titular(Cliente)],
      CLP.Especificacao                           AS [Especificação dos Produtos/Serviços(Cliente)],
      CLP.Pasta										              	AS [Referência/Pasta],
      CLP.Responsavel								          	  AS [Escritório Responsável],
      CLP.Advogado                                AS [Advogado Responsável],

      PRC.CLASSE_1										          	AS [Classe],
      PRC.DATA_DEPOSITO                           AS [Data Depósito],
      PRC.NUMERO									            		AS [Processo],
      PRC.NOME_TITULAR											      AS [Titular],
      PRC.ESPECIFICACAO								        		AS [Especificação dos Produtos/Serviços]
    FROM
      CLIENT_PROCESSES		          CLP
      JOIN PROCESSO_RADICAL         PRR ON PRR.MAIN = 0
                                           AND PRR.LENGTH_RADICAL > 3
                                           AND PRR.NUMERO_PROCESSO = CLP.Processo
      JOIN #CLIENT_PROCESSES_CLASS  CPC ON CPC.Processo = CLP.Processo
      JOIN (
             SELECT
               PRRS.LENGTH_RADICAL,
               PRRS.RADICAL,
               PRRS.NUMERO_PROCESSO,

               PRCS.ID_PROCESSO,
               PRCS.MARCA,

               dbo.GetJustClasse(PRCS.CLASSE_1) AS CLASSE_1,
               PRCS.CLASSE_2,
               PRCS.CLASSE_3,
               dbo.GetJustClasse(PRCS.CLASSE_INTERNACIONAL) AS CLASSE_INTERNACIONAL,

               PRCS.DATA_DEPOSITO,
               PRCS.NUMERO,
               PRCS.NOME_TITULAR,
               PRCS.ESPECIFICACAO
             FROM
               #PROCESS_TO_COLLIDE			      PRCS
               JOIN PROCESSO_RADICAL          PRRS ON PRRS.MAIN = 0
                                                      AND PRRS.LENGTH_RADICAL > 3
                                                      AND PRRS.NUMERO_PROCESSO = PRCS.NUMERO
           ) PRC ON PRC.LENGTH_RADICAL > PRR.LENGTH_RADICAL
                    AND
                    (
                       (
                         PRR.LENGTH_RADICAL = 4 AND PRC.LENGTH_RADICAL <= 7
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 5 AND PRC.LENGTH_RADICAL <= 9
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 6 AND PRC.LENGTH_RADICAL <= 11
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 7 AND PRC.LENGTH_RADICAL <= 13
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 8 AND PRC.LENGTH_RADICAL <= 15
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 9 AND PRC.LENGTH_RADICAL <= 17
                       )
                    )
                    AND PRC.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE '%' + PRR.RADICAL + '%'
      JOIN CLASSE_AFINIDADE CLF ON dbo.GetJustClasse(CLF.NUMERO_CLASSE_A) = dbo.GetJustClasse(CPC.class)
                                   AND dbo.GetJustClasse(CLF.NUMERO_CLASSE_B) = COALESCE(PRC.CLASSE_1, PRC.CLASSE_INTERNACIONAL)


    -- Radicais da RPI contidos no do cliente
    INSERT INTO #COLLIDED_PROCESS
    (
      ID_PROCESSO,
     [Marca(RPI)],
     [Marca(Cliente)],
     [Classe(Cliente)],
     [Data Depósito(Cliente)],
     [Processo(Cliente)],
     [Titular(Cliente)],
     [Especificação dos Produtos/Serviços(Cliente)],
     [Referência/Pasta],
     [Escritório Responsável],
     [Advogado Responsável],

      Classe,
     [Data Depósito],
     Processo,
     Titular,
     [Especificação dos Produtos/Serviços]
    )
	  SELECT
      DISTINCT
      PRC.ID_PROCESSO,
      PRC.MARCA										              	AS [Marca(RPI)],
      CLP.MARCA										              	AS [Marca(Cliente)],

      CLP.Classe                                  AS [Classe(Cliente)],
      CLP.Deposito                                AS [Data Depósito(Cliente)],
      CLP.Processo                                AS [Processo(Cliente)],
      CLP.Titular                                 AS [Titular(Cliente)],
      CLP.Especificacao                           AS [Especificação dos Produtos/Serviços(Cliente)],
      CLP.Pasta										              	AS [Referência/Pasta],
      CLP.Responsavel								          	  AS [Escritório Responsável],
      CLP.Advogado                                AS [Advogado Responsável],

      PRC.CLASSE_1										          	AS [Classe],
      PRC.DATA_DEPOSITO                           AS [Data Depósito],
      PRC.NUMERO									            		AS [Processo],
      PRC.NOME_TITULAR											      AS [Titular],
      PRC.ESPECIFICACAO								        		AS [Especificação dos Produtos/Serviços]
    FROM
      #PROCESS_TO_COLLIDE			      PRC
      JOIN PROCESSO_RADICAL         PRR ON PRR.MAIN = 1
                                           AND PRR.LENGTH_RADICAL > 3
                                           AND PRR.NUMERO_PROCESSO = PRC.NUMERO
      JOIN (
             SELECT
               PRRS.LENGTH_RADICAL,
               PRRS.RADICAL,
               PRRS.NUMERO_PROCESSO,

               CLPS.Marca,
               CLPS.Classe,
               CLPS.Deposito,
               CLPS.Processo,
               CLPS.Titular,
               CLPS.Especificacao,
               CLPS.Pasta,
               CLPS.Responsavel,
               CLPS.Advogado,
               CPC.class
             FROM
               CLIENT_PROCESSES		          CLPS
               JOIN PROCESSO_RADICAL        PRRS ON PRRS.MAIN = 1
                                                    AND PRRS.LENGTH_RADICAL > 3
                                                    AND PRRS.NUMERO_PROCESSO = CLPS.Processo
               JOIN #CLIENT_PROCESSES_CLASS  CPC ON CPC.Processo = CLPS.Processo
           ) CLP ON CLP.LENGTH_RADICAL > PRR.LENGTH_RADICAL
                    AND
                    (
                       (
                         PRR.LENGTH_RADICAL = 4 AND CLP.LENGTH_RADICAL <= 7
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 5 AND CLP.LENGTH_RADICAL <= 9
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 6 AND CLP.LENGTH_RADICAL <= 11
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 7 AND CLP.LENGTH_RADICAL <= 13
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 8 AND CLP.LENGTH_RADICAL <= 15
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 9 AND CLP.LENGTH_RADICAL <= 17
                       )
                    )
                    AND CLP.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE '%' + PRR.RADICAL + '%'
      JOIN CLASSE_AFINIDADE CLF ON dbo.GetJustClasse(CLF.NUMERO_CLASSE_A) = COALESCE(PRC.CLASSE_1, PRC.CLASSE_INTERNACIONAL)
                                   AND dbo.GetJustClasse(CLF.NUMERO_CLASSE_B) = dbo.GetJustClasse(CLP.class)

    INSERT INTO #COLLIDED_PROCESS
    (
      ID_PROCESSO,
     [Marca(RPI)],
     [Marca(Cliente)],
     [Classe(Cliente)],
     [Data Depósito(Cliente)],
     [Processo(Cliente)],
     [Titular(Cliente)],
     [Especificação dos Produtos/Serviços(Cliente)],
     [Referência/Pasta],
     [Escritório Responsável],
     [Advogado Responsável],

      Classe,
     [Data Depósito],
     Processo,
     Titular,
     [Especificação dos Produtos/Serviços]
    )
	  SELECT
      DISTINCT
      PRC.ID_PROCESSO,
      PRC.MARCA										              	AS [Marca(RPI)],
      CLP.MARCA										              	AS [Marca(Cliente)],

      CLP.Classe                                  AS [Classe(Cliente)],
      CLP.Deposito                                AS [Data Depósito(Cliente)],
      CLP.Processo                                AS [Processo(Cliente)],
      CLP.Titular                                 AS [Titular(Cliente)],
      CLP.Especificacao                           AS [Especificação dos Produtos/Serviços(Cliente)],
      CLP.Pasta										              	AS [Referência/Pasta],
      CLP.Responsavel								          	  AS [Escritório Responsável],
      CLP.Advogado                                AS [Advogado Responsável],

      PRC.CLASSE_1										          	AS [Classe],
      PRC.DATA_DEPOSITO                           AS [Data Depósito],
      PRC.NUMERO									            		AS [Processo],
      PRC.NOME_TITULAR											      AS [Titular],
      PRC.ESPECIFICACAO								        		AS [Especificação dos Produtos/Serviços]
    FROM
      #PROCESS_TO_COLLIDE			      PRC
      JOIN PROCESSO_RADICAL         PRR ON PRR.MAIN = 0
                                           AND PRR.LENGTH_RADICAL > 3
                                           AND PRR.NUMERO_PROCESSO = PRC.NUMERO
      JOIN (
             SELECT
               PRRS.LENGTH_RADICAL,
               PRRS.RADICAL,
               PRRS.NUMERO_PROCESSO,

               CLPS.Marca,
               CLPS.Classe,
               CLPS.Deposito,
               CLPS.Processo,
               CLPS.Titular,
               CLPS.Especificacao,
               CLPS.Pasta,
               CLPS.Responsavel,
               CLPS.Advogado,
               CPC.class
             FROM
               CLIENT_PROCESSES		          CLPS
               JOIN PROCESSO_RADICAL        PRRS ON PRRS.MAIN = 0
                                                    AND PRRS.LENGTH_RADICAL > 3
                                                    AND PRRS.NUMERO_PROCESSO = CLPS.Processo
               JOIN #CLIENT_PROCESSES_CLASS  CPC ON CPC.Processo = CLPS.Processo
           ) CLP ON CLP.LENGTH_RADICAL = PRR.LENGTH_RADICAL
                    AND
                    (
                       (
                         PRR.LENGTH_RADICAL = 4 AND CLP.LENGTH_RADICAL <= 7
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 5 AND CLP.LENGTH_RADICAL <= 9
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 6 AND CLP.LENGTH_RADICAL <= 11
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 7 AND CLP.LENGTH_RADICAL <= 13
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 8 AND CLP.LENGTH_RADICAL <= 15
                       )
                       OR
                       (
                         PRR.LENGTH_RADICAL = 9 AND CLP.LENGTH_RADICAL <= 17
                       )
                    )
                    AND CLP.RADICAL COLLATE SQL_Latin1_General_CP850_BIN2 LIKE '%' + PRR.RADICAL + '%'
      JOIN CLASSE_AFINIDADE CLF ON dbo.GetJustClasse(CLF.NUMERO_CLASSE_A) = COALESCE(PRC.CLASSE_1, PRC.CLASSE_INTERNACIONAL)
                                   AND dbo.GetJustClasse(CLF.NUMERO_CLASSE_B) = dbo.GetJustClasse(CLP.class)

    SELECT
      [Marca(RPI)],
      [Marca(Cliente)],

      [Classe(Cliente)],
      [Data Depósito(Cliente)],
      [Processo(Cliente)],
      [Titular(Cliente)],
      [Especificação dos Produtos/Serviços(Cliente)],
      [Referência/Pasta],
      [Escritório Responsável],
      [Advogado Responsável],

      [Classe],
      [Data Depósito],
      [Processo],
      [Titular],
      [Especificação dos Produtos/Serviços]
    FROM
      #COLLIDED_PROCESS
    ORDER BY
      [Marca(Cliente)],
      [Marca(RPI)]



    DROP TABLE CLIENT_PROCESSES
    DROP TABLE #PROCESS_TO_COLLIDE
		DROP TABLE #COLLIDED_PROCESS
    DROP TABLE #CLIENT_PROCESSES_CLASS
    DELETE FROM PROCESSO_RADICAL

  END TRY
  BEGIN CATCH

    DROP TABLE CLIENT_PROCESSES
    DROP TABLE #PROCESS_TO_COLLIDE
		DROP TABLE #COLLIDED_PROCESS
    DROP TABLE #CLIENT_PROCESSES_CLASS
    DELETE FROM PROCESSO_RADICAL

  END CATCH

END
