using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Repository
{
    public class SolveRepository_AD : ISolveRepository_AD
    {
        private readonly DataContext _context;

        public SolveRepository_AD(DataContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateSolve(Solve solve)
        {
            var updateSolve = _context.Solves.Where(sol => sol.Id == solve.Id).FirstOrDefault();

            updateSolve.Answer = solve.Answer;
            updateSolve.UpdatedAt = DateTime.Now;

            return Save();
            
        }
    }
}
