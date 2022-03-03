USE [PlantDB]
GO
/****** Object:  Table [dbo].[Plants]    Script Date: 2022-03-02 8:20:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plants](
	[PlantId] [int] IDENTITY(1,1) NOT NULL,
	[PlantName] [nvarchar](max) NULL,
	[status] [nvarchar](max) NULL,
	[lastWateredAt] [datetime2](7) NULL,
	[isWaterAllowed] [bit] NOT NULL,
 CONSTRAINT [PK_Plants] PRIMARY KEY CLUSTERED 
(
	[PlantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Plants] ON 

INSERT [dbo].[Plants] ([PlantId], [PlantName], [status], [lastWateredAt], [isWaterAllowed]) VALUES (1, N'Rose ', N'N', CAST(N'2022-03-02T19:00:09.4622880' AS DateTime2), 1)
INSERT [dbo].[Plants] ([PlantId], [PlantName], [status], [lastWateredAt], [isWaterAllowed]) VALUES (2, N'Lily', N'N', CAST(N'2022-03-02T18:58:15.7533129' AS DateTime2), 1)
INSERT [dbo].[Plants] ([PlantId], [PlantName], [status], [lastWateredAt], [isWaterAllowed]) VALUES (3, N'Tulip ', N'N', CAST(N'2022-03-02T18:58:17.2811913' AS DateTime2), 1)
INSERT [dbo].[Plants] ([PlantId], [PlantName], [status], [lastWateredAt], [isWaterAllowed]) VALUES (4, N'Orchid', N'N', CAST(N'2022-03-02T19:00:10.3936559' AS DateTime2), 1)
INSERT [dbo].[Plants] ([PlantId], [PlantName], [status], [lastWateredAt], [isWaterAllowed]) VALUES (5, N'Sunflower', N'N', CAST(N'2022-03-02T19:00:22.8817886' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Plants] OFF
GO
ALTER TABLE [dbo].[Plants] ADD  DEFAULT (CONVERT([bit],(0))) FOR [isWaterAllowed]
GO
