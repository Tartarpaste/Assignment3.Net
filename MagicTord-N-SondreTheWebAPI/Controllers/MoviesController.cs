using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagicTord_N_SondreTheWebAPI.Models;
using AutoMapper;
using MagicTord_N_SondreTheWebAPI.Services.Franchises;
using MagicTord_N_SondreTheWebAPI.Services.Movies;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Franchises;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Movies;
using System.Net;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Characters;

namespace MagicTord_N_SondreTheWebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly IMovieService _movieService;

        public MoviesController(IMapper mapper, DBContext context, IMovieService movieService)
        {
            _context = context;
            _movieService = movieService;
            _mapper = mapper;

        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return Ok(
                _mapper.Map<List<MovieDto>>(
                    await _movieService.GetAllAsync())
                );
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            try
            {
                return Ok(_mapper.Map<MovieDto>(
                        await _movieService.GetByIdAsync(id))
                    );
            }
            catch (Exception ex)
            {
                // Formatting an error code for the exception messages.
                // Using the built in Problem Details.
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }

        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.MovieID)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.MovieID }, movie);
        }

        // DELETE: api/Movies/5
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

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieID == id);
        }


        [HttpGet("{id}/characters")]
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
                // Formatting an error code for the exception messages.
                // Using the built in Problem Details.
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
