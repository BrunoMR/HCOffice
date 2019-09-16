ALTER FUNCTION [dbo].[REMOVER_VOGAIS](@marca VARCHAR(255))
RETURNS VARCHAR(100)
BEGIN
   DECLARE
      @semVogais VARCHAR(100),
      @semVogaisValidar VARCHAR(2000),
      @index INT

   SET @semVogais = upper(@marca);
   SET @semVogais = replace(@semVogais,'Å', 'A');
   SET @semVogais = replace(@semVogais,'Á', 'A');
   SET @semVogais = replace(@semVogais,'À', 'A');
   SET @semVogais = replace(@semVogais,'Â', 'A');
   SET @semVogais = replace(@semVogais,'Ä', 'A');
   SET @semVogais = replace(@semVogais,'Ã', 'A');
   SET @semVogais = replace(@semVogais,'É', 'E');
   SET @semVogais = replace(@semVogais,'È', 'E');
   SET @semVogais = replace(@semVogais,'Ê', 'E');
   SET @semVogais = replace(@semVogais,'Ë', 'E');
   SET @semVogais = replace(@semVogais,'Í', 'I');
   SET @semVogais = replace(@semVogais,'Ì', 'I');
   SET @semVogais = replace(@semVogais,'Î', 'I');
   SET @semVogais = replace(@semVogais,'Ï', 'I');
   SET @semVogais = replace(@semVogais,'Ó', 'O');
   SET @semVogais = replace(@semVogais,'Ò', 'O');
   SET @semVogais = replace(@semVogais,'Ô', 'O');
   SET @semVogais = replace(@semVogais,'Õ', 'O');
   SET @semVogais = replace(@semVogais,'Ö', 'O');
   SET @semVogais = replace(@semVogais,'Ú', 'U');
   SET @semVogais = replace(@semVogais,'Ù', 'U');
   SET @semVogais = replace(@semVogais,'Ü', 'U');
   SET @semVogais = replace(@semVogais,'Û', 'U');
   SET @semVogais = replace(@semVogais,'Ý', 'Y');
   SET @semVogais = replace(@semVogais,'Ñ', 'N');
   SET @semVogais = replace(@semVogais,'PH', 'F');
   SET @semVogais = replace(@semVogais,'Y', 'I');

   SET @semVogais = replace(@semVogais,'$', 'S');
   SET @semVogais = replace(@semVogais,'%', '');
   SET @semVogais = replace(@semVogais,'&', '');
   SET @semVogais = replace(@semVogais,'@', 'A');
   SET @semVogais = replace(@semVogais,'"', '');
   SET @semVogais = replace(@semVogais,'''', '');
   SET @semVogais = replace(@semVogais,'!', '');
   SET @semVogais = replace(@semVogais,'?', '');
   SET @semVogais = replace(@semVogais,'¿', '');
   SET @semVogais = replace(@semVogais,'¨', '');
   SET @semVogais = replace(@semVogais,'*', '');
   SET @semVogais = replace(@semVogais,'(', '');
   SET @semVogais = replace(@semVogais,')', '');
   SET @semVogais = replace(@semVogais,'+', ' ');
   SET @semVogais = replace(@semVogais,'-', ' ');
   SET @semVogais = replace(@semVogais,'_', ' ');
   SET @semVogais = replace(@semVogais,'=', ' ');
   SET @semVogais = replace(@semVogais,'|', ' ');
   SET @semVogais = replace(@semVogais,'\\', ' ');
   SET @semVogais = replace(@semVogais,'/', ' ');
   SET @semVogais = replace(@semVogais,'´', '');
   SET @semVogais = replace(@semVogais,'`', '');
   SET @semVogais = replace(@semVogais,'^', '');
   SET @semVogais = replace(@semVogais,'~', '');
   SET @semVogais = replace(@semVogais,',', ' ');
   SET @semVogais = replace(@semVogais,'.', ' ');
   SET @semVogais = replace(@semVogais,';', ' ');
   SET @semVogais = replace(@semVogais,':', ' ');
   SET @semVogais = replace(@semVogais,'ª', '');
   SET @semVogais = replace(@semVogais,'º', '');
   SET @semVogais = replace(@semVogais,'°', '');
   SET @semVogais = replace(@semVogais,'µ', '');
   SET @semVogais = replace(@semVogais,'¢', '');
   SET @semVogais = replace(@semVogais,'£', '');
   SET @semVogais = replace(@semVogais,'¦', '');
   SET @semVogais = replace(@semVogais,'½', '');
   SET @semVogais = replace(@semVogais,'¼', '');
   SET @semVogais = replace(@semVogais,'¾', '');
   SET @semVogais = replace(@semVogais,'÷', '');
   SET @semVogais = replace(@semVogais,'{', '');
   SET @semVogais = replace(@semVogais,'}', '');
   SET @semVogais = replace(@semVogais,'[', '');
   SET @semVogais = replace(@semVogais,']', '');
   SET @semVogais = replace(@semVogais,'§', '');
   SET @semVogais = replace(@semVogais,'<', '');
   SET @semVogais = replace(@semVogais,'>', '');
   SET @semVogais = replace(@semVogais,'#', '');

   SET @semVogais = dbo.REGEX_IN_THE_WHOLE_WORD('[AEIOU]', @semVogais, 0, 1, '', 1)

   /*IF (LEN(@semVogaisValidar) >= 3)
     SET @semVogais = @semVogaisValidar;*/

   RETURN ltrim(@semVogais)
END
go

