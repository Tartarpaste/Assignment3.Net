using MagicTord_N_SondreTheWebAPI.Models;

namespace MagicTord_N_SondreTheWebAPI.Services.Movies
{
    public interface IMovieService : ICrudService<Movie, int>
    {
        Task<ICollection<Character>> GetMovieCharactersAsync(int MovieId);

        Task UpdateMovieCharactersAsync(int[] characterIds, int MovieId);
    }
}
