namespace DocumentsPOC.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
