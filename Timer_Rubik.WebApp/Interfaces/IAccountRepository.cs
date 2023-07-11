using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Interfaces
{
    public interface IAccountRepository
    {
        ICollection<Account> GetAccounts();

        Account GetAccount(Guid accountId);

        bool CreateAccount(Account account);

        bool AccountExists(Guid accountId);

        bool Save();
    }
}
