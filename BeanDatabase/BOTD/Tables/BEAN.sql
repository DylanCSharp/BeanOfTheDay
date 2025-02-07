CREATE TABLE [BOTD].[BEAN] (
    [Id]          NVARCHAR (255) NOT NULL,
    [Index]       INT            NULL,
    [IsBOTD]      BIT            NULL,
    [Cost]        VARCHAR (255)  NULL,
    [Image]       VARCHAR (255)  NULL,
    [Colour]      VARCHAR (255)  NULL,
    [Name]        VARCHAR (255)  NULL,
    [Description] NVARCHAR (800) NULL,
    [Country]     VARCHAR (255)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

