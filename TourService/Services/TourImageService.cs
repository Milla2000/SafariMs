using TourService.Data;
using TourService.Model;
using TourService.Services.IServices;

namespace TourService.Services
{
    public class TourImageService : IImage
    {   
        private readonly ApplicationDbContext _context;
        public TourImageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddImages(Guid Id, List<TourImage> images)
        {
            // Find the tour by Id
            var tour = _context.Tours.Where(x => x.Id == Id).FirstOrDefault();

            if (tour == null)
            {
                return "Tour Not Found";
            }

            // Add each image to the tour's collection
            foreach (var image in images)
            {
                tour.SafariImages.Add(image);
            }

            await _context.SaveChangesAsync();
            return "Images Added!!!";
        }
    }
}
