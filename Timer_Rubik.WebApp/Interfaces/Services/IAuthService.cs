using Timer_Rubik.WebApp.DTO.Client;

namespace Timer_Rubik.WebApp.Interfaces.Services
{
    public interface IAuthService
    {
        LoginResponse Login(string email, string password);
    }
}
