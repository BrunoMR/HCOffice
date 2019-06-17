-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 20/10/2017
-- Description:	Esta função irá separar o termos com o delimitador passado por parâmetro
-- =============================================
CREATE FUNCTION [dbo].[SplitString]
(
    @string NVARCHAR(MAX),
    @delimiter VARCHAR(10)
)
RETURNS @output TABLE(splitdata NVARCHAR(MAX)
)
BEGIN
    DECLARE
      @start INT,
      @end INT,
      @lengthDelimiter INT = DATALENGTH(@delimiter)

    SELECT
      @string = LTRIM(RTRIM(@string))

    SELECT
      @start = 1,
      @end = CHARINDEX(@delimiter, @string)

    WHILE @start < LEN(@string) + 1
      BEGIN
        IF @end = 0
            SET @end = LEN(@string) + 1

        INSERT INTO @output
        (
          splitdata
        )
        VALUES
        (
          LTRIM(RTRIM(SUBSTRING(@string, @start, @end - @start)))
        )

        SET @start = @end + @lengthDelimiter
        SET @end = CHARINDEX(@delimiter, @string, @start)

      END
    RETURN
END
go

