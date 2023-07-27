using Microsoft.EntityFrameworkCore;
using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Interfaces.Repository;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Services
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly DataContext _context;

        public FavoriteRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateFavorite(Guid accountId, Favorite favorite)
        {
            var newFavorite = new Favorite()
            {
                Id = new Guid(),
                AccountId = accountId,
                ScrambleId = favorite.ScrambleId,
                Time = favorite.Time,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
            };

            _context.Favorites.Add(newFavorite);

            return Save();
        }

        public bool DeleteFavorite(Favorite favorite)
        {
            _context.Favorites.Remove(favorite);
            return Save();
        }

        public bool FavoriteExists(Guid favoriteId)
        {
            return _context.Favorites.Any(fav => fav.Id == favoriteId);
        }

        public Favorite GetFavorite(Guid favoriteId)
        {
            return _context.Favorites
                    .Where(fav => fav.Id == favoriteId)
                    .Include(fav => fav.Account)
                    .Include(fav => fav.Scramble)
                    .Include(fav => fav.Scramble.Category)
                    .FirstOrDefault();
        }

        public ICollection<Favorite> GetFavorites()
        {
            return _context.Favorites
                .OrderBy(fav => fav.Id)
                .Include(fav => fav.Account)
                .Include(fav => fav.Scramble)
                .Include(fav => fav.Scramble.Category)
                .ToList();
        }

        public ICollection<Favorite> GetFavoritesByScramble(Guid scrambleId)
        {
            return _context.Favorites
                    .Where(fav => fav.ScrambleId == scrambleId)
                    .Include(fav => fav.Account)
                    .Include(fav => fav.Scramble)
                    .Include(fav => fav.Scramble.Category)
                    .OrderBy(fav => fav.Id)
                    .ToList();
        }

        public ICollection<Favorite> GetFavoritesOfAccount(Guid accountId)
        {
            return _context.Favorites
                        .Where(fav => fav.AccountId == accountId)
                        .Include(fav => fav.Account)
                        .Include(fav => fav.Scramble)
                        .Include(fav => fav.Scramble.Category)
                        .OrderBy(fav => fav.Id)
                        .ToList();
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
