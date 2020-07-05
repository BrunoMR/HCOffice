-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 23/01/2020
-- Description:	Esta função irá retornar somente uma linha com classe e especificações concatenadas
-- =============================================
CREATE FUNCTION GetClassAndSpecficationOfProcess
(
	@idProcesso int
)
RETURNS @output TABLE
    (
    NUMERO_CLASSE char(5) not null,
    ESPECIFICAO   varchar(max)
    )
AS
BEGIN

    insert into @output
	select
	    top 1
	    NUMERO_CLASSE,
	    ESPECIFICAO
	from
	     PROCESSO_CLASSE
	where
	    ID_PROCESSO = @idProcesso
	    and TIPO in (
	                    select
	                        TIPO
	                    from
	                        TIPO_SITUACAO_CLASSE
	                    where
	                        DESCRICAO like 'pendente%'
	                        or DESCRICAO like 'Deferido%'
                    )
	RETURN

END
go



