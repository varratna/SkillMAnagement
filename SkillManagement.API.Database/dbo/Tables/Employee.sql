CREATE TABLE [dbo].[Employee] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (100) NOT NULL,
    [LastName]  NVARCHAR (100) NULL,
    [EmailId]   NVARCHAR (100) NOT NULL,
    [CreatedDate] DATETIME NULL, 
    [UpdatedDate] DATETIME NULL, 
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([Id] ASC)
);

