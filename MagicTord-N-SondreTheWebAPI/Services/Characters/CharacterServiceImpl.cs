using MagicTord_N_SondreTheWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicTord_N_SondreTheWebAPI.Services.Characters
{
    /// <summary>
    /// Implementation class for the character table in the database
    /// </summary>
    public class CharacterServiceImpl : ICharacterService
    {
        
        private readonly DBContext _dBContext;
        private readonly ILogger<CharacterServiceImpl> _logger;
        
        /// <summary>
        /// Constructor for the Character implementation class
        /// </summary>
        /// <param name="dBContext"></param>
        /// <param name="logger"></param>
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

            if (character != null)
            {
                // Set foreign key properties to null
                foreach (var movie in character.Movies)
                {
                    movie.FranchiseID = 0;
                }

                // Delete the character
                _dBContext.Remove(character);
                await _dBContext.SaveChangesAsync();
            }
            else
            {
                _logger.LogError("Rick Astley fan not found with Id: " + id);
            }
        }



        public async Task<ICollection<Character>> GetAllAsync()
        {
            return await _dBContext.Characters
                .Select(c => new Character
                {
                    CharacterID = c.CharacterID,
                    FullName = c.FullName,
                    Alias = c.Alias,
                    Gender = c.Gender,
                    PictureURL = c.PictureURL,
                    Movies = c.Movies.Select(m => new Movie
                    {
                        MovieID = m.MovieID,
                        MovieTitle = m.MovieTitle,
                        Genre = m.Genre,
                        ReleaseYear = m.ReleaseYear,
                        Director = m.Director,
                        PictureURL = m.PictureURL,
                        TrailerURL = m.TrailerURL,
                        FranchiseID = m.FranchiseID,
                        Characters = m.Characters.Select(c => new Character
                        {
                            CharacterID = c.CharacterID,
                            FullName = c.FullName,
                            Alias = c.Alias,
                            Gender = c.Gender,
                            PictureURL = c.PictureURL,
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();
        }
        
        public async Task<Character> GetByIdAsync(int id)
        {
            return await _dBContext.Characters
                .Where(p => p.CharacterID == id)
                .Select(c => new Character
                {
                    CharacterID = c.CharacterID,
                    FullName = c.FullName,
                    Alias = c.Alias,
                    Gender = c.Gender,
                    PictureURL = c.PictureURL,
                    Movies = c.Movies.Select(m => new Movie
                    {
                        MovieID = m.MovieID,
                        MovieTitle = m.MovieTitle,
                        Genre = m.Genre,
                        ReleaseYear = m.ReleaseYear,
                        Director = m.Director,
                        PictureURL = m.PictureURL,
                        TrailerURL = m.TrailerURL,
                        FranchiseID = m.FranchiseID,
                        Characters = m.Characters.Select(c => new Character
                        {
                            CharacterID = c.CharacterID,
                            FullName = c.FullName,
                            Alias = c.Alias,
                            Gender = c.Gender,
                            PictureURL = c.PictureURL,
                        }).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync();
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
    }
}