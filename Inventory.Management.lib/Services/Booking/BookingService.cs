using Inventory.Management.Infrastructure.Constants;
using Inventory.Management.Infrastructure.Data.EF;
using Inventory.Management.Infrastructure.DTO;
using Inventory.Management.Infrastructure.Interface;

namespace Inventory.Management.Infrastructure.Services.Booking
{
    public class BookingService : IBookingService
    {
        private readonly IInventoryRepository inventoryRepository;
        private readonly IBookingRepository bookingRepository;
        private readonly IMemberRepository memberRepository;
        private readonly InventoryDBContext dbContext;

        public BookingService(IInventoryRepository inventoryRepository, IBookingRepository bookingRepository,
            IMemberRepository memberRepository, InventoryDBContext dbContext
            )
        {
            this.inventoryRepository = inventoryRepository;
            this.bookingRepository = bookingRepository;
            this.memberRepository = memberRepository;
            this.dbContext = dbContext;
        }

        public async Task<BookingResponse> BookItemAsync(int memberId, int inventoryId)
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                var memberTask = memberRepository.GetMemberByIdAsync(memberId);
                var inventoryTask = inventoryRepository.GetInventoryByIdAsync(inventoryId);

                var member = await memberTask;
                var inventory = await inventoryTask;

                if (member is null || inventory is null || member.BookingCount >= BookingConstant.Max_Booking || inventory.RemainingCount <= 0)
                {
                    return new BookingResponse
                    {
                        Success = false,
                        Message = "Booking failed. User has exceeded the limit or inventory is unavailable."
                    };
                }

                var booking = new Data.EF.Model.Booking
                {
                    MemberId = memberId,
                    InventoryId = inventoryId,
                    BookingDate = DateTime.UtcNow
                };

                await bookingRepository.AddBookingAsync(booking);
                member.BookingCount++;
                inventory.RemainingCount--;

                // Update member and inventory in parallel
                await Task.WhenAll(
                    memberRepository.UpdateMemberAsync(member),
                    inventoryRepository.UpdateInventoryAsync(inventory)
                );

                await transaction.CommitAsync();

                return new BookingResponse
                {
                    Success = true,
                    Message = "Booking successful!"
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<BookingResponse> CancelBookingAsync(int bookingId)
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                var booking = await bookingRepository.GetBookingByIdAsync(bookingId);
                if (booking == null)
                {
                    return new BookingResponse
                    {
                        Success = false,
                        Message = "Booking not found"
                    };
                }

                booking.Member.BookingCount--;
                booking.Inventory.RemainingCount++;
                await Task.WhenAll(
                    memberRepository.UpdateMemberAsync(booking.Member),
                    inventoryRepository.UpdateInventoryAsync(booking.Inventory),
                    bookingRepository.RemoveBookingAsync(booking)
                    );

                await transaction.CommitAsync();
                return new BookingResponse
                {
                    Success = true,
                    Message = "Booking Cancelled successully!"
                };

            }
            catch (Exception)
            {
                // Rollback the transaction in case of an exception
                await transaction.RollbackAsync();
                throw; // Re-throw the exception to propagate it
            }

        }
    }
}
