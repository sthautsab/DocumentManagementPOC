using DocumentsPOC.Dto;

namespace DocumentsPOC.Repository
{
    public interface IDocumentRepository
    {
        Task<List<DocumentListDto>> GetAllDocumentTitlesAsync();

        Task<string?> GetDocumentContentByIdAsync(int id);

        Task InsertDocument(DocumentInsertDto documentInsertDto);

        Task AddDocumentToFolder(int docId, int folderId);
    }
}
