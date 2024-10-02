using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TravelUpdate.Dal;
using TravelUpdate.Models;
using TravelUpdate.Models.InputModels;
using TravelUpdate.Models.OutputModels;

namespace TravelUpdate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly TravelDBContext _context;

        public HotelsController(TravelDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetHotels()
        {
            var hotels = _context.Hotels
                
                .Include(h => h.HotelImages)
                .Include(h => h.HotelFacilities)
                .Include(h => h.Rooms);

            return Ok(hotels);
        }

        // GET: api/Hotels/5
        [HttpGet("{ID}")]
        public IActionResult GetHotel(int ID)
        {
            var hotel = _context.Hotels
                
                .Include(h => h.HotelImages)
                .Include(h => h.HotelFacilities)
                .Include(h => h.Rooms)
                .FirstOrDefault(h => h.HotelID == ID);

            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

        // POST api/Hotels
        [HttpPost]
        public async Task<ActionResult<int>> CreateHotel([FromBody] FacilityWiseHotel facilityWiseHotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Hotel hotel = new Hotel
            {
                HotelName = facilityWiseHotel.HotelName,
                Description = facilityWiseHotel.Description,
                StarRating = facilityWiseHotel.StarRating,
                Address = facilityWiseHotel.Address,
                ContactInfo = facilityWiseHotel.ContactInfo,
                HotelCode = facilityWiseHotel.HotelCode,
                LocationID = facilityWiseHotel.LocationId
            };

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            foreach (var hotelFacility in facilityWiseHotel.HotelFacilities)
            {
                HotelFacility hf = new HotelFacility
                {
                    HotelID = hotel.HotelID,
                    FacilityID = hotelFacility.FacilityID,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow
                };
                _context.HotelFacilities.Add(hf);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHotel), new { id = hotel.HotelID }, hotel.HotelID);
        }

        // PUT api/Hotels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] FacilityWiseHotel facilityWiseHotel)
        {
            if (id != facilityWiseHotel.HotelId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotel = await _context.Hotels
                .Include(h => h.HotelFacilities)
                .ThenInclude(hf => hf.Facility)
                .Include(h => h.HotelImages)
                .FirstOrDefaultAsync(h => h.HotelID == id);

            if (hotel == null)
            {
                return NotFound();
            }

            hotel.HotelName = facilityWiseHotel.HotelName;
            hotel.Description = facilityWiseHotel.Description;
            hotel.StarRating = facilityWiseHotel.StarRating;
            hotel.Address = facilityWiseHotel.Address;
            hotel.ContactInfo = facilityWiseHotel.ContactInfo;
            hotel.HotelCode = facilityWiseHotel.HotelCode;
            hotel.LocationID = facilityWiseHotel.LocationId;

            _context.Entry(hotel).State = EntityState.Modified;

            foreach (var hotelFacility in facilityWiseHotel.HotelFacilities)
            {
                if (hotelFacility.HotelFacilityId == 0)
                {
                    HotelFacility hf = new HotelFacility
                    {
                        HotelID = hotel.HotelID,
                        FacilityID = hotelFacility.FacilityID,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    };
                    _context.HotelFacilities.Add(hf);
                }
                else
                {
                    var existingHotelFacility = await _context.HotelFacilities
                        .FirstOrDefaultAsync(hf => hf.HotelFacilityID == hotelFacility.HotelFacilityId);

                    if (existingHotelFacility != null)
                    {
                        existingHotelFacility.FacilityID = hotelFacility.FacilityID;
                        existingHotelFacility.UpdatedOn = DateTime.UtcNow;
                    }
                }
            }

            await _context.SaveChangesAsync();

            return Ok(hotel.HotelID);
        }

        // DELETE api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels
                .Include(h => h.HotelFacilities)
                .ThenInclude(hf => hf.Facility)
                .Include(h => h.HotelImages)
                .FirstOrDefaultAsync(h => h.HotelID == id);

            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return Ok("Deleted succesfully");
        }
    }
}
