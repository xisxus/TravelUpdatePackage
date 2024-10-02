using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelUpdate.Dal;
using TravelUpdate.Models.InputModels;
using TravelUpdate.Models;

namespace TravelUpdate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportProviderController : ControllerBase
    {
        private readonly TravelDBContext _context;

        public TransportProviderController(TravelDBContext context)
        {
            _context = context;
        }

        // GET: api/TransportProvider/get-all
        [HttpGet("providers")]
        public async Task<IActionResult> GetAllTransportProviders()
        {
            var providers = await _context.TransportProviders
                .Select(tp => new
                {
                    tp.TransportProviderID,
                    tp.Name,
                    tp.CompanyName,
                    tp.ContactNumber,
                    tp.Address,
                    tp.IsVerified
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                data = providers
            });
        }

       

        // POST: api/TransportProvider/add
        [HttpPost("add-provider")]
        public async Task<IActionResult> AddTransportProvider([FromBody] TransportProviderInsertModel model, string? customUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transportProvider = new TransportProvider
            {
                Name = model.Name,
                CompanyName = model.CompanyName,
                ContactNumber = model.ContactNumber,
                Address = model.Address,
                IsVerified = model.IsVerified
            };

            await _context.TransportProviders.AddAsync(transportProvider);
            await _context.SaveChangesAsync();

            var url = customUrl ?? $"api/TransportProvider/get-by-id/{transportProvider.TransportProviderID}";

            return Ok(new
            {
                success = true,
                message = "Transport provider added successfully.",
                transportProviderID = transportProvider.TransportProviderID,
                url
            });
        }
        // GET: api/TransportProvider/get-by-id/{id}
        [HttpGet("get-provider-by-id/{id}")]
        public async Task<IActionResult> GetTransportProviderById(int id)
        {
            var provider = await _context.TransportProviders
                .Where(tp => tp.TransportProviderID == id)
                .Select(tp => new
                {
                    tp.TransportProviderID,
                    tp.Name,
                    tp.CompanyName,
                    tp.ContactNumber,
                    tp.Address,
                    tp.IsVerified
                })
                .FirstOrDefaultAsync();

            if (provider == null)
            {
                return NotFound(new { success = false, message = "Transport provider not found." });
            }

            return Ok(new
            {
                success = true,
                data = provider
            });
        }

        // PUT: api/TransportProvider/update/{id}
        [HttpPut("update-provider/{id}")]
        public async Task<IActionResult> UpdateTransportProvider(int id, [FromBody] TransportProviderInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProvider = await _context.TransportProviders.FindAsync(id);

            if (existingProvider == null)
            {
                return NotFound(new { success = false, message = "Transport provider not found." });
            }

            existingProvider.Name = model.Name;
            existingProvider.CompanyName = model.CompanyName;
            existingProvider.ContactNumber = model.ContactNumber;
            existingProvider.Address = model.Address;
            existingProvider.IsVerified = model.IsVerified;

            _context.TransportProviders.Update(existingProvider);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Transport provider updated successfully.",
                transportProviderID = existingProvider.TransportProviderID
            });
        }

        // DELETE: api/TransportProvider/delete/{id}
        [HttpDelete("delete-provider/{id}")]
        public async Task<IActionResult> DeleteTransportProvider(int id)
        {
            var provider = await _context.TransportProviders.FindAsync(id);

            if (provider == null)
            {
                return NotFound(new { success = false, message = "Transport provider not found." });
            }

            _context.TransportProviders.Remove(provider);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Transport provider deleted successfully."
            });
        }

    }
}
