USE StreamingService_Subscriptions;
GO

-- TEST 1: PricePerMonth må ikke være negativ (skal fejle pga CK_Plans_Price)
INSERT INTO dbo.Plans (Name, PricePerMonth, IsActive)
VALUES (N'BadPlan', -10.00, 1);
GO

-- TEST 2: Subscription må ikke pege på en PlanId der ikke findes (skal fejle pga FK_Subscriptions_Plans)
INSERT INTO dbo.Subscriptions (UserId, PlanId, StartDate, EndDate, IsActive)
VALUES (1, 999999, '2025-01-01', NULL, 1);
GO

-- TEST 3: EndDate må ikke være før StartDate (skal fejle pga CK_Subscriptions_EndDate)
INSERT INTO dbo.Subscriptions (UserId, PlanId, StartDate, EndDate, IsActive)
VALUES (1, 1, '2025-01-10', '2025-01-01', 1);
GO

-- TEST 4: Valid subscription (skal virke)
INSERT INTO dbo.Subscriptions (UserId, PlanId, StartDate, EndDate, IsActive)
VALUES (2, 1, '2025-01-01', NULL, 1);
GO

-- Tjek at den sidste faktisk kom ind
SELECT TOP 10 *
FROM dbo.Subscriptions
ORDER BY SubscriptionId DESC;
GO
