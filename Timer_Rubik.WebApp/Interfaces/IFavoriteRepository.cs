using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Interfaces
{
    public interface IFavoriteRepository
    {
        ICollection<Favorite> GetFavorites();

        Favorite GetFavorite(Guid favoriteId);

        bool FavoriteExists(Guid favoriteId);

        ICollection<Favorite> GetFavoritesOfAccount(Guid accountId);

        ICollection<Favorite> GetFavoritesByScramble(Guid scrambleId);

        bool CreateFavorite(Guid accountId, Favorite favorite);

        bool UpdateFavorite(Favorite favorite);

        bool Save();

        bool DeleteFavorite(Favorite favorite);
    }
}
