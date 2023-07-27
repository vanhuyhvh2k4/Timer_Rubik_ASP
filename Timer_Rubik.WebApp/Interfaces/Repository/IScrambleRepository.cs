using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Interfaces.Repository
{
    public interface IScrambleRepository
    {
        ICollection<Scramble> GetScrambles();

        Scramble GetScramble(Guid scrambleId);

        bool ScrambleExists(Guid scrambleId);

        ICollection<Scramble> GetScramblesOfAccount(Guid accountId);

        ICollection<Scramble> GetScrambleByCategory(Guid categoryId);

        bool CreateScramble(Guid accountId, Scramble scramble);

        bool UpdateScramble(Guid scrambleId, Scramble scramble);

        bool Save();

        bool DeleteScramble(Scramble scramble);
    }
}
