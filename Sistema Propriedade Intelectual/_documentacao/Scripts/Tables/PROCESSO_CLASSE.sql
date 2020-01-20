create table PROCESSO_CLASSE
(
    ID                  int identity primary key,
    ID_PROCESSO         int not null constraint FK_PROCESSO_CLASSE_PROCESSO references PROCESSO,
    NUMERO_CLASSE       char(5) not null constraint FK_PROCESSO_CLASSE_CLASSE references CLASSE
)
go