-- WatchHistory Indexes

-- Gør det hurtigt at finde "Hvad har bruger X set?"
CREATE NONCLUSTERED INDEX IX_WatchEvents_UserId_WatchedAt
ON WatchEvents (UserId, WatchedAt DESC);
GO

-- bruges til "Global Activity Feed" (hvad bliver set mest lige nu på tværs af alle brugere)
CREATE NONCLUSTERED INDEX IX_WatchEvents_WatchedAt
ON WatchEvents (WatchedAt DESC);
GO

