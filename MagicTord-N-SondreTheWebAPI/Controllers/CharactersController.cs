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

        // GET: api/v1/Characters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
        {
            return Ok(
                _mapper.Map<List<CharacterDto>>(
                    await _characterService.GetAllAsync())
                );
        }


        // GET: api/v1/Characters/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacter(int id)
        {
            var Character = await _context.Characters.FindAsync(id);

            if (Character == null)
            {
                return NotFound();
            }

            return Character;
        }

        // PUT: api/v1/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, Character Character)
        {
            if (id != Character.CharacterID)
            {
                return BadRequest();
            }

            _context.Entry(Character).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
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

        // POST: api/v1/Characters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(Character Character)
        {
            _context.Characters.Add(Character);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacter", new { id = Character.CharacterID }, Character);
        }

        // DELETE: api/v1/Characters/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var Character = await _context.Characters.FindAsync(id);
            if (Character == null)
            {
                return NotFound();
            }

            _context.Characters.Remove(Character);
            await _context.SaveChangesAsync();

            return NoContent();
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
