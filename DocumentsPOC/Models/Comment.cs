using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentsPOC.Models
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string Range { get; set; }
        public string? CommentContent { get; set; }

        [ForeignKey("Document")]
        public int? DocumentId { get; set; }
    }
}
