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
SET IDENTITY_INSERT dbo.Users ON
IF NOT EXISTS (Select * from dbo.Users where Id = 1)
BEGIN

insert into dbo.Users(Id,FirstName,LastName,EmailId)
values(1,'John','Dove','JDove@gmail.com')

END


IF NOT EXISTS (Select * from dbo.Users where Id = 2)
BEGIN
insert into dbo.Users(Id,FirstName,LastName,EmailId)
values(2,'John1','Dove1','JDove1@gmail.com')
END

IF NOT EXISTS (Select * from dbo.Users where Id = 3)
BEGIN
insert into dbo.Users(Id,FirstName,LastName,EmailId)
values(3,'John2','Dove2','JDove2@gmail.com')
END
SET IDENTITY_INSERT dbo.Users OFF



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

