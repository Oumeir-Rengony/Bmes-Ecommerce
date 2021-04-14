using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Models.Order;

namespace ECommerce.Repositories.Contracts
{
    public interface IOrderItemRepository
    {
        OrderItem FindOrderItemById(long id);
        IEnumerable<OrderItem> FindOrderItemByOrderId(long orderId);
        IEnumerable<OrderItem> GetAllOrderItems();
        void SaveOrderItem(OrderItem orderItem);
        void UpdateOrderItem(OrderItem orderItem);
        void DeleteOrderItem(OrderItem orderItem);
    }
}
