USE StreamingService_Subscriptions;
GO

-- Aktive subscriptions pr PlanId
SELECT
  PlanId,
  COUNT(*) AS ActiveSubscriptions
FROM Subscriptions
WHERE IsActive = 1
GROUP BY PlanId;

-- Aktive subscribtions pr PlanId med Plan navn
SELECT
  p.Name,
  COUNT(*) AS ActiveSubscriptions
FROM Subscriptions s
JOIN Plans p ON p.PlanId = s.PlanId
WHERE s.IsActive = 1
GROUP BY p.Name;
GO
