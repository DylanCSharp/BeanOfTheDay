-- =============================================
-- Author:		Dylan Cox
-- Create date: 2025/02/06
-- Description:	Refreshing the bean of the day 
-- =============================================
CREATE PROCEDURE [BOTD].[RefreshBOTD]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @NewBOTD  VARCHAR(255) = (SELECT TOP 1 ID FROM BOTD.BEAN WHERE IsBOTD = 0 ORDER BY NEWID());
	SELECT @NewBOTD

	UPDATE BOTD.BEAN SET IsBOTD = 0;

	UPDATE BOTD.BEAN SET IsBOTD = 1 WHERE Id = @NewBOTD
END