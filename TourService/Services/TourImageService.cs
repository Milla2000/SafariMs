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
        public async Task<string> AddImage(Guid Id, TourImage images)
        {
           //find the tour by Id
           var tour = _context.Tours.Where(x=>x.Id == Id).FirstOrDefault();
            tour.SafariImages.Add(images);
            await _context.SaveChangesAsync();
            return "Image Added!!!";
       
        }
    }
}
