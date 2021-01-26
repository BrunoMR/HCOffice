create type PROCESSOTITULARTYPE as table
(
    NUMERO_PROCESSO         varchar(20) not null,
    NOME_TITULAR            varchar(500),
    CPF_CNPJ_INPI_TITULAR   varchar(50),
	PAIS_TITULAR            char(2),
    UF_TITULAR              char(2)
)
go