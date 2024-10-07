using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelUpdate.Dal;
using TravelUpdate.Models.InputModels;
using TravelUpdate.Models;
using TravelUpdate.Models.OutputModels;

namespace TravelUpdate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly TravelDBContext _context;

        public RoomTypeController(TravelDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomTypeOutput>>> GetRoomTypes()
        {
            return await _context.RoomTypes
                .Select(r => new RoomTypeOutput
                {
                    RoomTypeID = r.RoomTypeID,
                    TypeName = r.TypeName
                }).ToListAsync();
        }

        // POST: api/RoomTypeOutput
        [HttpPost("add")]
        public async Task<ActionResult<RoomType>> PostRoomType(RoomTypeInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomType = new RoomType
            {
                TypeName = model.TypeName
            };

            _context.RoomTypes.Add(roomType);
            await _context.SaveChangesAsync();


            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = RemoveLastSegment(rowPath);

            var urlService = await _context.UrlServices
             .Include(u => u.RequestUrl)
             .FirstOrDefaultAsync(e => e.CurrentUrl == path.ToString());

            var requestUrl = "";

            if (urlService == null)
            {
                requestUrl = "dashboard";
            }
            else
            {
                requestUrl = urlService?.RequestUrl?.Url;
            }



            // Include a custom URL in the success message
            return CreatedAtAction(nameof(GetRoomType), new { id = roomType.RoomTypeID  },  new { url = requestUrl });
        }

        // GET: api/RoomType/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomTypeOutput>> GetRoomType(int id)
        {
            var roomType = await _context.RoomTypes.FindAsync(id);

            if (roomType == null)
            {
                return NotFound();
            }

            var output = new RoomTypeOutput
            {
                RoomTypeID = roomType.RoomTypeID,
                TypeName = roomType.TypeName
            };

            return output;
        }

        

        // PUT: api/RoomTypeOutput/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomType(int id, RoomTypeUpdateModel model)
        {
            if (id != model.RoomTypeID)
            {
                return BadRequest();
            }

            var roomType = await _context.RoomTypes.FindAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }

            roomType.TypeName = model.TypeName;

            _context.Entry(roomType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomTypeExists(id))
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


        // DELETE: api/RoomTypeOutput/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomType(int id)
        {
            var roomType = await _context.RoomTypes.FindAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }

            _context.RoomTypes.Remove(roomType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomTypeExists(int id)
        {
            return _context.RoomTypes.Any(e => e.RoomTypeID == id);
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
