CREATE TABLE [dbo].[CLIENT_COMPANY](
	[Processo] [varchar](2000) NULL,
	[Marca(Cliente)] [varchar](255) NULL,
	[MarcaModificada] [varchar](255) NULL,
	[MarcaOrtografada] [varchar](2000) NULL,
	[Classe] [varchar](2000) NULL,
	[Data Depósito] [varchar](30) NULL,
	[Titular] [varchar](255) NULL,
	[Especificacao] [varchar](8000) NULL,
	[Referência/Pasta] [varchar](255) NULL,
	[Escritório Responsável] [varchar](255) NULL,
	[Advogado Responsável] [varchar](255) NULL
) ON [PRIMARY]

GO