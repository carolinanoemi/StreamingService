EXEC dbo.AddWatchEvent 
    @UserId = 44, 
    @VideoId = 100, 
    @WatchedAt = '2025-12-25 12:00:00', 
    @SecondsWatched = 300, 
    @PositionSeconds = 650, 
    @DeviceType = 'Mobile';