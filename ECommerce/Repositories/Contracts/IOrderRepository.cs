using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Models.Order;

namespace ECommerce.Repositories.Contracts
{
    public interface IOrderRepository
    {
        Order FindOrderById(long id);
        IEnumerable<Order> GetAllOrders();
        void SaveOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
