using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelUpdate.Dal;
using TravelUpdate.Models;
using TravelUpdate.Models.InputModels;
using TravelUpdate.Models.OutputModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelUpdate.Models.InputModels.TravelUpdate.Models.InputModels;

namespace TravelUpdate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomSubTypeController : ControllerBase
    {
        private readonly TravelDBContext _context;

        public RoomSubTypeController(TravelDBContext context)
        {
            _context = context;
        }

        // GET: api/RoomSubType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomSubTypeOutput>>> GetRoomSubTypes()
        {
            var roomSubTypes = await _context.RoomSubTypes
                .Include(rs => rs.RoomType) // Include the related RoomType
                .Select(rs => new RoomSubTypeOutput
                {
                    RoomSubTypeID = rs.RoomSubTypeID,
                    SubTypeName = rs.SubTypeName,
                    RoomTypeID = rs.RoomTypeID,
                    RoomType = new RoomTypeOutput
                    {
                        RoomTypeID = rs.RoomType.RoomTypeID,
                        TypeName = rs.RoomType.TypeName
                    }
                })
                .ToListAsync();

            return roomSubTypes;
        }

        // GET: api/RoomSubType/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomSubTypeOutput>> GetRoomSubType(int id)
        {
            var roomSubType = await _context.RoomSubTypes
                .Include(rs => rs.RoomType) // Include the related RoomType
                .Where(rs => rs.RoomSubTypeID == id)
                .Select(rs => new RoomSubTypeOutput
                {
                    RoomSubTypeID = rs.RoomSubTypeID,
                    SubTypeName = rs.SubTypeName,
                    RoomTypeID = rs.RoomTypeID,
                    RoomType = new RoomTypeOutput
                    {
                        RoomTypeID = rs.RoomType.RoomTypeID,
                        TypeName = rs.RoomType.TypeName
                    }
                })
                .FirstOrDefaultAsync();

            if (roomSubType == null)
            {
                return NotFound();
            }

            return roomSubType;
        }

        // POST: api/RoomSubType
        [HttpPost]
        public async Task<ActionResult<RoomSubType>> PostRoomSubType(RoomSubTypeInsertModel model)
        {
            var roomSubType = new RoomSubType
            {
                SubTypeName = model.SubTypeName,
                RoomTypeID = model.RoomTypeID
            };

            _context.RoomSubTypes.Add(roomSubType);
            await _context.SaveChangesAsync();

            var url = Url.Action(nameof(GetRoomSubType), new { id = roomSubType.RoomSubTypeID });
            return Created(url, roomSubType);
        }

        // PUT: api/RoomSubType/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomSubType(int id, RoomSubTypeUpdateModel model)
        {
            if (id != model.RoomSubTypeID)
            {
                return BadRequest();
            }

            var roomSubType = await _context.RoomSubTypes.FindAsync(id);
            if (roomSubType == null)
            {
                return NotFound();
            }

            roomSubType.SubTypeName = model.SubTypeName;
            roomSubType.RoomTypeID = model.RoomTypeID;

            _context.Entry(roomSubType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomSubTypeExists(id))
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

        // DELETE: api/RoomSubType/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomSubType(int id)
        {
            var roomSubType = await _context.RoomSubTypes.FindAsync(id);
            if (roomSubType == null)
            {
                return NotFound();
            }

            _context.RoomSubTypes.Remove(roomSubType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomSubTypeExists(int id)
        {
            return _context.RoomSubTypes.Any(e => e.RoomSubTypeID == id);
        }
    }
}
