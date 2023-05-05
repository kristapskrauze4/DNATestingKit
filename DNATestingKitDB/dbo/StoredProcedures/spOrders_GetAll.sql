CREATE PROCEDURE [dbo].[spOrders_GetAll]
AS
BEGIN
	SELECT * FROM dbo.[Orders];
END