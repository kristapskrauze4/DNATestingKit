IF NOT EXISTS (SELECT 1 FROM dbo.[Orders] WHERE Id = -1)
BEGIN
	INSERT INTO dbo.[Orders] (CustomerId, Amount, DeliveryDate)
	VALUES (9999998, 2, '05.05.2023')
END
