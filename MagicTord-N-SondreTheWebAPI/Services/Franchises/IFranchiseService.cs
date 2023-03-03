using MagicTord_N_SondreTheWebAPI.Models;

namespace MagicTord_N_SondreTheWebAPI.Services.Franchises
{
    /// <summary>
    /// The interface for all the tasks one can perform with the Franchise table
    /// </summary>
    public interface IFranchiseService : ICrudService<Franchise, int>
    {
        /// <summary>
        /// Gets all movies that are witihn a franchise
        /// </summary>
        /// <param name="FranchiseId"></param>
        /// <returns>A collection of movies</returns>
        Task<ICollection<Movie>> GetFranchiseMoviesAsync(int FranchiseId);

        /// <summary>
        /// Gets all characters that are within a franchise
        /// </summary>
        /// <param name="FranchiseId"></param>
        /// <returns>A collection of characters</returns>
        Task<ICollection<Character>> GetFranchiseCharactersAsync(int FranchiseId);

        /// <summary>
        /// Updates the movies that are invlolved with a franchise
        /// </summary>
        /// <param name="movieIdS"></param>
        /// <param name="FranchiseId"></param>
        Task UpdateFranchiseMoviesAsync(int[] movieIdS, int FranchiseId);
    }
}
