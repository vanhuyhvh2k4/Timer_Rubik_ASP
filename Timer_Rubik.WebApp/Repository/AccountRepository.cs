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

        public bool CreateAccount(Account account)
        {
            var newAccount = new Account()
            {
                Id = new Guid(),
                Name = account.Name,
                Email = account.Email,
                Password = account.Password,
                RuleId = Guid.Parse("4e4d22d4-1fc2-11ee-8407-a02bb82e10f9"),
                Thumbnail = account.Thumbnail,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
            };

            _context.Accounts.Add(newAccount);

            return Save();
        }

        public Account GetAccount(Guid accountId)
        {
            return _context.Accounts.Find(accountId);
        }

        public ICollection<Account> GetAccounts()
        {
            return _context.Accounts.OrderBy(account => account.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
