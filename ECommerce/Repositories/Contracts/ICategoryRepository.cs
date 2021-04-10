using System.Collections.Generic;
using ECommerce.Models.Product;

namespace ECommerce.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        Category FindCategoryById(long id);
        IEnumerable<Category> GetAllCategories();
        void SaveCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
