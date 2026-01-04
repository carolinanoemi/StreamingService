USE StreamingService_WatchHistory;
GO

-- Seneste watch events for en user
DECLARE @userId INT = 1;
SELECT TOP 20 *
FROM WatchEvents
WHERE UserId = @userId
ORDER BY WatchedAt DESC;

-- Watch time pr user pr dag
SELECT
  UserId,
  CAST(WatchedAt AS date) AS WatchDate,
  SUM(SecondsWatched) AS TotalSeconds
FROM WatchEvents
GROUP BY UserId, CAST(WatchedAt AS date)
ORDER BY WatchDate DESC;
GO
