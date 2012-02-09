USE [LoadTest2010]
GO

/****** Object:  StoredProcedure [dbo].[Reporting_GetRequestDetails]    Script Date: 02/06/2012 21:35:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Reporting_GetRequestDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Reporting_GetRequestDetails]
GO

USE [LoadTest2010]
GO

/****** Object:  StoredProcedure [dbo].[Reporting_GetRequestDetails]    Script Date: 02/06/2012 21:35:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		KASI
-- Create date: 01/17/2012
-- Description:	SELECT's based on Command Line Generated Report input RunId
-- =============================================
CREATE PROCEDURE [dbo].[Reporting_GetRequestDetails]
	@RunId	varchar(50)
AS
BEGIN


DECLARE @LoadTestRunId int
SET @LoadTestRunId = (SELECT LoadTestRunId FROM LoadTestRun WITH (NOLOCK) WHERE RunId = @RunId)
	
SELECT 
testCase.TestCaseName,
scenario.ScenarioName,
map.RequestUri, 
detail.[Timestamp] as Page_StartTime,
detail.ResponseTime as Page_ResponseTime,
detail.EndTime as Page_EndTime,
testDetail.[Timestamp] as Test_StartTime,
testDetail.ElapsedTime as Test_ElapsedTime,
testDetail.EndTime as Test_EndTime,
testDetail.AgentId,
testDetail.UserId,
testDetail.Outcome

FROM WebLoadTestRequestMap map WITH (NOLOCK)

INNER JOIN LoadTestPageDetail detail WITH (NOLOCK)
ON detail.LoadTestRunId = map.LoadTestRunId

INNER JOIN LoadTestTestDetail testDetail WITH (NOLOCK)
ON testDetail.TestDetailId = detail.TestDetailId AND testDetail.LoadTestRunId = detail.LoadTestRunId

INNER JOIN LoadTestCase testCase WITH (NOLOCK)
ON testCase.TestCaseId = testDetail.TestCaseId AND testCase.LoadTestRunId = testDetail.LoadTestRunId

INNER JOIN LoadTestScenario scenario WITH (NOLOCK)
ON scenario.ScenarioId = testCase.ScenarioId AND scenario.LoadTestRunId = testCase.LoadTestRunId

WHERE map.LoadTestRunId = @LoadTestRunId
ORDER BY testDetail.UserId, detail.[Timestamp]


END

GO


