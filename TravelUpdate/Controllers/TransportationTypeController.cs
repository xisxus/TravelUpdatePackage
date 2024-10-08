using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelUpdate.Models.InputModels;
using TravelUpdate.Models;
using TravelUpdate.Dal;
using Microsoft.EntityFrameworkCore;

namespace TravelUpdate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransportationTypeController : ControllerBase
    {
        private readonly TravelDBContext _context;

        public TransportationTypeController(TravelDBContext context)
        {
            _context = context;
        }

        // GET: api/TransportationType/get-all
        [HttpGet("get")]
        public async Task<IActionResult> GetAllTypes()
        {
            var types = await _context.TransportationTypes
                .Select(t => new
                {
                    t.TransportationTypeID,
                    t.TypeName
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                data = types
            });
        }

        // GET: api/TransportationType/get-by-id/{id}
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetTypeById(int id)
        {
            var type = await _context.TransportationTypes
                .Where(t => t.TransportationTypeID == id)
                .Select(t => new
                {
                    t.TransportationTypeID,
                    t.TypeName
                })
                .FirstOrDefaultAsync();

            if (type == null)
            {
                return NotFound(new { success = false, message = "Transportation type not found." });
            }

            return Ok(new
            {
                success = true,
                data = type
            });
        }

        // POST: api/TransportationType/add-type
        [HttpPost("add")]
        public async Task<IActionResult> AddType([FromBody] TransportationTypeInsertModel model, string? customUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newType = new TransportationType
            {
                TypeName = model.TypeName
            };

            await _context.TransportationTypes.AddAsync(newType);
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


            return Ok(new
            {
                success = true,
                message = "Transportation type added successfully.",
                typeId = newType.TransportationTypeID,
                url = requestUrl
            });
        }

        // PUT: api/TransportationType/update-type/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateType(int id, [FromBody] TransportationTypeInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var type = await _context.TransportationTypes.FindAsync(id);
            if (type == null)
            {
                return NotFound(new { success = false, message = "Transportation type not found." });
            }

            type.TypeName = model.TypeName;

            _context.TransportationTypes.Update(type);
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

            return Ok(new
            {
                success = true,
                message = "Transportation type updated successfully.",
                typeId = type.TransportationTypeID,
                url = requestUrl
            });
        }

        // DELETE: api/TransportationType/delete-type/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            var type = await _context.TransportationTypes.FindAsync(id);
            if (type == null)
            {
                return NotFound(new { success = false, message = "Transportation type not found." });
            }

            _context.TransportationTypes.Remove(type);
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

            return Ok(new
            {
                success = true,
                message = "Transportation type deleted successfully."
            });
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
