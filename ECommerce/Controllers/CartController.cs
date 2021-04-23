using System.Threading.Tasks;
using ECommerce.Services;
using ECommerce.Services.Contracts;
using ECommerce.ViewModels.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> CartDetail()
        {
            ViewData["CartTotal"] = await _cartService.GetCartTotal();
            ViewData["CartItemsCount"] = await _cartService.CartItemsCount();
            ViewData["CartItems"] = await _cartService.GetCartItems();

            return View();
        }

        [HttpPost]
        public IActionResult RemoveCartItem(RemoveFromCartViewModel removeFromCartViewModel)
        {
            _cartService.RemoveFromCart(removeFromCartViewModel);
            return RedirectToAction("CartDetail");
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToCart(AddToCartViewModel addToCartViewModel)
        {
            await _cartService.AddToCart(addToCartViewModel);

            return RedirectToAction("Index", "Home", new { category_slug = addToCartViewModel.CategorySlug, brand_slug = addToCartViewModel.BrandSlug, page = addToCartViewModel.Page });

        }
    }
}