using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Interfaces
{
    public interface IScrambleRepository_Admin
    {
        ICollection<Scramble> GetScrambles();

        Scramble GetScramble(Guid scrambleId);

        bool ScrambleExists(Guid scrambleId);

        ICollection<Scramble> GetScramblesOfAccount(Guid accountId);

        ICollection<Scramble> GetScrambleByCategory(Guid categoryId);

        bool CreateScramble(Scramble scramble);

        bool UpdateScramble(Scramble scramble);

        bool Save();
    }
}
