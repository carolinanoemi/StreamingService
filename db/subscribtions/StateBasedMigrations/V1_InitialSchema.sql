CREATE DATABASE StreamingService_Subscriptions;
GO
USE StreamingService_Subscriptions;
GO

CREATE TABLE Plans (
    PlanId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    PricePerMonth DECIMAL(10,2) NOT NULL,
    IsActive BIT NOT NULL,
);
GO

CREATE TABLE Subscriptions (
    SubscriptionId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    PlanId INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NULL,
    IsActive BIT NOT NULL,
);
GO

