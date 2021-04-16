using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models.Domain.Product;
using ECommerce.Models.Helper;
using ECommerce.Repositories.Contracts;
using ECommerce.Services.Contracts;
using ECommerce.ViewModels.Catalogue;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Services
{
    public class CatalogueService : ICatalogueService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private const int _productPerPage = 9;

        public CatalogueService(IBrandRepository brandRepository,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository)
        {
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }
        public PagedProductViewModel FetchProducts(string categorySlug, string brandSlug, int productPage)
        {
            var brands = _brandRepository.GetAllBrands();
            var categories = _categoryRepository.GetAllCategories();
            var products = _productRepository.GetAllProducts();

            var filter = new FilterModel
            {
                CategorySlug = categorySlug,
                BrandSlug = brandSlug
            };

            IEnumerable<Product> filteredProducts = FilterProducts(filter, products).ToList();
            IEnumerable<Product> productsByPage = GetProductsByPage(filteredProducts, productPage).ToList();

            var totalPages =  GetTotalPages(filteredProducts);
            PaginationViewModel pagination = CreatePaginationViewModel(productsByPage, productPage, totalPages);
            PagedProductViewModel pagedProducts = CreatePagedProductViewModel(pagination, brands, categories);

            return pagedProducts;
        }

        public IEnumerable<Product> FilterProducts(FilterModel filter, IEnumerable<Product> products)
        {

            if (filter.CategorySlug != "all-categories")
            {
                products = products.Where(product => product.Category.Slug == filter.CategorySlug);
            }

            if (filter.BrandSlug != "all-brands")
            {
                products = products.Where(product => product.Brand.Slug == filter.BrandSlug);
            }

            return products;
        }

        public IEnumerable<Product> GetProductsByPage(IEnumerable<Product> products, int productPage)
        {
            return products
                .Skip((productPage - 1) * _productPerPage)
                .Take(_productPerPage);
        }

        public int GetTotalPages(IEnumerable<Product> product)
        {
            var productCount = product.Count();
            var totalPages = (int) Math.Ceiling((decimal)productCount / _productPerPage);

            return totalPages;
        }

        public PaginationViewModel CreatePaginationViewModel(IEnumerable<Product> products, int productPage, int totalPages)
        {
            int[] pages = Enumerable.Range(1, totalPages).ToArray();

            var pagedProduct = new PaginationViewModel
            {
                Products = products,
                HasPreviousPages = (productPage > 1),
                CurrentPage = productPage,
                HasNextPages = (productPage < totalPages),
                Pages = pages
            };

            return pagedProduct;
        }

        public PagedProductViewModel CreatePagedProductViewModel(
            PaginationViewModel paginationViewModel,
            IEnumerable<Brand> brands,
            IEnumerable<Category> categories)
        {
            var pagedProducts = new PagedProductViewModel
            {
                PagedProducts = paginationViewModel,
                Brands = brands,
                Categories = categories
            };

            return pagedProducts;
        }

    }
}
