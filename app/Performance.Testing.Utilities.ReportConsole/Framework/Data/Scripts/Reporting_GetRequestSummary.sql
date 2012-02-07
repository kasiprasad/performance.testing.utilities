USE [LoadTest2010]
GO

/****** Object:  StoredProcedure [dbo].[Reporting_GetRequestSummary]    Script Date: 02/06/2012 21:35:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Reporting_GetRequestSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Reporting_GetRequestSummary]
GO

USE [LoadTest2010]
GO

/****** Object:  StoredProcedure [dbo].[Reporting_GetRequestSummary]    Script Date: 02/06/2012 21:35:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		KASI
-- Create date: 01/17/2012
-- Description:	SELECT's based on Command Line Generated Report input RunId
-- =============================================
CREATE PROCEDURE [dbo].[Reporting_GetRequestSummary]
	@LoadTestRunId int
AS
BEGIN


	
SELECT

testCase.TestCaseName,
scenario.ScenarioName,
map.RequestUri,

data.[PageCount] as TotalRequests,
data.[Average] as AverageResponseTime,
data.[Minimum] as MinimumResponseTime,
data.[Maximum] as MaximumResponseTime,
data.Percentile90,
data.Percentile95,
data.Percentile99,
data.Median,
data.StandardDeviation


FROM LoadTestPageSummaryData data WITH (NOLOCK)

INNER JOIN WebLoadTestRequestMap map WITH (NOLOCK)
	ON map.LoadTestRunId = data.LoadTestRunId
	AND map.RequestId = data.PageId

INNER JOIN LoadTestCase testCase WITH (NOLOCK)
	ON testCase.LoadTestRunId = map.LoadTestRunId
	AND testCase.TestCaseId = map.TestCaseId
	
INNER JOIN LoadTestScenario scenario WITH (NOLOCK)
	ON scenario.LoadTestRunId = data.LoadTestRunId
	AND scenario.ScenarioId = testCase.ScenarioId
	
		
WHERE data.LoadTestRunId = @LoadTestRunId
ORDER BY testCase.TestCaseName, scenario.ScenarioName

END





GO


