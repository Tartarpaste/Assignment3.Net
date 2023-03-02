using MagicTord_N_SondreTheWebAPI.Models;

namespace MagicTord_N_SondreTheWebAPI.Services.Franchises
{
    public interface IFranchiseService : ICrudService<Franchise, int>
    {
        Task<ICollection<Movie>> GetFranchiseMoviesAsync(int FranchiseId);
        Task<ICollection<Character>> GetFranchiseCharactersAsync(int FranchiseId);

        Task UpdateFranchiseMoviesAsync(HashSet<Movie> movieIDS, int FranchiseId);
    }
}
