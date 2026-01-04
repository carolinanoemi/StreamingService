-- V6: Performance Indexes

-- 1. Analytics & Foreign Key support
-- Gør det hurtigt at finde antal subscribers pr. plan (til statistik)
CREATE INDEX IX_Subscription_PlanId
ON Subscriptions (PlanId);
GO

-- 2. "Active User History" Index 
-- Composite Index (sammensat index): Filtrer og find først UserId, dernæst IsActive, og sorter efter StartDate (nyeste først) - (sparer databasen CPU forbrug)
CREATE INDEX IX_Subscriptions_UserId_IsActive_StartDate
ON Subscriptions (UserId, IsActive, StartDate DESC);
GO