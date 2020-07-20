create table CLIENT_TO_COLLIDE
(
    ID                    int identity
        primary key nonclustered,
    Main                  bit,
    LengthRadical         int,
    Radical               nvarchar(255),
    NumeroProcesso        varchar(20),
    Marca                 varchar(2000),
    Classe                varchar(2000),
    Deposito              varchar(30),
    Concessao             varchar(30),
    Processo              varchar(2000),
    Titular               varchar(2000),
    Especificacao         varchar(8000),
    Pasta                 varchar(2000),
    Responsavel           varchar(2000),
    Advogado              varchar(2000),
    Class                 nvarchar(5),
    MarcaSemVogais        varchar(100),
    LenMarcaSemVogais     int,
    MarcaOriginal         varchar(2000),
    IdTipoProcessoRadical int
)
go