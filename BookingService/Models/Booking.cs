﻿namespace BookingService.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public double Discount { get; set; }
        public double BookingTotal { get; set; }
        public int Adults { get; set; }

        public int Kids { get; set; }

        public Guid TourId { get; set; }

        public Guid HotelId { get; set; }

        public string? StripeSessionId { get; set; }

        public string Status { get; set; } = "Pending";

        public string PaymentIntent { get; set; } = string.Empty;

    }
}
