namespace ECommerce.Models.Domain.Order
{
    public class OrderItem : BaseObject
    {
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public long ProductId { get; set; }
        public Product.Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
