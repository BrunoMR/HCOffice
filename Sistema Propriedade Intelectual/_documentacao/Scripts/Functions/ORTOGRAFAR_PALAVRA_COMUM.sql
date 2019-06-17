-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 09/09/2018
-- Description:	Este procedimento faz a ortografia para uso da palavra de uso comum
-- =============================================
CREATE FUNCTION [dbo].ORTOGRAFAR_PALAVRA_COMUM (@marca VARCHAR(2000))
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
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', 'C');
   
   SET @marcaOrtografada = replace(@marcaOrtografada,'$', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'+', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'%', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'&', '');
   SET @marcaOrtografada = replace(@marcaOrtografada,'@', '');
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
   SET @marcaOrtografada = replace(@marcaOrtografada,'�', '');
  
   RETURN ltrim(@marcaOrtografada)
END
