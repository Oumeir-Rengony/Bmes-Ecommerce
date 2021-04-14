namespace ECommerce.Models.Order
{
    using System.Collections.Generic;
    using ECommerce.Models.Customer;

    public class Order : BaseObject
    {
        public decimal OrderTotal { get; set; }
        public decimal OrderItemTotal { get; set; }
        public decimal ShippingCharge { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
