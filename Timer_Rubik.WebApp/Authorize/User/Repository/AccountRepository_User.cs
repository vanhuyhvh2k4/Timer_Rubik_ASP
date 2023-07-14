using Timer_Rubik.WebApp.Authorize.User.Interfaces;
using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Models;
using Timer_Rubik.WebApp.Utils;

namespace Timer_Rubik.WebApp.Authorize.User.Repository
{
    public class AccountRepository_User : IAccountRepository_User
    {
        private readonly DataContext _context;

        public AccountRepository_User(DataContext context)
        {
            _context = context;
        }

        public bool CreateAccount(Account account)
        {
            var UserRuleId = _context.Rules.Where(rule => rule.RoleName.Trim().ToUpper() == "USER").Select(rule => rule.Id).FirstOrDefault();

            string hashedPassword = Password.HashPassword(account.Password);

            var newAccount = new Account()
            {
                Id = new Guid(),
                Name = account.Name,
                Email = account.Email,
                Password = hashedPassword,
                RuleId = UserRuleId,
                Thumbnail = account.Thumbnail,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
            };

            _context.Accounts.Add(newAccount);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAccount(Account account)
        {
            var updateAccount = _context.Accounts.Where(acc => acc.Id == account.Id).FirstOrDefault();
            string hashedPassowrd = Password.HashPassword(account.Password);

            updateAccount.Name = account.Name;
            updateAccount.Thumbnail = account.Thumbnail;
            updateAccount.Password = hashedPassowrd;
            updateAccount.UpdatedAt = DateTime.Now;

            return Save();
        }
    }
}
