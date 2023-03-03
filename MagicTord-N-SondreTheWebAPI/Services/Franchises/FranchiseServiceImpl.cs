using MagicTord_N_SondreTheWebAPI.Models;
using MagicTord_N_SondreTheWebAPI.Services.Characters;
using Microsoft.EntityFrameworkCore;

namespace MagicTord_N_SondreTheWebAPI.Services.Franchises
{
    /// <summary>
    /// Implementation class for the movie table in the database
    /// </summary>
    public class FranchiseServiceImpl : IFranchiseService
    {

        private readonly DBContext _dBContext;
        private readonly ILogger<CharacterServiceImpl> _logger;

        /// <summary>
        /// Constructor for the Franchise implementation class
        /// </summary>
        /// <param name="dBContext"></param>
        /// <param name="logger"></param>
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

            if (franchise != null)
            {
                // Set foreign key properties to null
                foreach (var movie in franchise.Movies)
                {
                    movie.FranchiseID = 0;
                }

                // Delete the character
                _dBContext.Remove(franchise);
                await _dBContext.SaveChangesAsync();
            }
            else
            {
                _logger.LogError("Rick Astley fan not found with Id: " + id);
            }
        }

        public async Task<ICollection<Franchise>> GetAllAsync()
        {
            return await _dBContext.Franchises
                .Include(f => f.Movies)
                .ToListAsync();

        }

        public async Task<Franchise> GetByIdAsync(int id)
        {
            //ADD null check
            return await _dBContext.Franchises
                .Where(f => f.FranchiseID == id)
                .Include(f => f.Movies)
                .FirstAsync();


        }

        public async Task<ICollection<Character>> GetFranchiseCharactersAsync(int franchiseID)
        {
            return await _dBContext.Movies
                 .Where(f => f.FranchiseID == franchiseID)
                 .SelectMany(f => f.Characters)
                 .ToListAsync();
                    
        }

        public async Task<ICollection<Movie>> GetFranchiseMoviesAsync(int franchiseID)
        {
            return await _dBContext.Franchises
                 .Where(f => f.FranchiseID == franchiseID)
                 .SelectMany(f => f.Movies)
                 .ToListAsync();

        }

        public async Task UpdateAsync(Franchise entity)
        {
            _dBContext.Entry(entity).State = EntityState.Modified;
            await _dBContext.SaveChangesAsync();
        }

        public async Task UpdateFranchiseMoviesAsync(int[] movieIds, int franchiseId)
        {

            List<Movie> moviesToUpdate = await _dBContext.Movies
            .Where(m => movieIds.Contains(m.MovieID))
            .ToListAsync();

            foreach (Movie movie in moviesToUpdate)
            {
                movie.FranchiseID = franchiseId;
            }

            await _dBContext.SaveChangesAsync();

        }

    }
}
