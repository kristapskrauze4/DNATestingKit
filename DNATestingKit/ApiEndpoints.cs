using DatabaseAccess.Data;
using DatabaseAccess.Models;

namespace DNATestingKit
{
    public static class ApiEndpoints
    {
        public static void ConfigureApiEndpoints( this WebApplication app)
        {
            app.MapGet("/Orders", GetOrders);
            app.MapGet("/Orders/{customerId}", GetOrdersByCustomerId);
            app.MapPost("/Orders", InsertOrder);
        }

        private static async Task<IResult> GetOrders(IOrdersData data)
        {
            try
            {
                return Results.Ok(await data.GetOrders());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
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

        private static async Task<IResult> InsertOrder(OrderModel order, IOrdersData data)
        {
            try
            {
                await data.InsertOrder(order);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
