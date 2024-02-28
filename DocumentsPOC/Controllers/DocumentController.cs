using DocumentsPOC.Dto;
using DocumentsPOC.Models;
using DocumentsPOC.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DocumentsPOC.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ICommentRepository _commentRepository;
        private User LoggedInUser;

        public DocumentController(IDocumentRepository documentRepository, ICommentRepository commentRepository)
        {
            _documentRepository = documentRepository;
            _commentRepository = commentRepository;
            //string userInfo = HttpContext.Session.GetString("UserInfo") ?? "";

            //LoggedInUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserInfo"));
        }
        public async Task<IActionResult> Index()
        {
            List<DocumentListDto> documentsList = new List<DocumentListDto>();

            documentsList = await _documentRepository.GetAllDocumentTitlesAsync();

            return View(documentsList);
        }

        public async Task<IActionResult> GetDocumentContent(int docId)
        {
            if (HttpContext.Session.GetString("UserInfo") != null)
            {
                LoggedInUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserInfo"));

            }

            List<RangeAndContentDto> rangeCommentInfo = new List<RangeAndContentDto>();
            var document = await _documentRepository.GetDocumentById(docId, LoggedInUser.UserId);
            ContentOutputDto contentOutputDto = new ContentOutputDto()
            {
                Content = document.Content,
                IsSelectable = document.IsSelectable
            };

            foreach (var comment in document.Comments)
            {
                rangeCommentInfo.Add(new RangeAndContentDto
                {
                    Range = comment.Range,
                    CommentContent = comment.CommentContent,
                    CommentId = comment.CommentId
                });
            }

            //var rangesAndCommentsDto = await _commentRepository.GetAllCommentsByDocumentId(docId);
            return Ok(new { contentOutputDto, rangeCommentInfo });
        }

        public async Task AddDocumentToFolder(int docId, int folderId)
        {
            await _documentRepository.AddDocumentToFolder(docId, folderId);
        }

        [HttpPost]
        public async Task AddPartialDocumentToFolder([FromBody] PartialDocumentSaveDto partialDocumentSaveDto)
        {
            if (HttpContext.Session.GetString("UserInfo") != null)
            {
                LoggedInUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserInfo"));

            }

            var document = await _documentRepository.GetDocumentById(partialDocumentSaveDto.ParentDocId, LoggedInUser.UserId);

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
            LoggedInUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserInfo"));
            Comment comment = new Comment()
            {
                Range = addCommentDto.Range,
                CommentContent = addCommentDto.CommentContent,
                DocumentId = addCommentDto.DocumentId,
                UserId = LoggedInUser.UserId
            };
            try
            {
                await _commentRepository.AddComment(comment);
            }
            catch (Exception ex) { }

        }
    }
}
