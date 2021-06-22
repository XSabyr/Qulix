CREATE TABLE [dbo].[Worker]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [FatherName] NVARCHAR(50) NULL, 
    [EmploymentDate] DATE NOT NULL, 
    [PositionId] INT CONSTRAINT [FK_Worker_Position] FOREIGN KEY
        REFERENCES [dbo].[Position](Id) NOT NULL, 
    [CompanyId] INT CONSTRAINT [FK_Worker_Company] FOREIGN KEY
        REFERENCES [dbo].[Company](Id) NOT NULL
)
