using TourService.Model;
using TourService.Model.Dtos;

namespace TourService.Services.IServices
{
    public interface ITour
    {

        Task<List<ToursandImagesResponseDTo>> GetAllTours();

        Task<Tour> GetTour(Guid Id);

        Task<string> AddNewTour(Tour tour);
    }
}
