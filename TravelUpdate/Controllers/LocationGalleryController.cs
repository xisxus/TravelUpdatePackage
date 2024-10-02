using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TravelUpdate.Dal;
using TravelUpdate.Models;
using TravelUpdate.Models.InputModels;

namespace TravelUpdate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationGalleryController : ControllerBase
    {
        private readonly TravelDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public LocationGalleryController(TravelDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: api/LocationGallery/location/{locationId}
        [HttpGet("location/{locationId}")]
        public async Task<ActionResult<IEnumerable<LocationGallery>>> GetGalleriesByLocationId(int locationId)
        {
            var galleries = await _context.LocationGalleries
                .Where(g => g.LocationID == locationId)
                .ToListAsync();

            if (galleries == null || galleries.Count == 0)
            {
                return NotFound();
            }

            return galleries;
        }

        // POST: api/LocationGallery
        [HttpPost]
        public async Task<ActionResult<LocationGallery>> PostLocationGallery(LocationGalleryInsertModel model)
        {
            if (model.ImageFile == null || model.ImageFile.Length == 0)
            {
                return BadRequest("Image file is required.");
            }

            var locationGallery = new LocationGallery
            {
                IsPrimary = model.IsPrimary,
                ImageCaption = model.ImageCaption,
                LocationID = model.LocationID
            };

            locationGallery.ImageUrl = await SaveGallery(model.ImageFile);
            _context.LocationGalleries.Add(locationGallery);
            await _context.SaveChangesAsync();

            var url = Url.Action(nameof(GetGalleriesByLocationId), new { locationId = locationGallery.LocationID });
            return Created(url, locationGallery);
        }

        // PUT: api/LocationGallery/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocationGallery(int id, LocationGalleryInsertModel model)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var locationGallery = await _context.LocationGalleries.FindAsync(id);
            if (locationGallery == null)
            {
                return NotFound();
            }

            locationGallery.IsPrimary = model.IsPrimary;
            locationGallery.ImageCaption = model.ImageCaption;
            locationGallery.LocationID = model.LocationID;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                locationGallery.ImageUrl = await SaveGallery(model.ImageFile);
            }

            _context.Entry(locationGallery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationGalleryExists(id))
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

        // DELETE: api/LocationGallery/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationGallery(int id)
        {
            var locationGallery = await _context.LocationGalleries.FindAsync(id);
            if (locationGallery == null)
            {
                return NotFound();
            }

            _context.LocationGalleries.Remove(locationGallery);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<string> SaveGallery(IFormFile imageFile)
        {
            var fileExtension = Path.GetExtension(imageFile.FileName);
            var uniqueFileName = $"{Path.GetFileNameWithoutExtension(imageFile.FileName)}_{Guid.NewGuid()}{fileExtension}";
            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Uploads", "Locations");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return Path.Combine("Uploads", "Locations", uniqueFileName);
        }

        private bool LocationGalleryExists(int id)
        {
            return _context.LocationGalleries.Any(e => e.LocationGalleryID == id);
        }
    }
}
