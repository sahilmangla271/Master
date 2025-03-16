using CsvHelper.Configuration;
using Inventory.Management.Infrastructure;

namespace Inventory.Management.API.Mapper
{
    public class InventoryMapper : ClassMap<Inventory.Management.Infrastructure.Data.EF.Model.Inventory>
    {
        public InventoryMapper()
        {
            Map(m => m.Id).Ignore();
            Map(m => m.Title).Name("title");
            Map(m => m.Description).Name("description");
            Map(m => m.RemainingCount).Name("remaining_count");
            Map(m => m.ExpirationDate).Name("expiration_date").TypeConverter<CustomDateTimeConverter>(); ;
        }
    }
}
