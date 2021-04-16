using System.Collections.Generic;
using ECommerce.Models.Domain.Product;
using ECommerce.ViewModels.Catalogue;

namespace ECommerce.Services.Contracts
{
    public interface ICatalogueService
    {
        PagedProductViewModel FetchProducts(string categorySlug, string brandSlug, int page);
    }
}
