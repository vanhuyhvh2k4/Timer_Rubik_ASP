using BCrypt.Net;
using Timer_Rubik.WebApp.Authorize.User.Interfaces;
using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.User.Repository
{
    public class AccountRepository_U : IAccountRepository_U
    {
        private readonly DataContext _context;

        public AccountRepository_U(DataContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAccount(Account account)
        {
            var updateAccount = _context.Accounts.Where(acc => acc.Id == account.Id).FirstOrDefault();
            string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            string passwordWithSalt = account.Password + salt;
            string hashedPassowrd = BCrypt.Net.BCrypt.HashPassword(passwordWithSalt);

            updateAccount.Name = account.Name;
            updateAccount.Thumbnail = account.Thumbnail;
            updateAccount.Password = hashedPassowrd;
            updateAccount.UpdatedAt = DateTime.Now;

            return Save();
        }
    }
}
