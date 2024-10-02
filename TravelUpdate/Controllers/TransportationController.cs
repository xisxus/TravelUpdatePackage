using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelUpdate.Models.InputModels;
using TravelUpdate.Models;
using TravelUpdate.Dal;

namespace TravelUpdate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportationController : ControllerBase
    {
        private readonly TravelDBContext _context;

        public TransportationController(TravelDBContext context)
        {
            _context = context;
        }
        // GET: api/Transportation/get-all
        [HttpGet("transportations")]
        public async Task<IActionResult> GetAllTransportation()
        {
            var transportations = await _context.Transportations
                .Include(t => t.TransportProvider)
                .Select(t => new
                {
                    t.TransportationID,
                    t.IsActive,
                    t.TransportProviderID,
                    TransportProviderName = t.TransportProvider.Name,
                    t.Description
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                data = transportations
            });
        }

       

        // POST: api/Transportation/add
        [HttpPost("add-transport")]
        public async Task<IActionResult> AddTransportation([FromBody] TransportationInsertModel model, string? customUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transportation = new Transportation
            {
                IsActive = model.IsActive,
                TransportProviderID = model.TransportProviderID,
                Description = model.Description
            };

            await _context.Transportations.AddAsync(transportation);
            await _context.SaveChangesAsync();

            var url = customUrl ?? $"api/Transportation/get-by-id/{transportation.TransportationID}";

            return Ok(new
            {
                success = true,
                message = "Transportation added successfully.",
                transportationID = transportation.TransportationID,
                url
            });
        }

        // GET: api/Transportation/get-by-id/{id}
        [HttpGet("get-transport-by-id/{id}")]
        public async Task<IActionResult> GetTransportationById(int id)
        {
            var transportation = await _context.Transportations
                .Include(t => t.TransportProvider)
                .Where(t => t.TransportationID == id)
                .Select(t => new
                {
                    t.TransportationID,
                    t.IsActive,
                    t.TransportProviderID,
                    TransportProviderName = t.TransportProvider.Name,
                    t.Description
                })
                .FirstOrDefaultAsync();

            if (transportation == null)
            {
                return NotFound(new { success = false, message = "Transportation not found." });
            }

            return Ok(new
            {
                success = true,
                data = transportation
            });
        }

        // PUT: api/Transportation/update/{id}
        [HttpPut("update-transport/{id}")]
        public async Task<IActionResult> UpdateTransportation(int id, [FromBody] TransportationInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTransportation = await _context.Transportations.FindAsync(id);

            if (existingTransportation == null)
            {
                return NotFound(new { success = false, message = "Transportation not found." });
            }

            existingTransportation.IsActive = model.IsActive;
            existingTransportation.TransportProviderID = model.TransportProviderID;
            existingTransportation.Description = model.Description;

            _context.Transportations.Update(existingTransportation);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Transportation updated successfully.",
                transportationID = existingTransportation.TransportationID
            });
        }

        // DELETE: api/Transportation/delete/{id}
        [HttpDelete("delete-transport/{id}")]
        public async Task<IActionResult> DeleteTransportation(int id)
        {
            var transportation = await _context.Transportations.FindAsync(id);

            if (transportation == null)
            {
                return NotFound(new { success = false, message = "Transportation not found." });
            }

            _context.Transportations.Remove(transportation);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Transportation deleted successfully."
            });
        }
    }
}
