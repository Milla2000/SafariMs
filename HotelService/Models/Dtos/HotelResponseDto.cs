namespace HotelService.Models.Dtos
{
    public class HotelResponseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int AdultPrice { get; set; }

        public int KidsPrice { get; set; }
    }
}
