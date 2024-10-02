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
    public class TransportationCatagoryController : ControllerBase
    {
        private readonly TravelDBContext _context;

        public TransportationCatagoryController(TravelDBContext context)
        {
            _context = context;
        }

        // GET: api/TransportationCatagory/get-all
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.TransportationCatagories
                .Select(c => new
                {
                    c.TransportationCatagoryID,
                    c.TransportationCatagoryName
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                data = categories
            });
        }

        // GET: api/TransportationCatagory/get-by-id/{id}
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _context.TransportationCatagories
                .Where(c => c.TransportationCatagoryID == id)
                .Select(c => new
                {
                    c.TransportationCatagoryID,
                    c.TransportationCatagoryName
                })
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound(new { success = false, message = "Transportation category not found." });
            }

            return Ok(new
            {
                success = true,
                data = category
            });
        }

        // POST: api/TransportationCatagory/add-category
        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory([FromBody] TransportationCatagoryInsertModel model, string? customUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCategory = new TransportationCatagory
            {
                TransportationCatagoryName = model.TransportationCatagoryName
            };

            await _context.TransportationCatagories.AddAsync(newCategory);
            await _context.SaveChangesAsync();

            var url = customUrl ?? "get-all"; // Custom URL or default

            return Ok(new
            {
                success = true,
                message = "Transportation category added successfully.",
                categoryId = newCategory.TransportationCatagoryID,
                url // Include custom URL in response
            });
        }

        // PUT: api/TransportationCatagory/update-category/{id}
        [HttpPut("update-category/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] TransportationCatagoryInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.TransportationCatagories.FindAsync(id);
            if (category == null)
            {
                return NotFound(new { success = false, message = "Transportation category not found." });
            }

            category.TransportationCatagoryName = model.TransportationCatagoryName;

            _context.TransportationCatagories.Update(category);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Transportation category updated successfully."
            });
        }

        // DELETE: api/TransportationCatagory/delete-category/{id}
        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.TransportationCatagories.FindAsync(id);
            if (category == null)
            {
                return NotFound(new { success = false, message = "Transportation category not found." });
            }

            _context.TransportationCatagories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Transportation category deleted successfully."
            });
        }
    }
}
