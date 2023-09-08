 CREATE TABLE [Users] (
	[Id] int NOT NULL IDENTITY,
	[Email] nvarchar(max) NOT NULL,
	[Password] nvarchar(max) NOT NULL,
	[PhoneNumber] nvarchar(max) NOT NULL,
	[Salary] nvarchar(max) NOT NULL,
	[Level] int NOT NULL,
	CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

CREATE TABLE [Loans] (
	[Id] int NOT NULL IDENTITY,
	[UserId] int NOT NULL,
	[Amount] decimal(18,2) NOT NULL,
	[Date] datetime2 NOT NULL,
	[DueDate] datetime2 NULL,
	[Status] int NOT NULL,
	CONSTRAINT [PK_Loans] PRIMARY KEY ([Id])
);