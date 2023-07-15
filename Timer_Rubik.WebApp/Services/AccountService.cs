using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;
using Timer_Rubik.WebApp.Utils;

namespace Timer_Rubik.WebApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _context;

        public AccountService(DataContext context)
        {
            _context = context;
        }

        public bool CreateAccount(Account account)
        {
            var hashedPassword = Password.HashPassword(account.Password);

            var newAccount = new Account()
            {
                Id = new Guid(),
                Name = account.Name,
                Email = account.Email,
                Password = hashedPassword,
                Thumbnail = account.Thumbnail,
                RuleId = account.RuleId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
            };

            _context.Accounts.Add(newAccount);

            return Save();
        }

        public bool AccountExists(Guid accountId)
        {
            return _context.Accounts.Any(account => account.Id == accountId);
        }

        public Account GetAccount(Guid accountId)
        {
            return _context.Accounts.Find(accountId);
        }

        public Account GetAccount(string email)
        {
            return _context.Accounts.Where(account => account.Email == email).FirstOrDefault();
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

        public bool UpdateAccount(Account account)
        {
            var updateAccount = _context.Accounts.Where(ac => ac.Id == account.Id).FirstOrDefault();
            var hashedPassword = Password.HashPassword(account.Password);

            updateAccount.RuleId = account.RuleId;
            updateAccount.Name = account.Name;
            updateAccount.Thumbnail = account.Thumbnail;
            updateAccount.Email = account.Email;
            updateAccount.Password = hashedPassword;
            updateAccount.UpdatedAt = DateTime.Now;
            return Save();
        }

        public bool DeleteAccount(Account account)
        {
            _context.Accounts.Remove(account);
            return Save();
        }
    }
}
