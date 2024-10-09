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
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly TravelDBContext _context;

        public RoomsController(TravelDBContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public IActionResult GetRooms()
        {
            var rooms = _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.RoomType)
                .Include(r => r.RoomSubType);
            return Ok(rooms.Select(r => new RoomDTO
            {
                RoomID = r.RoomID,
                AveragePrice = r.AveragePrice,
                MaxOccupancy = r.MaxOccupancy,
                IsAvailable = r.IsAvailable,
                HotelID = r.HotelID,

                RoomTypeID = r.RoomTypeID,

                RoomSubTypeID = r.RoomSubTypeID,

            }));
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public IActionResult GetRoom(int id)
        {
            var room = _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.RoomType)
                .Include(r => r.RoomSubType)
                .FirstOrDefault(r => r.RoomID == id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(new RoomDTO
            {
                RoomID = room.RoomID,
                AveragePrice = room.AveragePrice,
                MaxOccupancy = room.MaxOccupancy,
                IsAvailable = room.IsAvailable,
                HotelID = room.HotelID,

                RoomTypeID = room.RoomTypeID,

                RoomSubTypeID = room.RoomSubTypeID,

            });
        }

        // POST: api/Rooms
        [HttpPost("add/room")]
        public  IActionResult CreateRoom(RoomDTOs roomDTO)
        {
            var room = _context.Hotels.Find(roomDTO.HotelID);
            var roomType = _context.RoomTypes.Find(roomDTO.RoomTypeID);
            var roomSubType = _context.RoomSubTypes.Find(roomDTO.RoomSubTypeID);

            if (room == null || roomType == null || roomSubType == null)
            {
                return NotFound();
            }

            var rooms = new Room
            {
                AveragePrice = roomDTO.AveragePrice,
                MaxOccupancy = roomDTO.MaxOccupancy,
                IsAvailable = roomDTO.IsAvailable,
                HotelID = roomDTO.HotelID,
                RoomTypeID = roomDTO.RoomTypeID,
                RoomSubTypeID = roomDTO.RoomSubTypeID,
            };

            _context.Rooms.Add(rooms);
            _context.SaveChanges();

            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService =  _context.UrlServices
                .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = "";

            if (urlService == null)
            {
                requestUrl = "dashboard";
            }
            else
            {
                requestUrl = urlService.Result.RequestUrl.Url;
            }
            // Return only `id` and `url` in the response
            return Ok(new { id = rooms.RoomID, url = requestUrl });
        }

        // PUT: api/Rooms/5
        [HttpPut("update/room/{id}")]
        public  IActionResult UpdateRoom(int id, RoomDTOs roomDTO)
        {
            var room = _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.RoomType)
                .Include(r => r.RoomSubType)
                .FirstOrDefault(r => r.RoomID == id);

            if (room == null)
            {
                return NotFound();
            }

            var hotel = _context.Hotels.Find(roomDTO.HotelID);
            var roomType = _context.RoomTypes.Find(roomDTO.RoomTypeID);
            var roomSubType = _context.RoomSubTypes.Find(roomDTO.RoomSubTypeID);

            if (hotel == null || roomType == null || roomSubType == null)
            {
                return NotFound();
            }

            // Update room properties
            room.AveragePrice = roomDTO.AveragePrice;
            room.MaxOccupancy = roomDTO.MaxOccupancy;
            room.IsAvailable = roomDTO.IsAvailable;
            room.HotelID = roomDTO.HotelID;
            room.RoomTypeID = roomDTO.RoomTypeID;
            room.RoomSubTypeID = roomDTO.RoomSubTypeID;

            _context.Rooms.Update(room);
            _context.SaveChanges();

            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService =  _context.UrlServices
                .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = "";

            if (urlService == null)
            {
                requestUrl = "dashboard";
            }
            else
            {
                requestUrl = urlService.Result.RequestUrl.Url;
            }
            // Return only the updated `id` and `url` in the response
            return Ok(new { id = room.RoomID, url = requestUrl });
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            _context.SaveChanges();
            return Ok("Deleted Sucessfull");
        }
        public static string RemoveLastSegment(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return url;
            }

            url = url.TrimStart('/');

            var segments = url.Split('/');

            if (segments.Length > 1)
            {
                var lastSegment = segments[^1];

                if (int.TryParse(lastSegment, out _))
                {
                    return string.Join("/", segments, 0, segments.Length - 1);
                }
            }

            return url;
        }
    }
}