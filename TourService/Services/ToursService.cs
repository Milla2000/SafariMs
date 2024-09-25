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

        public async Task<ToursandImagesResponseDTo> GetTour(Guid Id)
        {
            var tour = await _context.Tours
                .Include(t => t.SafariImages)
                .FirstOrDefaultAsync(t => t.Id == Id);

            if (tour == null)
            {
                return null; // Or handle accordingly if tour not found
            }

            // Map the Tour entity to ToursandImagesResponseDTo
            var tourDto = new ToursandImagesResponseDTo
            {
                Id = tour.Id,
                SafariName = tour.SafariName,
                SafariDescription = tour.SafariDescription,
                StartDate = tour.StartDate,
                EndDate = tour.EndDate,
                Price = tour.Price,
                TourImages = tour.SafariImages.Select(img => new AddTourImageDto
                {
                    Image = img.Image
                }).ToList()
            };

            return tourDto;
        }

    }
}
