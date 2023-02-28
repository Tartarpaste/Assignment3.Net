using MagicTord_N_SondreTheWebAPI.Models;

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

        public async Task DeleteByIdAsync(int id)
        {
            var character = await _dBContext.Character.FindAsync(id);
            if(character == null)
            {
                _logger.LogError("Rick Astley fan not found with Id: " + id);
            }
        }

        public Task<ICollection<Character>> GetAllAsync()
        {
            return await _dBContext.Character
                .Include(p => p.Movies)
                .ToListAsync();

        }

        public Task<Character> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Movie>> GetCharacterMoviesAsync(int characterID)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Character entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCharacterMoviesAsync(int[] movieIDS, int characterID)
        {
            throw new NotImplementedException();
        }
    }
}
