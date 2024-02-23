namespace DocumentsPOC.Dto
{
    public class AddCommentDto
    {
        public string Range { get; set; }
        public string? CommentContent { get; set; }

        public int? DocumentId { get; set; }
    }
}
