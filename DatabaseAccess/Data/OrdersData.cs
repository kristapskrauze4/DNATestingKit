using DatabaseAccess.DbAccess;
using DatabaseAccess.Models;
using System.Threading.Tasks;

namespace DatabaseAccess.Data
{
    public class OrdersData : IOrdersData
    {
        private readonly ISqlDataAccess _db;
        public OrdersData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<OrderModel>> GetOrders() =>
            _db.GetData<OrderModel, dynamic>("dbo.spOrders_GetAll", new { });

        public async Task<IEnumerable<OrderModel?>> GetOrdersByCustomerId(int customerId)
        {
            var results = await _db.GetData<OrderModel, dynamic>(
                "dbo.spOrders_GetByCustomerId",
                new { CustomerId = customerId });

            return results;
        }

        public Task InsertOrder(OrderModel order) =>
            _db.Save("dbo.spOrders_Insert", new { order.CustomerId, order.Amount, order.DeliveryDate });
    }
}
