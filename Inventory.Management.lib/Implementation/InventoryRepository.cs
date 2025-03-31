using Inventory.Management.Infrastructure.Data;
using Inventory.Management.Infrastructure.Interface;

namespace Inventory.Management.Infrastructure.Implementation
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IRepository<Data.EF.Model.Inventory> _repository;

        public InventoryRepository(IRepository<Data.EF.Model.Inventory> repository)
        {
            _repository = repository;
        }

        public async Task AddMembersAsync(List<Data.EF.Model.Inventory> members)
        {
            await _repository.AddRangeAsync(members);
        }


        public async Task<Data.EF.Model.Inventory> GetInventoryByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateInventoryAsync(Data.EF.Model.Inventory inventory)
        {
            await _repository.UpdateAsync(inventory);
        }
    }
}
