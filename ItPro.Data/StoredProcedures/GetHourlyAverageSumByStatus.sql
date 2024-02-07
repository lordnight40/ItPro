USE [ItPro]
GO
IF EXISTS(
	SELECT
		[type_desc],
		[type]
	FROM [sys].[procedures] WITH(NOLOCK)
	WHERE [name] = 'GetHourlyAverageSumByStatus'
	  AND [type] = 'p'
) DROP PROCEDURE [dbo].[GetHourlyAverageSumByStatus]
GO
CREATE PROCEDURE [dbo].[GetHourlyAverageSumByStatus]
@status nvarchar(max)
AS
BEGIN
	SELECT
		[i].[Hour]									AS [Start],
		CAST(AVG([o].[Amount]) AS DECIMAL(10,2))	AS [Average]
	FROM [Orders] AS [o] WITH (NOLOCK)
			 CROSS APPLY (
		SELECT
			CAST(DATEADD(HOUR, [s].[value], '00:00') AS TIME) AS [Hour]
		FROM GENERATE_SERIES(0, 23, 1) AS [s]
	) AS [i] --ON CAST([o].[CreatedAt] AS TIME) BETWEEN [i].[Hour] AND DATEADD(MINUTE, 59, [i].[Hour])
	WHERE LOWER([o].[Status]) = LOWER(@status)
	  AND CAST([o].[CreatedAt] AS TIME) BETWEEN [i].[Hour] AND DATEADD(MINUTE, 59, [i].[Hour])
	GROUP BY [i].[Hour]
	ORDER BY [Start] ASC;
END
GO