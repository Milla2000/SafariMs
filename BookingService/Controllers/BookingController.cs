 using AutoMapper;
using BookingService.Models;
using BookingService.Models.Dtos;
using BookingService.Services.Iservices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICoupon _couponService;
        private readonly IBooking _bookingService;
        private readonly IHotel _hotelService;
        private readonly ITour _tourService;
        private readonly ResponseDto _responseDto;

        public BookingController(IMapper mapper, ITour tour, IHotel hotel, IBooking booking, ICoupon coupon)
        {
            _bookingService = booking;
            _mapper = mapper;
            _couponService = coupon;
            _hotelService = hotel;
            _tourService = tour;
            _responseDto= new ResponseDto();
        }

        [HttpPost]

        public async Task<ActionResult<ResponseDto>> AddBooking(AddBookingDto dto)
        {
            //get the tour and the hotel
            var booking = _mapper.Map<Booking>(dto);

            var tour = await _tourService.GetById(booking.TourId);
            var hotel= await _hotelService.GetHotelById(booking.HotelId);

            if(hotel==null || tour==null)
            {
                _responseDto.Errormessage = "Invalid Values";
                return NotFound(_responseDto);
            }

            //calculate the total price

            var total = (tour.Price)+ (hotel.AdultPrice * dto.Adults * (tour.EndDate- tour.StartDate ).TotalDays) +
                (hotel.KidsPrice * dto.Kids * (tour.EndDate - tour.StartDate).TotalDays); 

            booking.BookingTotal = total;

            var res=await _bookingService.AddBooking(booking);

            // Set booking ID in the response
            _responseDto.Result = new
            {
                BookingTotalPrice = booking.BookingTotal,
                BookingId = booking.Id,
                Result = res
            };

            return Ok(_responseDto);

        }



        [HttpPost("Pay")]

        public async Task<ActionResult<ResponseDto>> makePayments(StripeRequestDto dto)
        {
    
           var sR= await _bookingService.MakePayments(dto);

            _responseDto.Result=sR;
            return Ok(_responseDto);           
        }

        [HttpPost("validate/{Id}")]

        public async Task<ActionResult<ResponseDto>> validatePayment(Guid Id)
        {

            var res = await _bookingService.ValidatePayments(Id);

            if (res)
            {
                _responseDto.Result = res;
                return Ok(_responseDto);
            }

            _responseDto.Errormessage = "Payment Failed!";
            return BadRequest(_responseDto);
        }

        [HttpGet("{userId}")]

        public async Task<ActionResult<ResponseDto>> GetUserBooking(Guid userId)
        {
            //get the tour and the hotel
            var res=await _bookingService.GetAll(userId);
            _responseDto.Result = res;
            return Ok(_responseDto);


        }

        [HttpPut("applycoupon")]

        public async Task<ActionResult<ResponseDto>> ApplyCoupon(Guid Id, string Code)
        {
            // Get the booking by ID
            var booking = await _bookingService.GetBookingById(Id);

            if (booking == null)
            {
                _responseDto.Errormessage = "Booking Not Found";
                return NotFound(_responseDto);
            }

            // Check if a coupon has already been applied
            if (!string.IsNullOrEmpty(booking.CouponCode))
            {
                _responseDto.Errormessage = "A coupon has already been applied to this booking.";
                return BadRequest(_responseDto);
            }

            // Get the coupon by code
            var coupon = await _couponService.GetCouponByCouponCode(Code);

            if (coupon == null)
            {
                _responseDto.Errormessage = "Coupon is not valid.";
                return NotFound(_responseDto);
            }

            // Check if the booking total meets the coupon's minimum amount requirement
            if (coupon.CouponMinAmount <= booking.BookingTotal)
            {
                // Apply the coupon to the booking
                booking.CouponCode = coupon.CouponCode;
                booking.Discount = coupon.CouponAmount;
                booking.BookingTotal -= coupon.CouponAmount;

                await _bookingService.saveChanges();

                _responseDto.Result = new
                {
                    Message = "Coupon applied successfully.",
                    Total = booking.BookingTotal
                };

                return Ok(_responseDto);
            }
            else
            {
                _responseDto.Errormessage = "Total amount is less than the minimum required for this coupon.";
                return BadRequest(_responseDto);
            }
        }

    }
}
