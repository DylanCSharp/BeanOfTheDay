-- =============================================
-- Author:		Dylan Cox
-- Create date: 2025/02/07
-- Description:	Merging the bean data
-- =============================================
CREATE PROCEDURE [BOTD].[MergeBean]
(
	@Id VARCHAR(255),
	@Index INT,
	@Name VARCHAR(255),
	@Description VARCHAR(800),
	@IsBOTD BIT,
	@Colour VARCHAR(255),
	@Country VARCHAR(255),
	@Image VARCHAR(255),
	@Cost VARCHAR(255)
)
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @temp AS TABLE
	(
		Id VARCHAR(255),
		[Index] INT,
		Name VARCHAR(255),
		Description VARCHAR(800),
		IsBOTD BIT,
		Colour VARCHAR(255),
		Country VARCHAR(255),
		Image VARCHAR(255),
		Cost VARCHAR(255)
	)

	DECLARE @result AS TABLE
	(
		Id VARCHAR(255)
	)

	INSERT INTO @temp VALUES (@Id, @Index, @Name, @Description, @IsBOTD, @Colour, @Country, @Image, @Cost)

	MERGE BOTD.BEAN T USING @temp S 
	ON T.Id = S.Id

	WHEN NOT MATCHED THEN
	INSERT
	(
		Id,
		[Index],
		Name,
		Description,
		IsBOTD,
		Colour,
		Country,
		Image,
		Cost
	)
	VALUES
	(
		S.Id,
		S.[Index],
		S.Name,
		S.Description,
		S.IsBOTD,
		S.Colour,
		S.Country,
		S.Image,
		S.Cost
	)

	WHEN MATCHED THEN
	UPDATE SET 
		[Index] = S.[Index],
		Name = S.Name,
		Description = S.Description,
		IsBOTD = S.IsBOTD,
		Colour = S.Colour,
		Country = S.Country,
		Image = S.Image,
		Cost = S.Cost OUTPUT INSERTED.Id INTO @result (Id);
END