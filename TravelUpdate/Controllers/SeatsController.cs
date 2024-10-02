using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelUpdate.Models.InputModels;
using TravelUpdate.Models;
using TravelUpdate.Dal;
using Microsoft.EntityFrameworkCore;

namespace TravelUpdate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatsController : ControllerBase
    {
        private readonly TravelDBContext _context;

        public SeatsController(TravelDBContext context)
        {
            _context = context;
        }

        // GET: api/Seats/get-all
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllSeats()
        {
            var seats = await _context.Seats
                .Include(s => s.PackageTransportation) // Assuming you want to include related PackageTransportation
                .Select(s => new
                {
                    s.SeatsID,
                    s.SeatsNumber,
                    PackageTransportationID = s.PackageTransportation != null ? s.PackageTransportation.PackageTransportationID : (int?)null // Adjust as necessary
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                data = seats
            });
        }

        // GET: api/Seats/get-by-id/{id}
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetSeatById(int id)
        {
            var seat = await _context.Seats
                .Include(s => s.PackageTransportation)
                .Where(s => s.SeatsID == id)
                .Select(s => new
                {
                    s.SeatsID,
                    s.SeatsNumber,
                    PackageTransportationID = s.PackageTransportation != null ? s.PackageTransportation.PackageTransportationID : (int?)null
                })
                .FirstOrDefaultAsync();

            if (seat == null)
            {
                return NotFound(new { success = false, message = "Seat not found." });
            }

            return Ok(new
            {
                success = true,
                data = seat
            });
        }

        // POST: api/Seats/add
        [HttpPost("add")]
        public async Task<IActionResult> AddSeat([FromBody] SeatsInsertModel model, string? customUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var seat = new Seats
            {
                SeatsNumber = model.SeatsNumber,
                PackageTransportationID = model.PackageTransportationID // Assuming this is a foreign key
            };

            await _context.Seats.AddAsync(seat);
            await _context.SaveChangesAsync();

            var url = customUrl ?? $"api/Seats/get-by-id/{seat.SeatsID}";

            return Ok(new
            {
                success = true,
                message = "Seat added successfully.",
                seatID = seat.SeatsID,
                url
            });
        }

        // PUT: api/Seats/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateSeat(int id, [FromBody] SeatsInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingSeat = await _context.Seats.FindAsync(id);

            if (existingSeat == null)
            {
                return NotFound(new { success = false, message = "Seat not found." });
            }

            existingSeat.SeatsNumber = model.SeatsNumber;
            existingSeat.PackageTransportationID = model.PackageTransportationID; // Assuming this is a foreign key

            _context.Seats.Update(existingSeat);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Seat updated successfully.",
                seatID = existingSeat.SeatsID
            });
        }

        // DELETE: api/Seats/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSeat(int id)
        {
            var seat = await _context.Seats.FindAsync(id);

            if (seat == null)
            {
                return NotFound(new { success = false, message = "Seat not found." });
            }

            _context.Seats.Remove(seat);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Seat deleted successfully."
            });
        }
    }
}
