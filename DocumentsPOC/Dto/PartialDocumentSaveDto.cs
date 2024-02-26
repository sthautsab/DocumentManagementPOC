namespace DocumentsPOC.Dto
{
    public class PartialDocumentSaveDto
    {
        public int ParentDocId { get; set; }
        public string PartialContent { get; set; }

        public int FolderId { get; set; }
    }
}
