﻿using BookingService.Models.Dtos;
using BookingService.Services.Iservices;
using Newtonsoft.Json;

namespace BookingService.Services
{
    public class HotelService : IHotel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HotelService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<HotelDto> GetHotelById(Guid id)
        {
            var client = _httpClientFactory.CreateClient("Hotels");
            var response = await client.GetAsync(id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.Result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<HotelDto>(responseDto.Result.ToString());
            }
            return new HotelDto();
        }
    }
}
