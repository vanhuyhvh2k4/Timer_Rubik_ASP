using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.User.Interfaces
{
    public interface IAccountRepository_U
    {
        bool UpdateAccount(Account account);

        bool Save();
    }
}
