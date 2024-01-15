using HotelService.Data;
using HotelService.Models;
using HotelService.Services.Iservices;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Services
{
    public class HotelsService : IHotel
    {
        private readonly ApplicationDbContext _context;
        public HotelsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddHotel(Hotel hotel)
        {
           _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return "Hotel Added";
        }

        public async Task<List<Hotel>> GetAllHotel(Guid TourId)
        {
            return await _context.Hotels.Where(x => x.TourId == TourId).ToListAsync();
        }

        public async Task<Hotel> GetHotelById(Guid Id)
        {
            return await _context.Hotels.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }
    }
}
