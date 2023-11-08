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

        public Booking(int id, DateTime bookedFrom, DateTime bookedTo, int customerID, int trailerId, bool returned, bool paid, Customer? customer, Trailer? trailer)
        {
            Id = id;
            BookedFrom = bookedFrom;
            BookedTo = bookedTo;
            CustomerID = customerID;
            TrailerId = trailerId;
            Returned = returned;
            Paid = paid;
            this.customer = customer;
            this.trailer = trailer;
        }

       
    }
}
