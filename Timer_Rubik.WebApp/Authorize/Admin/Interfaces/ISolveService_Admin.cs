using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Interfaces
{
    public interface ISolveService_Admin
    {
        ICollection<Solve> GetSolves();

        Solve GetSolve(Guid solveId);

        bool SolveExists(Guid solveId);

        Solve GetSolveOfScramble(Guid scrambleId);

        bool CreateSolve(Solve solve);

        bool UpdateSolve(Solve solve);

        bool Save();
    }
}
