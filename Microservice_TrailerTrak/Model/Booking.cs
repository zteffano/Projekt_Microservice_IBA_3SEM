namespace Microservice_TrailerTrak.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookedFrom { get; set; }
        public DateTime BookedTo { get; set; }
        public int CustomerID { get; set; }
        public int TrailerId { get; set; }
        public bool Returned { get; set; }
        public bool Paid { get; set; }
        public Customer? customer { get; set; }
        public Trailer? trailer { get; set; }
    }
}
