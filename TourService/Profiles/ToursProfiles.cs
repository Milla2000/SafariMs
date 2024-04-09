using AutoMapper;
using TourService.Model;
using TourService.Model.Dtos;

namespace TourService.Profiles
{
    public class ToursProfiles:Profile
    {

        public ToursProfiles()
        {
            CreateMap<AddTourDto, Tour>().ReverseMap();
            CreateMap<AddTourImageDto, TourImage>().ReverseMap();
        }
    }
}
