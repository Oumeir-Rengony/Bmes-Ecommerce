using ECommerce.Models;
using ECommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Models.Domain.Product;
using ECommerce.Services.Contracts;
using ECommerce.ViewModels.Catalogue;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {

        private ICatalogueService _catalogueService;
        private ICartService _cartService;

        public HomeController(ICatalogueService catalogueService, ICartService cartService)
        {
            _catalogueService = catalogueService;
            _cartService = cartService;
        }

        public IActionResult Index(string category_slug = "all-categories", string brand_slug = "all-brands", int page = 1)
        {
            ViewData["SelectedCategory"] = category_slug;
            ViewData["SelectedBrand"] = brand_slug;
            ViewData["Page"] = page;

            ViewData["CartTotal"] = _cartService.GetCartTotal();
            ViewData["CartItemsCount"] = _cartService.CartItemsCount();
            ViewData["CartItems"] = _cartService.GetCartItems();

            var pagedProducts = _catalogueService.FetchProducts(category_slug, brand_slug, page);

            return View(pagedProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
