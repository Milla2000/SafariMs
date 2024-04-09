using BookingService.Models.Dtos;

namespace BookingService.Services.Iservices
{
    public interface IHotel
    {

        Task<HotelDto> GetHotelById(Guid id);
    }
}
