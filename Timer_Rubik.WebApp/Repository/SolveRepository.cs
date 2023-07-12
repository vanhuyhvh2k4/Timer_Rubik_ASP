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

        public bool CreateSolve(Solve solve)
        {
            var newSolve = new Solve()
            {
                Id = new Guid(),
                ScrambleId = solve.ScrambleId,
                Answer = solve.Answer,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
            };

            _context.Solves.Add(newSolve);

            return Save();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SolveExists(Guid solveId)
        {
            return _context.Solves.Any(solve => solve.Id == solveId);
        }
    }
}
