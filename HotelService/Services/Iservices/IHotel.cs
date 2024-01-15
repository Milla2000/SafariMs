using HotelService.Models;

namespace HotelService.Services.Iservices
{
    public interface IHotel
    {

        Task<Hotel> GetHotelById(Guid Id);

        Task<string> AddHotel(Hotel hotel);

        Task<List<Hotel>> GetAllHotel(Guid TourId);
    }
}
