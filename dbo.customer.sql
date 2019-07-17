CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FirstName] NCHAR(10) NOT NULL, 
    [LastName] NCHAR(10) NOT NULL, 
    [Gender] NCHAR(10) NOT NULL, 
    [Email] NCHAR(10) NOT NULL, 
    [Password] NCHAR(10) NOT NULL, 
    [City] NCHAR(10) NULL, 
    [Street] NCHAR(10) NULL, 
    [PhoneNumber] NCHAR(10) NULL
)
