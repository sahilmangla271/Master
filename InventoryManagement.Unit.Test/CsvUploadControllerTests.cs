using CsvHelper;
using Inventory.Management.API.Controllers;
using Inventory.Management.Infrastructure.DTO;
using Inventory.Management.Infrastructure.Services.Member;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace InventoryManagement.Unit.Test
{
    public class CsvUploadControllerTests
    {
        private readonly IUploadService _uploadService;
        private readonly CsvUploadController _controller;
        public CsvUploadControllerTests()
        {
            _uploadService = Substitute.For<IUploadService>();
            _controller = new CsvUploadController(_uploadService);
        }

        private IFormFile CreateMockFile(string content, string fileName)
        {
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
            var file = new Mock<IFormFile>();
            file.Setup(f => f.OpenReadStream()).Returns(stream);
            file.Setup(f => f.FileName).Returns(fileName);
            file.Setup(f => f.Length).Returns(stream.Length);
            return file.Object;
        }

        [Fact]
        public async Task UploadMembers_Success_ReturnsOkResponse()
        {
            // Arrange
            var file = CreateMockFile("name,email\nJohn Doe,john@example.com", "members.csv");
            var expectedResponse = new UploadResponse { Success = true };

            _uploadService.UploadMembersAsync(Arg.Any<CsvReader>()).Returns(Task.FromResult(expectedResponse));

            // Act
            var result = await _controller.UploadMembers(file);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<UploadResponse>(actionResult.Value);
            Assert.True(response.Success);
        }

        [Fact]
        public async Task UploadMembers_Failure_ReturnsBadRequest()
        {
            // Arrange
            var file = CreateMockFile("name,email\nJohn Doe,john@example.com", "members.csv");
            var expectedResponse = new UploadResponse { Success = false };

            _uploadService.UploadMembersAsync(Arg.Any<CsvReader>()).Returns(Task.FromResult(expectedResponse));

            // Act
            var result = await _controller.UploadMembers(file);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task UploadMembers_NoFileUploaded_ReturnsBadRequestWithMessage()
        {
            // Act
            var result = await _controller.UploadMembers(null);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("No file uploaded.", actionResult.Value);
        }

        [Fact]
        public async Task UploadMembers_EmptyFile_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var emptyFile = CreateMockFile("", "members.csv");

            // Act
            var result = await _controller.UploadMembers(emptyFile);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("No file uploaded.", actionResult.Value);
        }

        [Fact]
        public async Task UploadMembers_Exception_ThrowsException()
        {
            // Arrange
            var file = CreateMockFile("name,email\nJohn Doe,john@example.com", "members.csv");
            _uploadService.UploadMembersAsync(Arg.Any<CsvReader>()).Throws(new Exception("Unexpected error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _controller.UploadMembers(file));
        }



        public async Task UploadInventory_Success_ReturnsOkResponse()
        {
            // Arrange
            var file = CreateMockFile("id,title,remaining_count\n1,Item1,10", "inventory.csv");
            var expectedResponse = new UploadResponse { Success = true };

            _uploadService.UploadInventoryAsync(Arg.Any<CsvReader>()).Returns(Task.FromResult(expectedResponse));

            // Act
            var result = await _controller.UploadInventory(file);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<UploadResponse>(actionResult.Value);
            Assert.True(response.Success);
        }

        [Fact]
        public async Task UploadInventory_Failure_ReturnsBadRequest()
        {
            // Arrange
            var file = CreateMockFile("id,title,remaining_count\n1,Item1,10", "inventory.csv");
            var expectedResponse = new UploadResponse { Success = false };

            _uploadService.UploadInventoryAsync(Arg.Any<CsvReader>()).Returns(Task.FromResult(expectedResponse));

            // Act
            var result = await _controller.UploadInventory(file);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UploadInventory_NoFileUploaded_ReturnsBadRequestWithMessage()
        {
            // Act
            var result = await _controller.UploadInventory(null);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No file uploaded.", actionResult.Value);
        }

        [Fact]
        public async Task UploadInventory_EmptyFile_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var emptyFile = CreateMockFile("", "inventory.csv");

            // Act
            var result = await _controller.UploadInventory(emptyFile);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No file uploaded.", actionResult.Value);
        }

        [Fact]
        public async Task UploadInventory_Exception_ThrowsException()
        {
            // Arrange
            var file = CreateMockFile("id,title,remaining_count\n1,Item1,10", "inventory.csv");
            _uploadService.UploadInventoryAsync(Arg.Any<CsvReader>()).Throws(new Exception("Unexpected error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _controller.UploadInventory(file));
        }
    }
}
