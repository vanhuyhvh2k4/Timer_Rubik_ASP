using Timer_Rubik.WebApp.DTO.Client;

namespace Timer_Rubik.WebApp.Interfaces.Services.Client
{
    public interface IAccountService
    {
        APIResponseDTO<GetAccountDTO> GetAccount(Guid ownerId, Guid accountId);

        APIResponseDTO<string> UpdateAccount(Guid ownerId, Guid accountId, UpdateAccountDTO updateAccount);
    }
}
