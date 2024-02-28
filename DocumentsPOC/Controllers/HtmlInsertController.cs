using DocumentsPOC.Dto;
using DocumentsPOC.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsPOC.Controllers
{
    [Authorize]
    public class HtmlInsertController : Controller
    {


        private readonly IDocumentRepository _documentRepository;

        public HtmlInsertController(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        //[Route("api/post")]
        public async Task<IActionResult> Post([FromBody] DocumentInsertDto documentInsertDto)
        {
            int createdDoc = await _documentRepository.InsertDocument(documentInsertDto);
            return Ok(documentInsertDto);
        }
    }
}
