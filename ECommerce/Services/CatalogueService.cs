using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models.Product;
using ECommerce.Repositories.Contracts;
using ECommerce.Services.Contracts;
using ECommerce.ViewModels.Catalogue;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Services
{
    public class CatalogueService : ICatalogueService
    {
        private IBrandRepository _brandRepository;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private readonly HttpContext _httpContext;
        private const int _productPerPage = 9;

        public CatalogueService(IHttpContextAccessor httpContextAccessor,
            IBrandRepository brandRepository,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository)
        {
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _httpContext = httpContextAccessor.HttpContext;
        }
        public PagedProductViewModel FetchProducts(string categorySlug, string brandSlug, int productPage)
        {
            var brands = _brandRepository.GetAllBrands();
            var categories = _categoryRepository.GetAllCategories();
            var products = _productRepository.GetAllProducts();

            IEnumerable<Product> ProductsByPage = new List<Product>();
            int productCount = 0;

            if (categorySlug == "all-categories" && brandSlug == "all-brands")
            {
                ProductsByPage = filterProductsByPage(products, productPage);
                productCount = products.Count();
            }

            else if (categorySlug != "all-categories" && brandSlug != "all-brands")
            {
                var filteredProducts = products.Where(product => product.Category.Slug == categorySlug && 
                                                                 product.Brand.Slug == brandSlug);
                productCount = filteredProducts.Count();
                ProductsByPage = filterProductsByPage(filteredProducts, productPage);
            }

            else if (categorySlug != "all-categories" && brandSlug == "all-brands")
            {
                var filteredProducts = products.Where(product => product.Category.Slug == categorySlug);
                productCount = filteredProducts.Count();
                ProductsByPage = filterProductsByPage(filteredProducts, productPage);
            }

            else if (categorySlug == "all-categories" && brandSlug != "all-brands")
            {
                var filteredProducts = products.Where(product => product.Brand.Slug == brandSlug);
                productCount = filteredProducts.Count();
                ProductsByPage = filterProductsByPage(filteredProducts, productPage);
            }

            var totalPages = (int)Math.Ceiling((decimal)productCount / _productPerPage);

            int[] pages = Enumerable.Range(1, totalPages).ToArray();

            var pagedProduct = new PaginationViewModel
            {
                Products = ProductsByPage,
                HasPreviousPages = (productPage > 1),
                CurrentPage = productPage,
                HasNextPages = (productPage < totalPages),
                Pages = pages
            };

            var pagedProducts = new PagedProductViewModel
            {
                PagedProducts = pagedProduct,
                Brands = brands,
                Categories = categories
            };

            return pagedProducts;
        }

        public IEnumerable<Product> filterProductsByPage(IEnumerable<Product> products, int productPage)
        {
            return products
                .Skip((productPage - 1) * _productPerPage)
                .Take(_productPerPage);
        }

    }
}
