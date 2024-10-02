using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TravelUpdate.Models.InputModels;
using TravelUpdate.Models.OutputModels;
using TravelUpdate.Models;
using TravelUpdate.Dal;

namespace TravelUpdate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemsController : ControllerBase
    {
        private readonly TravelDBContext _context;

        public FoodItemsController(TravelDBContext context)
        {
            _context = context;
        }
        // GET: api/FoodItems
        [HttpGet]
        public async Task<IActionResult> GetAllFoodItems()
        {

            var foodItems = await _context.FoodItems
                .Select(item => new FoodItemOutputModel
                {
                    FoodItemID = item.FoodItemID,
                    ItemName = item.ItemName,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt

                })
                .ToListAsync();

            if (!foodItems.Any())
            {
                return NotFound(new { success = false, message = "No food items found." });
            }

            return Ok(new { success = true, data = foodItems });
        }


        // POST: api/FoodItems
        [HttpPost("add-fooditem")]
        public async Task<IActionResult> CreateFoodItem([FromBody] FoodItemInputModel model, [FromQuery] string? customUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foodItem = new FoodItem
            {
                ItemName = model.ItemName,

                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,

            };

            _context.FoodItems.Add(foodItem);
            await _context.SaveChangesAsync();

            var url = customUrl ?? "getfooditem";

            return CreatedAtAction(nameof(CreateFoodItem), new { foodItemId = foodItem.FoodItemID }, new
            {
                success = true,
                message = "Food item created successfully.",
                foodItemId = foodItem.FoodItemID,
                url
            });
        }


        // PUT: api/FoodItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodItem(int id, FoodItemInputModel foodItemModel)
        {
            if (id != foodItemModel.FoodItemID)
            {
                return BadRequest();
            }

            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }


            foodItem.ItemName = foodItemModel.ItemName;
            foodItem.CreatedAt = foodItemModel.CreatedAt;
            foodItem.UpdatedAt = foodItemModel.UpdatedAt;

            _context.Entry(foodItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodItemExists(id))
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

        // DELETE: api/FoodItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodItem(int id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            _context.FoodItems.Remove(foodItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodItemExists(int id)
        {
            return _context.FoodItems.Any(e => e.FoodItemID == id);
        }
    }
}
