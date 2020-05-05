CREATE TABLE [dbo].[Levels] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [LevelName]   NVARCHAR (100) NOT NULL,
    [Description] NVARCHAR (100) NULL,
    [CreatedDate] DATETIME NULL, 
    [UpdatedDate] DATETIME NULL, 
    CONSTRAINT [PK_Levels] PRIMARY KEY CLUSTERED ([Id] ASC)
);

