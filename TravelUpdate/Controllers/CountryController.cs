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
    public class CountryController : ControllerBase
    {
        private readonly TravelDBContext _context;

        public CountryController(TravelDBContext context)
        {
            _context = context;
        }

        // GET: api/Country
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return await _context.Countries.Include(c => c.States).ToListAsync();
        }

        // GET: api/Country/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _context.Countries.Include(c => c.States)
                .FirstOrDefaultAsync(c => c.CountryID == id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        // POST: api/Country
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CountryInsertModel model)
        {
            var country = new Country
            {
                CountryName = model.CountryName
            };

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            var url = Url.Action(nameof(GetCountry), new { id = country.CountryID });
            return Created(url, country);
        }

        // PUT: api/Country/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, CountryInsertModel model)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            country.CountryName = model.CountryName;

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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

        // DELETE: api/Country/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.CountryID == id);
        }
    }
}
