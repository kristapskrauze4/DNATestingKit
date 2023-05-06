using DatabaseAccess.Models;

namespace DatabaseAccess.Data
{
    public interface IOrdersData
    {
        Task<IEnumerable<OrderModel?>> GetOrdersByCustomerId(int customerId);
        Task InsertOrder(OrderModel order);
    }
}