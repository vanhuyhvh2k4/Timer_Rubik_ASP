using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Interfaces.Repository
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();

        Category GetCategory(Guid categoryId);

        Category GetCategory(string categoryName);

        bool CategoryExists(Guid categoryId);

        bool CreateCategory(Category category);

        bool Save();

        bool UpdateCategory(Guid categoryId, Category category);

        bool DeleteCategory(Category category);
    }
}
