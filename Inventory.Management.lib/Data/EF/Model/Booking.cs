namespace Inventory.Management.Infrastructure.Data.EF.Model
{
    public partial class Booking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public int MemberId { get; set; }
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        public Member Member { get; set; }

    }
}
