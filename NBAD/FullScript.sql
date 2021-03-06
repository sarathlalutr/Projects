USE [master]
GO
/****** Object:  Database [NBAD]    Script Date: 1/27/2015 2:41:07 PM ******/
CREATE DATABASE [NBAD] ON  PRIMARY 
( NAME = N'NBAD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\NBAD.mdf' , SIZE = 12288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'NBAD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\NBAD_log.ldf' , SIZE = 57664KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NBAD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NBAD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NBAD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NBAD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NBAD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NBAD] SET ARITHABORT OFF 
GO
ALTER DATABASE [NBAD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NBAD] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [NBAD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NBAD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NBAD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NBAD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NBAD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NBAD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NBAD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NBAD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NBAD] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NBAD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NBAD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NBAD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NBAD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NBAD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NBAD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NBAD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NBAD] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NBAD] SET  MULTI_USER 
GO
ALTER DATABASE [NBAD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NBAD] SET DB_CHAINING OFF 
GO
USE [NBAD]
GO
/****** Object:  UserDefinedTableType [dbo].[EmployeeImportList]    Script Date: 1/27/2015 2:41:07 PM ******/
CREATE TYPE [dbo].[EmployeeImportList] AS TABLE(
	[EmployeeId] [nvarchar](255) NOT NULL,
	[EmployeeName] [nvarchar](255) NULL,
	[Gender] [varchar](255) NOT NULL,
	[Designation] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Branch] [nvarchar](255) NOT NULL,
	[Department] [nvarchar](255) NULL,
	[Location] [nvarchar](255) NOT NULL,
	[AccessTime] [datetime] NOT NULL
)
GO
/****** Object:  StoredProcedure [dbo].[tblDescriptionUpdate]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[tblDescriptionUpdate] 
    @DescriptionId int,
    @Description nvarchar(100)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[tblDescription]
	SET    [Description]= @Description
	WHERE  [DescriptionId] = @DescriptionId
	
	-- Begin Return Select <- do not remove
	SELECT [DescriptionId],[Description]
	FROM   [dbo].[tblDescription]
	WHERE  [DescriptionId]=[DescriptionId]
	-- End Return Select <- do not remove

	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_BranchDetailsSearch]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



create PROCEDURE [dbo].[usp_BranchDetailsSearch]
	@SearchTerm varchar(500) = NULL

AS
BEGIN
	SELECT 
		*
	FROM
		[dbo].[tblBranch]
	WHERE
	(
		[BranchID] LIKE '%'+ @SearchTerm +'%'
		OR
		[BranchName] LIKE '%'+ @SearchTerm +'%'
		OR
		@SearchTerm IS NULL
	)
	ORDER BY [BranchName]
END

--usp_BranchDetailsSearch
GO
/****** Object:  StoredProcedure [dbo].[usp_BranchSelectAll]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_BranchSelectAll]
	
AS
--BEGIN
--	SELECT 
--		distinct [Branch]
--	FROM
--		[dbo].[tblAccess]

--	ORDER BY [Branch]
--END

BEGIN
	SELECT 
		*
	FROM
		[dbo].[tblBranch]

	ORDER BY [BranchName]
END

--usp_BranchSelectAll 
GO
/****** Object:  StoredProcedure [dbo].[usp_DepartmentDetailsSearch]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[usp_DepartmentDetailsSearch]
	@SearchTerm varchar(500) = NULL

AS
BEGIN
	SELECT 
		*
	FROM
		[dbo].[tblDepartment]
	WHERE
	(
		
		[DepartmentName] LIKE '%'+ @SearchTerm +'%'
		OR
		@SearchTerm IS NULL
	)
	ORDER BY [DepartmentName]
END

--usp_DepartmentDetailsSearch
GO
/****** Object:  StoredProcedure [dbo].[usp_DepartmentSelectAll]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_DepartmentSelectAll]
	
AS
--BEGIN
--	SELECT 
--		distinct [Department]
--	FROM
--		[dbo].[tblAccess]

--	ORDER BY [Department]
--END
BEGIN
	SELECT 
		*
	FROM
		[dbo].[tblDepartment]

	ORDER BY [DepartmentName]
END
--usp_usp_DepartmentSelectAll
GO
/****** Object:  StoredProcedure [dbo].[usp_DescriptionDetailsSearch]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[usp_DescriptionDetailsSearch]
	@SearchTerm varchar(500) = NULL

AS
BEGIN
	SELECT 
		*
	FROM
	[dbo].[tblDescription]
	WHERE
	(
		
		[Description] LIKE '%'+ @SearchTerm +'%'
		OR
		@SearchTerm IS NULL
	)
	ORDER BY [Description]
END

--usp_DescriptionDetailsSearch
GO
/****** Object:  StoredProcedure [dbo].[usp_DesignationDetailsSearch]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[usp_DesignationDetailsSearch]
	@SearchTerm varchar(500) = NULL

AS
BEGIN
	SELECT 
		*
	FROM
		[dbo].[tblDesignation]
	WHERE
	(
		
		[DesignationName] LIKE '%'+ @SearchTerm +'%'
		OR
		@SearchTerm IS NULL
	)
	ORDER BY [DesignationName]
END

--usp_DesignationDetailsSearch
GO
/****** Object:  StoredProcedure [dbo].[usp_DesignationSelectAll]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_DesignationSelectAll]
	
AS
BEGIN
	SELECT 
		distinct [Designation]
	FROM
		[dbo].[tblAccess]
	
	ORDER BY [Designation]
END

--usp_DesignationSelectAll
GO
/****** Object:  StoredProcedure [dbo].[usp_DReportDesignation]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_DReportDesignation]
	@designation nvarchar(255)= null
AS
BEGIN
	SELECT 
		[EmployeeId]
      ,[EmployeeName]
      ,[Gender]
      ,[Designation]
      ,[Description]
      ,[Branch]
      ,[Department]
      ,[Location]
      ,CONVERT(varchar,[AccessTime],103)  AS [AccessTime]
	FROM
		[dbo].[tblAccess]
		where [Designation]=@designation
	
	ORDER BY [Designation]
END

--usp_DReportDesignation 'Accountant'
GO
/****** Object:  StoredProcedure [dbo].[usp_ImportEmployeeToDb]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[usp_ImportEmployeeToDb] 

@EmployeeImportList AS [dbo].[EmployeeImportList] READONLY 

AS
BEGIN
	
	INSERT INTO [dbo].[tblAccess]
	SELECT [EmployeeId],
		[EmployeeName],
		[Gender],
		[Designation],
		[Description],
		[Branch],
		[Department],
		[Location],
		[AccessTime]
	FROM @EmployeeImportList
	WHERE [EmployeeId] IS NOT NULL
		AND [EmployeeName] IS NOT NULL
		AND [Gender] IS NOT NULL
		AND [Designation] IS NOT NULL
		AND [Description] IS NOT NULL
		AND [Branch] IS NOT NULL
		AND [Department] IS NOT NULL
		AND [Location] IS NOT NULL
		AND [AccessTime] IS NOT NULL ;

	select @@ROWCOUNT

	
END
GO
/****** Object:  StoredProcedure [dbo].[usp_LocationDetailsSearch]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[usp_LocationDetailsSearch]
	@SearchTerm varchar(500) = NULL

AS
BEGIN
	SELECT 
		*
	FROM
		[dbo].[tblLocation]
	WHERE
	(
		
		[LocationName] LIKE '%'+ @SearchTerm +'%'
		OR
		@SearchTerm IS NULL
	)
	ORDER BY [LocationName]
END

--usp_LocationDetailsSearch
GO
/****** Object:  StoredProcedure [dbo].[usp_LocationSelectAll]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[usp_LocationSelectAll]
	
AS
BEGIN
	SELECT 
		distinct [Location]
	FROM
		[dbo].[tblAccess]

	ORDER BY [Location]
END

--usp_LocationSelectAll
GO
/****** Object:  StoredProcedure [dbo].[usp_ReportBranch]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_ReportBranch]
	@branch nvarchar(255)= null
AS
--BEGIN
--	SELECT 
--		[EmployeeId]
--      ,[EmployeeName]
--      ,[Gender]
--      ,[Designation]
--      ,[Description]
--      ,[Branch]
--      ,[Department]
--      ,[Location]
--      --,CONVERT(varchar,[AccessTime],103)  AS [AccessTime]
--	  , CONVERT(VARCHAR(10), [AccessTime], 103) 
--                        + ' ' + CONVERT(VARCHAR(8),[AccessTime], 108) as [AccessTime]
--	FROM
--		[dbo].[tblAccess]
--		where [Branch]=@branch
	
--	ORDER BY [Designation]
--END

BEGIN
	SELECT 
		[EmployeeId]
      ,[EmployeeName]
      ,[Gender]
      ,(SELECT [DesignationName]  FROM [dbo].[tblDesignation] WHERE [DesignationId]=tA.DesignationId) as Designation
      ,(select [Description]   from [dbo].[tblDescription] where [DescriptionId]=tA.DescriptionId) as Description
      ,(select [BranchName]  from [dbo].[tblBranch] where [BranchEntryId]=@branch) as Branch
      ,(select [DepartmentName]  from [dbo].[tblDepartment] where [DepartmentId]=ta.DepartmentId) as Department
      ,(select [LocationName]  from [dbo].[tblLocation] where [LocationId]=tA.SwipeInLocationId) as swipeInLocation
      --,CONVERT(varchar,[AccessTime],103)  AS [AccessTime]
	  , CONVERT(VARCHAR(10), [SwipeInTime], 103) 
                        + ' ' + CONVERT(VARCHAR(8),[SwipeInTime], 108) as [SwipeInTime]
	  ,CONVERT(VARCHAR(10), [SwipeOutTime], 103) 
                        + ' ' + CONVERT(VARCHAR(8),[SwipeOutTime], 108) as [SwipeOutTime]
	  ,(select [LocationName] from [dbo].[tblLocation] where [LocationId]=tA.SwipeOutLocationId)  as swipeOutLocation
	FROM
		[dbo].[tblAccess1] tA
		where [BranchEntryId]=@branch
	
	--ORDER BY [Designation]
END

--usp_ReportBranch '17'
GO
/****** Object:  StoredProcedure [dbo].[usp_ReportDepartment]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_ReportDepartment]
	@department nvarchar(255)= null
AS
--BEGIN
--	SELECT 
--		[EmployeeId]
--      ,[EmployeeName]
--      ,[Gender]
--      ,[Designation]
--      ,[Description]
--      ,[Branch]
--      ,[Department]
--      ,[Location]
--     --,CONVERT(varchar,[AccessTime],103)  AS [AccessTime]
--	  , CONVERT(VARCHAR(10), [AccessTime], 103) 
--                        + ' ' + CONVERT(VARCHAR(8),[AccessTime], 108) as [AccessTime]
--	FROM
--		[dbo].[tblAccess]
--		where [Department]=@department
	
--	ORDER BY [Department]
--END


BEGIN
	SELECT 
		[EmployeeId]
      ,[EmployeeName]
      ,[Gender]
     ,(SELECT [DesignationName]  FROM [dbo].[tblDesignation] WHERE [DesignationId]=tA.DesignationId) as Designation
      ,(select [Description]   from [dbo].[tblDescription] where [DescriptionId]=tA.DescriptionId) as Description
      ,(select [BranchName]  from [dbo].[tblBranch] where [BranchEntryId]=@department) as Branch
      ,(select [DepartmentName]  from [dbo].[tblDepartment] where [DepartmentId]=ta.DepartmentId) as Department
      ,(select [LocationName]  from [dbo].[tblLocation] where [LocationId]=tA.SwipeInLocationId) as swipeInLocation
      --,CONVERT(varchar,[AccessTime],103)  AS [AccessTime]
	  , CONVERT(VARCHAR(10), [SwipeInTime], 103) 
                        + ' ' + CONVERT(VARCHAR(8),[SwipeInTime], 108) as [SwipeInTime]
	  ,CONVERT(VARCHAR(10), [SwipeOutTime], 103) 
                        + ' ' + CONVERT(VARCHAR(8),[SwipeOutTime], 108) as [SwipeOutTime]
	  ,(select [LocationName] from [dbo].[tblLocation] where [LocationId]=tA.SwipeOutLocationId)  as swipeOutLocation
	FROM
		[dbo].[tblAccess1] tA
		where DepartmentId=@department
	
	--ORDER BY [Department]
END


--usp_ReportDepartment 'Sales'
GO
/****** Object:  StoredProcedure [dbo].[usp_ReportDesignation]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_ReportDesignation]
	@designation nvarchar(255)= null
AS
BEGIN
	SELECT 
		[EmployeeId]
      ,[EmployeeName]
      ,[Gender]
      ,[Designation]
      ,[Description]
      ,[Branch]
      ,[Department]
      ,[Location]
--,CONVERT(varchar,[AccessTime],103)  AS [AccessTime]
	  , CONVERT(VARCHAR(10), [AccessTime], 103) 
                        + ' ' + CONVERT(VARCHAR(8),[AccessTime], 108) as [AccessTime]
	FROM
		[dbo].[tblAccess]
		where [Designation]=@designation
	
	ORDER BY [Designation]
END

--usp_ReportDesignation 'Accountant'
GO
/****** Object:  StoredProcedure [dbo].[usp_ReportLocation]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_ReportLocation]
	@location nvarchar(255)= null
AS
BEGIN
	SELECT 
		[EmployeeId]
      ,[EmployeeName]
      ,[Gender]
      ,[Designation]
      ,[Description]
      ,[Branch]
      ,[Department]
      ,[Location]
      --,CONVERT(varchar,[AccessTime],103)  AS [AccessTime]
	  , CONVERT(VARCHAR(10), [AccessTime], 103) 
                        + ' ' + CONVERT(VARCHAR(8),[AccessTime], 108) as [AccessTime]
	FROM
		[dbo].[tblAccess]
		where [Location]=@location
	
	ORDER BY [Location]
END

--usp_ReportLocation 'GF_LIFT LOBY FL_D3'
GO
/****** Object:  StoredProcedure [dbo].[usp_tblAccessDelete]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblAccessDelete] 
    @NBADId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[tblAccess]
	WHERE  [NBADId] = @NBADId

	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_tblAccessInsert]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblAccessInsert] 
    @EmployeeId nvarchar(255),
    @EmployeeName nvarchar(255),
    @Gender nvarchar(255),
    @Designation nvarchar(255),
    @Description nvarchar(MAX) = NULL,
    @Branch nvarchar(255),
    @Department nvarchar(255),
    @Location nvarchar(255),
    @AccessTime datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[tblAccess] ([EmployeeId], [EmployeeName], [Gender], [Designation], [Description], [Branch], [Department], [Location], [AccessTime])
	SELECT @EmployeeId, @EmployeeName, @Gender, @Designation, @Description, @Branch, @Department, @Location, @AccessTime
	
	-- Begin Return Select <- do not remove
	SELECT [NBADId], [EmployeeId], [EmployeeName], [Gender], [Designation], [Description], [Branch], [Department], [Location], [AccessTime]
	FROM   [dbo].[tblAccess]
	WHERE  [NBADId] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_tblAccessInsertNew]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblAccessInsertNew] 
    @EmployeeId nvarchar(255),
    @EmployeeName nvarchar(255),
    @Gender nvarchar(255),
    @Designation INT,
    @Description int,
    @Branch int,
    @Department int,
    @SwipeInLocation int,
    @SwipeInTime datetime,
	@SwipeOutTime datetime,
    @SwipeOutLocation int
AS 
	--SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN 
	
	INSERT INTO [dbo].[tblAccess1] ([EmployeeId], [EmployeeName], [Gender],[DesignationId],[DescriptionId],[BranchEntryId],[DepartmentId],
	 [SwipeInLocationId],[SwipeInTime],[SwipeOutTime],[SwipeOutLocationId])
	SELECT @EmployeeId, @EmployeeName, @Gender, @Designation, @Description, @Branch, @Department,@SwipeInLocation,
	@SwipeInTime,@SwipeOutTime,@SwipeOutLocation
	
	
               
end

GO
/****** Object:  StoredProcedure [dbo].[usp_tblAccessSelect]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblAccessSelect] 
    @NBADId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [NBADId], [EmployeeId], [EmployeeName], [Gender], [Designation], [Description], [Branch], [Department], [Location], [AccessTime] 
	FROM   [dbo].[tblAccess] 
	WHERE  ([NBADId] = @NBADId OR @NBADId IS NULL) 

	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_tblAccessUpdate]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblAccessUpdate] 
    @NBADId int,
    @EmployeeId nvarchar(255),
    @EmployeeName nvarchar(255),
    @Gender nvarchar(255),
    @Designation nvarchar(255),
    @Description nvarchar(MAX) = NULL,
    @Branch nvarchar(255),
    @Department nvarchar(255),
    @Location nvarchar(255),
    @AccessTime datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[tblAccess]
	SET    [EmployeeId] = @EmployeeId, [EmployeeName] = @EmployeeName, [Gender] = @Gender, [Designation] = @Designation, [Description] = @Description, [Branch] = @Branch, [Department] = @Department, [Location] = @Location, [AccessTime] = @AccessTime
	WHERE  [NBADId] = @NBADId
	
	-- Begin Return Select <- do not remove
	SELECT [NBADId], [EmployeeId], [EmployeeName], [Gender], [Designation], [Description], [Branch], [Department], [Location], [AccessTime]
	FROM   [dbo].[tblAccess]
	WHERE  [NBADId] = @NBADId	
	-- End Return Select <- do not remove

	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_tblBranchDelete]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblBranchDelete] 
    @BranchEntryId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[tblBranch]
	WHERE  [BranchEntryId] = @BranchEntryId

	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_tblBranchInsert]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblBranchInsert] 
    @BranchID nvarchar(20),
    @BranchName nvarchar(50)
AS 
	--SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN
	IF NOT EXISTS(SELECT * FROM [dbo].[tblBranch]
	
	WHERE [BranchID]=@BranchID and [BranchName]=@BranchName )
	
	BEGIN 
	
	INSERT INTO [dbo].[tblBranch] ([BranchID], [BranchName])
	SELECT @BranchID, @BranchName
	
	


END
END
GO
/****** Object:  StoredProcedure [dbo].[usp_tblBranchSelect]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblBranchSelect] 
    @BranchEntryId int =NULL
AS 
	

	BEGIN 

	SELECT [BranchEntryId], [BranchID], [BranchName] 
	FROM   [dbo].[tblBranch] 
	WHERE  ([BranchEntryId] = @BranchEntryId OR @BranchEntryId IS NULL) 
	

	END

GO
/****** Object:  StoredProcedure [dbo].[usp_tblBranchUpdate]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblBranchUpdate] 
    @BranchEntryId int,
    @BranchID nvarchar(20),
    @BranchName nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[tblBranch]
	SET    [BranchID] = @BranchID, [BranchName] = @BranchName
	WHERE  [BranchEntryId] = @BranchEntryId
	
	-- Begin Return Select <- do not remove
	SELECT [BranchEntryId], [BranchID], [BranchName]
	FROM   [dbo].[tblBranch]
	WHERE  [BranchEntryId] = @BranchEntryId	
	-- End Return Select <- do not remove

	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_tblDepartmentDelete]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblDepartmentDelete] 
    @DepartmentId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[tblDepartment]
	WHERE  [DepartmentId] = @DepartmentId

	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_tblDepartmentInsert]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblDepartmentInsert] 
    @DepartmentName nvarchar(50)
AS 	SET XACT_ABORT ON  
	BEGIN
	IF NOT EXISTS(SELECT * FROM [dbo].[tblDepartment]
	
	WHERE [DepartmentName]=@DepartmentName)

	BEGIN 
	
	INSERT INTO [dbo].[tblDepartment] ([DepartmentName])
	SELECT @DepartmentName

               
end
end
GO
/****** Object:  StoredProcedure [dbo].[usp_tblDepartmentSelect]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblDepartmentSelect] 
    @DepartmentId int = null
AS 
	

	BEGIN 

	SELECT [DepartmentId], [DepartmentName] 
	FROM   [dbo].[tblDepartment] 
	WHERE  ([DepartmentId] = @DepartmentId OR @DepartmentId IS NULL) 

	end

GO
/****** Object:  StoredProcedure [dbo].[usp_tblDepartmentUpdate]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblDepartmentUpdate] 
    @DepartmentId int,
    @DepartmentName nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[tblDepartment]
	SET    [DepartmentName] = @DepartmentName
	WHERE  [DepartmentId] = @DepartmentId
	
	-- Begin Return Select <- do not remove
	SELECT [DepartmentId], [DepartmentName]
	FROM   [dbo].[tblDepartment]
	WHERE  [DepartmentId] = @DepartmentId	
	-- End Return Select <- do not remove

	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_tblDescriptionDelete]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[usp_tblDescriptionDelete] 
    @DescriptionId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM [dbo].[tblDescription]  
	WHERE   [DescriptionId]= @DescriptionId

	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_tblDescriptionInsert]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[usp_tblDescriptionInsert] 
    @Description nvarchar(100)
AS 
	BEGIN
	IF NOT EXISTS(SELECT * FROM [dbo].[tblDescription]
	
	WHERE [Description]=@Description)
	SET XACT_ABORT ON  
	
	BEGIN 
	
	INSERT INTO [dbo].[tblDescription]([Description])
	SELECT @Description
	
	
               
end
end

GO
/****** Object:  StoredProcedure [dbo].[usp_tblDescriptionSelect]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[usp_tblDescriptionSelect] 
     @DescriptionId int = null
AS 
	

	BEGIN 

	SELECT [DescriptionId],[Description]
	FROM   [dbo].[tblDescription]
	WHERE  ( [DescriptionId]= @DescriptionId OR @DescriptionId IS NULL) 

	end

GO
/****** Object:  StoredProcedure [dbo].[usp_tblDesignationDelete]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblDesignationDelete] 
    @DesignationId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[tblDesignation]
	WHERE  [DesignationId] = @DesignationId

	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_tblDesignationInsert]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblDesignationInsert] 
    @DesignationName nvarchar(50)
AS 

	SET XACT_ABORT ON  
	BEGIN
	IF NOT EXISTS(SELECT * FROM [dbo].[tblDesignation]
	
	WHERE [DesignationName]=@DesignationName)
	BEGIN 
	
	INSERT INTO [dbo].[tblDesignation] ([DesignationName])
	SELECT @DesignationName
	
	
               
end
end

GO
/****** Object:  StoredProcedure [dbo].[usp_tblDesignationSelect]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblDesignationSelect] 
    @DesignationId int=null
AS 
	

	BEGIN 

	SELECT [DesignationId], [DesignationName] 
	FROM   [dbo].[tblDesignation] 
	WHERE  ([DesignationId] = @DesignationId OR @DesignationId IS NULL) 

	end

GO
/****** Object:  StoredProcedure [dbo].[usp_tblDesignationUpdate]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblDesignationUpdate] 
    @DesignationId int,
    @DesignationName nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[tblDesignation]
	SET    [DesignationName] = @DesignationName
	WHERE  [DesignationId] = @DesignationId
	
	-- Begin Return Select <- do not remove
	SELECT [DesignationId], [DesignationName]
	FROM   [dbo].[tblDesignation]
	WHERE  [DesignationId] = @DesignationId	
	-- End Return Select <- do not remove

	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_tblLocationDelete]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblLocationDelete] 
    @LocationId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[tblLocation]
	WHERE  [LocationId] = @LocationId

	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_tblLocationInsert]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblLocationInsert] 
    @LocationName nvarchar(100)
AS 
	BEGIN
	IF NOT EXISTS(SELECT * FROM [dbo].[tblLocation]
	
	WHERE [LocationName]=@LocationName)
	SET XACT_ABORT ON  
	
	BEGIN 
	
	INSERT INTO [dbo].[tblLocation] ([LocationName])
	SELECT @LocationName
	
	
               
end
end

GO
/****** Object:  StoredProcedure [dbo].[usp_tblLocationSelect]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblLocationSelect] 
    @LocationId int=null
AS 


	BEGIN 

	SELECT [LocationId], [LocationName] 
	FROM   [dbo].[tblLocation] 
	WHERE  ([LocationId] = @LocationId OR @LocationId IS NULL) 

end
GO
/****** Object:  StoredProcedure [dbo].[usp_tblLocationUpdate]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblLocationUpdate] 
    @LocationId int,
    @LocationName nvarchar(100)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[tblLocation]
	SET    [LocationName] = @LocationName
	WHERE  [LocationId] = @LocationId
	
	-- Begin Return Select <- do not remove
	SELECT [LocationId], [LocationName]
	FROM   [dbo].[tblLocation]
	WHERE  [LocationId] = @LocationId	
	-- End Return Select <- do not remove

	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_tblLoginDelete]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblLoginDelete] 
    @UserId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[tblLogin]
	WHERE  [UserId] = @UserId

	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_tblLoginInsert]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblLoginInsert] 
    @UserName nvarchar(150),
    @Password nvarchar(150),
    @isActive bit,
    @Role nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[tblLogin] ([UserName], [Password], [isActive], [Role])
	SELECT @UserName, @Password, @isActive, @Role
	
	-- Begin Return Select <- do not remove
	SELECT [UserId], [UserName], [Password], [isActive], [Role]
	FROM   [dbo].[tblLogin]
	WHERE  [UserId] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_tblLoginSelect]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblLoginSelect] 
    @UserId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [UserId], [UserName], [Password], [isActive], [Role] 
	FROM   [dbo].[tblLogin] 
	WHERE  ([UserId] = @UserId OR @UserId IS NULL) 

	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_tblLoginUpdate]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_tblLoginUpdate] 
    @UserId int,
    @UserName nvarchar(150),
    @Password nvarchar(150),
    @isActive bit,
    @Role nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[tblLogin]
	SET    [UserName] = @UserName, [Password] = @Password, [isActive] = @isActive, [Role] = @Role
	WHERE  [UserId] = @UserId
	
	-- Begin Return Select <- do not remove
	SELECT [UserId], [UserName], [Password], [isActive], [Role]
	FROM   [dbo].[tblLogin]
	WHERE  [UserId] = @UserId	
	-- End Return Select <- do not remove

	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[usp_UserLoginSelect]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_UserLoginSelect]
	@UserName varchar(15)
	,@Password varchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		[UserId]
		, [UserName], [Password],  [Role]
		--, [IsActive], [LastModifiedBy], [LastModifedOn], [CreatedBy], [CreatedOn] 

	FROM
		[dbo].[tblLogin] A
	WHERE
		A.[UserName] = @UserName
	AND
		A.[Password] = @Password

	-- usp_UserLoginSelect 'admin', 'f865b53623b121fd34ee5426c792e5c33af8c227'
END

GO
/****** Object:  Table [dbo].[tblAccess]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAccess](
	[NBADId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [nvarchar](255) NOT NULL,
	[EmployeeName] [nvarchar](255) NOT NULL,
	[Gender] [nvarchar](255) NOT NULL,
	[Designation] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Branch] [nvarchar](255) NOT NULL,
	[Department] [nvarchar](255) NOT NULL,
	[Location] [nvarchar](255) NOT NULL,
	[AccessTime] [datetime] NOT NULL,
 CONSTRAINT [PK_tblAccess] PRIMARY KEY CLUSTERED 
(
	[NBADId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblAccess1]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAccess1](
	[NBADId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [nvarchar](255) NOT NULL,
	[EmployeeName] [nvarchar](255) NOT NULL,
	[Gender] [nvarchar](255) NOT NULL,
	[DesignationId] [int] NOT NULL,
	[DescriptionId] [int] NOT NULL,
	[BranchEntryId] [int] NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[SwipeInLocationId] [int] NOT NULL,
	[SwipeInTime] [datetime] NOT NULL,
	[SwipeOutTime] [datetime] NOT NULL,
	[SwipeOutLocationId] [int] NOT NULL,
 CONSTRAINT [PK_tblAccess1] PRIMARY KEY CLUSTERED 
(
	[NBADId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblBranch]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBranch](
	[BranchEntryId] [int] IDENTITY(1,1) NOT NULL,
	[BranchID] [nvarchar](20) NOT NULL,
	[BranchName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblBranch] PRIMARY KEY CLUSTERED 
(
	[BranchEntryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblDepartment]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDepartment](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblDepartment] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblDescription]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDescription](
	[DescriptionId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_tblDescription] PRIMARY KEY CLUSTERED 
(
	[DescriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblDesignation]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDesignation](
	[DesignationId] [int] IDENTITY(1,1) NOT NULL,
	[DesignationName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblDesignation] PRIMARY KEY CLUSTERED 
(
	[DesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblLocation]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLocation](
	[LocationId] [int] IDENTITY(1,1) NOT NULL,
	[LocationName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_tblLocation] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblLogin]    Script Date: 1/27/2015 2:41:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLogin](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](150) NOT NULL,
	[Password] [nvarchar](150) NOT NULL,
	[isActive] [bit] NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblLogin] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[tblBranch] ON 

INSERT [dbo].[tblBranch] ([BranchEntryId], [BranchID], [BranchName]) VALUES (17, N'NBAD001', N'BRANCH1')
SET IDENTITY_INSERT [dbo].[tblBranch] OFF
SET IDENTITY_INSERT [dbo].[tblDepartment] ON 

INSERT [dbo].[tblDepartment] ([DepartmentId], [DepartmentName]) VALUES (1, N'accounts')
INSERT [dbo].[tblDepartment] ([DepartmentId], [DepartmentName]) VALUES (3, N'guhjk')
SET IDENTITY_INSERT [dbo].[tblDepartment] OFF
SET IDENTITY_INSERT [dbo].[tblDescription] ON 

INSERT [dbo].[tblDescription] ([DescriptionId], [Description]) VALUES (1, N'Desc11')
SET IDENTITY_INSERT [dbo].[tblDescription] OFF
SET IDENTITY_INSERT [dbo].[tblDesignation] ON 

INSERT [dbo].[tblDesignation] ([DesignationId], [DesignationName]) VALUES (6, N'SSE2')
SET IDENTITY_INSERT [dbo].[tblDesignation] OFF
SET IDENTITY_INSERT [dbo].[tblLocation] ON 

INSERT [dbo].[tblLocation] ([LocationId], [LocationName]) VALUES (3, N'Loby2')
INSERT [dbo].[tblLocation] ([LocationId], [LocationName]) VALUES (4, N'Kitchen')
SET IDENTITY_INSERT [dbo].[tblLocation] OFF
SET IDENTITY_INSERT [dbo].[tblLogin] ON 

INSERT [dbo].[tblLogin] ([UserId], [UserName], [Password], [isActive], [Role]) VALUES (1, N'Admin', N'f865b53623b121fd34ee5426c792e5c33af8c227', 1, N'Admin')
SET IDENTITY_INSERT [dbo].[tblLogin] OFF
ALTER TABLE [dbo].[tblAccess1]  WITH CHECK ADD  CONSTRAINT [FK_tblAccess1_tblAccess1] FOREIGN KEY([SwipeOutLocationId])
REFERENCES [dbo].[tblLocation] ([LocationId])
GO
ALTER TABLE [dbo].[tblAccess1] CHECK CONSTRAINT [FK_tblAccess1_tblAccess1]
GO
ALTER TABLE [dbo].[tblAccess1]  WITH CHECK ADD  CONSTRAINT [FK_tblAccess1_tblBranch] FOREIGN KEY([BranchEntryId])
REFERENCES [dbo].[tblBranch] ([BranchEntryId])
GO
ALTER TABLE [dbo].[tblAccess1] CHECK CONSTRAINT [FK_tblAccess1_tblBranch]
GO
ALTER TABLE [dbo].[tblAccess1]  WITH CHECK ADD  CONSTRAINT [FK_tblAccess1_tblDepartment] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[tblDepartment] ([DepartmentId])
GO
ALTER TABLE [dbo].[tblAccess1] CHECK CONSTRAINT [FK_tblAccess1_tblDepartment]
GO
ALTER TABLE [dbo].[tblAccess1]  WITH CHECK ADD  CONSTRAINT [FK_tblAccess1_tblDescription] FOREIGN KEY([DescriptionId])
REFERENCES [dbo].[tblDescription] ([DescriptionId])
GO
ALTER TABLE [dbo].[tblAccess1] CHECK CONSTRAINT [FK_tblAccess1_tblDescription]
GO
ALTER TABLE [dbo].[tblAccess1]  WITH CHECK ADD  CONSTRAINT [FK_tblAccess1_tblDesignation] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[tblDesignation] ([DesignationId])
GO
ALTER TABLE [dbo].[tblAccess1] CHECK CONSTRAINT [FK_tblAccess1_tblDesignation]
GO
ALTER TABLE [dbo].[tblAccess1]  WITH CHECK ADD  CONSTRAINT [FK_tblAccess1_tblLocation] FOREIGN KEY([SwipeInLocationId])
REFERENCES [dbo].[tblLocation] ([LocationId])
GO
ALTER TABLE [dbo].[tblAccess1] CHECK CONSTRAINT [FK_tblAccess1_tblLocation]
GO
USE [master]
GO
ALTER DATABASE [NBAD] SET  READ_WRITE 
GO
