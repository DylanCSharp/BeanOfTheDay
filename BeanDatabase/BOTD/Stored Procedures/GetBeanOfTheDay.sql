-- =============================================
-- Author:		Dylan Cox
-- Create date: 2025/02/07
-- Description:	Get bean of the day
-- =============================================
CREATE PROCEDURE [BOTD].[GetBeanOfTheDay] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT TOP 1 * FROM BOTD.BEAN WHERE IsBOTD = 1
END