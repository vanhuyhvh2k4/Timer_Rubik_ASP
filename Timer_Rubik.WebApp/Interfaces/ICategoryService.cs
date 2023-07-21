using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Interfaces
{
    public interface ICategoryService
    {
        ICollection<Category> GetCategories();

        Category GetCategory(Guid categoryId);

        Category GetCategory(string categoryName);

        bool CategoryExists(Guid categoryId);

        bool CreateCategory(Category category);

        bool Save();

        bool UpdateCategory(Category category);

        bool DeleteCategory(Category category);
    }
}
