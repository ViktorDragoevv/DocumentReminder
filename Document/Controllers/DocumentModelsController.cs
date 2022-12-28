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
using System.Collections;
using System.Text.Json;

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
        /*[HttpPut("{id}")]
        public async Task<ActionResult<ViewDocument>> PutDocumentModel(Guid id, CreateUpdateDocumentcs documentModel)
        {
            return await _documentService.UpdateDocumentByID(documentModel, id);
        }*/

        [HttpPut("{id}")]
        public async Task<ActionResult<ViewDocument>> PutDocumentModelWithNotify(DocumentWithNotifications documentWithNotifications)
        {
            return await _documentService.UpdateDocumentByIDWithNotify(documentWithNotifications.CreateUpdateDocumentcs, documentWithNotifications.CreateUpdateDocumentcs.ID, documentWithNotifications.createUpdateNotifies);
        }




        // POST: api/DocumentModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Document")]
        public async Task<ActionResult<ViewDocument>> PostDocumentModel(CreateUpdateDocumentcs documentModel)
        {
            var document = await _documentService.CreateDocument(documentModel);
            if (document == null)
            {
                return NotFound();
            }
            return (document);
        }





        [HttpPost]
        [Route("Notify")]
        public async Task<ActionResult<ViewDocument>> PostDocumentWithNotify(DocumentWithNotifications documentWithNotifications)
        {
            var document = await _documentService.CreateDocumentWithNotifications(documentWithNotifications.CreateUpdateDocumentcs, documentWithNotifications.createUpdateNotifies);
            if (document == null)
            {
                return NotFound();

            }
            
            return (document);
        }


        /*[Route("Notify")]
        [HttpPost]
        public async Task<ActionResult<ViewDocument>> PostDocumentWithNotify(ArrayList paramList)
        {
            if (paramList.Count > 0)
            {
                CreateUpdateDocumentcs documentModel = JsonSerializer.Deserialize<CreateUpdateDocumentcs>(paramList[0].ToString());
                IEnumerable<CreateUpdateNotify> notifyModels = JsonSerializer.Deserialize<IEnumerable<CreateUpdateNotify>>(paramList[1].ToString());

                var document = await _documentService.CreateDocumentWithNotifications(documentModel, notifyModels);
                if (document == null)
                {
                    return NotFound();

                }

                return (document);

            }
            else return NotFound();
        }*/

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
