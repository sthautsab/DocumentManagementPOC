using DocumentsPOC.Dto;

namespace DocumentsPOC.Repository
{
    public interface IFolderRepository
    {
        Task<List<DocumentListDto>> GetDocumentsInFolder(int folderId);

        Task RemoveDocument(int docId);
    }
}
