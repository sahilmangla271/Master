using Inventory.Management.Infrastructure.DTO;
using Inventory.Management.Infrastructure.Services.Booking;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("book")]
        public async Task<ActionResult<BookingResponse>> Book([FromBody] BookingRequest request)
        {
            try
            {
                var response = await _bookingService.BookItemAsync(request.MemberId, request.InventoryId);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while processing the request.",
                    details = ex.Message
                });
            }
        }

        [HttpDelete("cancel/{bookingId}")]
        public async Task<ActionResult<BookingResponse>> Cancel(int bookingId)
        {
            try
            {
                var response = await _bookingService.CancelBookingAsync(bookingId);

                if (response.Success)
                {
                    return Ok(response);
                }
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while processing the request.",
                    details = ex.Message
                });
            }
        }

    }
}
