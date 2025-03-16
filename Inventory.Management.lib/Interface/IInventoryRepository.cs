namespace Inventory.Management.Infrastructure.Interface
{
    public interface IInventoryRepository
    {
        Task<Inventory.Management.Infrastructure.Data.EF.Model.Inventory> GetInventoryByIdAsync(int id);
        Task UpdateInventoryAsync(Data.EF.Model.Inventory inventory);
        Task AddMembersAsync(List<Data.EF.Model.Inventory> members);

    }
}
