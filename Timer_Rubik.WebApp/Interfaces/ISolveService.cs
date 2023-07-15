using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Interfaces
{
    public interface ISolveService
    {
        ICollection<Solve> GetSolves();

        Solve GetSolve(Guid solveId);

        bool SolveExists(Guid solveId);

        Solve GetSolveOfScramble(Guid scrambleId);

        bool CreateSolve(Solve solve);

        bool UpdateSolve(Solve solve);

        bool Save();

        bool DeleteSolve(Solve solve);
    }
}
