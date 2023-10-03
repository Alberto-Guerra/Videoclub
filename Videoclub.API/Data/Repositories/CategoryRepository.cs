using Videoclub.API.Context;
using Videoclub.API.Data.Repositories.Interfaces;
using Videoclub.API.Model;

namespace Videoclub.API.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private VideoclubContext _context;

    public CategoryRepository(VideoclubContext context)
    {
        _context = context;
    }

    public IEnumerable<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }

    public Category? GetCategoryByName(string name)
    {
        return _context.Categories.FirstOrDefault(c => c.Name == name);
    }
}
