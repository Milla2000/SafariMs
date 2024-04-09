using Microsoft.EntityFrameworkCore;
using TourService.Data;
using TourService.Model;
using TourService.Model.Dtos;
using TourService.Services.IServices;

namespace TourService.Services
{
    public class ToursService : ITour
    {
        private readonly ApplicationDbContext _context;
        public ToursService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddNewTour(Tour tour)
        {
            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();
            return "Tour added!!";
        }

        public async Task<List<ToursandImagesResponseDTo>> GetAllTours()
        {
            return await _context.Tours.Select(t => new ToursandImagesResponseDTo()
            {   
                Id = t.Id,
                SafariName = t.SafariName,
                SafariDescription = t.SafariDescription,
                Price = t.Price,
                EndDate = t.EndDate,
                StartDate = t.StartDate,
                TourImages = t.SafariImages.Select(x => new AddTourImageDto()
                {
                    Image = x.Image
                }).ToList()
            }).ToListAsync();

            //return _context.Tours.Include(x=>x.SafariImages).ThenInclude(st();
        }

        public async Task<Tour> GetTour(Guid Id)
        {
           return await _context.Tours.Where(x=>x.Id == Id).FirstOrDefaultAsync();
        }

    }
}
