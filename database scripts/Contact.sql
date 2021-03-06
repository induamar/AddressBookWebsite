USE [Contact]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 3/27/2019 11:42:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactViewModels]    Script Date: 3/27/2019 11:42:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactViewModels](
	[ContactID] [int] IDENTITY(1,1) NOT NULL,
	[Avatar] [varchar](max) NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Age] [int] NULL,
	[Gender] [nvarchar](max) NULL,
	[AddressOne] [nvarchar](max) NULL,
	[AddressTwo] [nvarchar](max) NULL,
	[Phone] [nvarchar](15) NULL,
	[Mobile] [nvarchar](15) NULL,
	[Email] [nvarchar](max) NULL,
 CONSTRAINT [PK_ContactViewModels] PRIMARY KEY CLUSTERED 
(
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreateUserRoleViewModels]    Script Date: 3/27/2019 11:42:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreateUserRoleViewModels](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Gender] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[ConfirmPassword] [nvarchar](max) NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_RegisterViewModels] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginViewModels]    Script Date: 3/27/2019 11:42:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginViewModels](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[RememberMe] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.LoginViewModels] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sproc_UpdateContacts]    Script Date: 3/27/2019 11:42:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[sproc_UpdateContacts] 

@avatar nvarchar(max),
@fullName nvarchar(max),
@age int,
@gender nvarchar(max),
@addressOne nvarchar(max),
@addressTwo nvarchar(max),
@phone nvarchar(15),
@mobile nvarchar(15),
@email nvarchar(max),
@contactID int

AS
BEGIN
    -- Insert statements for procedure here
	UPDATE ContactViewModels
	SET Avatar = @avatar, 
		FullName = @fullName,
		Age = @age,
		Gender = @gender,
		AddressOne = @addressOne,
		AddressTwo = @addressTwo,
		Phone = @phone,
		Mobile = @mobile,
		Email = @email

	WHERE ContactID = @contactID

END
GO
