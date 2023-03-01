using MagicTord_N_SondreTheWebAPI.Models;
using MagicTord_N_SondreTheWebAPI.Services.Characters;
using Microsoft.EntityFrameworkCore;

namespace MagicTord_N_SondreTheWebAPI.Services.Franchises
{
    public class FranchiseServiceImpl : IFranchiseService
    {

        private readonly DBContext _dBContext;
        private readonly ILogger<CharacterServiceImpl> _logger;

        public FranchiseServiceImpl(DBContext dBContext, ILogger<CharacterServiceImpl> logger)
        {
            _dBContext = dBContext;
            _logger = logger;
        }

        public async Task AddAsync(Franchise entity)
        {
            await _dBContext.AddAsync(entity);
            await _dBContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var franchise = await _dBContext.Franchises.FindAsync(id);
            if (franchise == null)
            {
                _logger.LogError("Rick Astley Franchise not found with Id: " + id);
            }
        }

        public async Task<ICollection<Franchise>> GetAllAsync()
        {
            return await _dBContext.Franchises
                .Include(p => p.Movies)
                .ToListAsync();

        }

        public async Task<Franchise> GetByIdAsync(int id)
        {
            //ADD null check
            return await _dBContext.Franchises
                .Where(p => p.FranchiseID == id)
                .Include(p => p.Movies)
                .FirstAsync();


        }

        public async Task<ICollection<Movie>> GetFranchiseMoviesAsync(int franchiseID)
        {
            return await _dBContext.Franchises
                 .Where(p => p.FranchiseID == franchiseID)
                 .Select(p => p.Movies)
                 .FirstAsync();

        }

        public async Task UpdateAsync(Franchise entity)
        {
            _dBContext.Entry(entity).State = EntityState.Modified;
            await _dBContext.SaveChangesAsync();

        }

        public async Task UpdateFranchiseMoviesAsync(HashSet<Movie> movieIDS, int franchiseID)
        {
            Franchise franchise = await _dBContext.Franchises
                .Where(p => p.FranchiseID == franchiseID)
                .FirstAsync();
            // Set the franchise movies
            franchise.Movies = movieIDS;
            _dBContext.Entry(franchise).State = EntityState.Modified;
            // Save all the changes
            await _dBContext.SaveChangesAsync();

        }

    }
}
