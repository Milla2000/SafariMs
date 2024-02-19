using BookingService.Models.Dtos;

namespace BookingService.Services.Iservices
{
    public interface ICoupon
    {

        Task<CouponDto> GetCouponByCouponCode(string couponCode);   
    }
}
