ALTER PROCEDURE [dbo].[LLTC_Get_List_By_All_Project]
AS
BEGIN
	SELECT
		  LLTC.[Main_Name_LLTC],
		  LLTC.Main_Name_Ower,
		  LLTC.Main_Total_Number,
		  LLTC.Main_Number,
		  WorkMain.CS_WorkTypeMain,
		  LLTC.Main_Name_Job,
		  LLTC_Sub.CS_tbLLTCNameSiteID,
		  Project.Project_Name,
		  Project.Site_Area
	  FROM [CWD].[dbo].[LLTC] as LLTC
	  inner join [CWD].[dbo].[CS_tbLLTCTypeSub] as LLTC_Sub
	  on LLTC.ID = LLTC_Sub.CS_tbLLTC_ID
	  inner join [CWD].[dbo].[Project] as Project
	  on LLTC_Sub.CS_tbLLTCNameSiteID = Project.ID
	   inner join [CWD].[dbo].[CS_tbWorkTypeMain] as WorkMain
	  on LLTC.Main_Name_Job = WorkMain.ID
	  order by  Project.Site_Area, Project.ID, WorkMain.ID
END
GO
