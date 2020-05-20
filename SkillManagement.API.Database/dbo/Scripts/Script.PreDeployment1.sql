/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
           WHERE TABLE_NAME = N'User_Roles')
BEGIN
  drop table User_Roles
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
           WHERE TABLE_NAME = N'Users')
BEGIN
  drop table Users
END

SET IDENTITY_INSERT dbo.Roles ON
IF NOT EXISTS (Select * from dbo.Roles where Id = 1)
BEGIN

insert into dbo.Roles(Id,Role)
values(1,'Admin')

END

IF NOT EXISTS (Select * from dbo.Roles where Id = 2)
BEGIN

insert into dbo.Roles(Id,Role)
values(2,'User')

END
SET IDENTITY_INSERT dbo.Roles OFF






SET IDENTITY_INSERT dbo.Employees ON
IF NOT EXISTS (Select * from dbo.Employees where Id = 1)
BEGIN

insert into dbo.Employees(Id,FirstName,LastName,EmailId,UserName,Password)
values(1,'John','Dove','JDove@gmail.com','admin','Password')

END


IF NOT EXISTS (Select * from dbo.Employees where Id = 2)
BEGIN
insert into dbo.Employees(Id,FirstName,LastName,EmailId,UserName,Password)
values(2,'John1','Dove1','JDove1@gmail.com','user','Password')
END

IF NOT EXISTS (Select * from dbo.Employees where Id = 3)
BEGIN
insert into dbo.Employees(Id,FirstName,LastName,EmailId,UserName,Password)
values(3,'John2','Dove2','JDove2@gmail.com','user','Password')
END
SET IDENTITY_INSERT dbo.Employees OFF



--SET IDENTITY_INSERT dbo.User_Roles ON
IF NOT EXISTS (Select * from dbo.EmployeeRoles where EmployeeId = 1 and RoleId = 1)
BEGIN

insert into dbo.EmployeeRoles(EmployeeId,RoleId)
values(1,1)

END

IF NOT EXISTS (Select * from dbo.EmployeeRoles where EmployeeId = 2  and RoleId = 2)
BEGIN

insert into dbo.EmployeeRoles(EmployeeId,RoleId)
values(2,2)

END
--SET IDENTITY_INSERT dbo.User_Roles OFF








SET IDENTITY_INSERT dbo.Levels ON
IF NOT EXISTS (Select * from dbo.Levels where Id = 1)
BEGIN
insert into dbo.Levels(Id,LevelName,Description)
values(1,'Low','Low')
END

IF NOT EXISTS (Select * from dbo.Levels where Id = 2)
BEGIN
insert into dbo.Levels(Id,LevelName,Description)
values(2,'Medium','Medium')
END

SET IDENTITY_INSERT dbo.Levels OFF


SET IDENTITY_INSERT dbo.Skills ON
IF NOT EXISTS (Select * from dbo.Skills where Id = 1)
BEGIN
insert into dbo.Skills(Id,SkillName,Description)
values(1,'.NET CORE','.NET CORE')
END

IF NOT EXISTS (Select * from dbo.Skills where Id = 2)
BEGIN
insert into dbo.Skills(Id,SkillName,Description)
values(2,'ASP.NET CORE','.NET CORE')
END

SET IDENTITY_INSERT dbo.Skills OFF

