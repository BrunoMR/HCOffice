CREATE TABLE PALAVRA_USO_COMUM
(
    Id              int identity(1, 1) primary key nonclustered (Id),
    Numero_Classe   char(5) not null,
    Palavra         varchar(255) not null
)
WITH (MEMORY_OPTIMIZED = ON)
go