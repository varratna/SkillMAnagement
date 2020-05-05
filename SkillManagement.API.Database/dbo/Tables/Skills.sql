CREATE TABLE [dbo].[Skills] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [SkillName]   NVARCHAR (100) NOT NULL,
    [Description] NVARCHAR (100) NULL,
    [CreatedDate] DATETIME NULL, 
    [UpdatedDate] DATETIME NULL, 
    CONSTRAINT [PK_Skills] PRIMARY KEY CLUSTERED ([Id] ASC)
);

