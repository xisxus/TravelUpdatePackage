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
    [ApiController]
    [Route("api/[controller]")]
    public class HotelImageController : ControllerBase
    {
        private readonly TravelDBContext _context;
        private readonly IWebHostEnvironment _env;

        public HotelImageController(TravelDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost]
        public async Task<ActionResult<InputImage>> CreateHotelImage([FromForm] InputImage inputImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string uniqueFileName = null;
            if (inputImage.ImageProfile != null)
            {
                string uploadsFolder = Path.Combine(_env.ContentRootPath, "Image");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                uniqueFileName = Guid.NewGuid().ToString() + "_" + inputImage.ImageProfile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await inputImage.ImageProfile.CopyToAsync(stream);
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred while uploading the file: {ex.Message}");
                }
            }

            HotelImage hotelImage = new HotelImage
            {
                HotelID = inputImage.HotelID,
                ImageUrl = uniqueFileName,
                Caption = inputImage.Caption,
                IsThumbnail = inputImage.IsThumbnail,
                CreatedOn = DateTime.UtcNow,

            };

            _context.HotelImages.Add(hotelImage);
            await _context.SaveChangesAsync();

            InputImage outputImage = new InputImage
            {
                HotelImageID = hotelImage.HotelImageID,
                ImageUrl = hotelImage.ImageUrl,
                Caption = hotelImage.Caption,
                IsThumbnail = hotelImage.IsThumbnail,
                CreatedOn = hotelImage.CreatedOn,
                HotelID = hotelImage.HotelID
            };

            return CreatedAtAction(nameof(GetHotelImage), new { id = hotelImage.HotelImageID }, outputImage);
        }

        // GET api/HotelImage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Outputimage>> GetHotelImage(int id)
        {
            HotelImage hotelImage = await _context.HotelImages.FindAsync(id);
            if (hotelImage == null)
            {
                return NotFound();
            }

            Outputimage outputImage = new Outputimage
            {
                HotelImageID = hotelImage.HotelImageID,
                ImageUrl = hotelImage.ImageUrl,
                Caption = hotelImage.Caption,
                IsThumbnail = hotelImage.IsThumbnail,
                CreatedOn = hotelImage.CreatedOn,
                HotelID = hotelImage.HotelID
            };

            return outputImage;
        }

        // PUT api/HotelImage/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotelImage(int id, [FromForm] InputImage inputImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HotelImage hotelImage = await _context.HotelImages.FindAsync(id);
            if (hotelImage == null)
            {
                return NotFound();
            }

            if (inputImage.ImageProfile != null)
            {
                string uploadsFolder = Path.Combine(_env.ContentRootPath, "Image");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + inputImage.ImageProfile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await inputImage.ImageProfile.CopyToAsync(stream);
                }
                hotelImage.ImageUrl = uniqueFileName;
            }
            hotelImage.Caption = inputImage.Caption;
            hotelImage.IsThumbnail = inputImage.IsThumbnail;


            _context.Entry(hotelImage).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            InputImage outputImage = new InputImage
            {
                HotelImageID = hotelImage.HotelImageID,
                ImageUrl = hotelImage.ImageUrl,
                Caption = hotelImage.Caption,
                IsThumbnail = hotelImage.IsThumbnail,
                CreatedOn = hotelImage.CreatedOn,
                HotelID = hotelImage.HotelID
            };

            return Ok(outputImage);
        }

        // DELETE api/HotelImage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelImage(int id)
        {
            HotelImage hotelImage = await _context.HotelImages.FindAsync(id);
            if (hotelImage == null)
            {
                return NotFound();
            }

            _context.HotelImages.Remove(hotelImage);
            await _context.SaveChangesAsync();

            return Ok("Deleted Successfully");
        }
    }
}
