using TourService.Model;

namespace TourService.Services.IServices
{
    public interface IImage
    {

        Task<string> AddImage(Guid Id, TourImage images);
    }
}
