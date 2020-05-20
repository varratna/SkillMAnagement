
CREATE TABLE [dbo].[Employees] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (100) NOT NULL,
    [LastName]  NVARCHAR (100) NULL,
    [EmailId]   NVARCHAR (100) NOT NULL,
    [CreatedDate] DATETIME NULL, 
    [UpdatedDate] DATETIME NULL, 
    [UserName] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [Token] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([Id] ASC)
);