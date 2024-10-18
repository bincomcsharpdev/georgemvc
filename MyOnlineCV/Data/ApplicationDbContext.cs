using Microsoft.EntityFrameworkCore;
using MyOnlineCV.Models;  

namespace MyOnlineCV.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor to pass DbContext options
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet for the Photo model
        public DbSet<Photo> Photos { get; set; }
    }
}
