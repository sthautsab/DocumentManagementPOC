using DocumentsPOC.Dto;
using DocumentsPOC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsPOC.Controllers
{
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
        public IActionResult Post([FromBody] DocumentInsertDto documentInsertDto)
        {
            _documentRepository.InsertDocument(documentInsertDto);
            return Ok(documentInsertDto);
        }
    }
}
