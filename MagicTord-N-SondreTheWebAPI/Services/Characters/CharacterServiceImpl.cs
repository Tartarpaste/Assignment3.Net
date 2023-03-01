using AutoMapper;
using MagicTord_N_SondreTheWebAPI.Models;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Characters;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Movies;
using Microsoft.EntityFrameworkCore;

namespace MagicTord_N_SondreTheWebAPI.Services.Characters
{
    public class CharacterServiceImpl : ICharacterService
    {

        private readonly DBContext _dBContext;
        private readonly ILogger<CharacterServiceImpl> _logger;
        private readonly IMapper _mapper;

        public CharacterServiceImpl(DBContext dBContext, ILogger<CharacterServiceImpl> logger, IMapper mapper)
        {
            _dBContext = dBContext;
            _logger = logger;
            _mapper = mapper;
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

        async Task<ICollection<MovieDto>> ICharacterService.GetCharacterMoviesAsync(int CharacterId)
        {
            var movies = await _dBContext.Movies
                .Where(m => m.Characters.Any(c => c.CharacterID == CharacterId))
                .ToListAsync();

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<List<Movie>, MovieDto>()
                    .ForMember(dto => dto.Characters, opt => opt.MapFrom(src => src.SelectMany(m => m.Characters)));
                cfg.CreateMap<Movie, MovieDto>()
                    .ForMember(dto => dto.Characters, opt => opt.MapFrom(src => src.Characters));
            }).CreateMapper();

            return mapper.Map<List<Movie>, List<MovieDto>>(movies);
        }




    }
}
