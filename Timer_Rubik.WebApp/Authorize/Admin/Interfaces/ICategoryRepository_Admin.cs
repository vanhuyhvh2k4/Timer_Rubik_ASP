using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Interfaces
{
    public interface ICategoryRepository_Admin
    {
        ICollection<Category> GetCategories();

        Category GetCategory(Guid categoryId);

        Category GetCategory(string categoryName);

        bool CategoryExists(Guid categoryId);

        bool CreateCategory(Category category);

        bool Save();

        bool UpdateCategory(Category category);
    }
}
