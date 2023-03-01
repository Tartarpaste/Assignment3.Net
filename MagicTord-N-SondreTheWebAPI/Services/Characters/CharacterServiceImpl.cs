using MagicTord_N_SondreTheWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicTord_N_SondreTheWebAPI.Services.Characters
{
    public class CharacterServiceImpl : ICharacterService
    {

        private readonly DBContext _dBContext;
        private readonly ILogger<CharacterServiceImpl> _logger;

        public CharacterServiceImpl(DBContext dBContext, ILogger<CharacterServiceImpl> logger)
        {
            _dBContext = dBContext;
            _logger = logger;
        }

        public async Task AddAsync(Character entity)
        {
            await _dBContext.AddAsync(entity);
            await _dBContext.SaveChangesAsync();
            
        }

        public async Task DeleteAsync(int id)
        {
            var character = await _dBContext.Characters.FindAsync(id);
            if(character == null)
            {
                _logger.LogError("Rick Astley fan not found with Id: " + id);
            }
        }

        public async Task<ICollection<Character>> GetAllAsync()
        {
            return await _dBContext.Characters
                .Include(p => p.Movies)
                .ToListAsync();

        }

        public async Task<Character> GetByIdAsync(int id)
        {
            //ADD null check
            return await _dBContext.Characters
                .Where(p => p.CharacterID == id)
                .Include(p => p.Movies)
                .FirstAsync();


        }

        public async Task<ICollection<Movie>> GetCharacterMoviesAsync(int characterID)
        {
            var query = _dBContext.Set<Movie>()
                .Where(m => _dBContext.Set<CharacterMovie>()
                    .Where(cm => cm.CharacterID == characterID)
                    .Select(cm => cm.MovieID)
                    .Contains(m.MovieID));

            return await query.ToListAsync();

        }

        public async Task UpdateAsync(Character entity)
        {
            _dBContext.Entry(entity).State = EntityState.Modified;
            await _dBContext.SaveChangesAsync();

        }

        public async Task UpdateCharacterMoviesAsync(HashSet<Movie> movieIDS, int characterID)
        {
            Character character = await _dBContext.Characters
                .Where(p => p.CharacterID == characterID)
                .FirstAsync();
            // Set the characters movies
            character.Movies = movieIDS;
            _dBContext.Entry(character).State = EntityState.Modified;
            // Save all the changes
            await _dBContext.SaveChangesAsync();

        }

    }
}
