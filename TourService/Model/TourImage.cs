using System.ComponentModel.DataAnnotations.Schema;

namespace TourService.Model
{
    public class TourImage
    {

        public Guid Id { get; set; }

        public string Image { get; set; }=string.Empty;

        [ForeignKey("TourId")]
        public Tour t { get; set; } = default!;

        public Guid TourId { get; set; }
    }
}