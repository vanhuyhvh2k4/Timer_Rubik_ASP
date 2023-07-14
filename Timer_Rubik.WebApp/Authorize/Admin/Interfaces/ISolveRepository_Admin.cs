using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Interfaces
{
    public interface ISolveRepository_Admin
    {
        bool UpdateSolve(Solve solve);

        bool Save();
    }
}
