
-- 1. Foreign Key (Subscription -> Plan)
ALTER TABLE Subscriptions
ADD CONSTRAINT FK_Subscriptions_Plans 
FOREIGN KEY (PlanId) REFERENCES Plans(PlanId);
GO

-- 2. Check Constraint (Pris må ikke være minus)
ALTER TABLE Plans
ADD CONSTRAINT CK_Plans_Price 
CHECK (PricePerMonth >= 0);
GO

-- 3. Check Constraint (Slutdato skal være efter startdato)
ALTER TABLE Subscriptions
ADD CONSTRAINT CK_Subscriptions_EndDate 
CHECK (EndDate IS NULL OR EndDate >= StartDate);
GO
