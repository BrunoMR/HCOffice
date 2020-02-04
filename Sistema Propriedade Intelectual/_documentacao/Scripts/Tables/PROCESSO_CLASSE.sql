create table PROCESSO_CLASSE
(
    ID                  int identity primary key,
    ID_PROCESSO         int not null constraint FK_PROCESSO_CLASSE_PROCESSO references PROCESSO,
    NUMERO_CLASSE       char(5) not null constraint FK_PROCESSO_CLASSE_CLASSE references CLASSE,
    TIPO                int not null constraint FK_PROCESSO_CLASSE_TIPO references TIPO_SITUACAO_CLASSE,
    ESPECIFICAO         varchar(max)
)
go