using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Repository
{
    public class ScrambleRepository : IScrambleRepository
    {
        private readonly DataContext _context;

        public ScrambleRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateScramble(Scramble scramble)
        {
            var newScramble = new Scramble()
            {
                Id = new Guid(),
                AccountId = scramble.AccountId,
                CategoryId = scramble.CategoryId,
                Algorithm = scramble.Algorithm,
                Thumbnail = scramble.Thumbnail,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
            };

            _context.Scrambles.Add(newScramble);

            return Save();
        }

        public Scramble GetScramble(Guid scrambleId)
        {
            return _context.Scrambles.Find(scrambleId);
        }

        public ICollection<Scramble> GetScrambleByCategory(Guid categoryId)
        {
            return _context.Scrambles.Where(scramble => scramble.CategoryId == categoryId).OrderBy(scramble => scramble.Id).ToList();
        }

        public ICollection<Scramble> GetScrambles()
        {
            return _context.Scrambles.OrderBy(scramble => scramble.Id).ToList();
        }

        public ICollection<Scramble> GetScramblesOfAccount(Guid accountId)
        {
            return _context.Scrambles.Where(scramble => scramble.AccountId == accountId).OrderBy(scramble => scramble.Id).ToList();
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
    }
}
