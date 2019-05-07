USE [Contact]
GO

/****** Object:  StoredProcedure [dbo].[sproc_UpdateContacts]    Script Date: 7/20/2017 7:36:40 PM ******/
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

