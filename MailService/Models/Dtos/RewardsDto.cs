namespace MailService.Models.Dtos
{
    public class RewardsDto
    {
        public Guid BookingId { get; set; }

        public double BookingTotal { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
