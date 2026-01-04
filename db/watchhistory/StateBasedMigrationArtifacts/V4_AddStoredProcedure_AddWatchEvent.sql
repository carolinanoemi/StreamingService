USE StreamingService_WatchHistory;
GO

CREATE OR ALTER PROCEDURE dbo.AddWatchEvent
  @UserId int,
  @VideoId int,
  @WatchedAt datetime2,
  @SecondsWatched int,
  @PositionSeconds int,
  @DeviceType varchar(20)
AS
BEGIN
  SET NOCOUNT ON;

  INSERT INTO dbo.WatchEvents (UserId, VideoId, WatchedAt, SecondsWatched, PositionSeconds, DeviceType)
  VALUES (@UserId, @VideoId, @WatchedAt, @SecondsWatched, @PositionSeconds, @DeviceType);
END
GO
