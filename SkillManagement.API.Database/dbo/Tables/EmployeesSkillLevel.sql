CREATE TABLE [dbo].[EmployeesSkillLevel] (
    [Id]      BIGINT IDENTITY (1, 1) NOT NULL,
    [EmployeeId]  BIGINT NOT NULL,
    [SkillId] BIGINT NOT NULL,
    [LevelId] BIGINT NOT NULL,
    [CreatedDate] DATETIME NULL, 
    [UpdatedDate] DATETIME NULL, 
    CONSTRAINT [PK_EmployeesSkillLevel] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EmployeesSkillLevel_Levels_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [dbo].[Levels] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EmployeesSkillLevel_Skills_SkillId] FOREIGN KEY ([SkillId]) REFERENCES [dbo].[Skills] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EmployeesSkillLevel_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_EmployeeSkillLevel_LevelId]
    ON [dbo].[EmployeesSkillLevel]([LevelId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EmployeeSkillLevel_SkillId]
    ON [dbo].[EmployeesSkillLevel]([SkillId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EmployeeSkillLevel_UserId]
    ON [dbo].[EmployeesSkillLevel]([EmployeeId] ASC);

