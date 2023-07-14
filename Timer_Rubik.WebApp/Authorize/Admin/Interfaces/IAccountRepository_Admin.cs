using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Interfaces
{
    public interface IAccountRepository_Admin
    {
        ICollection<Account> GetAccounts();

        Account GetAccount(Guid accountId);

        Account GetAccount(string email);

        bool AccountExists(Guid accountId);

        bool CreateAccount(Account account);

        bool Save();

        bool UpdateAccount(Account account);
    }
}
