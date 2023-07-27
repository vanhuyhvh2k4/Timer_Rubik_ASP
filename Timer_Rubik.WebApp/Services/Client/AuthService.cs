using Timer_Rubik.WebApp.DTO.Client;
using Timer_Rubik.WebApp.Interfaces.Repository;
using Timer_Rubik.WebApp.Interfaces.Services;
using Timer_Rubik.WebApp.Interfaces.Utils;

namespace Timer_Rubik.WebApp.Services.Client
{
    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailUtils _emailUtils;
        private readonly IJWTUtils _jWTUtils;
        private readonly IPasswordUtils _passwordUtils;

        public AuthService(IAccountRepository accountRepository, IEmailUtils emailUtils, IJWTUtils jWTUtils, IPasswordUtils passwordUtils)
        {
            _accountRepository = accountRepository;
            _emailUtils = emailUtils;
            _jWTUtils = jWTUtils;
            _passwordUtils = passwordUtils;
        }

        public LoginResponse Login(string email, string password)
        {
            var accountEntity = _accountRepository.GetAccount(email.Trim());

            if (accountEntity == null)
            {
                return new LoginResponse
                {
                    Status = 401,
                    Message = "Not Found Account",
                    Token = null,
                };
            }

            bool isCorrectPassword = _passwordUtils.VerifyPassword(password.Trim(), accountEntity.Password.Trim());

            if (!isCorrectPassword)
            {
                return new LoginResponse
                {
                    Status = 403,
                    Message = "Password is not correct",
                    Token = null,
                };
            }

            var accessToken = _jWTUtils.GenerateAccessToken(accountEntity.Id.ToString(), accountEntity.RuleId.ToString());

            return new LoginResponse
            {
                Status = 200,
                Message = "Success",
                Token = accessToken,
            }; ;
        }
    }
}
