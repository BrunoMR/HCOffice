CREATE procedure [dbo].[radical] 
  @CD_PROCESSO int,  
  @DS_MORTO VARCHAR(500), 
  @DS_TRADORTO VARCHAR(500), 
  @TAMANHO_RADICAL INT, 
  @TAMANHO_PREFIXO INT, 
  @TAMANHO_SUFIXO INT, 
  @CATEGORIA INT = 0 
with encryption as
begin

   exec canrun

   DECLARE @I INT
   DECLARE @RAD VARCHAR(750)
   DECLARE @C INT
   DECLARE @X INT

   DECLARE @DADOS VARCHAR(1000)

   SET @DADOS = 'T' + CONVERT(VARCHAR, LEN(@DS_MORTO)) + '        R' + CONVERT(VARCHAR, @TAMANHO_RADICAL) + '        P' + CONVERT(VARCHAR, @TAMANHO_PREFIXO) + '        S' + CONVERT(VARCHAR, @TAMANHO_SUFIXO)

   -- MARCAs
   IF (@DS_MORTO IS NOT NULL)
   BEGIN

       -- PREFIXO
       SET @RAD = LTRIM(RTRIM(SUBSTRING(@DS_MORTO, 1, @TAMANHO_PREFIXO)))
           
       IF NOT EXISTS
       (
           SELECT *
           FROM TB_RADICAL 
           WHERE CD_PROCESSO = @CD_PROCESSO
           AND DS_RADICAL = @RAD
           AND LG_PREFIXO = 1
           AND LG_SUFIXO = 0
           AND TAMANHO = @CATEGORIA
       )
       BEGIN    
           IF (@RAD <> '')
           BEGIN
               INSERT INTO TB_RADICAL(CD_PROCESSO, DS_RADICAL, LG_PREFIXO, ORIGEM, TAMANHO)
               VALUES (@CD_PROCESSO, @RAD, 1, 'PREFIXO        ' + @DADOS, @CATEGORIA)
           END
       END
   

       --SUFIXO
       SET @RAD = LTRIM(RTRIM(SUBSTRING(@DS_MORTO, LEN(@DS_MORTO) - (@TAMANHO_SUFIXO - 1), @TAMANHO_SUFIXO)))

       IF NOT EXISTS
       (
           SELECT *
           FROM TB_RADICAL 
           WHERE CD_PROCESSO = @CD_PROCESSO
           AND DS_RADICAL = @RAD
           AND LG_PREFIXO = 0 
           AND LG_SUFIXO = 1
           AND TAMANHO = @CATEGORIA
       )
       BEGIN    
           IF (@RAD <> '')
           BEGIN
               INSERT INTO TB_RADICAL(CD_PROCESSO, DS_RADICAL, LG_SUFIXO, ORIGEM, TAMANHO)
               VALUES (@CD_PROCESSO, @RAD, 1, 'SUFIXO        ' + @DADOS, @CATEGORIA)
           END
       END


       --RADICAIS
       SET @I = 1 -- INICIA NO 2º RADICAL
       SET @C = LEN(@DS_MORTO)
       SET @RAD = NULL

                       
       WHILE (@I <= (@C - @TAMANHO_RADICAL+1)) 
       BEGIN    
                   
           SET @RAD = RTRIM(LTRIM(SUBSTRING(@DS_MORTO, @I, @TAMANHO_RADICAL)))

           PRINT 'RADICAL'
           PRINT @RAD
           PRINT ''

           IF NOT EXISTS
           (
               SELECT *
               FROM TB_RADICAL 
               WHERE CD_PROCESSO = @CD_PROCESSO
               AND DS_RADICAL = @RAD
               AND LG_PREFIXO = 0 
               AND LG_SUFIXO = 0
               AND TAMANHO = @CATEGORIA
           )
           BEGIN    
               IF (@RAD <> '')
               BEGIN
                   INSERT INTO TB_RADICAL(CD_PROCESSO, DS_RADICAL, VL_ORDEM, ORIGEM, TAMANHO)
                   VALUES (@CD_PROCESSO, @RAD, @I, 'RADICAL        ' + @DADOS, @CATEGORIA)
               END
           END

           SET @I = @I + 1
       END
   END
       

   PRINT 'XYZ'
   --INSERT INTO TB_RADICAL(CD_PROCESSO, DS_RADICAL, VL_ORDEM)
   --VALUES (@CD_PROCESSO, 'TAMANHO MARCA ORTO: ' + CONVERT(VARCHAR, LEN(@DS_MORTO)), @I)            

   --INSERT INTO TB_RADICAL(CD_PROCESSO, DS_RADICAL, VL_ORDEM)
   --VALUES (@CD_PROCESSO, 'TAMANHO RADICAL: ' + CONVERT(VARCHAR, @TAMANHO_RADICAL), @I)            

   --INSERT INTO TB_RADICAL(CD_PROCESSO, DS_RADICAL, VL_ORDEM)
   --VALUES (@CD_PROCESSO, 'TAMANHO SUFIXO: ' + CONVERT(VARCHAR, @TAMANHO_SUFIXO), @I)            

   --INSERT INTO TB_RADICAL(CD_PROCESSO, DS_RADICAL, VL_ORDEM)
   --VALUES (@CD_PROCESSO, 'TAMANHO PREFIXO: ' + CONVERT(VARCHAR, @TAMANHO_PREFIXO), @I)            


   -- TRADUCAO
   IF (@DS_TRADORTO IS NOT NULL)
   BEGIN

       -- PREFIXO
       SET @RAD = RTRIM(LTRIM(SUBSTRING(@DS_TRADORTO, 1, @TAMANHO_PREFIXO)))

       IF NOT EXISTS
       (
           SELECT *
           FROM TB_RADICAL 
           WHERE CD_PROCESSO = @CD_PROCESSO
           AND DS_RADICAL = @RAD
           AND LG_PREFIXO = 1
           AND LG_SUFIXO = 0
           AND TAMANHO = @CATEGORIA
       )
       BEGIN    
           IF (@RAD <> '')
           BEGIN
               INSERT INTO TB_RADICAL(CD_PROCESSO, DS_RADICAL, LG_PREFIXO, ORIGEM, TAMANHO)
               VALUES (@CD_PROCESSO, @RAD, 1, 'PREFIXO TRAD        ' + @DADOS, @CATEGORIA)
           END
       END
   

       --SUFIXO
       SET @RAD = RTRIM(LTRIM(SUBSTRING(@DS_MORTO, LEN(@DS_MORTO) - (@TAMANHO_SUFIXO - 1), @TAMANHO_SUFIXO)))

       IF NOT EXISTS
       (
           SELECT *
           FROM TB_RADICAL 
           WHERE CD_PROCESSO = @CD_PROCESSO
           AND DS_RADICAL = @RAD
           AND LG_PREFIXO = 0 
           AND LG_SUFIXO = 1
           AND TAMANHO = @CATEGORIA
       )
       BEGIN    
           IF (@RAD <> '')
           BEGIN
               INSERT INTO TB_RADICAL(CD_PROCESSO, DS_RADICAL, LG_SUFIXO, ORIGEM, TAMANHO)
               VALUES (@CD_PROCESSO, @RAD, 1, 'SUFIXO TRAD        ' + @DADOS, @CATEGORIA)
           END
       END


       --RADICAIS
       SET @I = 1 -- INICIA NO 2º RADICAL
       SET @C = LEN(@DS_TRADORTO)
       SET @RAD = NULL

           
       WHILE (@I <= (@C - @TAMANHO_RADICAL + 1))
       BEGIN    
                   
           SET @RAD = RTRIM(LTRIM(SUBSTRING(@DS_TRADORTO, @I, @TAMANHO_RADICAL)))

           IF NOT EXISTS
           (
               SELECT *
               FROM TB_RADICAL 
               WHERE CD_PROCESSO = @CD_PROCESSO
               AND DS_RADICAL = @RAD
               AND LG_PREFIXO = 0 
               AND LG_SUFIXO = 0
               AND TAMANHO = @CATEGORIA
           )
           BEGIN    
               IF (@RAD <> '')
               BEGIN
                   INSERT INTO TB_RADICAL(CD_PROCESSO, DS_RADICAL, VL_ORDEM, ORIGEM, TAMANHO)
                   VALUES (@CD_PROCESSO, @RAD, @I, 'RADICAL TRAD        ' + @DADOS, @CATEGORIA)
               END
           END

           SET @I = @I + 1
       END
   END
END
GO