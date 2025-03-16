using Inventory.Management.Infrastructure.Data.EF.Model;

namespace Inventory.Management.Infrastructure.Interface
{
    public interface IBookingRepository
    {
        Task AddBookingAsync(Booking booking);
        Task<Booking?> GetBookingByIdAsync(int id);
        Task RemoveBookingAsync(Booking booking);
    }
}
