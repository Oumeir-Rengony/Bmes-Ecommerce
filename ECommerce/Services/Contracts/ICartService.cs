using ECommerce.ViewModels.Cart;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Models.Domain.Cart;

namespace ECommerce.Services.Contracts
{
    public interface ICartService
    {
        /*string UniqueCartId();
        Cart GetCart();*/
        Task<string> UniqueCartId();
        Task<Cart> GetCart();
        Task AddToCart(AddToCartViewModel addToCartViewModel);
        void RemoveFromCart(RemoveFromCartViewModel removeFromCartViewModel);
        /*IEnumerable<CartItem> GetCartItems();
        int CartItemsCount();
        decimal GetCartTotal();*/
        Task<IEnumerable<CartItem>> GetCartItems();
        Task<int> CartItemsCount();
        Task<decimal> GetCartTotal();
    }
}
