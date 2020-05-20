CREATE TABLE [dbo].[EmployeeSkillLevel] (
    [Id]      BIGINT IDENTITY (1, 1) NOT NULL,
    [EmployeeId]  BIGINT NOT NULL,
    [SkillId] BIGINT NOT NULL,
    [LevelId] BIGINT NOT NULL,
    [CreatedDate] DATETIME NULL, 
    [UpdatedDate] DATETIME NULL, 
    CONSTRAINT [PK_EmployeeSkillLevel] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EmployeeLevel_Levels_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [dbo].[Levels] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EmployeeLevel_Skills_SkillId] FOREIGN KEY ([SkillId]) REFERENCES [dbo].[Skills] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EmployeeLevel_Employee_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_EmployeeSkillLevel_LevelId]
    ON [dbo].[EmployeeSkillLevel]([LevelId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EmployeeSkillLevel_SkillId]
    ON [dbo].[EmployeeSkillLevel]([SkillId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EmployeeSkillLevel_EmployeeId]
    ON [dbo].[EmployeeSkillLevel]([EmployeeId] ASC);

