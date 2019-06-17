INSERT INTO dbo.DESPACHO
        ( CODIGO ,
          DESCRICAO ,
          DESCRICAO_COMPLETA 
          --TIPO ,
          --SITUACAO ,
          --PRAZO_1_EM_DIAS ,
          --PRAZO_2_EM_DIAS
        )
VALUES  ( '599' , -- CODIGO - varchar(100)
          'teste' , -- DESCRICAO - varchar(2000)
          'testando'  -- DESCRICAO_COMPLETA - varchar(max)
          --'' , -- TIPO - char(1)
          --0 , -- SITUACAO - int
          --0 , -- PRAZO_1_EM_DIAS - int
          --0  -- PRAZO_2_EM_DIAS - int
        )