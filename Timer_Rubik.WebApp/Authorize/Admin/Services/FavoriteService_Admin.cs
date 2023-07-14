using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Services
{
    public class FavoriteService_Admin : IFavoriteService_Admin
    {
        private readonly DataContext _context;

        public FavoriteService_Admin(DataContext context)
        {
            _context = context;
        }

        public bool CreateFavorite(Favorite favorite)
        {
            var newFavorite = new Favorite()
            {
                Id = new Guid(),
                AccountId = favorite.AccountId,
                ScrambleId = favorite.ScrambleId,
                Time = favorite.Time,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
            };

            _context.Favorites.Add(newFavorite);

            return Save();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFavorite(Favorite favorite)
        {
            var updateFavorite = _context.Favorites.Where(fav => fav.Id == favorite.Id).FirstOrDefault();

            updateFavorite.AccountId = favorite.AccountId;
            updateFavorite.ScrambleId = favorite.ScrambleId;
            updateFavorite.Time = favorite.Time;
            updateFavorite.UpdatedAt = DateTime.Now;

            return Save();
        }
    }
}
