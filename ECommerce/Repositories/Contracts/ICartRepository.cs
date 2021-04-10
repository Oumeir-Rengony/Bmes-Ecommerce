using ECommerce.Models.Cart;
using System.Collections.Generic;

namespace ECommerce.Repositories.Contracts
{
    public interface ICartRepository
    {
        Cart FindCartById(long id);
        IEnumerable<Cart> GetAllCarts();
        void SaveCart(Cart cart);
        void UpdateCart(Cart cart);
        void DeleteCart(Cart cart);
    }
}
