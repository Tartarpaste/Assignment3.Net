using Microsoft.AspNetCore.Mvc;
using MagicTord_N_SondreTheWebAPI.Models;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Franchises;
using MagicTord_N_SondreTheWebAPI.Services.Franchises;
using AutoMapper;
using System.Net;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Movies;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Characters;

namespace MagicTord_N_SondreTheWebAPI.Controllers
{
    /// <summary>
    /// The controller class for the franchise table in the database.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FranchisesController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly IFranchiseService _franchiseService;

        /// <summary>
        /// Constructor for the FranchisesController class
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="context"></param>
        /// <param name="franchiseService"></param>
        public FranchisesController(IMapper mapper, DBContext context, IFranchiseService franchiseService)
        {
            _context = context;
            _franchiseService = franchiseService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all franchises from the database
        /// </summary>
        /// <returns>A list of franchises</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseDto>>> GetFranchises()
        {
            return Ok(
                _mapper.Map<List<FranchiseDto>>(
                    await _franchiseService.GetAllAsync())
                );
        }

        /// <summary>
        /// Gets a franchise by id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A franchise</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseDto>> GetFranchise(int id)
        {
            try
            {
                return Ok(_mapper.Map<FranchiseDto>(
                        await _franchiseService.GetByIdAsync(id))
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
        /// Updates a franchise in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="franchise"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchisePutDto franchise)
        {
            Franchise updatedFranchise = new Franchise
            {
                FranchiseID = franchise.FranchiseID,
                Name = franchise.Name,
                Description = franchise.Description,
                Movies = null,
            };
            if (id != franchise.FranchiseID)
                return BadRequest();
            try
            {
                await _franchiseService.UpdateAsync(
                        _mapper.Map<Franchise>(updatedFranchise)
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
        /// Creates a new franchise in the database
        /// </summary>
        /// <param name="franchiseDTO"></param>
        [HttpPost]
        public async Task<ActionResult> PostFranchise(FranchisePostDto franchiseDTO)
        {
            Franchise franchise = _mapper.Map<Franchise>(franchiseDTO);
            _context.Franchises.Add(franchise);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetFranchise", new { id = franchise.FranchiseID }, franchise);
        }

        /// <summary>
        /// Gets all movies in a franchise from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of movies</returns>
        [HttpGet("Movies/{id}")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMoviesForFranchiseAsync(int id)
        {
            try
            {
                return Ok(
                _mapper.Map<List<MovieDto>>(
                            await _franchiseService.GetFranchiseMoviesAsync(id)
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

        /// <summary>
        /// Gets all charactes in a franchise from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of characters</returns>
        [HttpGet("Characters/{id}")]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetCharactersForFranchiseAsync(int id)
        {
            try
            {
                return Ok(
                _mapper.Map<List<CharacterDto>>(
                            await _franchiseService.GetFranchiseCharactersAsync(id)
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

        /// <summary>
        /// Updates what movies a franchise has in the database
        /// </summary>
        /// <param name="movieIds"></param>
        /// <param name="franchiseId"></param>
        [HttpPut("{id}/movies")]
        public async Task<IActionResult> UpdateMoviesForFranchiseAsync(int[] movieIds, int franchiseId)
        {
            try
            {
                await _franchiseService.UpdateFranchiseMoviesAsync(movieIds, franchiseId);
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
        /// Deletes a franchise from the database
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise == null)
            {
                return NotFound();
            }
            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}