CREATE NONCLUSTERED INDEX [PROCESSO_CLASSE_NUMERO_TIPO]
ON [dbo].[PROCESSO_CLASSE] ([ID_PROCESSO])
INCLUDE ([NUMERO_CLASSE],[TIPO])
GO
