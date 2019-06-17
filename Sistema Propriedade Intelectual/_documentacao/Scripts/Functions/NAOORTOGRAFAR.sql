CREATE FUNCTION NAOORTOGRAFAR(@marca VARCHAR(2000))
RETURNS VARCHAR(2000)
BEGIN
   DECLARE
      @marcaNaoOrtografada VARCHAR(2000),
      @index INT

  SET @marcaNaoOrtografada = upper(@marca);
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Å', 'A');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Á', 'A');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'À', 'A');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Â', 'A');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Ä', 'A');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Ã', 'A');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'É', 'E');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'È', 'E');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Ê', 'E');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Ë', 'E');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Í', 'I');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Ì', 'I');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Î', 'I');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Ï', 'I');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Ó', 'O');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Ò', 'O');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Ô', 'O');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Õ', 'O');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Ö', 'O');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Ú', 'U');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Ù', 'U');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Ü', 'U');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Û', 'U');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Y', 'I');
  
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '$', 'S');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '+', 'MAIS');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '%', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '&', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '@', 'A');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '"', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '''', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '!', ' ');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '?', ' ');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '¿', ' ');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '¨', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '*', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '(', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, ')', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '-', ' ');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '_', ' ');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '=', ' ');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '|', ' ');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '\\', ' ');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '/', ' ');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '´', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '`', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '^', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '~', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, ',', ' ');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '.', ' ');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, ';', ' ');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, ':', ' ');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'ª', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'º', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '°', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'µ', '');
  --SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'ß', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '¢', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '£', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '¦', '');
  --SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '¹', '');
  --SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '²', '');
  --SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '³', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '½', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '¼', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '¾', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '÷', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '{', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '}', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '[', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, ']', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '§', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '<', '');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '>', '');
  
  
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'CHA', 'XA');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'CHE', 'XE');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'CHI', 'XI');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'CHO', 'XO');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'CHU', 'XU');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'SHA', 'XA');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'SHE', 'XE');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'SHI', 'XI');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'SHO', 'XO');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'SHU', 'XU');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'CH', 'K');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'SH', 'X');

  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'CE', 'SE');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'CI', 'SI');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'GE', 'JE');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'GI', 'JI');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'C', 'K');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Ç', 'S');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'PH', 'F');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'XH', 'X');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Ñ', 'N');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'QU', 'K');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'Q', 'K');
  
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'KLA', 'KRA');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'KLE', 'KRE');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'KLI', 'KRI');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'KLO', 'KRO');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, 'KLU', 'KRU');
  
  
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '1', 'UM');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '2', 'DOIS');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '3', 'TRES');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '4', 'KATRO');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '5', 'SINKO');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '6', 'SEIS');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '7', 'SETE');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '8', 'OITO');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '9', 'NOVE');
  SET @marcaNaoOrtografada = replace(@marcaNaoOrtografada, '0', 'ZERO');
  
   IF ((substring(@marcaNaoOrtografada, LEN(@marcaNaoOrtografada), 1)) = 'H')
      SET @marcaNaoOrtografada = substring(@marcaNaoOrtografada, 0, LEN(@marcaNaoOrtografada));

  SET @marcaNaoOrtografada = dbo.REMOVE_REPEATED_CHARACTERS(@marcaNaoOrtografada);

  SET @marcaNaoOrtografada = dbo.REGEX_IN_THE_WHOLE_WORD('XH[AEIOU]', @marcaNaoOrtografada, 0, 3, 'X', 2)
  
   /*SET @index = PATINDEX('%XH[AEIOU]%',@marcaNaoOrtografada);
   IF (@index > 0)
      BEGIN
         SET @marcaNaoOrtografada = STUFF(@marcaNaoOrtografada, @index, 2, 'X');
         SET @index = 0;
      END*/

  SET @marcaNaoOrtografada = dbo.REGEX_IN_THE_WHOLE_WORD('ZH[AEIOU]', @marcaNaoOrtografada, 0, 3, 'Z', 2)
  
   /*SET @index = PATINDEX('%ZH[AEIOU]%',@marcaNaoOrtografada);
   IF (@index > 0)
      BEGIN
         SET @marcaNaoOrtografada = STUFF(@marcaNaoOrtografada, @index, 2, 'Z');
         SET @index = 0;
      END*/

  SET @marcaNaoOrtografada = dbo.REGEX_IN_THE_WHOLE_WORD('H[AEIOU]', @marcaNaoOrtografada, 0, 2, '', 1)
   /*SET @index = PATINDEX('%H[AEIOU]%',@marcaNaoOrtografada);
   IF (@index > 0)
      BEGIN
         SET @marcaNaoOrtografada = STUFF(@marcaNaoOrtografada, @index, 1, '');
         SET @index = 0;
      END*/

  SET @marcaNaoOrtografada = dbo.REGEX_IN_THE_WHOLE_WORD('[AEIOU]L[BCDFGHJKLMNPQRSTVWXYZ]', @marcaNaoOrtografada, 1, 3, 'U', NULL)
  /*SET @index = PATINDEX('%[AEIOU]L[BCDFGHJKLMNPQRSTVWXYZ]%',@marcaNaoOrtografada);
   IF (@index > 0)
      BEGIN
         SET @marcaNaoOrtografada = STUFF(@marcaNaoOrtografada, @index + 1, 1, 'U');
         SET @index = 0;
      END*/

  SET @marcaNaoOrtografada = dbo.REGEX_IN_THE_WHOLE_WORD('[BCDFGHJKLMNPQRSTVWXYZ]H[AEIOU]', @marcaNaoOrtografada, 1, 3, '', 1)
   /*SET @index = PATINDEX('%[BCDFGHJKLMNPQRSTVWXYZ]H[AEIOU]%',@marcaNaoOrtografada);
   IF (@index > 0)
      BEGIN
         SET @marcaNaoOrtografada = STUFF(@marcaNaoOrtografada, @index + 1, 1, '');
         SET @index = 0;
      END*/

   SET @marcaNaoOrtografada = dbo.REGEX_IN_THE_WHOLE_WORD('[AEIOU]H[BCDFGHJKLMNPQRSTVWXYZ]', @marcaNaoOrtografada, 1, 3, '', 1)
  
   /*SET @index = PATINDEX('%[AEIOU]H[BCDFGHJKLMNPQRSTVWXYZ]%',@marcaNaoOrtografada);
   IF (@index > 0)
      BEGIN
         SET @marcaNaoOrtografada = STUFF(@marcaNaoOrtografada, @index + 1, 1, '');
         SET @index = 0;
      END*/

   IF ((substring(@marcaNaoOrtografada, LEN(@marcaNaoOrtografada), 1)) = 'H')
      SET @marcaNaoOrtografada = substring(@marcaNaoOrtografada, 0, LEN(@marcaNaoOrtografada));

  IF ((substring(@marcaNaoOrtografada, LEN(@marcaNaoOrtografada), 1)) = 'L')
      SET @marcaNaoOrtografada =  substring(@marcaNaoOrtografada, 0, LEN(@marcaNaoOrtografada)) + 'U';

  IF ((substring(@marcaNaoOrtografada, LEN(@marcaNaoOrtografada), 1)) = 'N')
      SET @marcaNaoOrtografada =  substring(@marcaNaoOrtografada, 0, LEN(@marcaNaoOrtografada)) + 'M';
  
  SET @marcaNaoOrtografada = dbo.REGEX_IN_THE_WHOLE_WORD('[AEIOU]N[BCDFGHJKLMNPQRSTVWXYZ]', @marcaNaoOrtografada, 1, 3, 'M', NULL )
  
  SET @marcaNaoOrtografada = dbo.REMOVE_REPEATED_CHARACTERS(@marcaNaoOrtografada);
  
   RETURN ltrim(@marcaNaoOrtografada)
END
go

