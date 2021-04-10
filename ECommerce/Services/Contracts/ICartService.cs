using ECommerce.Models.Cart;
using ECommerce.ViewModels.Cart;
using System.Collections.Generic;

namespace ECommerce.Services.Contracts
{
    public interface ICartService
    {
        string UniqueCartId();
        Cart GetCart();
        void AddToCart(AddToCartViewModel addToCartViewModel);
        void RemoveFromCart(RemoveFromCartViewModel removeFromCartViewModel);
        IEnumerable<CartItem> GetCartItems();
        int CartItemsCount();
        decimal GetCartTotal();
    }
}
