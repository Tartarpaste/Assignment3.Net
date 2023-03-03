using MagicTord_N_SondreTheWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicTord_N_SondreTheWebAPI.Services.Movies
{
    /// <summary>
    /// Implementation class for the movie table in the database
    /// </summary>
    public class MovieServiceImpl : IMovieService
    {
        private readonly DBContext _dBContext;
        private readonly ILogger<MovieServiceImpl> _logger;

        /// <summary>
        /// Constructor for the Movie implementation class
        /// </summary>
        /// <param name="dBContext"></param>
        /// <param name="logger"></param>
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
            var movie = await _dBContext.Movies.FindAsync(id);

            if (movie != null)
            {
                // Set foreign key properties to null
                foreach (var character in movie.Characters)
                {
                    character.CharacterID = 0;
                }

                // Delete the character
                _dBContext.Remove(movie);
                await _dBContext.SaveChangesAsync();
            }
            else
            {
                _logger.LogError("Rick Astley fan not found with Id: " + id);
            }
        }

        public async Task<ICollection<Movie>> GetAllAsync()
        {
            return await _dBContext.Movies
                 .Select(m => new Movie
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
                         Movies = c.Movies.Select(n => new Movie
                         {
                             MovieID = n.MovieID,
                             MovieTitle = n.MovieTitle,
                             Genre = n.Genre,
                             ReleaseYear = n.ReleaseYear,
                             Director = n.Director,
                             PictureURL = n.PictureURL,
                             TrailerURL = n.TrailerURL,
                             FranchiseID = n.FranchiseID,
                         }).ToList()
                         // Other movie properties
                     }).ToList()
                 })
                 .ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _dBContext.Movies
                .Where(m => m.MovieID == id)
                .Select(m => new Movie
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
                        Movies = c.Movies.Select(n => new Movie
                        {
                            MovieID = n.MovieID,
                            MovieTitle = n.MovieTitle,
                            Genre = n.Genre,
                            ReleaseYear = n.ReleaseYear,
                            Director = n.Director,
                            PictureURL = n.PictureURL,
                            TrailerURL = n.TrailerURL,
                            FranchiseID = n.FranchiseID,
                        }).ToList()
                        // Other movie properties
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (movie == null)
            {
                // Handle null case
                throw new Exception($"Movie with ID {id} not found.");
            }

            return movie;
        }

        public async Task<ICollection<Character>> GetMovieCharactersAsync(int movieID)
        {
            var query = _dBContext.Set<Character>()
                .Where(c => _dBContext.Set<CharacterMovie>()
                    .Where(cm => cm.MovieID == movieID)
                    .Select(cm => cm.CharacterID)
                    .Contains(c.CharacterID));

            return await query.ToListAsync();
        }

        public async Task UpdateAsync(Movie entity)
        {
            _dBContext.Entry(entity).State = EntityState.Modified;
            await _dBContext.SaveChangesAsync();

        }

        public async Task UpdateMovieCharactersAsync(int[] characterIds, int movieId)
        {
            List<CharacterMovie> charactersToUpdate = characterIds.Select(characterId => new CharacterMovie { CharacterID = characterId, MovieID = movieId }).ToList();
            
            _dBContext.CharacterMovies.AddRange(charactersToUpdate);
            await _dBContext.SaveChangesAsync();




        }
    }
}