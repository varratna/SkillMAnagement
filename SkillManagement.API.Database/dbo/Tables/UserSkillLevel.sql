CREATE TABLE [dbo].[UserSkillLevel] (
    [Id]      BIGINT IDENTITY (1, 1) NOT NULL,
    [UserId]  BIGINT NOT NULL,
    [SkillId] BIGINT NOT NULL,
    [LevelId] BIGINT NOT NULL,
    [CreatedDate] DATETIME NULL, 
    CONSTRAINT [PK_UserSkillLevel] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserSkillLevel_Levels_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [dbo].[Levels] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserSkillLevel_Skills_SkillId] FOREIGN KEY ([SkillId]) REFERENCES [dbo].[Skills] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserSkillLevel_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserSkillLevel_LevelId]
    ON [dbo].[UserSkillLevel]([LevelId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserSkillLevel_SkillId]
    ON [dbo].[UserSkillLevel]([SkillId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserSkillLevel_UserId]
    ON [dbo].[UserSkillLevel]([UserId] ASC);

