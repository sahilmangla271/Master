namespace Inventory.Management.Infrastructure.Data.EF.Model
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RemainingCount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
