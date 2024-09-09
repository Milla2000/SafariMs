using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TourService.Model;
using TourService.Model.Dtos;
using TourService.Services.IServices;

namespace TourService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ITour _tourService;
        private readonly IImage _imageService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;
        public ImagesController(IMapper mapper , IImage image, ITour tour)
        {
            _imageService = image;
            _tourService = tour;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [HttpPost("{Id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto>> AddImages(Guid Id, [FromBody] List<AddTourImageDto> addTourImageDtos)
        {
            var tour = await _tourService.GetTour(Id);

            if (tour == null)
            {
                _responseDto.Errormessage = "Tour Not Found";
                return NotFound(_responseDto);
            }

            // Map the list of AddTourImageDto to List<TourImage>
            var images = _mapper.Map<List<TourImage>>(addTourImageDtos);

            // Set the TourId for each image
            foreach (var image in images)
            {
                image.TourId = Id;
            }

            var res = await _imageService.AddImages(Id, images);
            _responseDto.Result = res;
            return Created("", _responseDto);
        }
    }
}
