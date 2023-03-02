using MagicTord_N_SondreTheWebAPI.Models;

namespace MagicTord_N_SondreTheWebAPI.Services.Characters
{
    public interface ICharacterService : ICrudService<Character, int>
    {
        Task <ICollection<Movie>> GetCharacterMoviesAsync (int CharacterId);

        Task UpdateCharacterMoviesAsync(HashSet<Movie> movieIDS, int CharacterId);
    }
}
