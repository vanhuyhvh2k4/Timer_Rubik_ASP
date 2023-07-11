using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public bool AccountExists(Guid accountId)
        {
            return _context.Accounts.Any(account => account.Id == accountId);
        }

        public Account GetAccount(Guid accountId)
        {
            return _context.Accounts.Find(accountId);
        }

        public ICollection<Account> GetAccounts()
        {
            return _context.Accounts.OrderBy(account => account.Id).ToList();
        }
    }
}
