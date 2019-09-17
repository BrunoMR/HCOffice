create table CLIENT_PROCESSES
(
    ID               int identity
        primary key nonclustered,
    ProprioTerceiro  nvarchar(255),
    Processo         varchar(100),
    Marca            varchar(255),
    MarcaOrtografada varchar(255),
    MarcaSemVogais   varchar(100),
    Classe           varchar(100),
    Deposito         varchar(30),
    Concessao        varchar(30),
    Especificacao    varchar(1000),
    Titular          varchar(255),
    Pasta            varchar(1000),
    Responsavel      varchar(255),
    Advogado         varchar(255)
)
go