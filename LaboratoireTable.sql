
CREATE TABLE [dbo].[LABORATOIRES](
	[ID] [varchar](50) NOT NULL,
	[Universite] [char](9) NULL,
	[Acronyme] [varchar](50) NULL,
	[Nom] [varchar](max) NULL,
	[Address] [varchar](max) NULL,
	[Faculte] [varchar](50) NULL,
	[Departement] [varchar](50) NULL,
	[DateCreation] [datetime] NULL,
	[NumeroAgrement] [varchar](10) NULL,
	[Email] [varchar](200) NULL,
	[Telephone] [varchar](50) NULL,
	[WebSite] [varchar](max) NULL,
	[Logo] [varbinary](max) NULL,
 CONSTRAINT [PK_Laboratoire] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[LABORATOIRES] ADD  CONSTRAINT [DF_LABORATOIRES_Universite]  DEFAULT ((932033522)) FOR [Universite]
GO


