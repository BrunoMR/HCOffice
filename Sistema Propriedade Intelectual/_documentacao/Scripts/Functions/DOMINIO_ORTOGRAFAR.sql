ALTER FUNCTION [dbo].[DOMINIO_ORTOGRAFAR](@marca VARCHAR(2000))
RETURNS VARCHAR(2000)
BEGIN
   DECLARE
      @marcaOrtografada VARCHAR(2000)

   SET @marcaOrtografada = upper(@marca);
   SET @marcaOrtografada = replace(@marcaOrtografada, ' ', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'A');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'A');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'A');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'A');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'A');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'A');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'E');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'E');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'E');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'E');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'I');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'I');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'I');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'I');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'O');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'O');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'O');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'O');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'O');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'U');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'U');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'U');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'U');

   SET @marcaOrtografada = replace(@marcaOrtografada,'$', 'S');
   SET @marcaOrtografada = replace(@marcaOrtografada,'+', 'MAIS');
   SET @marcaOrtografada = replace(@marcaOrtografada,'%', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'&', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'@', 'A');
   SET @marcaOrtografada = replace(@marcaOrtografada,'"', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'''', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'!', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'?', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'*', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'(', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,')', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'-', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'_', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'=', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'|', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'\\', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'/', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'`', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'^', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'~', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,',', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'.', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,';', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,':', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'{', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'}', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'[', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,']', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'<', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'>', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'#', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'C');
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', '');
  
   RETURN ltrim(@marcaOrtografada)
END
