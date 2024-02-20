using DocumentsPOC.Dto;
using DocumentsPOC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsPOC.Controllers
{
    [ApiController]
    public class HtmlPostController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;

        public HtmlPostController(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
        [HttpPost]
        [Route("api/post")]
        public IActionResult Post([FromBody] DocumentInsertDto documentInsertDto)
        {
            _documentRepository.InsertDocument(documentInsertDto);
            return Ok(documentInsertDto);
        }
    }
}
