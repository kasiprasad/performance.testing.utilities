USE [LoadTest2010]
GO

/****** Object:  StoredProcedure [dbo].[Reporting_GetRequestsBySec]    Script Date: 02/06/2012 21:35:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Reporting_GetRequestsBySec]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Reporting_GetRequestsBySec]
GO

USE [LoadTest2010]
GO

/****** Object:  StoredProcedure [dbo].[Reporting_GetRequestsBySec]    Script Date: 02/06/2012 21:35:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Reporting_GetRequestsBySec]
	@LoadTestRunId	int
AS
BEGIN

WITH Summary (TestCaseName, ScenarioName, RequestUri, RequestsPerSec, Date1SecIncrement)
AS (

	SELECT 
	TestCaseName,
	ScenarioName,
	RequestUri,
	RequestsPerSec,
	Date1SecIncrement
	FROM (
					SELECT CAST(CONVERT(CHAR(19), detail.[Timestamp], 120) AS DATETIME) as Date1SecIncrement,
					COUNT(*) as RequestsPerSec,
					testCase.TestCaseName as TestCaseName,
					scenario.ScenarioName as ScenarioName,
					map.RequestUri as RequestUri
					
					FROM WebLoadTestRequestMap map WITH (NOLOCK)

					INNER JOIN LoadTestPageDetail detail WITH (NOLOCK)
					ON detail.LoadTestRunId = map.LoadTestRunId
					AND detail.PageId = map.RequestId

					INNER JOIN LoadTestTestDetail testDetail WITH (NOLOCK)
					ON testDetail.TestDetailId = detail.TestDetailId 
					AND testDetail.LoadTestRunId = detail.LoadTestRunId

					INNER JOIN LoadTestCase testCase WITH (NOLOCK)
					ON testCase.TestCaseId = testDetail.TestCaseId 
					AND testCase.LoadTestRunId = testDetail.LoadTestRunId
					
					INNER JOIN LoadTestScenario scenario WITH (NOLOCK)
					ON scenario.ScenarioId = testCase.ScenarioId AND 
					scenario.LoadTestRunId = map.LoadTestRunId

					WHERE map.LoadTestRunId = @LoadTestRunId
					GROUP BY CAST(CONVERT(CHAR(19), detail.[Timestamp], 120) AS DATETIME), TestCaseName, ScenarioName, RequestUri
	) SQ
	GROUP BY Date1SecIncrement, RequestsPerSec, RequestUri, TestCaseName, ScenarioName
)

SELECT TestCaseName, ScenarioName, RequestUri, RequestsPerSec, Date1SecIncrement FROM Summary 
ORDER BY TestCaseName, RequestUri, Date1SecIncrement

END

GO


