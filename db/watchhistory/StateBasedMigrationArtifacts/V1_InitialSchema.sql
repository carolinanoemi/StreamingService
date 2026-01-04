CREATE DATABASE StreamingService_WatchHistory;
GO
USE StreamingService_WatchHistory;
GO

CREATE TABLE WatchEvents (
    WatchEventId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    VideoId INT NOT NULL,
    WatchedAt DATETIME2 NOT NULL,
    SecondsWatched INT NOT NULL,
    PositionSeconds INT NOT NULL,
    DeviceType VARCHAR(20) NOT NULL
);
GO
