using Microsoft.EntityFrameworkCore;
using TourService.Model;

namespace TourService.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<Tour> Tours { get; set; }

        public DbSet<TourImage> ToursImages { get; set; }
    }
}
