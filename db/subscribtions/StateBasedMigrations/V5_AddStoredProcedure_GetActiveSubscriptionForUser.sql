USE StreamingService_Subscriptions;
GO

CREATE OR ALTER PROCEDURE dbo.GetActiveSubscriptionForUser
  @UserId int
AS
BEGIN
  SET NOCOUNT ON;

  SELECT TOP 1 *
  FROM dbo.Subscriptions
  WHERE UserId = @UserId AND IsActive = 1
  ORDER BY StartDate DESC;
END
GO
