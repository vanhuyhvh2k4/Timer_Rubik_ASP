using Timer_Rubik.WebApp.DTO.Client;

namespace Timer_Rubik.WebApp.Interfaces.Services
{
    public interface IAccountService
    {
        APIResponseDTO<GetAccountDTO> GetAccount(Guid accountId);

        APIResponseDTO<string> UpdateAccount(Guid accountId, UpdateAccountDTO updateAccount);
    }
}
