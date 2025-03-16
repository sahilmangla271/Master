using CsvHelper.Configuration;
using Inventory.Management.Infrastructure.Data.EF.Model;

namespace Inventory.Management.API.Mapper
{
    public class MemberMapper : ClassMap<Member>
    {
        public MemberMapper()
        {
            Map(m => m.Id).Ignore();
            Map(m => m.Name).Name("name");
            Map(m => m.Surname).Name("surname");
            Map(m => m.BookingCount).Name("booking_count");
            Map(m => m.DateJoined).Name("date_joined");
        }
    }
}
