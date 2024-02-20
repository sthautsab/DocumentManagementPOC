using DocumentsPOC.Context;
using DocumentsPOC.Dto;
using Microsoft.EntityFrameworkCore;

namespace DocumentsPOC.Repository
{
    public class FolderRepository : IFolderRepository
    {
        private readonly DocumentDbContext _context;
        public FolderRepository(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<List<DocumentListDto>> GetDocumentsInFolder(int folderId)
        {
            var folder = await _context.Folders.Where(x => x.Id == folderId).Include(x => x.Documents).FirstAsync();
            var documents = folder.Documents;
            var documentsList = folder.Documents.Select(x => new DocumentListDto { Id = x.Id, Title = x.Title }).ToList();

            return documentsList;
        }

        public async Task RemoveDocument(int docId)
        {
            var document = await _context.Documents.Where(x => x.Id == docId).FirstOrDefaultAsync();

            if (document != null)
            {
                document.FolderId = null;
                await _context.SaveChangesAsync();
            }
        }
    }
}
