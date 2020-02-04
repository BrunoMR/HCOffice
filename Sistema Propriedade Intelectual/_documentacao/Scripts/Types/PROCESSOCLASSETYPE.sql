create type PROCESSOCLASSETYPE as table
(
    NUMERO_PROCESSO varchar(20) not null,
    NUMERO_CLASSE   char(5)     not null,
    TIPO_DESCRICAO  varchar(40) not null,
    ESPECIFICACAO     varchar(max)
)
go