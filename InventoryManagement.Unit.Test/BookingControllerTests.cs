using Inventory.Management.API.Controllers;
using Inventory.Management.Infrastructure.DTO;
using Inventory.Management.Infrastructure.Services.Booking;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace InventoryManagement.Unit.Test
{
    public class BookingControllerTests
    {
        private readonly IBookingService _bookingService;
        private readonly BookingController _controller;


        public BookingControllerTests()
        {
            _bookingService = Substitute.For<IBookingService>();
            _controller = new BookingController(_bookingService);

        }

        [Fact]
        public async Task Book_Success_ReturnsOkResponse()
        {
            // Arrange
            var request = new BookingRequest { MemberId = 1, InventoryId = 1 };
            var expectedResponse = new BookingResponse { Success = true, Message = "Booking successful!" };

            _bookingService.BookItemAsync(request.MemberId, request.InventoryId)
                           .Returns(Task.FromResult(expectedResponse));

            // Act
            var result = await _controller.Book(request);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<BookingResponse>(actionResult.Value);
            Assert.True(response.Success);
        }

        [Fact]
        public async Task Book_Failure_ReturnsBadRequestResponse()
        {
            // Arrange
            var request = new BookingRequest { MemberId = 1, InventoryId = 1 };
            var expectedResponse = new BookingResponse { Success = false, Message = "Booking failed. User has exceeded the limit or inventory is unavailable." };

            _bookingService.BookItemAsync(request.MemberId, request.InventoryId)
                           .Returns(Task.FromResult(expectedResponse));

            // Act
            var result = await _controller.Book(request);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var response = Assert.IsType<BookingResponse>(actionResult.Value);
            Assert.False(response.Success);
        }


        [Fact]
        public async Task Book_Exception_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var request = new BookingRequest { MemberId = 1, InventoryId = 1 };
            var exceptionMessage = "An error occurred";

            _bookingService.BookItemAsync(request.MemberId, request.InventoryId)
                           .Throws(new Exception(exceptionMessage));

            // Act
            var result = await _controller.Book(request);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(exceptionMessage, actionResult.Value);
        }

        [Fact]
        public async Task Cancel_Success_ReturnsOkResponse()
        {
            // Arrange
            int bookingId = 123;
            var expectedResponse = new BookingResponse { Success = true };

            _bookingService.CancelBookingAsync(bookingId).Returns(Task.FromResult(expectedResponse));

            // Act
            var result = await _controller.Cancel(bookingId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<BookingResponse>(actionResult.Value);
            Assert.True(response.Success);
        }

        [Fact]
        public async Task Cancel_Failure_ReturnsBadRequestResponse()
        {
            // Arrange
            int bookingId = 123;
            var expectedResponse = new BookingResponse { Success = false };

            _bookingService.CancelBookingAsync(bookingId).Returns(Task.FromResult(expectedResponse));

            // Act
            var result = await _controller.Cancel(bookingId);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var response = Assert.IsType<BookingResponse>(actionResult.Value);
            Assert.False(response.Success);
        }

        [Fact]
        public async Task Cancel_Exception_ReturnsBadRequestWithMessage()
        {
            // Arrange
            int bookingId = 123;
            var exceptionMessage = "An error occurred while canceling";

            _bookingService.CancelBookingAsync(bookingId).Throws(new Exception(exceptionMessage));

            // Act
            var result = await _controller.Cancel(bookingId);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(exceptionMessage, actionResult.Value);
        }

    }


}
