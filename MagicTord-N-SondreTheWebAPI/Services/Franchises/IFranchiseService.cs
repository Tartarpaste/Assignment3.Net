using MagicTord_N_SondreTheWebAPI.Models;

namespace MagicTord_N_SondreTheWebAPI.Services.Franchises
{
    public interface IFranchiseService
    {
        Task<ICollection<Movie>> GetFranchiseMoviesAsync(int FranchiseId);

        Task UpdateFranchiseMoviesAsync(HashSet<Movie> movieIDS, int FranchiseId);
    }
}
