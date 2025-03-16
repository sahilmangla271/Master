namespace Inventory.Management.Infrastructure.Data.EF.Model
{
    public partial class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BookingCount { get; set; }
        public DateTime DateJoined { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
