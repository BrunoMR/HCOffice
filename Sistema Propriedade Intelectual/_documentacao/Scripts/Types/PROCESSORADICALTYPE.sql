create type PROCESSORADICALTYPE as table
(
    Id                       int IDENTITY(1,1) PRIMARY KEY NONCLUSTERED,
    NUMERO_PROCESSO          varchar(20)   not null,
    RADICAL                  nvarchar(255) not null,
    LENGTH_RADICAL           int           not null,
    ID_TIPO_PROCESSO_RADICAL int,
    MAIN                     bit
)
WITH
    (MEMORY_OPTIMIZED = ON)
go