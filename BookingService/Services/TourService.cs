using BookingService.Models.Dtos;
using BookingService.Services.Iservices;
using Newtonsoft.Json;

namespace BookingService.Services
{
    public class TourService : ITour
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TourService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async  Task<TourDto> GetById(Guid id)
        {
            var client = _httpClientFactory.CreateClient("Tours");
            var response = await client.GetAsync(id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.Result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TourDto>(responseDto.Result.ToString());
            }
            return new TourDto();
        }
    }
}
