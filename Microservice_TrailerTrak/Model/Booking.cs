namespace Microservice_TrailerTrak.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookedFrom { get; set; }
        public DateTime BookedTo { get; set; }
        public bool Returned { get; set; }
        public bool Paid { get; set; }
        public Customer CustomerData { get; set; }
        public Trailer TrailerData { get; set; }
    }
}
