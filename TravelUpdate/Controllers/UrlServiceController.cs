using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelUpdate.Dal;
using TravelUpdate.Models;
using TravelUpdate.Models.InputModels;

namespace TravelUpdate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlServiceController : ControllerBase
    {
        private readonly TravelDBContext _context;

        public UrlServiceController(TravelDBContext context)
        {
            _context = context;
        }

        // URL Service Endpoints

        // GET: api/UrlService/urlservices
        [HttpGet("urlservices")]
        public async Task<ActionResult<IEnumerable<UrlServiceDto>>> GetUrlServices()
        {
            var urlServices = await _context.UrlServices
                .Include(u => u.RequestUrl)
                .Select(u => new UrlServiceDto
                {
                    UrlServiceId = u.UrlServiceId,
                    CurrentUrl = u.CurrentUrl,
                    Description = u.Description,
                    RequestUrl = new RequestUrlDto
                    {
                        RequestUrlId = u.RequestUrl.RequestUrlId,
                        Url = u.RequestUrl.Url,
                        UrlName = u.RequestUrl.UrlName
                    }
                })
                .ToListAsync();

            return Ok(urlServices);
        }

        // GET: api/UrlService/urlservices/{id}
        [HttpGet("urlservices/{id}")]
        public async Task<ActionResult<UrlServiceDto>> GetUrlService(int id)
        {
            var urlService = await _context.UrlServices
                .Include(u => u.RequestUrl)
                .Select(u => new UrlServiceDto
                {
                    UrlServiceId = u.UrlServiceId,
                    CurrentUrl = u.CurrentUrl,
                    Description = u.Description,
                    RequestUrl = new RequestUrlDto
                    {
                        RequestUrlId = u.RequestUrl.RequestUrlId,
                        Url = u.RequestUrl.Url,
                        UrlName = u.RequestUrl.UrlName
                    }
                })
                .FirstOrDefaultAsync(u => u.UrlServiceId == id);

            if (urlService == null)
                return NotFound();

            return Ok(urlService);
        }

        // POST: api/UrlService/urlservices
        [HttpPost("urlservices")]
        public async Task<ActionResult<UrlServiceDto>> PostUrlService([FromBody] CreateUrlServiceDto createUrlServiceDto)
        {
            var requestUrl = await _context.RequestUrls.FirstOrDefaultAsync(r => r.RequestUrlId == createUrlServiceDto.RequestUrlId);
            if (requestUrl == null)
                return BadRequest("RequestUrl not found.");

            var newUrlService = new UrlService
            {
                CurrentUrl = createUrlServiceDto.CurrentUrl,
                Description = createUrlServiceDto.Description,
                RequestUrlId = createUrlServiceDto.RequestUrlId,
                RequestUrl = requestUrl
            };

            _context.UrlServices.Add(newUrlService);
            await _context.SaveChangesAsync();

            //// Fetching the path from the HttpContext
            //var request = HttpContext.Request;
            //var path = request.Path.ToString();

            //// Finding the UrlService based on the CurrentUrl
            //var urlService = await _context.UrlServices
            //    .Include(u => u.RequestUrl)
            //    .FirstOrDefaultAsync(e => e.CurrentUrl == path);

            //if (urlService == null)
            //{
            //    // Return NotFound if no matching UrlService is found
            //    return NotFound("No UrlService found for the current URL.");
            //}

            //var urlServiceDto = new UrlServiceDto
            //{
            //    UrlServiceId = urlService.UrlServiceId,
            //    CurrentUrl = urlService.CurrentUrl,
            //    Description = urlService.Description,
            //    RequestUrl = new RequestUrlDto
            //    {
            //        RequestUrlId = urlService.RequestUrl.RequestUrlId,
            //        Url = urlService.RequestUrl.Url ?? "home", // Fallback to "home" if Url is null
            //        UrlName = urlService.RequestUrl.UrlName
            //    }
            //};

            // Return Created response with the DTO
            //return CreatedAtAction(nameof(GetUrlService), new { id = newUrlService.UrlServiceId } );
            return Ok("created")
        }

        // PUT: api/UrlService/urlservices/{id}
        [HttpPut("urlservices/{id}")]
        public async Task<IActionResult> PutUrlService(int id, [FromBody] CreateUrlServiceDto updateUrlServiceDto)
        {
            var urlService = await _context.UrlServices.FindAsync(id);
            if (urlService == null)
                return NotFound();

            var requestUrl = await _context.RequestUrls.FirstOrDefaultAsync(r => r.RequestUrlId == updateUrlServiceDto.RequestUrlId);
            if (requestUrl == null)
                return BadRequest("RequestUrl not found.");

            urlService.CurrentUrl = updateUrlServiceDto.CurrentUrl;
            urlService.Description = updateUrlServiceDto.Description;
            urlService.RequestUrlId = updateUrlServiceDto.RequestUrlId;
            urlService.RequestUrl = requestUrl;

            _context.Entry(urlService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UrlServiceExists(id))
                    return NotFound();
                else
                    throw;
            }

            // Get the current URL from HttpContext
            var request = HttpContext.Request;
            var currentPath = request.Path.ToString();

            // Find the UrlService in the database using the current URL
            var urlService1 = await _context.UrlServices
                .Include(u => u.RequestUrl) // Include the RequestUrl for return
                .FirstOrDefaultAsync(e => e.CurrentUrl == currentPath);

            return Ok(new { Url = urlService1.RequestUrl.Url });
        }

        // DELETE: api/UrlService/urlservices/{id}
        [HttpDelete("urlservices/{id}")]
        public async Task<IActionResult> DeleteUrlService(int id)
        {
            var urlService = await _context.UrlServices.FindAsync(id);
            if (urlService == null)
                return NotFound();

            _context.UrlServices.Remove(urlService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UrlServiceExists(int id)
        {
            return _context.UrlServices.Any(e => e.UrlServiceId == id);
        }

        // Request URL Endpoints

        // GET: api/UrlService/requesturls
        [HttpGet("requesturls")]
        public async Task<ActionResult<IEnumerable<RequestUrlDto>>> GetRequestUrls()
        {
            var requestUrls = await _context.RequestUrls
                .Select(r => new RequestUrlDto
                {
                    RequestUrlId = r.RequestUrlId,
                    Url = r.Url,
                    UrlName = r.UrlName
                })
                .ToListAsync();

            return Ok(requestUrls);
        }

        // GET: api/UrlService/requesturls/{id}
        [HttpGet("requesturls/{id}")]
        public async Task<ActionResult<RequestUrlDto>> GetRequestUrl(int id)
        {
            var requestUrl = await _context.RequestUrls
                .Select(r => new RequestUrlDto
                {
                    RequestUrlId = r.RequestUrlId,
                    Url = r.Url,
                    UrlName = r.UrlName
                })
                .FirstOrDefaultAsync(r => r.RequestUrlId == id);

            if (requestUrl == null)
                return NotFound();

            return Ok(requestUrl);
        }

        // POST: api/UrlService/requesturls
        [HttpPost("requesturls")]
        public async Task<ActionResult<RequestUrlDto>> PostRequestUrl([FromBody] CreateRequestUrlDto createRequestUrlDto)
        {
            var newRequestUrl = new RequestUrl
            {
                Url = createRequestUrlDto.Url,
                UrlName = createRequestUrlDto.UrlName
            };

            _context.RequestUrls.Add(newRequestUrl);
            await _context.SaveChangesAsync();

            var requestUrlDto = new RequestUrlDto
            {
                RequestUrlId = newRequestUrl.RequestUrlId,
                Url = newRequestUrl.Url,
                UrlName = newRequestUrl.UrlName
            };

            return CreatedAtAction(nameof(GetRequestUrl), new { id = requestUrlDto.RequestUrlId }, requestUrlDto);
        }

        // PUT: api/UrlService/requesturls/{id}
        [HttpPut("requesturls/{id}")]
        public async Task<IActionResult> PutRequestUrl(int id, [FromBody] CreateRequestUrlDto updateRequestUrlDto)
        {
            if (id <= 0)
                return BadRequest("Invalid RequestUrl ID.");

            var requestUrl = await _context.RequestUrls.FindAsync(id);
            if (requestUrl == null)
                return NotFound();

            requestUrl.Url = updateRequestUrlDto.Url;
            requestUrl.UrlName = updateRequestUrlDto.UrlName;

            _context.Entry(requestUrl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestUrlExists(id))
                    return NotFound();
                else
                    throw;
            }

            // Get the current URL from HttpContext
            var request = HttpContext.Request;
            var currentPath = request.Path.ToString();

            // Find the UrlService in the database using the current URL
            var urlService = await _context.UrlServices
                .Include(u => u.RequestUrl) // Include the RequestUrl for return
                .FirstOrDefaultAsync(e => e.CurrentUrl == currentPath);

            return Ok(new { Url = urlService.RequestUrl.Url });
        }

        // DELETE: api/UrlService/requesturls/{id}
        [HttpDelete("requesturls/{id}")]
        public async Task<IActionResult> DeleteRequestUrl(int id)
        {
            var requestUrl = await _context.RequestUrls.FindAsync(id);
            if (requestUrl == null)
                return NotFound();

            _context.RequestUrls.Remove(requestUrl);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestUrlExists(int id)
        {
            return _context.RequestUrls.Any(e => e.RequestUrlId == id);
        }
    }
}
