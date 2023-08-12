using AutoMapper;
using Timer_Rubik.WebApp.DTO;
using Timer_Rubik.WebApp.DTO.Client;
using Timer_Rubik.WebApp.Interfaces.Repository;
using Timer_Rubik.WebApp.Interfaces.Services.Client;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Services.Client
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public APIResponseDTO<GetAccountDTO> GetAccount(Guid ownerId, Guid accountId)
        {
            if (accountId != ownerId)
            {
                return new APIResponseDTO<GetAccountDTO>
                {
                    Status = 403,
                    Message = "Not allowed"
                };
            }

            var account = _mapper.Map<GetAccountDTO>(_accountRepository.GetAccount(accountId));

            if (account == null)
            {
                return new APIResponseDTO<GetAccountDTO>
                {
                    Status = 404,
                    Message = "Not found account",
                };
            }

            return new APIResponseDTO<GetAccountDTO>
            {
                Status = 201,
                Message = "Success",
                Data = account
            };
        }

        public APIResponseDTO<string> UpdateAccount(Guid ownerId, Guid accountId, UpdateAccountDTO updateAccount)
        {
            if (accountId != ownerId)
            {
                return new APIResponseDTO<string>
                {
                    Status = 403,
                    Message = "Not allowed"
                };
            }

            if (updateAccount.Password.Length < 6)
            {
                return new APIResponseDTO<string>
                {
                    Status = 400,
                    Message = "Password at least 6 characters",
                };
            }

            if (!_accountRepository.AccountExists(accountId))
            {
                return new APIResponseDTO<string>
                {
                    Status = 404,
                    Message = "Not found account"
                };
            }

            var accountMap = _mapper.Map<Account>(updateAccount);

            _accountRepository.UpdateAccount_User(accountId, accountMap);

            return new APIResponseDTO<string>
            {
                Status = 200,
                Message = "Updated successful"
            };
        }
    }
}
