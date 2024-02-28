using DocumentsPOC.Context;
using DocumentsPOC.Dto;
using DocumentsPOC.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentsPOC.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DocumentDbContext _context;
        public DocumentRepository(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task<List<DocumentListDto>> GetAllDocumentTitlesAsync()
        {
            var documents = await _context.Documents.Where(m => m.ParentId == null).Select(x => new DocumentListDto { Id = x.Id, Title = x.Title }).ToListAsync();
            return documents;
        }

        public async Task<Document> GetDocumentById(int docId, int loggedInUserId)
        {
            //var document = await _context.Documents
            //    .Include(x => x.Comments.Where(c => c.UserId == loggedInUserId))
            //    .Where(x => x.Id == docId)
            //    .FirstOrDefaultAsync();
            var document = await _context.Documents
            .Where(x => x.Id == docId)
            .Select(x => new Document
            {
                Id = x.Id,
                Content = x.Content,
                Title = x.Title,
                ParentId = x.ParentId,
                FolderId = x.FolderId,
                IsSelectable = x.IsSelectable,
                Comments = x.Comments.Where(c => c.UserId == loggedInUserId).ToList(),
                // Include other properties as needed
            })
            .FirstOrDefaultAsync();
            return document;
        }


        public async Task<string?> GetDocumentContentByIdAsync(int id)
        {
            var content = await _context.Documents.Where(m => m.Id == id).Select(x => x.Content).FirstOrDefaultAsync();
            return content;
        }

        public async Task<int> InsertDocument(DocumentInsertDto documentInsertDto)
        {
            Document document = new Document
            {
                Title = documentInsertDto.Title,
                Content = documentInsertDto.Html,
                IsSelectable = documentInsertDto.Selectable ?? false,
                ParentId = documentInsertDto.ParentId,

            };
            try
            {

                _context.Documents.Add(document);
                await _context.SaveChangesAsync();
                return document.Id;

            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public async Task AddDocumentToFolder(int docId, int folderId)
        {
            var document = await _context.Documents.FindAsync(docId);

            //Not already in the folder
            if (document.FolderId != folderId)
            {
                //update
                document.FolderId = folderId;
                await _context.SaveChangesAsync();
            }
        }

    }
}
