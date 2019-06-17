DECLARE
  @sqlExcel NVARCHAR(MAX),
  @fileOfClient VARCHAR(2000)

  SET @fileOfClient = 'C:\Dominios\DominiosAgosto2018.xlsx'

SET @sqlExcel = '
INSERT INTO PALAVRA_USO_COMUM
(
  NUMERO_CLASSE,
  PALAVRA
)
SELECT
  [CLASSE(RPI)],
  [TERMO(RPI)]
FROM
	OPENROWSET(''Microsoft.ACE.OLEDB.12.0'', ''Excel 12.0 XML;Database='+ @fileOfClient +';HDR=YES'', ''SELECT * FROM   [USO$]'') USO
WHERE
  USO.[TERMO(RPI)] != '''''

--print @sqlExcel
EXECUTE(@sqlExcel)


--delete from PALAVRA_USO_COMUM


-- TERMO(RPI)
-- CLASSE(RPI)
-- TERMO