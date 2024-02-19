namespace HotelService.Models.Dtos
{
    public class AddHotel
    {

        public string Name { get; set; } = string.Empty;

        public Guid TourId { get; set; }
        public int AdultPrice { get; set; }

        public int KidsPrice { get; set; }
    }
}
