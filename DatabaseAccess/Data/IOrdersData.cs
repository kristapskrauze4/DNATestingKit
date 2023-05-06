using DatabaseAccess.Models;
using DNATestingKit.Models;

namespace DatabaseAccess.Data
{
    public interface IOrdersData
    {
        Task<IEnumerable<OrderModel?>> GetOrdersByCustomerId(int customerId);
        Task InsertOrder(InsertOrderModel order);
    }
}