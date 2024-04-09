using BookingService.Models.Dtos;

namespace BookingService.Services.Iservices
{
    public interface IUser
    {

        Task<UserDto> GetUserById(string Id);
    }
}
