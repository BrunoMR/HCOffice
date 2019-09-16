CREATE TABLE PROCESSO_RADICAL
(
    ID                       int identity primary key nonclustered,
    NUMERO_PROCESSO          varchar(20)   not null,
    RADICAL                  nvarchar(255) not null,
    LENGTH_RADICAL           int           not null,
    ID_TIPO_PROCESSO_RADICAL int,
    MAIN                     bit
)
go

create index IDX_NUMERO_RADICAL_PROCESSO_RADICAL on PROCESSO_RADICAL (NUMERO_PROCESSO, RADICAL, LENGTH_RADICAL)
go