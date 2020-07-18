create table PALAVRA_USO_COMUM_EMPRESARIAL
(
    ID            int identity
        constraint PK_PALAVRA_USO_COMUM_EMPRESARIAL
            primary key,
    PALAVRA       nvarchar(2000) not null
)
go