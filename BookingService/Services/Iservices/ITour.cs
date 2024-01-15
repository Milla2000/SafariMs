using BookingService.Models.Dtos;

namespace BookingService.Services.Iservices
{
    public interface ITour
    {
        Task<TourDto> GetById(Guid id);
    }
}
