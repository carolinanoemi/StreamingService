USE StreamingService_Subscriptions;
GO

INSERT INTO Plans (Name, PricePerMonth, IsActive)
VALUES
(N'Basic', 49.00, 1),
(N'Pro', 99.00, 1);

INSERT INTO Subscriptions (UserId, PlanId, StartDate, EndDate, IsActive)
VALUES
(1, 2, '2025-12-01', NULL, 1),
(2, 1, '2025-11-01', NULL, 1),
(3, 1, '2025-10-01', '2025-11-01', 0);
GO
