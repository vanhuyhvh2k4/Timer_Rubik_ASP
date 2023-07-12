using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Repository
{
    public class AccountRepository_AD : IAccountRepository_AD
    {
        private readonly DataContext _context;

        public AccountRepository_AD(DataContext context)
        {
            _context = context;
        }

        public bool CreateAccount(Account account)
        {
            var newAccount = new Account()
            {
                Id = new Guid(),
                Name = account.Name,
                Email = account.Email,
                Password = account.Password,
                Thumbnail = account.Thumbnail,
                RuleId = account.RuleId,
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
            var updateAccount = _context.Accounts.Where(ac => ac.Id == account.Id).FirstOrDefault();

            updateAccount.RuleId = account.RuleId;
            updateAccount.Name = account.Name;
            updateAccount.Thumbnail = account.Thumbnail;
            updateAccount.Email = account.Email;
            updateAccount.Password = account.Password;
            updateAccount.UpdatedAt = DateTime.Now;
            return Save();
        }
    }
}
