using System.Collections.Generic;
using ECommerce.Models.Domain.Cart;

namespace ECommerce.Repositories.Contracts
{
    public interface ICartItemRepository
    {
        CartItem FindCartItemById(long id);
        IEnumerable<CartItem> FindCartItemsByCartId(long cartId);
        void SaveCartItem(CartItem cartItem);
        void UpdateCartItem(CartItem cartItem);
        void DeleteCartItem(CartItem cartItem);
    }
}
