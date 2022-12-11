using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Document.Data;
using Document.Models;
using Document.Repositories;
using Document.Services;

namespace Document.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILocationService _locationService;

        public LocationModelsController(ApplicationDbContext context, ILocationService locationService)
        {
            _context = context;
            _locationService = locationService;
        }

        // GET: api/LocationModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewLocation>>> GetLocationModel()
        {
            /*if (_context.LocationModel == null)
            {
                return NotFound();
            }
              return await _context.LocationModel.ToListAsync();*/

            var location = await _locationService.GetAllLocations();
            if (location == null)
            {
                return NotFound();
            }
            return location.ToList();
        }

        // GET: api/LocationModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationModel>> GetLocationModel(Guid id)
        {
          if (_context.LocationModel == null)
          {
              return NotFound();
          }
            var locationModel = await _context.LocationModel.FindAsync(id);

            if (locationModel == null)
            {
                return NotFound();
            }

            return locationModel;
        }

        // PUT: api/LocationModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocationModel(Guid id, LocationModel locationModel)
        {
            if (id != locationModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(locationModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationModelExists(id))
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

        // POST: api/LocationModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LocationModel>> PostLocationModel(LocationModel locationModel)
        {
          if (_context.LocationModel == null)
          {
              return Problem("Entity set 'ApplicationDbContext.LocationModel'  is null.");
          }
            _context.LocationModel.Add(locationModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocationModel", new { id = locationModel.ID }, locationModel);
        }

        // DELETE: api/LocationModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationModel(Guid id)
        {
            if (_context.LocationModel == null)
            {
                return NotFound();
            }
            var locationModel = await _context.LocationModel.FindAsync(id);
            if (locationModel == null)
            {
                return NotFound();
            }

            _context.LocationModel.Remove(locationModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationModelExists(Guid id)
        {
            return (_context.LocationModel?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
