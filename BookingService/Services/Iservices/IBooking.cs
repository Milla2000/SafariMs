using BookingService.Models;
using BookingService.Models.Dtos;

namespace BookingService.Services.Iservices
{
    public interface IBooking
    {
        Task<string> AddBooking(Booking booking);

        Task saveChanges();

        Task<List<Booking>> GetAll(Guid userId);

        Task<Booking> GetBookingById(Guid Id);

        Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto);


        Task<bool> ValidatePayments(Guid BookingId );
    }
}
