
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL
	CONSTRAINT [PK_dbo.Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Genre] [bit] NOT NULL,
	[Birth] [datetime] NULL,
	[IdRole] [int] not null
CONSTRAINT [PK_dbo.Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Employee] add CONSTRAINT FK_Role FOREIGN KEY (IdRole)
    REFERENCES Role(Id)

CREATE TABLE [dbo].[Dependent](
	[IdEmployee] int not null,
	[Id] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_dbo.Dependent] PRIMARY KEY CLUSTERED,
	[Name] [nvarchar](50) NOT NULL,
) ON [PRIMARY]

ALTER TABLE [dbo].[Dependent] add CONSTRAINT FK_User FOREIGN KEY (IdEmployee)
    REFERENCES [dbo].[Employee](Id)