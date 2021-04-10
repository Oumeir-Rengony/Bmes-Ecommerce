using ECommerce.Services;
using ECommerce.Services.Contracts;
using ECommerce.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult CartDetail()
        {
            ViewData["CartTotal"] = _cartService.GetCartTotal();
            ViewData["CartItemsCount"] = _cartService.CartItemsCount();
            ViewData["CartItems"] = _cartService.GetCartItems();

            return View();
        }

        [HttpPost]
        public IActionResult RemoveCartItem(RemoveFromCartViewModel removeFromCartViewModel)
        {
            _cartService.RemoveFromCart(removeFromCartViewModel);
            return RedirectToAction("CartDetail");
        }

        [HttpPost]
        public IActionResult AddItemToCart(AddToCartViewModel addToCartViewModel)
        {
            _cartService.AddToCart(addToCartViewModel);

            return RedirectToAction("Index", "Home", new { category_slug = addToCartViewModel.CategorySlug, brand_slug = addToCartViewModel.BrandSlug, page = addToCartViewModel.Page });

        }
    }
}