namespace Microservice_TrailerTrak.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookedFrom { get; set; }
        public DateOnly BookedTo { get; set; }
    }
}
