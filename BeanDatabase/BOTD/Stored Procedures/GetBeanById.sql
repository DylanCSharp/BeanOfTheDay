-- =============================================
-- Author:		Dylan Cox
-- Create date: 2025/02/07
-- Description:	Getting bean by Id
-- =============================================
CREATE PROCEDURE [BOTD].[GetBeanById] 
(
	@Id INT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * FROM BOTD.BEAN WHERE Id = @Id
END