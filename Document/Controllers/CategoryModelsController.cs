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
    [Authorize]
    
    public class CategoryModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CategoryModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategoryModel()
        {
          if (_context.CategoryModel == null)
          {
              return NotFound();
          }
            return await _context.CategoryModel.ToListAsync();
        }

        // GET: api/CategoryModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetCategoryModel(int id)
        {
          if (_context.CategoryModel == null)
          {
              return NotFound();
          }
            var categoryModel = await _context.CategoryModel.FindAsync(id);

            if (categoryModel == null)
            {
                return NotFound();
            }

            return categoryModel;
        }

        // PUT: api/CategoryModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryModel(Guid id, CategoryModel categoryModel)
        {
            if (id != categoryModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(categoryModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryModelExists(id))
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

        // POST: api/CategoryModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryModel>> PostCategoryModel([FromBody]CategoryModel categoryModel)
        {
          if (_context.CategoryModel == null)
          {
              return Problem("Entity set 'ApplicationDbContext.CategoryModel'  is null.");
          }
            _context.CategoryModel.Add(categoryModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryModel", new { id = categoryModel.ID }, categoryModel);
        }

        // DELETE: api/CategoryModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryModel(Guid id)
        {
            if (_context.CategoryModel == null)
            {
                return NotFound();
            }
            var categoryModel = await _context.CategoryModel.FindAsync(id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            _context.CategoryModel.Remove(categoryModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryModelExists(Guid id)
        {
            return (_context.CategoryModel?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
