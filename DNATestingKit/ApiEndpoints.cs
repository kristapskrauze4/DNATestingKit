using DatabaseAccess.Data;
using DatabaseAccess.Models;

namespace DNATestingKit
{
    public static class ApiEndpoints
    {
        public static void ConfigureApiEndpoints( this WebApplication app)
        {
            app.MapGet("/Orders/{customerId}", GetOrdersByCustomerId);
            app.MapPost("/Orders", InsertOrder);
        }

        private static async Task<IResult> GetOrdersByCustomerId(int customerId, IOrdersData data)
        {
            try
            {
                var result = await data.GetOrdersByCustomerId(customerId);

                if (result == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> InsertOrder(InsertOrderModel order, IOrdersData data)
        {
            try
            {
                var orderModel = new OrderModel()
                {
                    CustomerId = order.CustomerId,
                    Amount = order.Amount,
                    DeliveryDate = order.DeliveryDate
                };

                await data.InsertOrder(orderModel);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private class InsertOrderModel
        {
            public int CustomerId { get; set; }
            public int Amount { get; set; }
            public DateTime DeliveryDate { get; set; }
        }

    }
}
