ALTER FUNCTION [dbo].[DOMINIO_ORTOGRAFAR](@marca VARCHAR(2000))
RETURNS VARCHAR(2000)
BEGIN
   DECLARE
      @marcaOrtografada VARCHAR(2000)

   SET @marcaOrtografada = upper(@marca);
   SET @marcaOrtografada = replace(@marcaOrtografada, ' ', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Å', 'A');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Á', 'A');
   SET @marcaOrtografada = replace(@marcaOrtografada,'À', 'A');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Â', 'A');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Ä', 'A');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Ã', 'A');
   SET @marcaOrtografada = replace(@marcaOrtografada,'É', 'E');
   SET @marcaOrtografada = replace(@marcaOrtografada,'È', 'E');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Ê', 'E');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Ë', 'E');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Í', 'I');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Ì', 'I');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Î', 'I');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Ï', 'I');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Ó', 'O');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Ò', 'O');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Ô', 'O');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Õ', 'O');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Ö', 'O');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Ú', 'U');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Ù', 'U');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Ü', 'U');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Û', 'U');

   SET @marcaOrtografada = replace(@marcaOrtografada,'$', 'S');
   SET @marcaOrtografada = replace(@marcaOrtografada,'+', 'MAIS');
   SET @marcaOrtografada = replace(@marcaOrtografada,'%', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'&', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'@', 'A');
   SET @marcaOrtografada = replace(@marcaOrtografada,'"', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'''', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'!', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'?', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'¿', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'¨', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'*', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'(', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,')', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'-', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'_', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'=', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'|', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'\\', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'/', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'´', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'`', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'^', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'~', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,',', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'.', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,';', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,':', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'ª', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'º', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'°', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'µ', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'¢', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'£', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'¦', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'½', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'¼', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'¾', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'÷', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'{', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'}', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'[', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,']', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'§', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'<', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'>', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'#', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Ç', 'C');
   SET @marcaOrtografada = replace(@marcaOrtografada,'¨', '');
  
   RETURN ltrim(@marcaOrtografada)
END
