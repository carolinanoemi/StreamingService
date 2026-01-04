USE StreamingService_Subscriptions;
GO

CREATE OR ALTER PROCEDURE dbo.GetActiveSubscriptionsPerPlan
AS
BEGIN
  SET NOCOUNT ON;

  SELECT
    p.Name,
    COUNT(*) AS ActiveSubscriptions
  FROM dbo.Subscriptions s
  JOIN dbo.Plans p ON p.PlanId = s.PlanId
  WHERE s.IsActive = 1
  GROUP BY p.Name;
END
GO
