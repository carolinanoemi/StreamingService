USE StreamingService_WatchHistory;
GO

CREATE OR ALTER PROCEDURE dbo.GetRecentWatchEventsForUser
  @UserId int
AS
BEGIN
  SET NOCOUNT ON;

  SELECT TOP 20 *
  FROM dbo.WatchEvents
  WHERE UserId = @UserId
  ORDER BY WatchedAt DESC;
END
GO
