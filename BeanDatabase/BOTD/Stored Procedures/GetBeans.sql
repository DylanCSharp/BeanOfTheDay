-- =============================================
-- Author:		Dylan Cox
-- Create date: 2025/02/06
-- Description:	Getting all the beans
-- =============================================
CREATE PROCEDURE [BOTD].[GetBeans]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM BOTD.BEAN
END