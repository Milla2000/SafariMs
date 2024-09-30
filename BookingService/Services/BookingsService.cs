using BookingService.Data;
using BookingService.Models;
using BookingService.Models.Dtos;
using BookingService.Services.Iservices;
using Microsoft.EntityFrameworkCore;
using SafariMessageBus;
using Stripe;
using Stripe.Checkout;

namespace BookingService.Services
{
    public class BookingsService : IBooking
    {
        private readonly ApplicationDbContext _context;
        private readonly ITour _tourService;
        private readonly IUser _userService;
        private readonly IMessageBus _messageBUs;
        public BookingsService(ApplicationDbContext context, ITour tourService,IUser user, IMessageBus message)
        {
            _context = context;
            _tourService = tourService;
            _userService = user;
            _messageBUs = message;

        }

        public async Task<string> AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return "Booking added successfully";
        }

        public async Task<List<Booking>> GetAll(Guid userId)
        {
            return await _context.Bookings.Where(x=>x.UserId == userId).ToListAsync();
        }

        public async  Task<Booking> GetBookingById(Guid Id)
        {
            return await _context.Bookings.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }



        public async Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto)
        {

            var booking = await _context.Bookings.Where(x => x.Id == stripeRequestDto.BookingId).FirstOrDefaultAsync();
            var tour = await _tourService.GetById(booking.TourId);
            var options = new SessionCreateOptions()
            {
                SuccessUrl = stripeRequestDto.ApprovedUrl,
                CancelUrl = stripeRequestDto.CancelUrl,
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>()
            };



            var item = new SessionLineItemOptions()
            {
                PriceData = new SessionLineItemPriceDataOptions()
                {
                    UnitAmount = (long)booking.BookingTotal * 100,
                    Currency = "usd",

                    ProductData = new SessionLineItemPriceDataProductDataOptions()
                    {
                        Name = tour.SafariName,
                        Description = tour.SafariDescription,
                        Images = new List<string> { "https://imgs.search.brave.com/av4uh1BAXrv7q2gkJt-E709vrIz3mB1-wrcPDtDyZNI/rs:fit:500:0:0/g:ce/aHR0cHM6Ly93d3cu/ZXhwZXJ0YWZyaWNh/LmNvbS9pbWFnZXMv/YXJlYS8xODI5X2wu/anBn" }
                    }
                },
                Quantity = 1


            };

            options.LineItems.Add(item);

            //discount

            var DiscountObj = new List<SessionDiscountOptions>()
            {
                new SessionDiscountOptions()
                {
                    Coupon=booking.CouponCode
                }
            };

            if (booking.Discount > 0)
            {
                options.Discounts = DiscountObj;

            }
            var service = new SessionService();
            Session session = service.Create(options);

            // URL//ID

            stripeRequestDto.StripeSessionUrl=session.Url;
            stripeRequestDto.StripeSessionId = session.Id;

            //update Database =>status/ SessionId 

            booking.StripeSessionId = session.Id;
            booking.Status = "Ongoing";
            await _context.SaveChangesAsync();

            return stripeRequestDto;
        }




        public async Task saveChanges()
        {
            await _context.SaveChangesAsync();
        }




        public async Task<bool> ValidatePayments(Guid BookingId)
        {
            var booking = await _context.Bookings.Where(x => x.Id == BookingId).FirstOrDefaultAsync();

            var service=new SessionService();
            Session session = service.Get(booking.StripeSessionId);

            PaymentIntentService paymentIntentService= new PaymentIntentService();

            PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

            if (paymentIntent.Status == "succeeded")
            {
                //the payment was success

                booking.Status = "Paid";
                booking.PaymentIntent = paymentIntent.Id;
                await _context.SaveChangesAsync();
                var user =await _userService.GetUserById(booking.UserId.ToString());

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    return false;
                }
                else
                {
                    var reward = new RewardsDto()
                    {
                        BookingId = booking.Id,
                        BookingTotal=booking.BookingTotal,
                        Name= user.Name,
                        Email= user.Email

                    };
                    await _messageBUs.PublishMessage(reward, "bookingadded");
                }

                // Send an Email to User
                //Reward the user with some Bonus Points 
                return true;

            }
            return false;
        }
    }
}
