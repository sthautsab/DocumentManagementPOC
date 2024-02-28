using DocumentsPOC.Dto;
using DocumentsPOC.Models;

namespace DocumentsPOC.Repository
{
    public interface IDocumentRepository
    {
        Task<List<DocumentListDto>> GetAllDocumentTitlesAsync();

        Task<string?> GetDocumentContentByIdAsync(int id);

        Task<int> InsertDocument(DocumentInsertDto documentInsertDto);

        Task AddDocumentToFolder(int docId, int folderId);

        Task<Document> GetDocumentById(int docId, int loggedInUserId);
    }
}
