using System.Collections.Generic;
using ECommerce.Data;
using ECommerce.Models.Domain.Product;
using ECommerce.Repositories.Contracts;

namespace ECommerce.Repositories.Sql
{
    public class SqlCategoryRepository: ICategoryRepository

    {
    private ApplicationDbContext _context;

    public SqlCategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Category FindCategoryById(long id)
    {
        var note = _context.Categories.Find(id);
        return note;
    }

    public IEnumerable<Category> GetAllCategories()
    {
        var notes = _context.Categories;
        return notes;
    }

    public void SaveCategory(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
    }

    public void UpdateCategory(Category category)
    {
        _context.Categories.Update(category);
        _context.SaveChanges();
    }

    public void DeleteCategory(Category category)
    {
        _context.Categories.Remove(category);
        _context.SaveChanges();
    }
    }
}
