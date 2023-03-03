using MagicTord_N_SondreTheWebAPI.Models;

namespace MagicTord_N_SondreTheWebAPI.Services.Movies
{
    /// <summary>
    /// The interface for all the tasks one can perform with the Movie table
    /// </summary>
    public interface IMovieService : ICrudService<Movie, int>
    {
        /// <summary>
        /// Gets all characters that play in a movie
        /// </summary>
        /// <param name="MovieId"></param>
        /// <returns>A collection of characters</returns>
        Task<ICollection<Character>> GetMovieCharactersAsync(int MovieId);

        /// <summary>
        /// Updates a movie to inlcude new characters. 
        /// Movies that a character already plays in are kept to follow through with the many to many relationship
        /// </summary>
        /// <param name="characterIds"></param>
        /// <param name="MovieId"></param>
        Task UpdateMovieCharactersAsync(int[] characterIds, int MovieId);
    }
}
