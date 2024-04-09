namespace TourService.Model
{
    public class Tour
    {

        public Guid Id { get; set; }

        public string SafariName { get; set; } = string.Empty;

        public string SafariDescription { get; set; } = string.Empty;

        public List<TourImage> SafariImages { get; set; } = new List<TourImage>();

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Price { get; set; }

    }
}
