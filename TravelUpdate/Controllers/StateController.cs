using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelUpdate.Dal;
using TravelUpdate.Models;
using TravelUpdate.Models.InputModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelUpdate.Models.OutputModels;

namespace TravelUpdate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly TravelDBContext _context;

        public StateController(TravelDBContext context)
        {
            _context = context;
        }

        // GET: api/State
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StateOutputModel>>> GetStates()
        {
            var states = await _context.States
                .Include(s => s.Country)
                .Select(s => new StateOutputModel
                {
                    StateID = s.StateID,
                    StateName = s.StateName,
                    CountryName = s.Country.CountryName
                })
                .ToListAsync();

            return states;
        }



        // GET: api/State/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<State>> GetState(int id)
        {
            var state = await _context.States.Include(s => s.Country).Include(s => s.Locations)
                .FirstOrDefaultAsync(s => s.StateID == id);

            if (state == null)
            {
                return NotFound();
            }

            return state;
        }

        // POST: api/State
        [HttpPost]
        public async Task<ActionResult<State>> PostState(StateInsertModel model)
        {
            var state = new State
            {
                StateName = model.StateName,
                CountryID = model.CountryID
            };

            _context.States.Add(state);
            await _context.SaveChangesAsync();

            var url = Url.Action(nameof(GetState), new { id = state.StateID });
            return Created(url, state);
        }

        // PUT: api/State/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutState(int id, StateInsertModel model)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            state.StateName = model.StateName;
            state.CountryID = model.CountryID;

            _context.Entry(state).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(id))
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

        // DELETE: api/State/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(int id)
        {
            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            _context.States.Remove(state);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StateExists(int id)
        {
            return _context.States.Any(e => e.StateID == id);
        }
    }
}
