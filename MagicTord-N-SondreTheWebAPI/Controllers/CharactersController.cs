using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagicTord_N_SondreTheWebAPI.Models;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Characters;
using AutoMapper;
using MagicTord_N_SondreTheWebAPI.Services.Characters;
using MagicTord_N_SondreTheWebAPI.Services.Movies;
using System.Net;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Movies;

namespace MagicTord_N_SondreTheWebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly ICharacterService _characterService;
        
        public CharacterController(IMapper mapper, DBContext context, ICharacterService characterService)
        {
            _context = context;
            _characterService = characterService;
            _mapper = mapper;   
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetCharacters()
        {
            return Ok(
                _mapper.Map<List<CharacterDto>>(
                    await _characterService.GetAllAsync())
                );
        }

        // GET: api/v1/Characters/1
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

        // PUT: api/v1/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
                Movies = null,
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
        // POST: api/v1/Characters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CharacterDto>> PostCharacter(Character Character)
        {
            _context.Characters.Add(Character);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetCharacter", new { id = Character.CharacterID }, Character);
        }

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


        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.CharacterID == id);
        }
        
        [HttpGet("{id}/movies")]
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