-- =============================================
-- Author:		Bruno Machado Rodrigues
-- Create date: 20/10/2017
-- Description:	Esta função irá concatenar as classes, retornando a classe formatada
-- =============================================
CREATE FUNCTION FormatClasses
(
	@numeroClasse VARCHAR(5),
	@numeroClasse2 VARCHAR(5),
	@numeroClasse3 VARCHAR(5),
	@numeroClasseInternacional VARCHAR(5)
)
RETURNS VARCHAR(30)
AS
BEGIN
	-- Declare the return variable here
	DECLARE
		@justClass VARCHAR(2),
		@justClass2 VARCHAR(2),
		@justClass3 VARCHAR(2),

		@justSubClass VARCHAR(2),
		@justSubClass2 VARCHAR(2),
		@justSubClass3 VARCHAR(2),

		@result VARCHAR(30)

	IF (@numeroClasseInternacional IS NOT NULL AND LTRIM(@numeroClasseInternacional) != '')
	  BEGIN
			SELECT
			  @result = CONCAT('Ncl(',
                      dbo.GetJustEditionClasse(@numeroClasseInternacional),
                      ') ',
                      dbo.GetJustClasse(@numeroClasseInternacional))

	  END
	ELSE
		BEGIN
			SELECT
			  @justClass = dbo.GetJustClasse(@numeroClasse),
			  @justClass2 = dbo.GetJustClasse(@numeroClasse2),
			  @justClass3 = dbo.GetJustClasse(@numeroClasse3),

			  @justSubClass = dbo.GetJustSubClasse(@numeroClasse),
			  @justSubClass2 = dbo.GetJustSubClasse(@numeroClasse2),
			  @justSubClass3 = dbo.GetJustSubClasse(@numeroClasse3)

      IF (@justClass IS NOT NULL)
        BEGIN
          SET @result = CONCAT(@justClass, '/', @justSubClass)

          IF (@justClass2 IS NOT NULL)
            BEGIN
              IF (@justClass2 = @justClass)
                BEGIN
                  SET @result = CONCAT(@result, '.', @justSubClass2)
                END
              ELSE
                BEGIN
                  SET @result = CONCAT(@result, ', ', @justClass2, '/', @justSubClass2)
                END
            END

          IF (@justClass3 IS NOT NULL)
            BEGIN
              IF (@justClass3 = @justClass2)
                BEGIN
                  SET @result = CONCAT(@result, '.', @justSubClass3)
                END
              ELSE
                BEGIN
                  SET @result = CONCAT(@result, ', ', @justClass3, '/', @justSubClass3)
                END
            END
        END


		END
	-- Return the result of the function
	RETURN @result

END
go

