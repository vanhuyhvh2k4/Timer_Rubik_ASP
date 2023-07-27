using Microsoft.EntityFrameworkCore;
using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Services
{
    public class ScrambleRepository : IScrambleRepository
    {
        private readonly DataContext _context;

        public ScrambleRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateScramble(Guid accountId, Scramble scramble)
        {
            var newScramble = new Scramble()
            {
                Id = new Guid(),
                AccountId = accountId,
                CategoryId = scramble.CategoryId,
                Algorithm = scramble.Algorithm,
                Thumbnail = scramble.Thumbnail,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
            };

            _context.Scrambles.Add(newScramble);

            return Save();
        }

        public bool DeleteScramble(Scramble scramble)
        {
            _context.Scrambles.Remove(scramble);
            return Save();
        }

        public Scramble GetScramble(Guid scrambleId)
        {
            return _context.Scrambles
                    .Where(scramble => scramble.Id == scrambleId)
                    .Include(scramble => scramble.Account)
                    .Include(scramble => scramble.Category)
                    .FirstOrDefault()!;
        }

        public ICollection<Scramble> GetScrambleByCategory(Guid categoryId)
        {
            return _context.Scrambles
                    .Where(scramble => scramble.CategoryId == categoryId)
                    .Include(scramble => scramble.Account)
                    .Include(scramble => scramble.Category)
                    .OrderBy(scramble => scramble.Id)
                    .ToList();
        }

        public ICollection<Scramble> GetScrambles()
        {
            return _context.Scrambles
                .OrderBy(scramble => scramble.Id)
                .Include(scramble => scramble.Account)
                .Include(scramble => scramble.Category)
                .ToList();
        }

        public ICollection<Scramble> GetScramblesOfAccount(Guid accountId)
        {
            return _context.Scrambles
                        .Where(scramble => scramble.AccountId == accountId)
                        .Include(scramble => scramble.Account)
                        .Include(scramble => scramble.Category)
                        .OrderBy(scramble => scramble.Id)
                        .ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool ScrambleExists(Guid scrambleId)
        {
            return _context.Scrambles.Any(scramble => scramble.Id == scrambleId);
        }

        public bool UpdateScramble(Guid scrambleId, Scramble scramble)
        {
            var updateScramble = _context.Scrambles.Where(scram => scram.Id == scrambleId).FirstOrDefault()!;

            updateScramble.CategoryId = scramble.CategoryId;
            updateScramble.Algorithm = scramble.Algorithm;
            updateScramble.Thumbnail = scramble.Thumbnail;
            updateScramble.UpdatedAt = DateTime.Now;

            return Save();
        }
    }
}
