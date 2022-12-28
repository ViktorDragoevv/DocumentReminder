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
    public class FilesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public FilesController(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        // GET: api/Files
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Files>>> GetFiles()
        {
          if (_context.Files == null)
          {
              return NotFound();
          }
            return await _context.Files.ToListAsync();
        }

        // GET: api/Files/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Files>> GetFiles(Guid id)
        {
          if (_context.Files == null)
          {
              return NotFound();
          }
            var files = await _context.Files.FindAsync(id);

            if (files == null)
            {
                return NotFound();
            }

            return files;
        }

        // PUT: api/Files/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFiles(Guid id, Files files)
        {
            if (id != files.ID)
            {
                return BadRequest();
            }

            _context.Entry(files).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilesExists(id))
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

        // POST: api/Files
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Files>> PostFiles([FromForm] CreateFile files)
        {
          /*if (_context.Files == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Files'  is null.");
          }
            _context.Files.Add(files);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFiles", new { id = files.ID }, files);*/
          //foreach(var file in files)
           // {
                //await _fileService.CreateFile(files.files);
            //}
            var newFile = await _fileService.CreateFile(files.files);
            return newFile;

        }

        // DELETE: api/Files/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFiles(Guid id)
        {
            if (_context.Files == null)
            {
                return NotFound();
            }
            var files = await _context.Files.FindAsync(id);
            if (files == null)
            {
                return NotFound();
            }

            //_context.Files.Remove(files);
            //await _context.SaveChangesAsync();
            await _fileService.DeleteFiles(id);






            return NoContent();
        }

        private bool FilesExists(Guid id)
        {
            return (_context.Files?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
