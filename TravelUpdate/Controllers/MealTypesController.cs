using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TravelUpdate.Dal;
using TravelUpdate.Models;

namespace TravelUpdate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealTypesController : ControllerBase
    {
        private readonly TravelDBContext _context;

        public MealTypesController(TravelDBContext context)
        {
            _context = context;
        }

        // GET: api/MealTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealType>>> GetMealTypes()
        {
            return await _context.MealTypes.ToListAsync();
        }

        // GET: api/MealTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MealType>> GetMealType(int id)
        {
            var mealType = await _context.MealTypes.FindAsync(id);

            if (mealType == null)
            {
                return NotFound();
            }

            return mealType;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMealType(int id, MealType mealType)
        {
            if (id != mealType.MealTypeID)
            {
                return BadRequest();
            }

            _context.Entry(mealType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealTypeExists(id))
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

        [HttpPost]
        public async Task<ActionResult<MealType>> PostMealType(MealType mealType)
        {
            _context.MealTypes.Add(mealType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMealType", new { id = mealType.MealTypeID }, mealType);
        }

        // DELETE: api/MealTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMealType(int id)
        {
            var mealType = await _context.MealTypes.FindAsync(id);
            if (mealType == null)
            {
                return NotFound();
            }

            _context.MealTypes.Remove(mealType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MealTypeExists(int id)
        {
            return _context.MealTypes.Any(e => e.MealTypeID == id);
        }
    }
}
