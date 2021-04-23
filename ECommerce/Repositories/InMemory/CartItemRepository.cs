using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Models.Domain.Cart;
using ECommerce.Repositories.Contracts;
using Microsoft.Extensions.Caching.Memory;

namespace ECommerce.Repositories.InMemory
{
    public class CartItemRepository:ICartItemRepository
    {
        private readonly IMemoryCache _memoryCache;
        private static readonly IList CartItemList = new List<CartItem>();
        private const string CartItemCacheKey = "CartItemCacheKey";

        public CartItemRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public CartItem FindCartItemById(long id)
        {
            /*List<CartItem> cartItems = _memoryCache.Get<List<CartItem>>(CartItemCacheKey);*/
            var cartItems = GetDataFromCache();
            var cartItem = cartItems.Find(c => c.Id == id);
            return cartItem;
        }

        public IEnumerable<CartItem> FindCartItemsByCartId(long cartId)
        {
            /*List<CartItem> cartItems = _memoryCache.Get<List<CartItem>>(CartItemCacheKey);*/
            var cartItems = GetDataFromCache();
            var cartItemsByCarId = cartItems.Where(c => c.CartId == cartId);
            return cartItemsByCarId;
        }

        public void SaveCartItem(CartItem cartItem)
        {
            CartItemList.Add(cartItem);
            CacheData();
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            var cartItemToDelete = FindCartItemById(cartItem.Id);
            DeleteCartItem(cartItemToDelete);
            SaveCartItem(cartItem);
        }

        public void DeleteCartItem(CartItem cartItem)
        {
            CartItemList.Remove(cartItem);
            CacheData();
        }

        public List<CartItem> GetDataFromCache()
        {
            return _memoryCache.Get<List<CartItem>>(CartItemCacheKey);
        }

        public void CacheData()
        {
            _memoryCache.Set(CartItemCacheKey, CartItemList);
        }

    }
}
