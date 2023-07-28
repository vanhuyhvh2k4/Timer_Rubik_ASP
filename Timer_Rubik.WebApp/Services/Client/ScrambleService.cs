using AutoMapper;
using Timer_Rubik.WebApp.DTO.Client;
using Timer_Rubik.WebApp.Interfaces.Repository;
using Timer_Rubik.WebApp.Interfaces.Services;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Services.Client
{
    public class ScrambleService : IScrambleService
    {
        private readonly IScrambleRepository _scrambleRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ScrambleService(IScrambleRepository scrambleRepository, IAccountRepository accountRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _scrambleRepository = scrambleRepository;
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public APIResponseDTO<string> CreateScramble(Guid ownerId, CreateScrambleDTO createScramble)
        {
            var scrambleEntity = _scrambleRepository.GetScramble(createScramble.Algorithm);

            if (scrambleEntity != null)
            {
                return new APIResponseDTO<string>
                {
                    Status = 409,
                    Message = "Scramble already exist"
                };
            }

            if (!_categoryRepository.CategoryExists(createScramble.CategoryId))
            {
                return new APIResponseDTO<string>
                {
                    Status = 404,
                    Message = "Not found category"
                };
            }

            var scrambleMap = _mapper.Map<Scramble>(createScramble);

            _scrambleRepository.CreateScramble(ownerId, scrambleMap);

            return new APIResponseDTO<string>
            {
                Status = 201,
                Message = "Created successful"
            };
        }

        public APIResponseDTO<string> DeleteScramble(Guid ownerId, Guid scrambleId)
        {
            if (!_scrambleRepository.ScrambleExists(scrambleId))
            {
                return new APIResponseDTO<string>
                {
                    Status = 404,
                    Message = "Not found scramble"
                };
            }

            var accountId = _accountRepository.GetAccountByScramble(scrambleId).Id;

            if (ownerId != accountId)
            {
                return new APIResponseDTO<string>
                {
                    Status = 403,
                    Message = "Not allowed"
                };
            }

            var scrambleEntity = _scrambleRepository.GetScramble(scrambleId);

            _scrambleRepository.DeleteScramble(scrambleEntity);

            return new APIResponseDTO<string>
            {
                Status = 200,
                Message = "Successful"
            };
        }

        public APIResponseDTO<GetScrambleDTO> GetScramble(Guid scrambleId)
        {
            var scramble = _scrambleRepository
                                   .GetScramble(scrambleId);

            if (scramble == null)
            {
                return new APIResponseDTO<GetScrambleDTO>
                {
                    Status = 404,
                    Message = "Not found scramble"
                };
            }

            var scrambleRes = new GetScrambleDTO
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
                    Thumbnail = scramble.Account.Thumbnail,
                },
                Algorithm = scramble.Algorithm,
                Solve = scramble?.Solve,
                CreatedAt = scramble.CreatedAt,
                UpdatedAt = scramble.UpdatedAt,
            };

            return new APIResponseDTO<GetScrambleDTO>
            {
                Status = 200,
                Message = "Successful",
                Data = scrambleRes
            };
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
                                            Thumbnail = scramble.Account.Thumbnail,
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
                    Message = "Not found scramble"
                };
            }

            return new APIResponseDTO<ICollection<GetScrambleDTO>>
            {
                Status = 200,
                Message = "Successful",
                Data = scrambles
            };
        }

        public APIResponseDTO<ICollection<GetScrambleDTO>> GetScramblesOfAccount(Guid accountId)
        {
            var scrambles = _scrambleRepository
                                   .GetScramblesOfAccount(accountId)
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
                                           Thumbnail = scramble.Account.Thumbnail,
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
                    Message = "Not found scramble"
                };
            }

            return new APIResponseDTO<ICollection<GetScrambleDTO>>
            {
                Status = 200,
                Message = "Successful",
                Data = scrambles
            };
        }

        public APIResponseDTO<string> UpdateScramble(Guid ownerId, Guid scrambleId, UpdateScrambleDTO updateScramble)
        {
            if (!_categoryRepository.CategoryExists(updateScramble.CategoryId))
            {
                return new APIResponseDTO<string>
                {
                    Status = 404,
                    Message = "Not found category"
                };
            }

            if (!_scrambleRepository.ScrambleExists(scrambleId))
            {
                return new APIResponseDTO<string>
                {
                    Status = 404,
                    Message = "Not found scramble"
                };
            }

            var accountId = _accountRepository.GetAccountByScramble(scrambleId).Id;

            if (ownerId != accountId)
            {
                return new APIResponseDTO<string>
                {
                    Status = 403,
                    Message = "Not allowed"
                };
            }

            var categoryMap = _mapper.Map<Scramble>(updateScramble);

            _scrambleRepository.UpdateScramble(scrambleId, categoryMap);

            return new APIResponseDTO<string>
            {
                Status = 200,
                Message = "Updated successful"
            };
        }
    }
}
