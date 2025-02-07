-- =============================================
-- Author:		Dylan Cox
-- Create date: 2025/02/07
-- Description:	Searching for bean
-- =============================================
CREATE PROCEDURE [BOTD].[BeanSearch] 
(
	@SearchTerm VARCHAR(255)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * FROM BOTD.BEAN WHERE Name LIKE '%' + @SearchTerm + '%' OR Description LIKE '%' + @SearchTerm + '%' OR Colour LIKE '%' + @SearchTerm + '%' OR Country LIKE '%' + @SearchTerm + '%'
END