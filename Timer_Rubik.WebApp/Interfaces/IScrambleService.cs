using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Interfaces
{
    public interface IScrambleService
    {
        ICollection<Scramble> GetScrambles();

        Scramble GetScramble(Guid scrambleId);

        bool ScrambleExists(Guid scrambleId);

        ICollection<Scramble> GetScramblesOfAccount(Guid accountId);

        ICollection<Scramble> GetScrambleByCategory(Guid categoryId);

        bool CreateScramble(Scramble scramble);

        bool UpdateScramble(Scramble scramble);

        bool Save();

        bool DeleteScramble(Scramble scramble);
    }
}
