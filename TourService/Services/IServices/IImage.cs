using TourService.Model;

namespace TourService.Services.IServices
{
    public interface IImage
    {
        Task<string> AddImages(Guid Id, List<TourImage> images);
    }
}
