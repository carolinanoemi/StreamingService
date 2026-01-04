ALTER TABLE WatchEvents
ADD CONSTRAINT CK_WatchEvents_SecondsWatched CHECK (SecondsWatched >= 0);

ALTER TABLE WatchEvents
ADD CONSTRAINT CK_WatchEvents_PositionSeconds CHECK (PositionSeconds >= 0);

ALTER TABLE WatchEvents
ADD CONSTRAINT CK_WatchEvents_DeviceType
CHECK (DeviceType IN ('mobile','desktop','tv','tablet'));