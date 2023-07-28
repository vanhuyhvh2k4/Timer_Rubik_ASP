using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Interfaces.Repository;
using Timer_Rubik.WebApp.Interfaces.Utils;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Services
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;
        private readonly IPasswordUtils _passwordUtils;

        public AccountRepository(DataContext context, IPasswordUtils passwordUtils)
        {
            _context = context;
            _passwordUtils = passwordUtils;
        }

        public bool AccountExists(Guid accountId)
        {
            return _context.Accounts.Any(account => account.Id == accountId);
        }

        public Account GetAccount(Guid accountId)
        {
            return _context.Accounts.Where(ac => ac.Id == accountId).FirstOrDefault()!;
        }

        public Account GetAccount(string email)
        {
            return _context.Accounts.Where(account => account.Email.Trim().ToUpper() == email.Trim().ToUpper()).FirstOrDefault()!;
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

        public bool UpdateAccount(Guid accountId, Account account)
        {
            var updateAccount = _context.Accounts.Where(ac => ac.Id == accountId).FirstOrDefault()!;
            var hashedPassword = _passwordUtils.HashPassword(account.Password.Trim());

            updateAccount.Name = account.Name.Trim();
            updateAccount.Thumbnail = account.Thumbnail.Trim();
            updateAccount.Password = hashedPassword;
            updateAccount.UpdatedAt = DateTime.Now;
            return Save();
        }

        public bool DeleteAccount(Account account)
        {
            _context.Accounts.Remove(account);
            return Save();
        }

        public bool RegisterAccount(Account account)
        {
            var hashedPassword = _passwordUtils.HashPassword(account.Password.Trim());

            var newAccount = new Account()
            {
                Id = new Guid(),
                Name = account.Name.Trim(),
                Email = account.Email.Trim(),
                Thumbnail = account.Thumbnail.Trim(),
                Password = hashedPassword,
                RuleId = Guid.Parse("4e4d22d4-1fc2-11ee-8407-a02bb82e10f9"),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
            };

            _context.Accounts.Add(newAccount);
            return Save();
        }

        public bool UpdateAccount_User(Guid accountId, Account account)
        {
            var updateAccount = _context.Accounts.Where(ac => ac.Id == accountId).FirstOrDefault()!;
            var hashedPassword = _passwordUtils.HashPassword(account.Password.Trim());

            updateAccount.Name = account.Name.Trim();
            updateAccount.Thumbnail = account.Thumbnail.Trim();
            updateAccount.Password = hashedPassword;
            updateAccount.UpdatedAt = DateTime.Now;
            return Save();
        }

        public bool ChangePassword(Guid accountId, string newPassword)
        {
            var updateAccount = _context.Accounts.Where(ac => ac.Id == accountId).FirstOrDefault()!;
            var hashedPassword = _passwordUtils.HashPassword(newPassword.Trim());

            updateAccount.Password = hashedPassword;
            updateAccount.UpdatedAt = DateTime.Now;
            return Save();
        }

        public Account GetAccountByFavorite(Guid favoriteId)
        {
            return _context.Favorites.Where(fav => fav.Id == favoriteId).Select(fav => fav.Account).FirstOrDefault()!;
        }

        public Account GetAccountByScramble(Guid scrambleId)
        {
            return _context.Scrambles.Where(sc => sc.Id == scrambleId).Select(sc => sc.Account).FirstOrDefault()!;
        }
    }
}
