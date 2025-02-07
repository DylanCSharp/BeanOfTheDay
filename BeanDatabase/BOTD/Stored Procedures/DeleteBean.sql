-- =============================================
-- Author:		Dylan Cox
-- Create date: 2025/02/06
-- Description:	Deleting a bean
-- =============================================
CREATE PROCEDURE [BOTD].[DeleteBean] 
(
	@Id VARCHAR(255)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE FROM BOTD.BEAN WHERE Id = @Id
END