using CsvHelper;
using Inventory.Management.Infrastructure.DTO;
using Inventory.Management.Infrastructure.Services.Member;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Inventory.Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CsvUploadController : ControllerBase
    {

        [HttpPost("uploadmembers")]
        public async Task<ActionResult<UploadResponse>> UploadMembers(IFormFile file, [FromServices] IUploadService uploadService)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                using var reader = new StreamReader(file.OpenReadStream());
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var response = await uploadService.UploadMembersAsync(csv);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while processing the file.",
                    details = ex.Message
                });
            }
        }

        [HttpPost("uploadinventory")]
        public async Task<IActionResult> UploadInventory(IFormFile file, [FromServices] IUploadService uploadService)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                using var reader = new StreamReader(file.OpenReadStream());
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var response = await uploadService.UploadInventoryAsync(csv);

                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while processing the file.",
                    details = ex.Message
                });
            }

        }

    }
}
