CREATE TABLE [dbo].[Company]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [OrganizationalFormId] INT CONSTRAINT [FK_Company_] FOREIGN KEY
        REFERENCES [dbo].[OrganizationalForm]([Id]) NOT NULL
)
