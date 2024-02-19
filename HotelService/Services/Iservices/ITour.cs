using HotelService.Models.Dtos;

namespace HotelService.Services.Iservices
{
    public interface ITour
    {

        Task<TourDto> GetTourById(Guid Id);
    }
}
