CREATE TABLE PROCESS_TO_COLLIDE_FULL
(
    ID                   INT IDENTITY(1, 1) not null,
	Main				 bit,
    LENGTH_RADICAL       int,
    RADICAL              nvarchar(255),
    ID_PROCESSO          int,
    MARCA                varchar(1000),
    CODIGO               varchar(100),
    FORMAT_CLASSES       varchar(30),
    CLASSE_1             varchar(5),
    CLASSE_INTERNACIONAL varchar(5),
    DATA_DEPOSITO        varchar(30),
    DATA_CONCESSAO       varchar(30),
    NUMERO               varchar(20),
    NOME_TITULAR         varchar(500),
    NOME_PROCURADOR      varchar(500),
    ESPECIFICACAO        varchar(2000),
	MarcaSemVogais		 varchar(100),
    LenMarcaSemVogais    int
    primary key nonclustered (ID)
)
WITH (MEMORY_OPTIMIZED = ON)