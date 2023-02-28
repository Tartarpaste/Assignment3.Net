using MagicTord_N_SondreTheWebAPI.Models;

namespace MagicTord_N_SondreTheWebAPI.Services.Movies
{
    public interface IMovieService
    {
        Task<ICollection<Character>> GetMovieCharactersAsync(int MovieId);

        Task UpdateMovieCharactersAsync(HashSet<Character> characterIDS, int MovieId);
    }
}
