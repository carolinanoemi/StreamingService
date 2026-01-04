USE StreamingService_WatchHistory;
GO

-- se data
SELECT TOP 10 *
FROM dbo.WatchEvents
ORDER BY WatchedAt DESC;
GO

-- TEST 1: SecondsWatched må ikke være negativ (skal fejle)
INSERT INTO dbo.WatchEvents (UserId, VideoId, WatchedAt, SecondsWatched, PositionSeconds, DeviceType)
VALUES (1, 2002, SYSUTCDATETIME(), -5, 10, 'mobile');
GO

-- TEST 2: PositionSeconds må ikke være negativ (skal fejle)
INSERT INTO dbo.WatchEvents (UserId, VideoId, WatchedAt, SecondsWatched, PositionSeconds, DeviceType)
VALUES (1, 2002, SYSUTCDATETIME(), 30, -1, 'desktop');
GO

-- TEST 3: DeviceType skal være en af de tilladte værdier (skal fejle)
INSERT INTO dbo.WatchEvents (UserId, VideoId, WatchedAt, SecondsWatched, PositionSeconds, DeviceType)
VALUES (1, 2002, SYSUTCDATETIME(), 30, 10, 'fridge');
GO

-- TEST 4: Valid insert (skal virke)
INSERT INTO dbo.WatchEvents (UserId, VideoId, WatchedAt, SecondsWatched, PositionSeconds, DeviceType)
VALUES (2, 2003, SYSUTCDATETIME(), 120, 50, 'tv');
GO
