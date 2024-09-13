using AutoMapper;
using HotelService.Models;
using HotelService.Models.Dtos;
using HotelService.Services.Iservices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotel _hotelService;
        private readonly ITour _tourService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;

        public HotelsController(IHotel hotel, ITour tour, IMapper mapper)
        {
            _hotelService = hotel;
            _tourService = tour;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> AddHotel(AddHotel newHotel)
        {
            var hotel = _mapper.Map<Hotel>(newHotel);
            //if tour exists
            var tour = await _tourService.GetTourById(hotel.TourId);
            if (string.IsNullOrWhiteSpace(tour.SafariName))
            {
                _response.Errormessage = "Tour Not Found";
                Console.WriteLine(tour);
                return NotFound(_response);
            }

            //tour exists 

            var res = await _hotelService.AddHotel(hotel);
            _response.Result = res;
            return Ok(_response);
        }


        [HttpGet("{TourId}")]
        public async Task<ActionResult<ResponseDto>> getHotelsByTours(Guid TourId)
        {
            var hotels = await _hotelService.GetAllHotel(TourId);
            var mappedHotels= _mapper.Map<List<HotelResponseDto>>(hotels);
            _response.Result = mappedHotels;
            return Ok(_response);

        }


        [HttpGet("single/{Id}")]
        public async Task<ActionResult<ResponseDto>> GetHotel(Guid Id)
        {
            var hotel = await _hotelService.GetHotelById(Id);
            _response.Result = hotel;
            return Ok(_response);

        }
    }
    }
