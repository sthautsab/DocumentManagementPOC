using DocumentsPOC.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentsPOC.Context
{
    public class DocumentDbContext : DbContext
    {

        public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options)
        {
        }

        // DbSet properties for your entities
        public DbSet<Document> Documents { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
