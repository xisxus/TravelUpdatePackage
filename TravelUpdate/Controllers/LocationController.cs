using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelUpdate.Dal;
using TravelUpdate.Models;
using TravelUpdate.Models.InputModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelUpdate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly TravelDBContext _context;

        public LocationController(TravelDBContext context)
        {
            _context = context;
        }

        // GET: api/Location
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            return await _context.Locations
                .Include(l => l.State)
                .Include(l => l.LocationGalleries)
                .Include(l => l.Hotels)
                .ToListAsync();
        }

        // GET: api/Location/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var location = await _context.Locations
                .Include(l => l.State)
                .Include(l => l.LocationGalleries)
                .Include(l => l.Hotels)
                .FirstOrDefaultAsync(l => l.LocationID == id);

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        // POST: api/Location
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(LocationInsertModel model)
        {
            var location = new Location
            {
                LocationName = model.LocationName,
                LocationDescription = model.LocationDescription,
                StateID = model.StateID
            };

            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            var url = Url.Action(nameof(GetLocation), new { id = location.LocationID });
            return Created(url, location);
        }

        // PUT: api/Location/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, LocationInsertModel model)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            location.LocationName = model.LocationName;
            location.LocationDescription = model.LocationDescription;
            location.StateID = model.StateID;

            _context.Entry(location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // DELETE: api/Location/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.LocationID == id);
        }
    }
}
