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

namespace Document.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICompanyService _companyService;

        public CompanyModelsController(ApplicationDbContext context, ICompanyService companyService)
        {
            _context = context;
            _companyService = companyService;
        }

        // GET: api/CompanyModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewCompany>>> GetCompanyModel()
        {
            /*if (_context.CompanyModel == null)
            {
                return NotFound();
            }
              return await _context.CompanyModel.ToListAsync();*/
            var company = await _companyService.GetAllCompanies();
            if (company == null)
            {
                return NotFound();
            }
            return (company.ToList());
        }

        // GET: api/CompanyModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyModel>> GetCompanyModel(Guid id)
        {
          if (_context.CompanyModel == null)
          {
              return NotFound();
          }
            var companyModel = await _context.CompanyModel.FindAsync(id);

            if (companyModel == null)
            {
                return NotFound();
            }

            return companyModel;
        }

        // PUT: api/CompanyModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ViewCompany>> PutCompanyModel(Guid id, CreateUpdateCompany companyModel)
        {
            /*if (id != companyModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(companyModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();*/
            return await _companyService.EditCompany(companyModel, id);
        }

        // POST: api/CompanyModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ViewCompany>> PostCompanyModel(CreateUpdateCompany companyModel)
        {
            /*if (_context.CompanyModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CompanyModel'  is null.");
            }
              _context.CompanyModel.Add(companyModel);
              await _context.SaveChangesAsync();

              return CreatedAtAction("GetCompanyModel", new { id = companyModel.ID }, companyModel);*/

            var company = await _companyService.AddCompany(companyModel);
            if (company == null)
            {
                return NotFound();
            }
            return (company);
        }

        // DELETE: api/CompanyModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyModel(Guid id)
        {
            if (_context.CompanyModel == null)
            {
                return NotFound();
            }
            var companyModel = await _context.CompanyModel.FindAsync(id);
            if (companyModel == null)
            {
                return NotFound();
            }

            _context.CompanyModel.Remove(companyModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyModelExists(Guid id)
        {
            return (_context.CompanyModel?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
