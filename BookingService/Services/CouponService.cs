using BookingService.Models.Dtos;
using BookingService.Services.Iservices;
using Newtonsoft.Json;

namespace BookingService.Services
{
    public class CouponService : ICoupon
    {

        private readonly IHttpClientFactory _httpClientFactory;
        public CouponService( IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<CouponDto> GetCouponByCouponCode(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupons");
            var response= await client.GetAsync(couponCode);
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if(responseDto.Result != null && response.IsSuccessStatusCode ) 
            {
                return JsonConvert.DeserializeObject<CouponDto>(responseDto.Result.ToString());
            }
            return new CouponDto();
        }
    }
}
