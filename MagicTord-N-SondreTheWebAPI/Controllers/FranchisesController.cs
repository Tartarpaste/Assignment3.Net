using Microsoft.AspNetCore.Mvc;
using MagicTord_N_SondreTheWebAPI.Models;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Franchises;
using MagicTord_N_SondreTheWebAPI.Services.Franchises;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace MagicTord_N_SondreTheWebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FranchisesController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly IFranchiseService _franchiseService;
        public FranchisesController(IMapper mapper, DBContext context, IFranchiseService franchiseService)
        {
            _context = context;
            _franchiseService = franchiseService;
            _mapper = mapper;
        }
        // GET: api/v1/Franchises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseDto>>> GetFranchises()
        {
            return Ok(
                _mapper.Map<List<FranchiseDto>>(
                    await _franchiseService.GetAllAsync())
                );
        }
        // GET: api/v1/Franchises/1
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
        // PUT: api/v1/Franchises/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        // POST: api/v1/Franchises
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostFranchise(FranchisePostDto franchiseDTO)
        {
            Franchise franchise = _mapper.Map<Franchise>(franchiseDTO);
            _context.Franchises.Add(franchise);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetFranchise", new { id = franchise.FranchiseID }, franchise);
        }
        // DELETE: api/v1/Franchises/1
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