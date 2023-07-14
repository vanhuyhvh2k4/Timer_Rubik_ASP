using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Repository
{
    public class ScrambleRepository_Admin : IScrambleRepository_Admin
    {
        private readonly DataContext _context;

        public ScrambleRepository_Admin(DataContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateScramble(Scramble scramble)
        {
            var updateScramble = _context.Scrambles.Where(scram => scram.Id == scramble.Id).FirstOrDefault();

            updateScramble.CategoryId = scramble.CategoryId;
            updateScramble.AccountId = scramble.AccountId;
            updateScramble.Algorithm = scramble.Algorithm;
            updateScramble.Thumbnail = scramble.Thumbnail;
            updateScramble.UpdatedAt = DateTime.Now;

            return Save();
        }
    }
}
