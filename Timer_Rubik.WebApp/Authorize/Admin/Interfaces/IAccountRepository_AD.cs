using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Interfaces
{
    public interface IAccountRepository_AD
    {
        bool CreateAccount(Account account);

        bool Save();
    }
}
