using Inventory.Management.Infrastructure.DTO;

namespace Inventory.Management.Infrastructure.Services.Booking
{
    public interface IBookingService
    {
        Task<BookingResponse> BookItemAsync(int memberId, int inventoryId);
        Task<BookingResponse> CancelBookingAsync(int bookingId);
    }
}
