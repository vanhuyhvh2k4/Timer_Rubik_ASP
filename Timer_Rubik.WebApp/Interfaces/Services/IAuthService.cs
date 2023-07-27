using Timer_Rubik.WebApp.DTO.Client;

namespace Timer_Rubik.WebApp.Interfaces.Services
{
    public interface IAuthService
    {
        APIResponseDTO<LoginResponse> Login(LoginRequest loginRequest);

        APIResponseDTO<string> Register(RegisterRequest registerRequest);
    }
}
