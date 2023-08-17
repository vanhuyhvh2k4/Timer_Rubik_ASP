using AutoMapper;
using Timer_Rubik.WebApp.DTO;
using Timer_Rubik.WebApp.DTO.Admin;
using Timer_Rubik.WebApp.Interfaces.Repository;
using Timer_Rubik.WebApp.Interfaces.Services.Admin;
using Timer_Rubik.WebApp.Interfaces.Utils;

namespace Timer_Rubik.WebApp.Services.Admin
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordUtils _passwordUtils;
        private readonly IJWTUtils _jWTUtils;
        private readonly IMapper _mapper;
        private readonly string adminId;

        public AccountService(IAccountRepository accountRepository, IPasswordUtils passwordUtils, IJWTUtils jWTUtils, IConfiguration config, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _passwordUtils = passwordUtils;
            _jWTUtils = jWTUtils;
            _mapper = mapper;
            adminId = config.GetSection("Admin_Id").Value!;
        }

        public APIResponseDTO<ICollection<GetAccountDTO>> GetAccounts(Guid ownerId)
        {
            var accounts = _mapper.Map<List<GetAccountDTO>>(_accountRepository.GetAccounts().Where(ac => ac.Id != ownerId));

            if (accounts.Count == 0)
            {
                return new APIResponseDTO<ICollection<GetAccountDTO>>
                {
                    Status = 404,
                    Message = "Not Found Account"
                };
            }

            return new APIResponseDTO<ICollection<GetAccountDTO>>
            {
                Status = 200,
                Message = "Successful",
                Data = accounts
            };
        }

        public APIResponseDTO<string> Login(LoginDTO loginRequest)
        {
            var accountEntity = _accountRepository.GetAccount(loginRequest.Email);

            bool isCorrectPassword = _passwordUtils.VerifyPassword(loginRequest.Password, accountEntity.Password);

            bool isAdmin = accountEntity.RuleId.ToString().Equals(adminId);

            string token = _jWTUtils.GenerateAccessToken(accountEntity.Id.ToString(), accountEntity.RuleId.ToString());

            if (!isCorrectPassword || accountEntity == null || !isAdmin)
            {
                return new APIResponseDTO<string>
                {
                    Status = 403,
                    Message = "Username or Password is not correct",
                };
            }

            return new APIResponseDTO<string>
            {
                Status = 200,
                Message = "Success",
                Data = token
            };
        }
    }
}
