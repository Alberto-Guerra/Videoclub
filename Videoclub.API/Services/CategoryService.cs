using Videoclub.API.Data.Repositories.Interfaces;
using Videoclub.API.Model;
using Videoclub.API.Services.Interfaces;

namespace Videoclub.API.Services;

public class CategoryService : ICategoryService
{
    private ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    //gets all the categories in the database
    public IEnumerable<Category> GetCategories()
    {
        return _categoryRepository.GetCategories();
    }

    public Category GetCategoryByName(string name)
    {
        Category? category = _categoryRepository.GetCategoryByName(name);
        if (category == null)
        {
            throw new InvalidOperationException("Category does not exist");
        }
        return category;
    }
}
