using Microsoft.AspNetCore.Mvc;
using MagicTord_N_SondreTheWebAPI.Models;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Characters;
using AutoMapper;
using MagicTord_N_SondreTheWebAPI.Services.Characters;
using System.Net;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Movies;

namespace MagicTord_N_SondreTheWebAPI.Controllers
{

    /// <summary>
    /// The controller class for the character table in the database.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly ICharacterService _characterService;
        
        /// <summary>
        /// Constructor for the CharactersController class
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="context"></param>
        /// <param name="characterService"></param>
        public CharacterController(IMapper mapper, DBContext context, ICharacterService characterService)
        {
            _context = context;
            _characterService = characterService;
            _mapper = mapper;   
            
        }

        /// <summary>
        /// Gets all characters from the database
        /// </summary>
        /// <returns>A list of characters</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetCharacters()
        {
            return Ok(
                _mapper.Map<List<CharacterDto>>(
                    await _characterService.GetAllAsync())
                );
        }

        /// <summary>
        /// Gets a single character by id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A character</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDto>> GetCharacter(int id)
        {
            try
            {
                return Ok(_mapper.Map<CharacterDto>(
                        await _characterService.GetByIdAsync(id))
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

        /// <summary>
        /// Updates a character in the database with new values
        /// </summary>
        /// <param name="id"></param>
        /// <param name="character"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, CharacterPutDto character)
        {
            Character updatedcharacter = new Character
            {
                CharacterID = character.CharacterID,
                FullName = character.FullName,
                Alias = character.Alias,
                Gender = character.Gender,
                PictureURL = character.PictureURL,
                Movies = null!,
            };

            if (id != character.CharacterID)
                return BadRequest();

            try
            {
                await _characterService.UpdateAsync(
                        _mapper.Map<Character>(updatedcharacter)
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
        /// Creates a new character in the database
        /// </summary>
        /// <param name="CharacterDTO"></param>
        [HttpPost]
        public async Task<ActionResult> PostCharacter(CharacterPostDto CharacterDTO)
        {
            Character Character = _mapper.Map<Character>(CharacterDTO);
            _context.Characters.Add(Character);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacter", new { id = Character.CharacterID }, Character);
        }

        /// <summary>
        /// Deletes a character from the database
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            try
            {
                await _characterService.DeleteAsync(id);
                return Ok(_mapper.Map<CharacterDto>(
                        await _characterService.GetByIdAsync(id)));
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
        /// Gets all movies a character plays in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of movies</returns>
        [HttpGet("Movies/{id}")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMoviesForCharactersAsync(int id)
        {
            try
            {
                return Ok(
                _mapper.Map<List<MovieDto>>(
                            await _characterService.GetCharacterMoviesAsync(id)
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