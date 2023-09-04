using AutoMapper;
using Timer_Rubik.WebApp.DTO;
using Timer_Rubik.WebApp.DTO.Admin;
using Timer_Rubik.WebApp.Interfaces.Repository;
using Timer_Rubik.WebApp.Interfaces.Services.Admin;

namespace Timer_Rubik.WebApp.Services.Admin
{
    public class ScrambleService : IScrambleService
    {
        private readonly IScrambleRepository _scrambleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public ScrambleService(IScrambleRepository scrambleRepository, ICategoryRepository categoryRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _scrambleRepository = scrambleRepository;
            _categoryRepository = categoryRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public APIResponseDTO<ICollection<GetScrambleDTO>> GetScramblesByCategory(Guid categoryId)
        {
            var scrambles = _scrambleRepository
                                    .GetScrambleByCategory(categoryId)
                                    .Select(scramble => new GetScrambleDTO
                                    {
                                        Id = scramble.Id,
                                        Category = new
                                        {
                                            Id = scramble.CategoryId,
                                            Name = scramble.Category.Name
                                        },
                                        Account = new
                                        {
                                            Id = scramble.AccountId,
                                            Name = scramble.Account.Name,
                                            Email = scramble.Account.Email,
                                        },
                                        Algorithm = scramble.Algorithm,
                                        Solve = scramble?.Solve,
                                        CreatedAt = scramble.CreatedAt,
                                        UpdatedAt = scramble.UpdatedAt,
                                    })
                                   .ToList();

            if (scrambles.Count == 0)
            {
                return new APIResponseDTO<ICollection<GetScrambleDTO>>
                {
                    Status = 404,
                    Message = "Not Found Scrambles"
                };
            }

            return new APIResponseDTO<ICollection<GetScrambleDTO>>
            {
                Status = 200,
                Message = "Successful",
                Data = scrambles
            };
        }
    }
}
