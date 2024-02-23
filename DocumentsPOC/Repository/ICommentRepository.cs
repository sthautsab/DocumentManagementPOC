using DocumentsPOC.Models;

namespace DocumentsPOC.Repository
{
    public interface ICommentRepository
    {
        Task<Guid> AddComment(Comment comment);
    }
}
