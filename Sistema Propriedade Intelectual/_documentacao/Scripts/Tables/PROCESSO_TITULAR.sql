create table PROCESSO_TITULAR
(
    ID                  	int identity primary key,
    ID_PROCESSO         	int not null constraint FK_PROCESSO_TITULAR_PROCESSO references PROCESSO,
	NOME_TITULAR            varchar(500),
    CPF_CNPJ_INPI_TITULAR   varchar(50),
	PAIS_TITULAR            char(2),
    UF_TITULAR              char(2)
)
go