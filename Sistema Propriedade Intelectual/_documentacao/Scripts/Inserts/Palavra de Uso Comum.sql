DECLARE
  @sqlExcel NVARCHAR(MAX),
  @fileOfClient VARCHAR(2000)

  SET @fileOfClient = 'C:\Empresas\Empresa23052019'

SET @sqlExcel = '
INSERT INTO PALAVRA_USO_COMUM
(
	Palavra,
	Numero_Classe
)
SELECT
	[TERMO(RPI)],
	[CLASSE(RPI)]
FROM
	OPENROWSET(''Microsoft.ACE.OLEDB.12.0'', ''Excel 12.0 XML;Database='+ @fileOfClient +';HDR=YES'', ''SELECT * FROM   [USO$]'') USO'

--print @sqlExcel
EXECUTE(@sqlExcel)

--delete from PALAVRA_USO_COMUM