using HotelService.Models.Dtos;
using HotelService.Services.Iservices;
using Newtonsoft.Json;

namespace HotelService.Services
{
    public class ToursServvices : ITour
    {
        private readonly IHttpClientFactory _HttpClientFactory;
        public ToursServvices(IHttpClientFactory httpClientFactory)
        {
            _HttpClientFactory = httpClientFactory;
        }


        public async Task<TourDto> GetTourById(Guid Id)
        {
            var client = _HttpClientFactory.CreateClient("Tours");
            var response = await client.GetAsync($"{Id}");
            var content = await response.Content.ReadAsStringAsync();//string
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);

            if(response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TourDto>(Convert.ToString(responseDto.Result));
            }
            return new TourDto();
        }
    }
}
