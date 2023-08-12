using NuGet.Protocol.Plugins;
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
        private readonly string adminId;

        public AccountService(IAccountRepository accountRepository, IPasswordUtils passwordUtils, IJWTUtils jWTUtils, IConfiguration config)
        {
            _accountRepository = accountRepository;
            _passwordUtils = passwordUtils;
            _jWTUtils = jWTUtils;
            adminId = config.GetSection("Admin_Id").Value!;
        }

        public APIResponseDTO<string> Login(LoginDTO loginRequest)
        {
            var accountEntity = _accountRepository.GetAccount(loginRequest.Email);

            bool isCorrectPassword = _passwordUtils.VerifyPassword(loginRequest.Password, accountEntity.Password);

            bool isAdmin = accountEntity.RuleId.ToString().Equals(adminId);

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
                Message = "Success"
            };
        }
    }
}
