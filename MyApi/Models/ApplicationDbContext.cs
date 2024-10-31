using System.Data.Entity;

namespace MyApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection") { }

        public DbSet<User> Users { get; set; }
    }
}
