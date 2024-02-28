using DocumentsPOC.Context;
using DocumentsPOC.Dto;
using DocumentsPOC.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentsPOC.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DocumentDbContext _context;
        public CommentRepository(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task<Guid?> AddComment(Comment comment)
        {
            try
            {

                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();

                return comment.CommentId;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RangeAndCommentsDto> GetAllCommentsByDocumentId(int docId)
        {
            var ranges = await _context.Comments.Where(x => x.DocumentId == docId).Select(x => x.Range).ToListAsync();

            RangeAndCommentsDto rangeAndCommentsDto = new RangeAndCommentsDto() { Ranges = ranges };

            return rangeAndCommentsDto;

        }
    }
}
