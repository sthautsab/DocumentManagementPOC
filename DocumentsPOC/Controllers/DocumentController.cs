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

        public async Task<IActionResult> GetDocumentContent(int docId)
        {
            var document = await _documentRepository.GetDocumentById(docId);
            ContentOutputDto contentOutputDto = new ContentOutputDto()
            {
                Content = document.Content,
                IsSelectable = document.IsSelectable
            };
            string content = await _documentRepository.GetDocumentContentByIdAsync(docId);
            return Ok(contentOutputDto);
        }

        public async Task AddDocumentToFolder(int docId, int folderId)
        {
            await _documentRepository.AddDocumentToFolder(docId, folderId);
        }

        [HttpPost]
        public async Task AddPartialDocumentToFolder([FromBody] PartialDocumentSaveDto partialDocumentSaveDto)
        {
            var document = await _documentRepository.GetDocumentById(partialDocumentSaveDto.ParentDocId);

            var partialDocumentName = document.Title;

            DocumentInsertDto docInsert = new DocumentInsertDto()
            {
                Html = partialDocumentSaveDto.PartialContent,
                Title = partialDocumentName,
                ParentId = document.Id
            };

            int insertedDocId = await _documentRepository.InsertDocument(docInsert);

            await AddDocumentToFolder(insertedDocId, partialDocumentSaveDto.FolderId);
        }

        [HttpPost]
        public async Task AddCommentOnDocument([FromBody] AddCommentDto addCommentDto)
        {

        }
    }
}
