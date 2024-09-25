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
            try
            {
                var response = await client.GetAsync($"getATour/{Id}");
                var content = await response.Content.ReadAsStringAsync();
                //var serviceUri = GetAsync($"getATour/{Id}")

                if (response.IsSuccessStatusCode)
                {
                    var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
                    return JsonConvert.DeserializeObject<TourDto>(Convert.ToString(responseDto.Result));
                }
                else
                {
                    Console.WriteLine($"Error: - {response} - {content}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }

            return new TourDto();
        }
    }

}