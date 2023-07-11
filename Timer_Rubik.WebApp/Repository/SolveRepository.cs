using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Repository
{
    public class SolveRepository : ISolveRepository
    {
        private readonly DataContext _context;

        public SolveRepository(DataContext context)
        {
            _context = context;
        }

        public Solve GetSolve(Guid solveId)
        {
            return _context.Solves.Find(solveId);
        }

        public Solve GetSolveOfScramble(Guid scrambleId)
        {
            return _context.Solves.Where(solve => solve.ScrambleId == scrambleId).FirstOrDefault();
        }

        public ICollection<Solve> GetSolves()
        {
            return _context.Solves.OrderBy(solve => solve.Id).ToList();
        }

        public bool SolveExists(Guid solveId)
        {
            return _context.Solves.Any(solve => solve.Id == solveId);
        }
    }
}
