CREATE TABLE CLIENT_TO_COLLIDE
(
    ID					INT IDENTITY(1, 1) not null,
	Main				bit,
    LengthRadical		int,
    Radical				nvarchar(255),
    NumeroProcesso		varchar(20),
    Marca				varchar(255),
    Classe				varchar(100),
    Deposito			varchar(30),
    Concessao			varchar(30),
    Processo			varchar(100),
    Titular				varchar(255),
    Especificacao		varchar(2000),
    Pasta				varchar(255),
    Responsavel			varchar(255),
    Advogado			varchar(255),
    Class				nvarchar(5),
	MarcaSemVogais		varchar(100),
    LenMarcaSemVogais	int
    primary key nonclustered (ID)
)
WITH (MEMORY_OPTIMIZED = ON)