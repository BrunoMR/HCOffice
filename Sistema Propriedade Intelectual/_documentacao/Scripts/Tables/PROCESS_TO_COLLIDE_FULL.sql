CREATE TABLE PROCESS_TO_COLLIDE_FULL
(
    ID                   INT IDENTITY(1, 1) not null,
	Main				 bit,
    LengthRadical        int,
    Radical              nvarchar(255),
    IdProcesso           int NOT NULL INDEX [PROCESS_ID] NONCLUSTERED,
    Marca                varchar(1000),
    MarcaOrtografada     varchar(1000),
    Codigo               varchar(100),
    FormatClasses        varchar(30),
    Classe1              varchar(5),
    ClasseInternacional  varchar(5),
    DataDeposito         varchar(30),
    DataConcessao        varchar(30),
    Numero               varchar(20),
    NomeTitular          varchar(500),
    NomeProcurador       varchar(500),
	MarcaSemVogais		 varchar(100),
    LenMarcaSemVogais    int
    primary key nonclustered (ID)
)
WITH (MEMORY_OPTIMIZED = ON)