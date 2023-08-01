using AutoMapper;
using Timer_Rubik.WebApp.DTO.Client;
using Timer_Rubik.WebApp.Interfaces.Repository;
using Timer_Rubik.WebApp.Interfaces.Services.Client;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Services.Client
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IScrambleRepository _scrambleRepository;
        private readonly IMapper _mapper;

        public FavoriteService(IFavoriteRepository favoriteRepository, IAccountRepository accountRepository, IScrambleRepository scrambleRepository, IMapper mapper)
        {
            _favoriteRepository = favoriteRepository;
            _accountRepository = accountRepository;
            _scrambleRepository = scrambleRepository;
            _mapper = mapper;
        }

        public APIResponseDTO<string> CreateFavorite(Guid ownerId, CreateFavoriteDTO createFavorite)
        {
            if (!_scrambleRepository.ScrambleExists(createFavorite.ScrambleId))
            {
                return new APIResponseDTO<string>
                {
                    Status = 404,
                    Message = "Not found scramble"
                };
            }

            var favoriteEntity = _favoriteRepository.GetFavoritesOfAccount(ownerId).Where(fav => fav.ScrambleId == createFavorite.ScrambleId).FirstOrDefault();

            if (favoriteEntity != null)
            {
                return new APIResponseDTO<string>
                {
                    Status = 409,
                    Message = "Favorite already exists"
                };
            }

            var favoriteMap = _mapper.Map<Favorite>(createFavorite);

            _favoriteRepository.CreateFavorite(ownerId, favoriteMap);

            return new APIResponseDTO<string>
            {
                Status = 200,
                Message = "Successful",
            };
        }

        public APIResponseDTO<string> DeleteFavorite(Guid ownerId, Guid favoriteId)
        {
            if (!_favoriteRepository.FavoriteExists(favoriteId))
            {
                return new APIResponseDTO<string>
                {
                    Status = 404,
                    Message = "Not found favorite"
                };
            }

            var accountId = _accountRepository.GetAccountByFavorite(favoriteId).Id;


            if (accountId != ownerId)
            {
                return new APIResponseDTO<string>
                {
                    Status = 403,
                    Message = "Not allowed"
                };
            }

            var favoriteEntity = _favoriteRepository.GetFavorite(favoriteId);

            _favoriteRepository.DeleteFavorite(favoriteEntity);

            return new APIResponseDTO<string>
            {
                Status = 200,
                Message = "Successful"
            };
        }

        public APIResponseDTO<GetFavoriteDTO> GetFavorite(Guid favoriteId)
        {
            var favorite = _favoriteRepository.GetFavorite(favoriteId);

            if (favorite == null)
            {
                return new APIResponseDTO<GetFavoriteDTO>
                {
                    Status = 404,
                    Message = "Not found favorite"
                };
            }

            var favoriteRes = new GetFavoriteDTO
            {
                Id = favorite.Id,
                Account = new
                {
                    Id = favorite.AccountId,
                    Name = favorite.Account.Name,
                    Thumnail = favorite.Account.Thumbnail,
                },
                Scramble = new
                {
                    Id = favorite.ScrambleId,
                    favorite.Scramble.Algorithm,
                    Category = favorite.Scramble.Category.Name
                },
                Time = favorite.Time,
                CreatedAt = favorite.CreatedAt
            };

            return new APIResponseDTO<GetFavoriteDTO>
            {
                Status = 200,
                Message = "Successful",
                Data = favoriteRes
            };
        }

        public APIResponseDTO<ICollection<GetFavoriteDTO>> GetFavoritesByScramble(Guid scrambleId)
        {
            var favorites = _favoriteRepository
                                   .GetFavoritesByScramble(scrambleId)
                                    .Select(fav => new GetFavoriteDTO
                                    {
                                        Id = fav.Id,
                                        Account = new
                                        {
                                            Id = fav.AccountId,
                                            Name = fav.Account.Name,
                                            Thumnail = fav.Account.Thumbnail,
                                        },
                                        Scramble = new
                                        {
                                            Id = fav.ScrambleId,
                                            fav.Scramble.Algorithm,
                                            Category = fav.Scramble.Category.Name
                                        },
                                        Time = fav.Time,
                                        CreatedAt = fav.CreatedAt
                                    })
                                   .ToList(); ;

            if (favorites.Count == 0)
            {
                return new APIResponseDTO<ICollection<GetFavoriteDTO>>
                {
                    Status = 404,
                    Message = "Not found favorite"
                };
            }

            return new APIResponseDTO<ICollection<GetFavoriteDTO>>
            {
                Status = 200,
                Message = "Successful",
                Data = favorites
            };
        }

        public APIResponseDTO<ICollection<GetFavoriteDTO>> GetFavoritesOfAccount(Guid accountId)
        {
            var favorites = _favoriteRepository
                                    .GetFavoritesOfAccount(accountId)
                                     .Select(fav => new GetFavoriteDTO
                                     {
                                         Id = fav.Id,
                                         Account = new
                                         {
                                             Id = fav.AccountId,
                                             Name = fav.Account.Name,
                                             Thumnail = fav.Account.Thumbnail,
                                         },
                                         Scramble = new
                                         {
                                             Id = fav.ScrambleId,
                                             fav.Scramble.Algorithm,
                                             Category = fav.Scramble.Category.Name
                                         },
                                         Time = fav.Time,
                                         CreatedAt = fav.CreatedAt
                                     })
                                    .ToList(); ;

            if (favorites.Count == 0)
            {
                return new APIResponseDTO<ICollection<GetFavoriteDTO>>
                {
                    Status = 404,
                    Message = "Not found favorite"
                };
            }

            return new APIResponseDTO<ICollection<GetFavoriteDTO>>
            {
                Status = 200,
                Message = "Successful",
                Data = favorites
            };
        }
    }
}
