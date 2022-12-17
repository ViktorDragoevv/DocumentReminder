using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Document.Data;
using Document.Models;

namespace Document.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifyModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NotifyModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/NotifyModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotifyModel>>> GetNotifyModels()
        {
          if (_context.NotifyModels == null)
          {
              return NotFound();
          }
            return await _context.NotifyModels.ToListAsync();
        }

        // GET: api/NotifyModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotifyModel>> GetNotifyModel(Guid id)
        {
          if (_context.NotifyModels == null)
          {
              return NotFound();
          }
            var notifyModel = await _context.NotifyModels.FindAsync(id);

            if (notifyModel == null)
            {
                return NotFound();
            }

            return notifyModel;
        }

        // PUT: api/NotifyModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotifyModel(Guid id, NotifyModel notifyModel)
        {
            if (id != notifyModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(notifyModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotifyModelExists(id))
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

        // POST: api/NotifyModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NotifyModel>> PostNotifyModel(NotifyModel notifyModel)
        {
          if (_context.NotifyModels == null)
          {
              return Problem("Entity set 'ApplicationDbContext.NotifyModels'  is null.");
          }
            _context.NotifyModels.Add(notifyModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotifyModel", new { id = notifyModel.ID }, notifyModel);
        }

        // DELETE: api/NotifyModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotifyModel(Guid id)
        {
            if (_context.NotifyModels == null)
            {
                return NotFound();
            }
            var notifyModel = await _context.NotifyModels.FindAsync(id);
            if (notifyModel == null)
            {
                return NotFound();
            }

            _context.NotifyModels.Remove(notifyModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotifyModelExists(Guid id)
        {
            return (_context.NotifyModels?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
