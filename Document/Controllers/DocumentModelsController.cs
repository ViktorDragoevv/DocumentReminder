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
    public class DocumentModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IDocumentService _documentService;

        public DocumentModelsController(ApplicationDbContext context, IDocumentService documentService)
        {
            _context = context;
            _documentService = documentService;
        }

        // GET: api/DocumentModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewDocument>>> GetDocumentModels()
        {
            /*if (_context.DocumentModels == null)
            {
                return NotFound();
            }
              return await _context.DocumentModels.ToListAsync();*/

            var documents = await _documentService.GetAllDocuments();
            if (documents == null)
            {
                return NotFound();
            }
            return (documents.ToList());
        }

        // GET: api/DocumentModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentModel>> GetDocumentModel(Guid id)
        {
          if (_context.DocumentModels == null)
          {
              return NotFound();
          }
            var documentModel = await _context.DocumentModels.FindAsync(id);

            if (documentModel == null)
            {
                return NotFound();
            }

            return documentModel;
        }

        // PUT: api/DocumentModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentModel(Guid id, DocumentModel documentModel)
        {
            if (id != documentModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(documentModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentModelExists(id))
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

        // POST: api/DocumentModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ViewDocument>> PostDocumentModel(CreateUpdateDocumentcs documentModel)
        {
            var document = await _documentService.CreateDocument(documentModel);
            if (document == null)
            {
                return NotFound();
            }
            return (document);
        }

        // DELETE: api/DocumentModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentModel(Guid id)
        {
            if (_context.DocumentModels == null)
            {
                return NotFound();
            }
            var documentModel = await _context.DocumentModels.FindAsync(id);
            if (documentModel == null)
            {
                return NotFound();
            }

            _context.DocumentModels.Remove(documentModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocumentModelExists(Guid id)
        {
            return (_context.DocumentModels?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
