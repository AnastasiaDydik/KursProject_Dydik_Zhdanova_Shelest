namespace Kurs.Admin.Repository
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DeviceId { get; set; }
        public int Quantity { get; set; }
        public bool IsSold { get; set; }
    }
}
