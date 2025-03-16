using CsvHelper;
using Inventory.Management.Infrastructure.DTO;

namespace Inventory.Management.Infrastructure.Services.Member
{
    public interface IUploadService
    {
        Task<UploadResponse> UploadMembersAsync(CsvReader csvReader);
        Task<UploadResponse> UploadInventoryAsync(CsvReader csvReader);

    }
}
