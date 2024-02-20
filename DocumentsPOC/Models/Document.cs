using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentsPOC.Models
{
    public class Document
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        [ForeignKey("Folder")]
        public int? FolderId { get; set; }


    }
}
