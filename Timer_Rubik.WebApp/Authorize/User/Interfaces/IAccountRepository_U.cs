using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.User.Interfaces
{
    public interface IAccountRepository_U
    {
        bool CreateAccount(Account account);

        bool UpdateAccount(Account account);

        bool Save();
    }
}
