-- =============================================
-- Author:		Dylan Cox
-- Create date: 2025/02/06
-- Description:	Merging all imported beans
-- =============================================
CREATE PROCEDURE [BOTD].[ImportBeans]
(
	@Input udtBeanImport readonly
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @result AS TABLE
	(
		Id VARCHAR(255)
	)	

    MERGE BOTD.BEAN T USING @Input S 
	ON T.Id = S.Id

	WHEN MATCHED THEN
	UPDATE SET 
		[Index] = S.[Index],
		IsBOTD = S.IsBOTD,
		Cost = S.Cost,
		Image = S.Image,
		Colour = S.Colour,
		Name = S.Name,
		Description = S.Description,
		Country = S.Country
		
	
	WHEN NOT MATCHED THEN
	INSERT
	(
		Id,
		[Index],
		IsBOTD,
		Cost,
		Image,
		Colour,
		Name,
		Description,
		Country
	)
	VALUES
	(
		S.Id,
		S.[Index],
		S.IsBOTD,
		S.Cost,
		S.Image,
		S.Colour,
		S.Name,
		S.Description,
		S.Country
	) OUTPUT INSERTED.Id INTO @result (Id);

	SELECT TOP 1 Id FROM @result;
END