using Videoclub.API.Model;

namespace Videoclub.API.Services.Interfaces;

public interface ICategoryService
{
    IEnumerable<Category> GetCategories();
    Category GetCategoryByName(string name);
}
