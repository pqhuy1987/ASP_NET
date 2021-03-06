ALTER PROCEDURE [dbo].[LLTC_Get_List_By_All_Project]
AS
BEGIN
	SELECT
		  LLTC.[Main_Name_LLTC],
		  LLTC.Main_Name_Ower,
		  LLTC.Main_Total_Number,
		  LLTC.Main_Number,
		  LLTC.Main_Status,
		  WorkMain.CS_WorkTypeMain,
		  LLTC.Main_Name_Job,
		  LLTC_Sub.CS_tbLLTCNameSiteID,
		  LLTC_Sub.CS_tbLLTCNameJobDetailsSub,
		  Work.SubWorkType,
		  Project.Project_Name,
		  Project.Site_Area
	  FROM [CWD].[dbo].[LLTC] as LLTC
	  inner join [CWD].[dbo].[CS_tbLLTCTypeSub] as LLTC_Sub
	  on LLTC.ID = LLTC_Sub.CS_tbLLTC_ID
	  inner join [CWD].[dbo].[Project] as Project
	  on LLTC_Sub.CS_tbLLTCNameSiteID = Project.ID
	   inner join [CWD].[dbo].[CS_tbWorkTypeMain] as WorkMain
	  on LLTC.Main_Name_Job = WorkMain.ID
	  inner join [CWD].[dbo].[CS_tbWorkType] as Work
	  on LLTC_Sub.CS_tbLLTCNameJobDetailsSub = Work.ID
	  order by  Project.Site_Area, Project.ID, WorkMain.ID
END
GO

ALTER PROCEDURE [dbo].[LLTC_Get_List_By_All_Project_And_By_Date]
	@WorkCountForDate as date
AS
BEGIN
	SELECT
		WorkCount.ID,
		Project.Site_Area,
		Project.Project_Name,
		WorkTypeMain.CS_WorkTypeMain,
		WorkType.SubWorkType,
		LLTC.Main_Name_LLTC,
		LLTC.Main_Name_Ower,
		LLTC.Main_Status,
		LLTC.Main_Number,
		WorkCount.tb_WorkCountForDate,
		WorkCount.tb_mTotalCount,
		WorkCount_Sub.CS_tbNumberDailyCount
	FROM [CWD].[dbo].[CS_tbWorkCount] as WorkCount
	inner join [CWD].[dbo].[CS_tbWorkCount_Sub] as WorkCount_Sub
	on WorkCount_Sub.CS_tbWorkCount_ID = WorkCount.ID
	inner join [CWD].[dbo].[Project] as Project
	on WorkCount.tb_WorkCountProject_ID = Project.ID
	inner join [CWD].[dbo].[LLTC] as LLTC
	on WorkCount_Sub.CS_LLTC_ID = LLTC.ID
	inner join [CWD].[dbo].CS_tbLLTCTypeSub as LLTCTypeSub
	on WorkCount_Sub.CS_tbLLTCTypeSub_ID = LLTCTypeSub.ID
	inner join [CWD].[dbo].CS_tbWorkTypeMain as WorkTypeMain
	on WorkTypeMain.ID = LLTC.Main_Name_Job
	inner join [CWD].[dbo].CS_tbWorkType as WorkType
	on WorkType.ID = LLTCTypeSub.CS_tbLLTCNameJobDetailsSub
	where WorkCount.tb_WorkCountForDate = @WorkCountForDate order by WorkCount.ID
END
GO
