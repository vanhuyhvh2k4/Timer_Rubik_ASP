using AutoMapper;
using Timer_Rubik.WebApp.DTO.Client;
using Timer_Rubik.WebApp.Interfaces.Repository;
using Timer_Rubik.WebApp.Interfaces.Services;
using Timer_Rubik.WebApp.Interfaces.Utils;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Services.Client
{
    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailUtils _emailUtils;
        private readonly IJWTUtils _jWTUtils;
        private readonly IPasswordUtils _passwordUtils;
        private readonly IMapper _mapper;

        public AuthService(IAccountRepository accountRepository, IEmailUtils emailUtils, IJWTUtils jWTUtils, IPasswordUtils passwordUtils, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _emailUtils = emailUtils;
            _jWTUtils = jWTUtils;
            _passwordUtils = passwordUtils;
            _mapper = mapper;
        }

        public APIResponseDTO<LoginResponse> Login(LoginRequest loginRequest)
        {
            var accountEntity = _accountRepository.GetAccount(loginRequest.Email.Trim());

            bool isCorrectPassword = _passwordUtils.VerifyPassword(loginRequest.Password.Trim(), accountEntity.Password.Trim());

            if (!isCorrectPassword || accountEntity == null)
            {
                return new APIResponseDTO<LoginResponse>
                {
                    Status = 403,
                    Message = "Username or Password is not correct",
                    Data = new LoginResponse
                    {
                        Token =null
                    }
                };
            }

            var accessToken = _jWTUtils.GenerateAccessToken(accountEntity.Id.ToString(), accountEntity.RuleId.ToString());

            return new APIResponseDTO<LoginResponse>
            {
                Status = 200,
                Message = "Success",
                Data = new LoginResponse
                {
                    Token = accessToken
                }
            }; 
        }

        public APIResponseDTO<string> Register(RegisterRequest registerRequest)
        {
            if (!_emailUtils.EmailValid(registerRequest.Email.Trim()))
            {
                return new APIResponseDTO<string>
                {
                    Status = 400,
                    Message = "Email is invalid"
                };
            }

            if (registerRequest.Password.Trim().Length < 6)
            {
                return new APIResponseDTO<string>
                { 
                    Status = 400,
                    Message = "Password at least 6 characters"
                };
            }

            if (_accountRepository.GetAccount(registerRequest.Email.Trim()) != null)
            {
                return new APIResponseDTO<string>
                {
                    Status = 409,
                    Message = "Email already exists"
                };
            }

            var accountMap = _mapper.Map<Account>(registerRequest);

            _accountRepository.RegisterAccount(accountMap);

            return new APIResponseDTO<string>
            {
                Status = 200,
                Message = "Created successful"
            };
        }
    }
}
