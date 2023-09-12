using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projectfolio.Models;

namespace Projectfolio.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // reference each of our model as DbSet objects - these have CRUD methods built in to them
        public DbSet<Project> Projects { get; set; }
        public DbSet<Technology> technologies { get; set; }
    }
}