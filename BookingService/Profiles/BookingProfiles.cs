using AutoMapper;
using BookingService.Models;
using BookingService.Models.Dtos;

namespace BookingService.Profiles
{
    public class BookingProfiles:Profile
    {

        public BookingProfiles()
        {
            
            CreateMap<AddBookingDto, Booking>().ReverseMap();
        }
    }
}
