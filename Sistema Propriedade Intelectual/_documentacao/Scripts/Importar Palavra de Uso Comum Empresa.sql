DECLARE
  @sqlExcel NVARCHAR(MAX),
  @fileOfClient VARCHAR(2000)

  SET @fileOfClient = 'C:\Users\bruno.machado\Google Drive\3D3\Sistema Marcas e Patentes\Colidência empresa\TermoUsoComumNomeEmpresarial_05072020.xlsx'

SET @sqlExcel = '
INSERT INTO PALAVRA_USO_COMUM_EMPRESARIAL
(
  PALAVRA
)
SELECT
  TERMO
FROM
	OPENROWSET(''Microsoft.ACE.OLEDB.12.0'', ''Excel 12.0 XML;Database='+ @fileOfClient +';HDR=YES'', ''SELECT * FROM   [EMPRESA$]'') USO
WHERE
  USO.TERMO != '''''

--print @sqlExcel
EXECUTE(@sqlExcel)


--delete from PALAVRA_USO_COMUM


-- TERMO(RPI)
-- CLASSE(RPI)
-- TERMO