using System.Collections.Generic;

namespace ECommerce.Models.Domain.Cart
{
    public class Cart:BaseObject
    {
        public string UniqueCartId { get; set; }
        public CartStatus CartStatus { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }
    }
}
