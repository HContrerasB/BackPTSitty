using Microsoft.EntityFrameworkCore;
using SittyPruebaTecnica.Models;

namespace SittyPruebaTecnica.Data
{
   

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
    }

}
