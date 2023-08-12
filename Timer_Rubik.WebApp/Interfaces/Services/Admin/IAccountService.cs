using Timer_Rubik.WebApp.DTO;
using Timer_Rubik.WebApp.DTO.Admin;

namespace Timer_Rubik.WebApp.Interfaces.Services.Admin
{
    public interface IAccountService
    {
        APIResponseDTO<string> Login(LoginDTO loginRequest);
    }
}
