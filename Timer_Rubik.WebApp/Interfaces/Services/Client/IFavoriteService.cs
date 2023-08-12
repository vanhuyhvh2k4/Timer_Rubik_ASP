using Timer_Rubik.WebApp.DTO;
using Timer_Rubik.WebApp.DTO.Client;

namespace Timer_Rubik.WebApp.Interfaces.Services.Client
{
    public interface IFavoriteService
    {
        APIResponseDTO<ICollection<GetFavoriteDTO>> GetFavoritesByScramble(Guid scrambleId);

        APIResponseDTO<ICollection<GetFavoriteDTO>> GetFavoritesOfAccount(Guid accountId);

        APIResponseDTO<GetFavoriteDTO> GetFavorite(Guid favoriteId);

        APIResponseDTO<string> CreateFavorite(Guid ownerId, CreateFavoriteDTO createFavorite);

        APIResponseDTO<string> DeleteFavorite(Guid ownerId, Guid favoriteId);
    }
}
