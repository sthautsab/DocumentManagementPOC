namespace DocumentsPOC.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
