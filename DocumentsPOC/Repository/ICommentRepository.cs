using DocumentsPOC.Dto;
using DocumentsPOC.Models;

namespace DocumentsPOC.Repository
{
    public interface ICommentRepository
    {
        Task<Guid?> AddComment(Comment comment);

        Task<RangeAndCommentsDto> GetAllCommentsByDocumentId(int docId);
    }
}
