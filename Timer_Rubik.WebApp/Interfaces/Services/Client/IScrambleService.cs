using Timer_Rubik.WebApp.DTO;
using Timer_Rubik.WebApp.DTO.Client;

namespace Timer_Rubik.WebApp.Interfaces.Services.Client
{
    public interface IScrambleService
    {
        APIResponseDTO<ICollection<GetScrambleDTO>> GetScramblesByCategory(Guid categoryId);

        APIResponseDTO<ICollection<GetScrambleDTO>> GetScramblesOfAccount(Guid accountId);

        APIResponseDTO<GetScrambleDTO> GetScramble(Guid scrambleId);

        APIResponseDTO<string> CreateScramble(Guid onwerId, CreateScrambleDTO createScramble);

        APIResponseDTO<string> UpdateScramble(Guid ownerId, Guid scrambleId, UpdateScrambleDTO updateScramble);

        APIResponseDTO<string> DeleteScramble(Guid ownerId, Guid scrambleId);
    }
}
