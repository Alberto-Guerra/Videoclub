using Videoclub.API.Model;

namespace Videoclub.API.Data.Repositories.Interfaces;

public interface ICategoryRepository
{
    IEnumerable<Category> GetCategories();
    Category? GetCategoryByName(string name);
}
