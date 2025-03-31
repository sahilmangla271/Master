using Inventory.Management.Infrastructure.Data;
using Inventory.Management.Infrastructure.Data.EF.Model;
using Inventory.Management.Infrastructure.Interface;

namespace Inventory.Management.Infrastructure.Implementation
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IRepository<Booking> _repository;

        public BookingRepository(IRepository<Booking> repository)
        {
            _repository = repository;
        }

        public async Task AddBookingAsync(Booking booking)
        {
            await _repository.AddAsync(booking);
        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _repository.GetAllByIdAsync(id, b => b.Member, b => b.Inventory);
        }

        public async Task RemoveBookingAsync(Booking booking)
        {
            await _repository.RemoveAsync(booking);

        }
    }
}
