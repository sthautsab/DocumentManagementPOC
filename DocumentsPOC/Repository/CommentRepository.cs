using DocumentsPOC.Context;
using DocumentsPOC.Models;

namespace DocumentsPOC.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DocumentDbContext _context;
        public CommentRepository(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return comment.CommentId;
        }
    }
}
