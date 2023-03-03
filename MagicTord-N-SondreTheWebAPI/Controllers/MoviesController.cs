using Microsoft.AspNetCore.Mvc;
using MagicTord_N_SondreTheWebAPI.Models;
using AutoMapper;
using MagicTord_N_SondreTheWebAPI.Services.Movies;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Movies;
using System.Net;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Characters;

namespace MagicTord_N_SondreTheWebAPI.Controllers
{
    /// <summary>
    /// The controller class for the movie table in the database.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly IMovieService _movieService;

        /// <summary>
        /// Constructor for the MoviesController class
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="context"></param>
        /// <param name="movieService"></param>
        public MoviesController(IMapper mapper, DBContext context, IMovieService movieService)
        {
            _context = context;
            _movieService = movieService;
            _mapper = mapper;

        }

        /// <summary>
        /// Gets all movies from the database
        /// </summary>
        /// <returns>A list of movies</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            return Ok(
                _mapper.Map<List<MovieDto>>(
                    await _movieService.GetAllAsync())
                );
        }

        /// <summary>
        /// Gets a movie by id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A movie</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            try
            {
                return Ok(_mapper.Map<MovieDto>(
                        await _movieService.GetByIdAsync(id))
                    );
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }
        }

        /// <summary>
        /// Updates a movie in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movie"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieAsync(int id, MoviePutDto movie)
        {
            Movie updatedMovie = new Movie 
            { 
            MovieID = movie.MovieID,
            MovieTitle =  movie.MovieTitle,
            Genre = movie.Genre,
            ReleaseYear= movie.ReleaseYear, 
            Director= movie.Director,
            PictureURL= movie.PictureURL,   
            TrailerURL= movie.TrailerURL,
            FranchiseID= movie.FranchiseID,
            Characters = null
            };

            if (id != movie.MovieID)
                return BadRequest();

            try
            {
                await _movieService.UpdateAsync(
                        _mapper.Map<Movie>(updatedMovie)
                    );
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }
        }

        /// <summary>
        /// Creates a new movie in the database
        /// </summary>
        /// <param name="movieDto"></param>
        [HttpPost]
        public async Task<ActionResult> PostMovie(MoviePostDto movieDto)
        {
            Movie movie = _mapper.Map<Movie>(movieDto);
            await _movieService.AddAsync(movie);
            return CreatedAtAction("GetMovie", new { id = movie.MovieID }, movie);

        }

        /// <summary>
        /// Deletes a movie from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Updates the characters that plays in a movie.
        /// </summary>
        /// <param name="characterIds"></param>
        /// <param name="movieId"></param>
        [HttpPut("{id}/characters")]
        public async Task<IActionResult> UpdateCharactersForMovieAsync(int[] characterIds, int movieId)
        {
            try
            {
                await _movieService.UpdateMovieCharactersAsync(characterIds,movieId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }
        }

        /// <summary>
        /// Gets all characters that play in a movie from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of characters</returns>
        [HttpGet("Characters/{id}")]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetCharactersForMovieAsync(int id)
        {
            try
            {
                return Ok(
                        _mapper.Map<List<CharacterDto>>(
                            await _movieService.GetMovieCharactersAsync(id)
                        )
                    );
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }
        }
    }
}
