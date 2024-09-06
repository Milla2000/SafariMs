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

            // Mapping from AddTourDto to Tour with custom mapping for SafariImages
            CreateMap<AddTourDto, Tour>()
                .ForMember(dest => dest.SafariImages, opt => opt.MapFrom(src => src.SafariImages.Select(imageUrl => new TourImage
                {
                    Image = imageUrl
                }).ToList()))
                .ReverseMap();

            // Mapping for AddTourImageDto to TourImage
            CreateMap<AddTourImageDto, TourImage>().ReverseMap();
        }
    }
}
