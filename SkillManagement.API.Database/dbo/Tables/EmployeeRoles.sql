CREATE TABLE [dbo].[EmployeeRoles]
(
	[EmployeeId] BIGINT NOT NULL, 
    [RoleId] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NULL, 
    [UpdatedDate] DATETIME NULL

	CONSTRAINT [PK_Employee_Roles] PRIMARY KEY CLUSTERED ([EmployeeId], [RoleId]),
    CONSTRAINT [UQ_ReversePK] UNIQUE ([RoleId], [EmployeeId])
)

