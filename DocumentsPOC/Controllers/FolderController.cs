using DocumentsPOC.Dto;
using DocumentsPOC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsPOC.Controllers
{
    public class FolderController : Controller
    {

        private readonly IFolderRepository _folderRepository;

        public FolderController(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }
        public async Task<IActionResult> Index()
        {
            List<DocumentListDto> documentsList = new List<DocumentListDto>();

            documentsList = await _folderRepository.GetDocumentsInFolder(1);

            return View(documentsList);
        }

        public async Task<IActionResult> RemoveDocumentFromFolder(int docId)
        {
            await _folderRepository.RemoveDocument(docId);
            return RedirectToAction("Index");
        }
    }
}
