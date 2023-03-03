using MagicTord_N_SondreTheWebAPI.Models;

namespace MagicTord_N_SondreTheWebAPI.Services.Characters
{
    /// <summary>
    /// The interface for all the tasks one can perform with the Character table
    /// </summary>
    public interface ICharacterService : ICrudService<Character, int>
    {
        /// <summary>
        /// Gets all movies a character appears in
        /// </summary>
        /// <param name="CharacterId"></param>
        /// <returns>A collection of movies</returns>
        Task <ICollection<Movie>> GetCharacterMoviesAsync (int CharacterId);
    }
}
