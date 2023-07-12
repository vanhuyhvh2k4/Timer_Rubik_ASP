using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.User.Interfaces
{
    public interface IFavoriteRepository_U
    {
        bool CreateFavorite(Favorite favorite);

        bool Save();
    }
}
