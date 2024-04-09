using AutoMapper;
using HotelService.Models;
using HotelService.Models.Dtos;

namespace HotelService.Profiles
{
    public class HoteslProfile:Profile
    {

        public HoteslProfile()
        {
            
            CreateMap<AddHotel, Hotel>().ReverseMap();
            CreateMap<HotelResponseDto, Hotel>().ReverseMap();
        }
    }
}
