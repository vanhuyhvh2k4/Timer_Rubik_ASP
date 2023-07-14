using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Interfaces
{
    public interface ICategoryRepository_Admin
    {
        bool CreateCategory(Category category);

        bool Save();

        bool UpdateCategory(Category category);
    }
}
