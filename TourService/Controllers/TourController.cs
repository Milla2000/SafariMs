using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourService.Model;
using TourService.Model.Dtos;
using TourService.Services.IServices;

namespace TourService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {

        private readonly ITour _tourService;

        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;

        public TourController(IMapper mapper, IImage image, ITour tour)
        {

            _tourService = tour;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [HttpPost("AddNewTour")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto>> AddNewTour(AddTourDto addTourDto)
        {
            var tour = _mapper.Map<Tour>(addTourDto);

            var res = await _tourService.AddNewTour(tour);

            // Map the image URLs from DTO to the TourImages list
            foreach (var imageUrl in addTourDto.SafariImages)
            {
                tour.SafariImages.Add(new TourImage
                {
                   // Id = Guid.NewGuid(),
                    Image = imageUrl,
                    TourId = tour.Id
                });
            }

            _responseDto.Result = res;
            return Created("", _responseDto);

        }


        [HttpGet("getAllTours")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> getAllTourz()
        {
           
            var res = await _tourService.GetAllTours();
            _responseDto.Result = res;
            return Ok(_responseDto);

        }


        [HttpGet("getATour/{Id}")]

        public async Task<ActionResult<ResponseDto>> getATour(Guid Id)
        {

            var res = await _tourService.GetTour(Id);
            if (res == null)
            {
                _responseDto.Errormessage = "Tour Not Found";
                return NotFound(_responseDto);
            }
            _responseDto.Result = res;
            return Ok(_responseDto);

        }


    }
}
