using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Interfaces
{
    public interface IScrambleRepository_AD
    {
        bool UpdateScramble(Scramble scramble);

        bool Save();
    }
}
