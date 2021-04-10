using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Services.Contracts;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {

        private ICatalogueService _catalogueService;

        public HomeController(ICatalogueService catalogueService)
        {
            _catalogueService = catalogueService;
        }

        public IActionResult Index(string category_slug = "all-categories", string brand_slug = "all-brands", int page = 1)
        {
            ViewData["SelectedCategory"] = category_slug;
            ViewData["SelectedBrand"] = brand_slug;

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
