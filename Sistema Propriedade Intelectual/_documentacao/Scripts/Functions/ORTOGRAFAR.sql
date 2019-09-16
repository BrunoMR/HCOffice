CREATE FUNCTION [dbo].[ORTOGRAFAR](@marca VARCHAR(255))
RETURNS VARCHAR(255)
BEGIN
   DECLARE
      @marcaOrtografada VARCHAR(255),
      @index INT

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
   --SET @marcaOrtografada = replace(@marcaOrtografada,'ß', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'¢', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'£', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'¦', '');
   --SET @marcaOrtografada = replace(@marcaOrtografada,'¹', '');
   --SET @marcaOrtografada = replace(@marcaOrtografada,'²', '');
   --SET @marcaOrtografada = replace(@marcaOrtografada,'³', '');
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

   SET @marcaOrtografada = replace(@marcaOrtografada,'CHA', 'XA');
   SET @marcaOrtografada = replace(@marcaOrtografada,'CHE', 'XE');
   SET @marcaOrtografada = replace(@marcaOrtografada,'CHI', 'XI');
   SET @marcaOrtografada = replace(@marcaOrtografada,'CHO', 'XO');
   SET @marcaOrtografada = replace(@marcaOrtografada,'CHU', 'XU');
   SET @marcaOrtografada = replace(@marcaOrtografada,'CH', 'K');
   SET @marcaOrtografada = replace(@marcaOrtografada,'SH', 'X');
   SET @marcaOrtografada = replace(@marcaOrtografada,'$', 'S');

   SET @marcaOrtografada = replace(@marcaOrtografada,'CE', 'SE');
   SET @marcaOrtografada = replace(@marcaOrtografada,'CI', 'SI');
   SET @marcaOrtografada = replace(@marcaOrtografada,'GE', 'JE');
   SET @marcaOrtografada = replace(@marcaOrtografada,'GI', 'JI');

   SET @marcaOrtografada = replace(@marcaOrtografada,'C', 'K');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Ç', 'S');
   SET @marcaOrtografada = replace(@marcaOrtografada,'C', 'K');;
   SET @marcaOrtografada = replace(@marcaOrtografada,'T', 'D');
   SET @marcaOrtografada = replace(@marcaOrtografada,'PH', 'F');
   SET @marcaOrtografada = replace(@marcaOrtografada,'XH', 'X');
   SET @marcaOrtografada = replace(@marcaOrtografada,'N', 'M');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Ñ', 'M');
   SET @marcaOrtografada = replace(@marcaOrtografada,'P', 'B');
   SET @marcaOrtografada = replace(@marcaOrtografada,'QU', 'K');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Q', 'K');
   SET @marcaOrtografada = replace(@marcaOrtografada,'W', 'V');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Y', 'I');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Z', 'S');

  SET @marcaOrtografada = replace(@marcaOrtografada, 'KLA', 'KRA');
  SET @marcaOrtografada = replace(@marcaOrtografada, 'KLE', 'KRE');
  SET @marcaOrtografada = replace(@marcaOrtografada, 'KLI', 'KRI');
  SET @marcaOrtografada = replace(@marcaOrtografada, 'KLO', 'KRO');
  SET @marcaOrtografada = replace(@marcaOrtografada, 'KLU', 'KRU');

   SET @marcaOrtografada = replace(@marcaOrtografada,'1', 'UM');
   SET @marcaOrtografada = replace(@marcaOrtografada,'2', 'DOIS');
   SET @marcaOrtografada = replace(@marcaOrtografada,'3', 'DRES');
   SET @marcaOrtografada = replace(@marcaOrtografada,'4', 'KADRO');
   SET @marcaOrtografada = replace(@marcaOrtografada,'5', 'SIMKO');
   SET @marcaOrtografada = replace(@marcaOrtografada,'6', 'SEIS');
   SET @marcaOrtografada = replace(@marcaOrtografada,'7', 'SEDE');
   SET @marcaOrtografada = replace(@marcaOrtografada,'8', 'OIDO');
   SET @marcaOrtografada = replace(@marcaOrtografada,'9', 'MOVE');
   SET @marcaOrtografada = replace(@marcaOrtografada,'0', 'SERO');

   SET @marcaOrtografada = replace(@marcaOrtografada,'Y', 'I');
   SET @marcaOrtografada = replace(@marcaOrtografada,'T', 'D');
   SET @marcaOrtografada = replace(@marcaOrtografada,'P', 'B');
   SET @marcaOrtografada = replace(@marcaOrtografada,'Z', 'S');
   SET @marcaOrtografada = replace(@marcaOrtografada,'PH', 'F');
   SET @marcaOrtografada = replace(@marcaOrtografada,'AHRA', 'ARA');

  SET @marcaOrtografada = dbo.REMOVE_REPEATED_CHARACTERS(@marcaOrtografada);

   IF ((SUBSTRING(@marcaOrtografada, LEN(@marcaOrtografada), 1)) = 'L')
      SET @marcaOrtografada =  substring(@marcaOrtografada, 0, LEN(@marcaOrtografada)) + 'U';

   IF (LEN(@marcaOrtografada) > 1)
     IF ((SUBSTRING(@marcaOrtografada, LEN(@marcaOrtografada), 1)) = 'H')
        SET @marcaOrtografada = SUBSTRING(@marcaOrtografada, 0, LEN(@marcaOrtografada));

  SET @marcaOrtografada = dbo.REGEX_IN_THE_WHOLE_WORD('XH[AEIOU]', @marcaOrtografada, 0, 3, 'X', 2)

  SET @marcaOrtografada = dbo.REGEX_IN_THE_WHOLE_WORD('ZH[AEIOU]', @marcaOrtografada, 0, 3, 'Z', 2)

  SET @marcaOrtografada = dbo.REGEX_IN_THE_WHOLE_WORD('H[AEIOU]', @marcaOrtografada, 0, 2, '', 1)

  SET @marcaOrtografada = dbo.REGEX_IN_THE_WHOLE_WORD('[AEIOU]L[BCDFGHJKLMNPQRSTVWXYZ]', @marcaOrtografada, 1, 3, 'U', NULL)

  SET @marcaOrtografada = dbo.REGEX_IN_THE_WHOLE_WORD('[BCDFGHJKLMNPQRSTVWXYZ]H[AEIOU]', @marcaOrtografada, 1, 3, '', 1)

  SET @marcaOrtografada = dbo.REGEX_IN_THE_WHOLE_WORD('[AEIOU]H[BCDFGHJKLMNPQRSTVWXYZ]', @marcaOrtografada, 1, 3, '', 1)

   IF (LEN(@marcaOrtografada) > 1)
     IF ((SUBSTRING(@marcaOrtografada, LEN(@marcaOrtografada), 1)) = 'H')
        SET @marcaOrtografada = SUBSTRING(@marcaOrtografada, 0, LEN(@marcaOrtografada));

   SET @marcaOrtografada = dbo.REMOVE_REPEATED_CHARACTERS(@marcaOrtografada);

   RETURN ltrim(@marcaOrtografada)
END
go

