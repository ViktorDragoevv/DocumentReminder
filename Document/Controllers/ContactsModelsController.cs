using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Document.Data;
using Document.Models;
using Document.Services;
using Microsoft.AspNetCore.Authorization;

namespace Document.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IContactService _contactService;

        public ContactsModelsController(ApplicationDbContext context, IContactService contactService)
        {
            _context = context;
            _contactService = contactService;
        }

        // GET: api/ContactsModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewContact>>> GetContactsModel()
        {
            /*if (_context.ContactsModel == null)
            {
                return NotFound();
            }
              return await _context.ContactsModel.ToListAsync();*/
            
            var contacts = await _contactService.GetAllContacts();
            if (contacts == null)
            {
                return NotFound();
            }
            return (contacts.ToList());

        }

        // GET: api/ContactsModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactsModel>> GetContactsModel(Guid id)
        {
            /*if (_context.ContactsModel == null)
            {
                return NotFound();
            }
              var contactsModel = await _context.ContactsModel.FindAsync(id);

              if (contactsModel == null)
              {
                  return NotFound();
              }

              return contactsModel;*/

            var contact = await _contactService.GetContactByID(id);
            if (contact == null)
            {
                return NotFound();
            }
            return (contact);


        }

        // PUT: api/ContactsModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ViewContact>> PutContactsModel(Guid id, CreateUpdateContact contactsModel)
        {
           /* if (id != contactsModel.ID)
            {
                return BadRequest();
            }

            //_context.Entry(contactsModel).State = EntityState.Modified;

            try
            {
               
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
            }*/

            return await _contactService.UpdateContactByID(contactsModel, id);



            //return NoContent();
        }

        // POST: api/ContactsModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ViewContact>> PostContactsModel(CreateUpdateContact contactsModel)
        {
            /*if (_context.ContactsModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ContactsModel'  is null.");
            }
              _context.ContactsModel.Add(contactsModel);
              await _context.SaveChangesAsync();

              return CreatedAtAction("GetContactsModel", new { id = contactsModel.ID }, contactsModel);*/

            var contact = await _contactService.CreateContact(contactsModel);
            if (contact == null)
            {
                return NotFound();
            }
            return (contact);
        }

        // DELETE: api/ContactsModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactsModel(Guid id)
        {
            /*if (_context.ContactsModel == null)
            {
                return NotFound();
            }
            var contactsModel = await _context.ContactsModel.FindAsync(id);
            if (contactsModel == null)
            {
                return NotFound();
            }

            _context.ContactsModel.Remove(contactsModel);
            await _context.SaveChangesAsync();*/
            var deleteContact = await _contactService.DeleteContact(id);
            if (deleteContact == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        private bool ContactsModelExists(Guid id)
        {
            return (_context.ContactsModel?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
