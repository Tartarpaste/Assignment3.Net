using MagicTord_N_SondreTheWebAPI.Models;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Movies;

namespace MagicTord_N_SondreTheWebAPI.Services.Characters
{
    public interface ICharacterService : ICrudService<Character, int>
    {
        Task <ICollection<MovieDto>> GetCharacterMoviesAsync (int CharacterId);

        Task UpdateCharacterMoviesAsync(HashSet<Movie> movieIDS, int CharacterId);
    }
}
