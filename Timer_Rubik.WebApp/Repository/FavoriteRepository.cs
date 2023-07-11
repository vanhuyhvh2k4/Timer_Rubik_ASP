using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly DataContext _context;

        public FavoriteRepository(DataContext context)
        {
            _context = context;
        }

        public bool FavoriteExists(Guid favoriteId)
        {
            return _context.Favorites.Any(fav => fav.Id == favoriteId);
        }

        public Favorite GetFavorite(Guid favoriteId)
        {
            return _context.Favorites.Find(favoriteId);
        }

        public ICollection<Favorite> GetFavorites()
        {
            return _context.Favorites.OrderBy(fav => fav.Id).ToList();
        }

        public ICollection<Favorite> GetFavoritesByScramble(Guid scrambleId)
        {
            return _context.Favorites.Where(fav => fav.ScrambleId == scrambleId).OrderBy(fav => fav.Id).ToList();
        }

        public ICollection<Favorite> GetFavoritesOfAccount(Guid accountId)
        {
            return _context.Favorites.Where(fav => fav.AccountId == accountId).OrderBy(fav => fav.Id).ToList();
        }
    }
}
