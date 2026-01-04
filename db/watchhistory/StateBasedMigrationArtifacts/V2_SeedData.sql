USE StreamingService_WatchHistory;
GO

INSERT INTO WatchEvents (UserId, VideoId, WatchedAt, SecondsWatched, PositionSeconds, DeviceType)
VALUES
(1, 2002, SYSUTCDATETIME(), 120, 120, 'desktop'),
(1, 2002, DATEADD(day,-1, SYSUTCDATETIME()), 300, 420, 'mobile'),
(2, 2003, SYSUTCDATETIME(), 60, 60, 'tv');
GO
