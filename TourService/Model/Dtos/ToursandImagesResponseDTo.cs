namespace TourService.Model.Dtos
{
    public class ToursandImagesResponseDTo
    {
        public Guid Id { get; set; }
        public string SafariName { get; set; } = string.Empty;

        public string SafariDescription { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Price { get; set; }

        public List<AddTourImageDto> TourImages { get; set; }= new List<AddTourImageDto>();
    }
}
