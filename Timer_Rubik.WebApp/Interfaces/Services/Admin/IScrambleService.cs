using Timer_Rubik.WebApp.DTO;
using Timer_Rubik.WebApp.DTO.Admin;

namespace Timer_Rubik.WebApp.Interfaces.Services.Admin
{
    public interface IScrambleService
    {
        APIResponseDTO<ICollection<GetScrambleDTO>> GetScramblesByCategory(Guid categoryId);

        APIResponseDTO<string> CreateScramble(Guid ownerId, CreateScrambleDTO createScramble);
    }
}
