using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentsPOC.Models
{
    public class Document
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public int? ParentId { get; set; }
        public bool IsSelectable { get; set; }

        [ForeignKey("Folder")]
        public int? FolderId { get; set; }

        public virtual Comment Comments { get; set; }


    }
}
