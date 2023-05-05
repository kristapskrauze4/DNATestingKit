CREATE PROCEDURE [dbo].[spOrders_GetByCustomerId]
	@CustomerId int
AS
BEGIN
	SELECT * 
	FROM dbo.[Orders]
	WHERE CustomerId = @CustomerId;
END
