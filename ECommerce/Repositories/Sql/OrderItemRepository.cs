using System.Collections.Generic;
using System.Linq;
using ECommerce.Data;
using ECommerce.Models.Domain.Order;
using ECommerce.Repositories.Contracts;

namespace ECommerce.Repositories.Sql
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private ApplicationDbContext _context;

        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public OrderItem FindOrderItemById(long id)
        {
            var orderItem =  _context.OrderItems.Find(id);
            return orderItem;
        }

        public IEnumerable<OrderItem> FindOrderItemByOrderId(long orderId)
        {
            var orderItems = _context.OrderItems.Where(o => o.OrderId == orderId);
            return orderItems;
        }

        public IEnumerable<OrderItem> GetAllOrderItems()
        {
            var orderItems = _context.OrderItems;
            return orderItems;
        }

        public void SaveOrderItem(OrderItem orderItem)
        {
             _context.OrderItems.Add(orderItem);
             _context.SaveChanges();
        }

        public void  UpdateOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
             _context.SaveChanges();
        }

        public void  DeleteOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Remove(orderItem);
             _context.SaveChanges();
        }
    }
}
