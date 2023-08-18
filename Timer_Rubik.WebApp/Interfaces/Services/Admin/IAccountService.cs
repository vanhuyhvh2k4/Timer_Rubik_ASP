using Timer_Rubik.WebApp.DTO;
using Timer_Rubik.WebApp.DTO.Admin;

namespace Timer_Rubik.WebApp.Interfaces.Services.Admin
{
    public interface IAccountService
    {
        APIResponseDTO<string> Login(LoginDTO loginRequest);

        APIResponseDTO<ICollection<GetAccountDTO>> GetAccounts(Guid ownerId);

        APIResponseDTO<GetAccountDTO> GetAccount(Guid accountId);

        APIResponseDTO<string> UpdateAccount(Guid accountId, UpdateAccountDTO updateAccount);
    }
}
