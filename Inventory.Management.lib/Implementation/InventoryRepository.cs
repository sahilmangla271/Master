using Inventory.Management.Infrastructure.Data.EF;
using Inventory.Management.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Management.Infrastructure.Implementation
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryDBContext _context;

        public InventoryRepository(InventoryDBContext context)
        {
            _context = context;
        }

        public async Task AddMembersAsync(List<Data.EF.Model.Inventory> members)
        {
            await _context.Inventories.AddRangeAsync(members);
            await _context.SaveChangesAsync();
        }


        public async Task<Data.EF.Model.Inventory> GetInventoryByIdAsync(int id)
        {
            return await _context.Inventories.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task UpdateInventoryAsync(Data.EF.Model.Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            await _context.SaveChangesAsync();
        }
    }
}
