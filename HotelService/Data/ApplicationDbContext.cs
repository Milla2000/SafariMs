using HotelService.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<Hotel> Hotels { get; set; }
    }
}
