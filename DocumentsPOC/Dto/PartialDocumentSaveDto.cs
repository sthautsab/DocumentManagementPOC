using System.Web.Mvc;

namespace DocumentsPOC.Dto
{
    public class PartialDocumentSaveDto
    {
        public int ParentDocId { get; set; }
        [AllowHtml]
        public string PartialContent { get; set; }

        public int FolderId { get; set; }
    }
}
