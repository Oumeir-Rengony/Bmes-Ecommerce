namespace ECommerce.Models.Domain.Cart
{
    public class CartItem:BaseObject
    {
        public long CartId { get; set; }
        public Cart Cart { get; set; }
        public long ProductId { get; set; }
        public Product.Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
