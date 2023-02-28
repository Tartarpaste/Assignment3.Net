using MagicTord_N_SondreTheWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicTord_N_SondreTheWebAPI.Services.Movies
{
    public class MovieServiceImpl : IMovieService
    {
        private readonly DBContext _dBContext;
        private readonly ILogger<MovieServiceImpl> _logger;

        public MovieServiceImpl(DBContext dBContext, ILogger<MovieServiceImpl> logger)
        {
            _dBContext = dBContext;
            _logger = logger;
        }

        public async Task AddAsync(Movie entity)
        {
            await _dBContext.AddAsync(entity);
            await _dBContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var character = await _dBContext.Movies.FindAsync(id);
            if (character == null)
            {
                _logger.LogError("Rick Astley fan not found with Id: " + id);
            }
        }

        public async Task<ICollection<Movie>> GetAllAsync()
        {
            return await _dBContext.Movies
                .Include(p => p.Characters)
                .ToListAsync();

        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            //ADD null check
            return await _dBContext.Movies
                .Where(p => p.MovieID == id)
                .Include(p => p.Characters)
                .FirstAsync();


        }

        public async Task<ICollection<Character>> GetMovieCharactersAsync(int movieID)
        {
            return await _dBContext.Movies
                 .Where(p => p.MovieID == movieID)
                 .Select(p => p.Characters)
                 .FirstAsync();

        }

        public async Task UpdateAsync(Movie entity)
        {
            _dBContext.Entry(entity).State = EntityState.Modified;
            await _dBContext.SaveChangesAsync();

        }

        public async Task UpdateMovieCharactersAsync(HashSet<Character> characterIDS, int movieID)
        {
            Movie movie = await _dBContext.Movies
                .Where(p => p.MovieID == movieID)
                .FirstAsync();
            // Set the characters movies
            movie.Characters = characterIDS;
            _dBContext.Entry(movie).State = EntityState.Modified;
            // Save all the changes
            await _dBContext.SaveChangesAsync();

        }
}
}
