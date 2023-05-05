CREATE PROCEDURE [dbo].[spOrders_Insert]
	@CustomerId int,
	@Amount int,
	@DeliveryDate DateTime
AS
BEGIN
	insert into dbo.[Orders] (CustomerId, Amount, DeliveryDate)
	values (@CustomerId, @Amount, @DeliveryDate)
END
