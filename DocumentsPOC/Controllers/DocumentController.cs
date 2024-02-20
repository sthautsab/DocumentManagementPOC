using DocumentsPOC.Dto;
using DocumentsPOC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsPOC.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentController(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
        public async Task<IActionResult> Index()
        {
            List<DocumentListDto> documentsList = new List<DocumentListDto>();

            documentsList = await _documentRepository.GetAllDocumentTitlesAsync();

            return View(documentsList);
        }

        public async Task<string> GetDocumentContent(int docId)
        {
            string content = await _documentRepository.GetDocumentContentByIdAsync(docId);
            return content;
        }

        public async Task AddDocumentToFolder(int docId, int folderId)
        {
            await _documentRepository.AddDocumentToFolder(docId, folderId);
        }
    }
}
