using Inventory.Management.Infrastructure.Data.EF;
using Inventory.Management.Infrastructure.Data.EF.Model;
using Inventory.Management.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Management.Infrastructure.Implementation
{
    public class BookingRepository : IBookingRepository
    {
        private readonly InventoryDBContext _context;

        public BookingRepository(InventoryDBContext context)
        {
            _context = context;
        }

        public async Task AddBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Member)
                .Include(b => b.Inventory)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task RemoveBookingAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }
}
