using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Document.Data;
using Document.Models;
using Microsoft.AspNetCore.Authorization;

namespace Document.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactsModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ContactsModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactsModel>>> GetContactsModel()
        {
          if (_context.ContactsModel == null)
          {
              return NotFound();
          }
            return await _context.ContactsModel.ToListAsync();
        }

        // GET: api/ContactsModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactsModel>> GetContactsModel(Guid id)
        {
          if (_context.ContactsModel == null)
          {
              return NotFound();
          }
            var contactsModel = await _context.ContactsModel.FindAsync(id);

            if (contactsModel == null)
            {
                return NotFound();
            }

            return contactsModel;
        }

        // PUT: api/ContactsModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactsModel(Guid id, ContactsModel contactsModel)
        {
            if (id != contactsModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(contactsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactsModelExists(id))
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

        // POST: api/ContactsModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactsModel>> PostContactsModel(ContactsModel contactsModel)
        {
          if (_context.ContactsModel == null)
          {
              return Problem("Entity set 'ApplicationDbContext.ContactsModel'  is null.");
          }
            _context.ContactsModel.Add(contactsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactsModel", new { id = contactsModel.ID }, contactsModel);
        }

        // DELETE: api/ContactsModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactsModel(Guid id)
        {
            if (_context.ContactsModel == null)
            {
                return NotFound();
            }
            var contactsModel = await _context.ContactsModel.FindAsync(id);
            if (contactsModel == null)
            {
                return NotFound();
            }

            _context.ContactsModel.Remove(contactsModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactsModelExists(Guid id)
        {
            return (_context.ContactsModel?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
