using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Interfaces
{
    public interface IAccountService
    {
        ICollection<Account> GetAccounts();

        Account GetAccount(Guid accountId);

        Account GetAccount(string email);

        Account GetAccountByFavorite(Guid favoriteId);

        bool AccountExists(Guid accountId);

        bool CreateAccount(Account account);

        bool RegisterAccount(Account account);

        bool Save();

        bool UpdateAccount(Account account);

        bool UpdateAccount_User(Account account);

        bool DeleteAccount(Account account);

        bool ChangePassword(Guid accountId, string newPassword);
    }
}
