using System.Collections.Generic;
using ECommerce.Models.Domain.Product;

namespace ECommerce.ViewModels.Catalogue
{
    public class PagedProductViewModel
    {
        public PaginationViewModel PagedProducts { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
