using System;
using System.Collections.Generic;
using ECommerce.Models.Domain.Product;

namespace ECommerce.Repositories.Contracts
{
    public interface IBrandRepository
    {
        Brand FindBrandById(Guid id);
        IEnumerable<Brand> GetAllBrands();
        void SaveBrand(Brand brand);
        void UpdateBrand(Brand brand);
        void DeleteBrand(Brand brand);
    }
}
