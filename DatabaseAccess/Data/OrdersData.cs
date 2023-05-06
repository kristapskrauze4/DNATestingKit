using DatabaseAccess.AggragateModels;
using DatabaseAccess.DbAccess;
using DatabaseAccess.Models;

namespace DatabaseAccess.Data
{
    public class OrdersData : IOrdersData
    {
        private readonly ISqlDataAccess _db;
        public OrdersData(ISqlDataAccess db)
        {
            _db = db;
        }
        public async Task<IEnumerable<OrderModel?>> GetOrdersByCustomerId(int customerId)
        {
            var results = await _db.GetData<OrderModel, dynamic>(
                "dbo.spOrders_GetByCustomerId",
                new { CustomerId = customerId });

            results.ToList().ForEach(x => 
            {
                if (x.Amount >= 10 && x.Amount < 50)
                {
                    x.Sum = Round(x.Amount * x.Price * Discounts.FivePercentDiscount);
                }
                else if (x.Amount >= 50)
                {
                    x.Sum = Round(x.Amount * x.Price * Discounts.FifteenPercentDiscount);
                }
                else
                {
                    x.Sum = Round(x.Amount * x.Price);
                }
            });

            return results;
        }

        public async Task InsertOrder(OrderModel order)
        {
            ValidateOrder(order);
            await _db.Save("dbo.spOrders_Insert", new { order.CustomerId, order.Amount, order.DeliveryDate });
        }

        private static void ValidateOrder(OrderModel order)
        {
            if (order == null)
            {
                throw new ArgumentException("Order is null");
            }

            if (order.DeliveryDate <= DateTime.Now)
            {
                throw new InvalidDataException("Delivery date is not in future");
            }

            if (order.Amount < 1 || order.Amount > 999)
            {
                throw new InvalidDataException("Amount must be between 1 and 999");
            }
        }

        private static double Round(double value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }
    }
}
