USE [ItPro]
GO
CREATE PROCEDURE [dbo].[GetBirthdayReceiptSumByClients]
AS
BEGIN
    SELECT
        [c].[Id]				AS [Id],
        [c].[Name]				AS [Name],
        [c].[Surname]			AS [Surname],
        [c].[BirthDay]			AS [BirthDay],
        SUM([o].[Amount])		AS [Sum]
    FROM [Orders] AS [o]
             INNER JOIN [Clients] AS [c]
                        ON [c].[Id] = [o].[ClientId]
    WHERE [o].[Status] = 'Completed'
      AND (
                MONTH([c].[BirthDay]) = MONTH([o].[CreatedAt])
            AND
                DAY([c].[BirthDay]) = DAY([o].[CreatedAt])
        )
    GROUP BY [c].[Id], [c].[Name], [c].[Surname], [BirthDay]
    ORDER BY [Sum] DESC;
END
GO