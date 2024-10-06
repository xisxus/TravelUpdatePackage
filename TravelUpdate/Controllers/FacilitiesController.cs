using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelUpdate.Models.InputModels;
using TravelUpdate.Models.OutputModels;
using TravelUpdate.Models;
using TravelUpdate.Dal;
using Microsoft.EntityFrameworkCore;

namespace TravelUpdate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilitiesController : ControllerBase
    {
        private readonly TravelDBContext _context;

        public FacilitiesController(TravelDBContext context)
        {
            _context = context;
        }

        // GET: api/Facilities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacilityOutputModel>>> GetFacilities()
        {
            var facilities = await _context.Facilities
                .Select(f => new FacilityOutputModel
                {
                    FacilityID = f.FacilityID,
                    FacilityName = f.FacilityName,
                    Description = f.Description,
                    IsAvailable = f.IsAvailable
                })
                .ToListAsync();

            return Ok(facilities);
        }

        // GET: api/Facilities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacilityOutputModel>> GetFacility(int id)
        {
            var facility = await _context.Facilities
                .Where(f => f.FacilityID == id)
                .Select(f => new FacilityOutputModel
                {
                    FacilityID = f.FacilityID,
                    FacilityName = f.FacilityName,
                    Description = f.Description,
                    IsAvailable = f.IsAvailable
                })
                .FirstOrDefaultAsync();

            if (facility == null)
            {
                return NotFound();
            }

            return Ok(facility);
        }

        // POST: api/Facilities
        [HttpPost]
        public async Task<ActionResult<FacilityOutputModel>> CreateFacility(FacilityInputModel inputModel)
        {
            var facility = new Facility
            {
                FacilityName = inputModel.FacilityName,
                Description = inputModel.Description,
                IsAvailable = inputModel.IsAvailable
            };

            _context.Facilities.Add(facility);
            await _context.SaveChangesAsync();

            var outputModel = new FacilityOutputModel
            {
                FacilityID = facility.FacilityID,
                FacilityName = facility.FacilityName,
                Description = facility.Description,
                IsAvailable = facility.IsAvailable
            };

            return CreatedAtAction(nameof(GetFacility), new { id = outputModel.FacilityID }, outputModel);
        }

        // PUT: api/Facilities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFacility(int id, FacilityInputModel inputModel)
        {
            var facility = await _context.Facilities.FindAsync(id);

            if (facility == null)
            {
                return NotFound();
            }

            facility.FacilityName = inputModel.FacilityName;
            facility.Description = inputModel.Description;
            facility.IsAvailable = inputModel.IsAvailable;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Facilities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacility(int id)
        {
            var facility = await _context.Facilities.FindAsync(id);

            if (facility == null)
            {
                return NotFound();
            }

            _context.Facilities.Remove(facility);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

