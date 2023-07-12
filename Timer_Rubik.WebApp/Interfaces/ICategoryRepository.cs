using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();

        Category GetCategory(Guid categoryId);

        Category GetCategory(string categoryName);

        bool CategoryExists(Guid categoryId);
    }
}
