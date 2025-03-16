using CsvHelper;
using Inventory.Management.API.Mapper;
using Inventory.Management.Infrastructure.DTO;
using Inventory.Management.Infrastructure.Interface;

namespace Inventory.Management.Infrastructure.Services.Member
{
    public class UploadService : IUploadService
    {
        private readonly IMemberRepository memberService;
        private readonly IInventoryRepository inventoryRepository;

        public UploadService(IMemberRepository memberService, IInventoryRepository inventoryRepository)
        {
            this.memberService = memberService;
            this.inventoryRepository = inventoryRepository;
        }

        public async Task<UploadResponse> UploadInventoryAsync(CsvReader csvReader)
        {
            try
            {
                csvReader.Context.RegisterClassMap<InventoryMapper>();
                var records = csvReader.GetRecords<Data.EF.Model.Inventory>().ToList();
                await this.inventoryRepository.AddMembersAsync(records);

                return new UploadResponse
                {
                    Message = $"{records.Count} members uploaded successfully.",
                    Success = true,
                    TotalRecords = records.Count
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UploadResponse> UploadMembersAsync(CsvReader csvReader)
        {
            try
            {
                csvReader.Context.RegisterClassMap<MemberMapper>();
                var records = csvReader.GetRecords<Data.EF.Model.Member>().ToList();
                await this.memberService.AddMembersAsync(records);

                return new UploadResponse
                {
                    Message = $"{records.Count} members uploaded successfully.",
                    Success = true,
                    TotalRecords = records.Count
                };
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
